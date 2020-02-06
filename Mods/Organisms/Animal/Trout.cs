namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class Trout : AnimalEntity
    {
        public Trout(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class TroutSpecies : AnimalSpecies
        {
            public TroutSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Trout);

                // Info
                this.Name = "Trout";
                this.DisplayName = Localizer.DoStr("Trout");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(Waterweed)};
                this.CalorieValue = 25f;
                // Movement
                this.Flying = false;
                this.Swimming = true;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(TroutItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(FishBrain);
                this.WanderingSpeed = 1f;
                this.Speed = 2f;
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
    }
}
