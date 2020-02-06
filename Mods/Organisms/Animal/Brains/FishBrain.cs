namespace Eco.Mods.Organisms
{
    using System;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;

    public class FishBrain : Brain
    {
        private class BT : FunctionalBehaviorTree<Animal> { }
        private static readonly Func<Animal, BTStatus> FishTreeRoot;
        static FishBrain()
        {
            const float MinIdleTime = 1f;
            const float MaxIdleTime = 5f;
            const float ChanceToIdle = 0.3f;
            FishTreeRoot =
                BT.Selector(
                    MovementBehaviors.SwimFlee,
                    BT.If(MovementBehaviors.ShouldReturnHome,
                        MovementBehaviors.SwimWanderHome),
                    BT.If(x => RandomUtil.Chance(1 - ChanceToIdle),
                        MovementBehaviors.SwimWander),
                    PlayAnimation(AnimalAnimationState.Idle, _ => RandomUtil.Range(MinIdleTime, MaxIdleTime))
                );
        }

        public override void TickBehaviors(Animal animal)
        {
            FishTreeRoot(animal);
        }
    }
}
