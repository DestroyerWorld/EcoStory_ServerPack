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

    public partial class PlantLayerSettingsSaguaroCactus : PlantLayerSettings
    {
        public PlantLayerSettingsSaguaroCactus() : base()
        {
            this.Name = "SaguaroCactus";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Saguaro Cactus"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.05f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Saguaro Cactus";
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
    public partial class SaguaroCactus : TreeEntity
    {
        public SaguaroCactus(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public SaguaroCactus() { }
        static TreeSpecies species;
        public partial class SaguaroCactusSpecies : TreeSpecies
        {
            public SaguaroCactusSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(SaguaroCactus);

                // Info
                this.Decorative = false;
                this.Name = "SaguaroCactus";
                this.DisplayName = Localizer.DoStr("Saguaro Cactus");
                // Lifetime
                this.MaturityAgeDays = 2.5f;
                // Generation
                // Food
                this.CalorieValue = 12;
                // Resources
                this.PostHarvestingGrowth = 0;
                this.ScythingKills = false; 
                this.PickableAtPercent = 0;
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(LogItem), new Range(0, 30), 1)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                // Climate
                this.ReleasesCO2ppmPerDay = -0.001f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.1f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "FertileGround", ConsumedCapacityPerPop =  4 });
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "CanopySpace", ConsumedCapacityPerPop =  19 }); 
                this.GenerationSpawnCountPerPoint = new Range(3, 7); 
                this.GenerationSpawnPointMultiplier = 0.09f; 
                this.IdealTemperatureRange = new Range(0.8f, 0.9f);
                this.IdealMoistureRange = new Range(0.1f, 0.2f);
                this.IdealWaterRange = new Range(0, 0.1f);
                this.WaterExtremes = new Range(0, 0.2f);
                this.TemperatureExtremes = new Range(0.7f, 1);
                this.MoistureExtremes = new Range(0, 0.38f);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 20;

            }
        }
    }
}
