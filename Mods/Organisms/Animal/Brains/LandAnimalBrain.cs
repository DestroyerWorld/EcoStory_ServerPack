namespace Eco.Mods.Organisms
{
    using System;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Shared.Utils;
    using Eco.Shared.States;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;

    public class LandAnimalBrain : Brain
    {
        private class BT : FunctionalBehaviorTree<Animal> { }
        public static readonly Func<Animal, BTStatus> LyingDownTree;
        public static readonly Func<Animal, BTStatus> LandAnimalTreeRoot;
        public const float MinIdleTime = 2f;
        public const float MaxIdleTime = 5f;
        public const float MinLyingIdleTime = 10f;
        public const float MaxLyingIdleTime = 20f;
        public const float LyingTickDuration = 10f;
        public const float ChanceToIdle = 0.3f;
        public const float ChanceToStopLyingDown = 0.1f;
        public const float ChanceToLieDown = 0.3f;
        public const float AlertIdleTime = 2.5f;
        public const float AlertThreshold = 50;
        public const float FullyAlertThreshold = 200;
        public const float StarvingThreshold = 200;
        static LandAnimalBrain()
        {
            LyingDownTree =
                BT.If(animal => animal.LyingDown,
                    BT.Selector(
                        BT.If(x => x.AnimationState == AnimalAnimationState.Sleeping && x.Alertness > Animal.WakeUpAlertness,
                            // waking up takes time, start the transition animation then decide what to do next tick
                            PlayFixedTimeAnimation(AnimalAnimationState.LyingDown, x => x.Species.TimeWakeUp)),
                        BT.If(x => x.Alertness > Animal.FleeThreshold,
                            PlayFixedTimeAnimation(AnimalAnimationState.Flee, x => x.Species.TimeLayToStand)),
                        BT.If(x => x.Hunger > HungerThreshold,
                            PlayFixedTimeAnimation(AnimalAnimationState.Idle, x => x.Species.TimeLayToStand)),
                        BT.If(x => x.ShouldSleep,
                            PlayAnimation(AnimalAnimationState.Sleeping, LyingTickDuration)),
                        BT.If(x => RandomUtil.Chance(ChanceToStopLyingDown),
                            PlayAnimation(AnimalAnimationState.Idle, x => x.Species.TimeLayToStand + RandomUtil.Range(MinIdleTime, MaxIdleTime))),
                        PlayAnimation(AnimalAnimationState.LyingDown, _ => RandomUtil.Range(MinLyingIdleTime, MaxLyingIdleTime))));

            LandAnimalTreeRoot =
                BT.Selector(
                    LyingDownTree,
                    BT.If(x => x.Alertness > AlertThreshold && x.Alertness < FullyAlertThreshold && x.AnimationState != AnimalAnimationState.Flee && x.AnimationState != AnimalAnimationState.AlertIdle && x.OnFlatGround,
                        // play an alert reaction before fleeing and intermittently while alert
                        PlayFixedTimeAnimation(AnimalAnimationState.AlertIdle, AlertIdleTime)),
                    MovementBehaviors.Flee,
                    new FindAndEatPlantsMemoryBehavior() { NoNearbyFoodBehavior = LandAnimalFindFood },
                    BT.If(MovementBehaviors.ShouldReturnHome,
                        MovementBehaviors.WanderHome),
                    BT.If(x => x.Alertness < Animal.FleeThreshold && RandomUtil.Chance(ChanceToLieDown) && x.OnFlatGround,
                        PlayAnimation(AnimalAnimationState.LyingDown, LyingTickDuration)),
                    BT.If(x => RandomUtil.Chance(ChanceToIdle) && x.OnFlatGround,
                        PlayAnimation(AnimalAnimationState.Idle, _ => RandomUtil.Range(MinIdleTime, MaxIdleTime))),
                    MovementBehaviors.Wander);
        }

        public static BTStatus LandAnimalFindFood(Animal agent)
        {
            // let animals wander to weird places while hungry, but if they are starving go home
            if (agent.Hunger > StarvingThreshold && MovementBehaviors.ShouldReturnHome(agent))
                return MovementBehaviors.WanderHome(agent);
            else
                return MovementBehaviors.Wander(agent);
        }

        public override void TickBehaviors(Animal animal)
        {
            LandAnimalTreeRoot(animal);
        }
    }
}
