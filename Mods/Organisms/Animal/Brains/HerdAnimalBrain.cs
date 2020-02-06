namespace Eco.Mods.Organisms
{
    using System;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Shared.Utils;
    using Eco.Shared.States;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;

    public class HerdAnimalBrain : Brain
    {
        private class BT : FunctionalBehaviorTree<Animal> { }
        public static readonly Func<Animal, BTStatus> HerdAnimalTreeRoot;
        static HerdAnimalBrain()
        {
            HerdAnimalTreeRoot =
                BT.Selector(
                    LandAnimalBrain.LyingDownTree,
                    BT.If(x => x.Alertness > LandAnimalBrain.AlertThreshold && x.Alertness < LandAnimalBrain.FullyAlertThreshold && x.AnimationState != AnimalAnimationState.Flee && x.AnimationState != AnimalAnimationState.AlertIdle && x.OnFlatGround,
                        // play an alert reaction before fleeing and intermittently while alert
                        PlayFixedTimeAnimation(AnimalAnimationState.AlertIdle, LandAnimalBrain.AlertIdleTime)),
                    MovementBehaviors.Flee,
                    GroupBehaviors.SleepNearLeader,
                    new FindAndEatPlantsMemoryBehavior() { NoNearbyFoodBehavior = LandAnimalBrain.LandAnimalFindFood },
                    GroupBehaviors.StayNearLeader(20f),
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
            HerdAnimalTreeRoot(animal);
        }
    }
}
