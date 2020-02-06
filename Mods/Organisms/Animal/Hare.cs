namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class Hare : AnimalEntity
    {
        public Hare(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class HareSpecies : AnimalSpecies
        {
            public HareSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Hare);

                // Info
                this.Name = "Hare";
                this.DisplayName = Localizer.DoStr("Hare");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(Camas), typeof(Wheat), typeof(Bunchgrass), typeof(Sagebrush), typeof(Corn), typeof(Huckleberry), typeof(BigBluestem)};
                this.CalorieValue = 50f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(HareCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(HerdAnimalBrain);
                this.WanderingSpeed = 1f;
                this.Speed = 5f;
                this.Health = 1f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 0.8f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }
    }
}
