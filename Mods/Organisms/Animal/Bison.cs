namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.Organisms.Behaviors;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class Bison : AnimalEntity
    {
        public Bison(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class BisonSpecies : AnimalSpecies
        {
            public BisonSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Bison);

                // Info
                this.Name = "Bison";
                this.DisplayName = Localizer.DoStr("Bison");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() { typeof(CommonGrass), typeof(Bunchgrass), typeof(Wheat), typeof(BigBluestem), typeof(Switchgrass) };
                this.CalorieValue = 500f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(BisonCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(HerdAnimalBrain);
                this.WanderingSpeed = 0.8f;
                this.Speed = 4f;
                this.Health = 7f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 0.6f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                this.HeadDistance = 1f;
                this.TimeLayToStand = 4f;
                this.TimeStandToLay = 3f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }

        public override void FleeFrom(Vector3 position)
        {
            base.FleeFrom(position);
            GroupBehaviors.SyncFleePosition(this);
        }
    }
}
