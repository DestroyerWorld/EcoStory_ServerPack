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

    public partial class PlantLayerSettingsUrchin : PlantLayerSettings
    {
        public PlantLayerSettingsUrchin() : base()
        {
            this.Name = "Urchin";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Urchin"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.333333f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Urchin";
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
    public partial class Urchin : PlantEntity
    {
        public Urchin(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Urchin() { }
        static PlantSpecies species;
        public partial class UrchinSpecies : PlantSpecies
        {
            public UrchinSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Urchin);

                // Info
                this.Decorative = false;
                this.Name = "Urchin";
                this.DisplayName = Localizer.DoStr("Urchin");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = true; 
                // Food
                this.CalorieValue = 5;
                // Resources
                this.PostHarvestingGrowth = 0;
                this.PickableAtPercent = 0;
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(UrchinItem), new Range(1, 3), 1)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(UrchinBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = 0;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.2f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "UnderwaterFertileGround", ConsumedCapacityPerPop =  6 }); 
                this.BlanketSpawnPercent = 0.2f; 
                this.IdealTemperatureRange = new Range(0.5f, 0.9f);
                this.IdealMoistureRange = new Range(0.1f, 0.9f);
                this.IdealWaterRange = new Range(0.5f, 1);
                this.WaterExtremes = new Range(0.4f, 1.1f);
                this.TemperatureExtremes = new Range(0.38f, 1);
                this.MoistureExtremes = new Range(0, 1);
                this.MaxPollutionDensity = 0.7f;
                this.PollutionDensityTolerance = 0.1f;
                this.VoxelsPerEntry = 5;

            }
        }
    }
    [Serialized]
    [UnderWater] 
    public partial class UrchinBlock :
        InteractablePlantBlock { } 
}
