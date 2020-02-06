namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class Fox : AnimalEntity
    {
        public Fox(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class FoxSpecies : AnimalSpecies
        {
            public FoxSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Fox);

                // Info
                this.Name = "Fox";
                this.DisplayName = Localizer.DoStr("Fox");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(Hare), typeof(Turkey)};
                this.CalorieValue = 100f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(FoxCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(LandPredatorBrain);
                this.WanderingSpeed = 1.5f;
                this.Speed = 5f;
                this.Health = 2f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 0.9f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                this.TimeLayToStand = 2.5f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }
    }
}
