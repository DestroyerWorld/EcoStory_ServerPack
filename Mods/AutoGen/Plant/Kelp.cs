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

    public partial class PlantLayerSettingsKelp : PlantLayerSettings
    {
        public PlantLayerSettingsKelp() : base()
        {
            this.Name = "Kelp";
            this.DisplayName = string.Format("{0} {1}", Localizer.DoStr("Kelp"), Localizer.DoStr("Population"));
            this.InitMultiplier = 1;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = new Range(0f, 0.333333f);
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Kelp";
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
    public partial class Kelp : PlantEntity
    {
        public Kelp(WorldPosition3i mapPos, PlantPack plantPack) : base(species, mapPos, plantPack) { }
        public Kelp() { }
        static PlantSpecies species;
        public partial class KelpSpecies : PlantSpecies
        {
            public KelpSpecies() : base()
            {
                species = this;
                this.InstanceType = typeof(Kelp);

                // Info
                this.Decorative = false;
                this.Name = "Kelp";
                this.DisplayName = Localizer.DoStr("Kelp");
                // Lifetime
                this.MaturityAgeDays = 0.8f;
                // Generation
                this.Water = true; 
                // Food
                this.CalorieValue = 2;
                // Resources
                this.PostHarvestingGrowth = 0;
                this.PickableAtPercent = 0;
                this.ResourceList = new List<SpeciesResource>()
                {
                    new SpeciesResource(typeof(KelpItem), new Range(1, 3), 1)
                };
                this.ResourceBonusAtGrowth = 0.9f;
                // Visuals
                this.BlockType = typeof(KelpBlock);
                // Climate
                this.ReleasesCO2ppmPerDay = -0.000005f;
                // WorldLayers
                this.MaxGrowthRate = 0.01f;
                this.MaxDeathRate = 0.005f;
                this.SpreadRate = 0.001f;
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Nitrogen", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.05f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Phosphorus", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.05f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "Potassium", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.05f });
                this.ResourceConstraints.Add(new ResourceConstraint() { LayerName = "SoilMoisture", HalfSpeedConcentration =  0.1f, MaxResourceContent =  0.1f }); 
                this.CapacityConstraints.Add(new CapacityConstraint() { CapacityLayerName = "UnderwaterFertileGround", ConsumedCapacityPerPop =  4 }); 
                this.BlanketSpawnPercent = 0.5f; 
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
    [UnderWater, Reapable] 
    [MoveEfficiency(0.5f)] 
    public partial class KelpBlock :
        PlantBlock { } 
}
