namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class Tuna : AnimalEntity
    {
        public Tuna(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class TunaSpecies : AnimalSpecies
        {
            public TunaSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Tuna);

                // Info
                this.Name = "Tuna";
                this.DisplayName = Localizer.DoStr("Tuna");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() { typeof(Kelp), typeof(Seagrass) };
                this.CalorieValue = 100f;
                // Movement
                this.Flying = false;
                this.Swimming = true;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(TunaItem), new Range(1f, 1f)),
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
