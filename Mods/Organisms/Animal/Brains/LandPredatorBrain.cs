namespace Eco.Mods.Organisms
{
    using System;
    using System.Linq;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.RouteProbing;
    using Eco.Simulation.Time;
    using Eco.Simulation.Types;
    using Eco.Simulation.WorldLayers;

    public class LandPredatorBrain : Brain
    {
        public const float ChanceCaughtPreyWhileOffscreen = 0.5f;

        private class BT : FunctionalBehaviorTree<Animal> { };
        public static readonly Func<Animal, BTStatus> PredatorTreeRoot;
        static LandPredatorBrain()
        {
            PredatorTreeRoot = BT.Selector(
                    LandAnimalBrain.LyingDownTree,
                    BT.If(x => x.Alertness > LandAnimalBrain.AlertThreshold && x.Alertness < LandAnimalBrain.FullyAlertThreshold && x.AnimationState != AnimalAnimationState.Flee && x.AnimationState != AnimalAnimationState.AlertIdle && x.OnFlatGround,
                        // play an alert reaction before fleeing and intermittently while alert
                        PlayFixedTimeAnimation(AnimalAnimationState.AlertIdle, LandAnimalBrain.AlertIdleTime)),
                    MovementBehaviors.Flee,
                    GroupBehaviors.SleepNearLeader,
                    BT.If(x => x.Hunger > HungerThreshold,
                        BT.Selector(
                            TryEatNearbyCorpses,
                            new HuntMemoryBehavior() { NoNearbyFoodBehavior = LandAnimalBrain.LandAnimalFindFood })),
                    GroupBehaviors.StayNearLeader(10f),
                        //TODO: show a failed chase if there are no corpses neary to eat
                    BT.If(MovementBehaviors.ShouldReturnHome,
                        MovementBehaviors.WanderHome),
                    BT.If(x => x.Alertness < Animal.FleeThreshold && RandomUtil.Chance(LandAnimalBrain.ChanceToLieDown) && x.OnFlatGround,
                        PlayAnimation(AnimalAnimationState.LyingDown, LandAnimalBrain.LyingTickDuration)),
                    BT.If(x => RandomUtil.Chance(LandAnimalBrain.ChanceToIdle) && x.OnFlatGround,
                        PlayAnimation(AnimalAnimationState.Idle, _ => RandomUtil.Range(LandAnimalBrain.MinIdleTime, LandAnimalBrain.MaxIdleTime))),
                    MovementBehaviors.Wander);
        }

        public override void TickBehaviors(Animal animal)
        {
            PredatorTreeRoot(animal);
        }

        // temporary solution for hunting without affecting the environment
        // spawn a corpse nearby when a hungry animal becomes visible
        public override void OnBecameVisible(IWorldObserver observer, Animal animal)
        {
            base.OnBecameVisible(observer, animal);

            if (animal.Dead || animal.Hunger < HungerThreshold || !RandomUtil.Chance(ChanceCaughtPreyWhileOffscreen))
                return;

            var preyType = this.GetRegionalPrey(animal);
            if (preyType != null)
            {
                var preySpecies = EcoSim.GetSpecies(preyType.Name) as AnimalSpecies;
                var spawnPos = (Vector3)animal.Position.WorldPosition3i.SpiralOutXZIter(10).Skip(20)
                    .Select(x => RouteManager.NearestWalkableY(x, 10)).Where(x => x.IsValid && RouteCacheData.IsFlatGround(x)).FirstOrDefault();
                if (spawnPos != default(Vector3))
                    EcoSim.AnimalSim.SpawnCorpse(preySpecies, spawnPos);
            }
        }

        public Type GetRegionalPrey(Animal animal)
        {
            return animal.Species.FoodSources.Where(x => typeof(Animal).IsAssignableFrom(x)).Shuffle()
                .Where(x => WorldLayerManager.GetLayer(x.Name).EntryWorldPos(animal.Position.XZi) > 1f).FirstOrDefault();
        }

        public static BTStatus TryEatNearbyCorpses(Animal agent)
        {
            Animal targetCorpse = null;
            if (agent.TryGetMemory<Animal>("targetCorpse", out targetCorpse) && !targetCorpse.Active)
            {
                targetCorpse = null;
                agent.Brain.Memory.Remove("targetCorpse");
            }

            if (targetCorpse == null)
                foreach (var corpse in Animal.Corpses.Shuffle())
                    if (!corpse.Active)
                        Animal.Corpses.Remove(corpse);
                    else if (Vector3.WrappedDistance(corpse.Position, agent.Position) < 40 && agent.Species.Eats(corpse.Species))
                    {
                        targetCorpse = corpse;
                        agent.Brain.Memory["targetCorpse"] = targetCorpse;
                        break;
                    }

            if (targetCorpse != null)
            {
                if (agent.AnimationState == AnimalAnimationState.Eating)
                {
                    // finished eating, go do something else
                    agent.Hunger = 0;
                    agent.Brain.Memory.Remove("targetCorpse");
                    return BTStatus.Failure;
                }
                // eat or walk then eat
                agent.AnimationState = AnimalAnimationState.Eating;
                if (Vector3.WrappedDistance(targetCorpse.Position, agent.Position) < 3)
                {
                    agent.NextTick = WorldTime.Seconds + 10f;
                    agent.Target.Set(targetCorpse);
                    return BTStatus.Success;
                }
                else
                {
                    var route = AIUtilities.GetRouteFacingTarget(agent.Position, targetCorpse.Position, agent.Species.WanderingSpeed, agent.Species.HeadDistance);
                    agent.Target.SetPath(route);
                    agent.Target.Set(targetCorpse);
                    agent.NextTick = agent.Target.TargetTime + 20f;
                    return BTStatus.Success;
                }
            }
            return BTStatus.Failure;
        }
    }
}
