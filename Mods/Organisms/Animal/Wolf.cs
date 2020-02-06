namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Time;
    using Eco.Simulation.Types;

    public class Wolf : AnimalEntity
    {
        public Wolf(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class WolfSpecies : AnimalSpecies
        {
            public WolfSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Wolf);

                // Info
                this.Name = "Wolf";
                this.DisplayName = Localizer.DoStr("Wolf");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(Hare), typeof(Elk), typeof(MountainGoat), typeof(Deer)};
                this.CalorieValue = 100f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(WolfCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(LandPredatorBrain);
                this.WanderingSpeed = 0.7f;
                this.Speed = 5.5f;
                this.Health = 1.5f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 0.5f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                this.HeadDistance = 0.8f;
                this.TimeLayToStand = 4f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }

        public override bool ShouldSleep { get { return !WorldTime.IsNight(); } }
    }
}
