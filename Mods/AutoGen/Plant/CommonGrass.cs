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

    public partial class PlantLayerSettingsCommonGrass : PlantLayerSettings
    {
        public PlantLayerSettingsCommonGrass() : base()
        {
            this.Name = "CommonGrass";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Common Grass"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.333333f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Common Grass";
            this.VoxelsPerEntry = 5;
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
    public partial class CommonGrass : PlantEntity
    {
        public CommonGrass(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public CommonGrass() { }
        static PlantSpecies species;
        public partial class CommonGrassSpecies : PlantSpecies
        {
            public CommonGrassSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(CommonGrass);

                // Info
                this.Decorative = false;
                this.Name = "CommonGrass";
                this.DisplayName = Localizer.DoStr("Common Grass");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                // Food
                this.CalorieValue = 0.8f;
                // Resources
                this.PostHarvestingGrowth = 0.2f;
                this.ScythingKills = false; 
                this.PickableAtPercent = 0;
                this.RequireHarvestable = false; 
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(PlantFibersItem), new Range(1, 3), 1)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(CommonGrassBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -0.000005f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.02f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop =  1 }); 
                this.BlanketSpawnPercent = 0.2f; 
                this.IdealTemperatureRange = new Range(0.52f, 0.78f);
                this.IdealMoistureRange = new Range(0.32f, 0.48f);
                this.IdealWaterRange = new Range(0, 0.1f);
                this.WaterExtremes = new Range(0, 0.2f);
                this.TemperatureExtremes = new Range(0.48f, 0.82f);
                this.MoistureExtremes = new Range(0.28f, 0.52f);
                this.MaxPollutionDensity = 1;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }
    [Serialized]
    [Reapable] 
    public partial class CommonGrassBlock :
        PlantBlock { } 
}
