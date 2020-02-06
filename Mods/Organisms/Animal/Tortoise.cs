namespace Eco.Mods.Organisms
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Gameplay.Interactions;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.States;
    using Eco.Shared.Utils;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Agents.AI;
    using Eco.Simulation.Types;

    public class Tortoise : AnimalEntity
    {
        public Tortoise(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class TortoiseSpecies : AnimalSpecies
        {
            public TortoiseSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Tortoise);

                // Info
                this.Name = "Tortoise";
                this.DisplayName = Localizer.DoStr("Tortoise");
                // Lifetime
                this.MaturityAgeDays = 2f;
                // Food
                this.FoodSources = new List<System.Type>() { typeof(Sagebrush), typeof(CreosoteBush), typeof(PricklyPear), typeof(Agave) };
                this.CalorieValue = 10f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(RawMeatItem), new Range(1f, 2f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(Brain<Tortoise>);
                this.WanderingSpeed = 0.1f;
                this.Speed = 0.2f;
                this.Health = 1f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 1f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }

        // tortoise behavior
        private class BT : FunctionalBehaviorTree<Tortoise> { }
        public static readonly Func<Tortoise, BTStatus> TortoiseTreeRoot;
        static Tortoise()
        {
            const float MinHidingTime = 5f;
            const float MaxHidingTime = 10f;

            TortoiseTreeRoot =
                BT.Selector(
                    // Tortoise hide instead of fleeing, fixed time to block pointless flee interruptions
                    BT.If(x => x.Alertness > Animal.FleeThreshold,
                        Brain.PlayFixedTimeAnimation(AnimalAnimationState.Hiding, _ => RandomUtil.Range(MinHidingTime, MaxHidingTime))),
                    LandAnimalBrain.LandAnimalTreeRoot);

            Brain<Tortoise>.SharedBehavior = TortoiseTreeRoot;
        }

        public override bool TryApplyDamage(INetObject damager, float damage, InteractionContext context, Type damageDealer = null)
        {
            // turtle power! (or uhh tortoise power!)
            return base.TryApplyDamage(damager, this.AnimationState == AnimalAnimationState.Hiding ? damage / 4 :  damage, context, damageDealer);
        }
    }
}
