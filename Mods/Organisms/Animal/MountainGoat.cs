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

    public class MountainGoat : AnimalEntity
    {
        public MountainGoat(Animal parent, Vector3 pos, bool corpse = false) : base(parent, pos, species, corpse) { }
        static AnimalSpecies species;
        public class MountainGoatSpecies : AnimalSpecies
        {
            public MountainGoatSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(MountainGoat);

                // Info
                this.Name = "MountainGoat";
                this.DisplayName = Localizer.DoStr("MountainGoat");
                // Lifetime
                this.MaturityAgeDays = 1f;
                // Food
                this.FoodSources = new List<System.Type>() {typeof(PeatMoss), typeof(Saxifrage), typeof(Lupine), typeof(Fireweed), typeof(DwarfWillow), typeof(ArcticWillow)};
                this.CalorieValue = 100f;
                // Movement
                this.Flying = false;
                this.Swimming = false;
                // Resources
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(MountainGoatCarcassItem), new Range(1f, 1f)),
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Behavior
                this.BrainType = typeof(HerdAnimalBrain);
                this.WanderingSpeed = 1.2f;
                this.Speed = 5.5f;
                this.Health = 3.2f;
                this.Damage = 1f;
                this.DelayBetweenAttacksRangeSec = new Range(0.8f, 2.5f);
                this.FearFactor = 1f;
                this.FleePlayers = true;
                this.AttackRange = 1f;
                this.HeadDistance = 0.75f;
                this.TimeLayToStand = 2.5f;
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
