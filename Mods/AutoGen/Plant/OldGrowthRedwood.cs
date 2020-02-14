namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
}
// WORLD LAYER INFO
namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public partial class PlantLayerSettingsOldGrowthRedwood : PlantLayerSettings
    {
        public PlantLayerSettingsOldGrowthRedwood() : base()
        {
            this.Name = "OldGrowthRedwood";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Old Growth Redwood"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.05f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Old Growth Redwood";
            this.VoxelsPerEntry = 20;
            this.Category = WorldLayerCategory.Plant;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
        }
    }
}

namespace Eco.Mods.Organisms
{
    using System.Collections.Generic; 
    using Eco.Gameplay.Plants;
    using Eco.Mods.TechTree;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.World.Blocks;

    [Serialized]
    public partial class OldGrowthRedwood : TreeEntity
    {
        public OldGrowthRedwood(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public OldGrowthRedwood() { }
        static TreeSpecies species;
        public partial class OldGrowthRedwoodSpecies : TreeSpecies
        {
            public OldGrowthRedwoodSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(OldGrowthRedwood);

                // Info
                this.Decorative = false;
                this.NoSpread = true; 
                this.Name = "OldGrowthRedwood";
                this.DisplayName = Localizer.DoStr("Old Growth Redwood");
                // Lifetime
                this.MaturityAgeDays = 10;
                // Generation
                // Food
                this.CalorieValue = 50;
                // Resources
                this.PostHarvestingGrowth = 0;
                this.ScythingKills = false; 
                this.PickableAtPercent = 0;
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(LogItem), new Range(450, 500), 1)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                // Climate
                this.ReleasesCO2ppmPerDay = -0.004f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.1f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop =  1 });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "CanopySpace", ConsumedCapacityPerPop =  20 }); 
                this.BlanketSpawnPercent = 0.3f; 
                this.IdealTemperatureRange = new Range(0.22f, 0.48f);
                this.IdealMoistureRange = new Range(0.42f, 0.48f);
                this.IdealWaterRange = new Range(0, 0.1f);
                this.WaterExtremes = new Range(0, 0.2f);
                this.TemperatureExtremes = new Range(0.18f, 0.52f);
                this.MoistureExtremes = new Range(0.39f, 0.51f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 20;

            }
        }
    }
}
