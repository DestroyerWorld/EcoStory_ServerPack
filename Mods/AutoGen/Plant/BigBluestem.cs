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

    public partial class PlantLayerSettingsBigBluestem : PlantLayerSettings
    {
        public PlantLayerSettingsBigBluestem() : base()
        {
            this.Name = "BigBluestem";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Big Bluestem"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.333333f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Big Bluestem";
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
    public partial class BigBluestem : PlantEntity
    {
        public BigBluestem(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public BigBluestem() { }
        static PlantSpecies species;
        public partial class BigBluestemSpecies : PlantSpecies
        {
            public BigBluestemSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(BigBluestem);

                // Info
                this.Decorative = false;
                this.Name = "BigBluestem";
                this.DisplayName = Localizer.DoStr("Big Bluestem");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                // Food
                this.CalorieValue = 4;
                // Resources
                this.PostHarvestingGrowth = 0.2f;
                this.ScythingKills = false; 
                this.PickableAtPercent = 0;
                this.RequireHarvestable = false; 
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(PlantFibersItem), new Range(2, 7), 1),
                   new SpeciesResource(typeof(BigBluestemSeedItem), new Range(1, 2), 0.1f)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(BigBluestemBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -0.00001f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.01f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.01f, MaxResourceContent =  0.02f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop =  1 }); 
                this.BlanketSpawnPercent = 0.35f; 
                this.IdealTemperatureRange = new Range(0.63f, 0.79f);
                this.IdealMoistureRange = new Range(0.32f, 0.48f);
                this.IdealWaterRange = new Range(0, 0.1f);
                this.WaterExtremes = new Range(0, 0.2f);
                this.TemperatureExtremes = new Range(0.6f, 0.82f);
                this.MoistureExtremes = new Range(0.28f, 0.52f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }
    [Serialized]
    [Reapable] 
    [MoveEfficiency(0.8f)] 
    public partial class BigBluestemBlock :
        PlantBlock { } 
}
