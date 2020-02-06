namespace Eco.Mods.Organisms
{
    using System.Collections.Generic;
    using Eco.Gameplay.Animals;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.Agents;
    using Eco.Simulation.Types;

    public class BighornSheep : AnimalEntity
    {
        public BighornSheep(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class BighornSheepSpecies : AnimalSpecies
        {
            public BighornSheepSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(BighornSheep);

                // Info
                this.Name = "BighornSheep";
                this.DisplayName = Localizer.DoStr("BighornSheep");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(DwarfWillow), typeof(PricklyPear), typeof(Agave), typeof(Sagebrush)};
                this.CalorieValue = 100f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(BighornCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(HerdAnimalBrain);
                this.WanderingSpeed = 1f;
                this.Speed = 5.5f;
                this.Health = 3.2f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 1f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                this.HeadDistance = 0.9f;
                this.TimeLayToStand = 2.5f;
                // Climate
                this.ReleasesCO2ppmPerDay = 0f;

            }
        }
    }
}
