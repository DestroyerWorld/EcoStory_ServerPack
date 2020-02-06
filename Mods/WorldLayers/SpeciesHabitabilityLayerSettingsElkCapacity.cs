namespace Eco.Mods.WorldLayers
{
    using System;
    using System.Collections.Generic;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Components.Habitability;
    using Eco.Simulation.WorldLayers.Layers;

    public class SpeciesHabitabilityLayerSettingsElkCapacity : SpeciesHabitabilityLayerSettings
    {
        public SpeciesHabitabilityLayerSettingsElkCapacity() : base()
        {
            this.Species = "Elk";
            this.HabitabilityComponents.Add( new ConsumerComponent() { CalorieConsumption = 300, Prey = new List<Type> { typeof(PlantLayerSettingsHuckleberry), typeof(PlantLayerSettingsCedar), typeof(PlantLayerSettingsBirch), typeof(PlantLayerSettingsFern), typeof(PlantLayerSettingsBeans), typeof(PlantLayerSettingsSalal) } } );
            this.HabitabilityComponents.Add( new LayerRequirementComponent() { Layer = typeof(SumLayerSettingsTreePlantGroup), RequiredPerOrganism = 3 } );
            this.Name = "ElkCapacity";
            this.DisplayName = Localizer.DoStr("Elk Capacity");
            this.InitMultiplier = 1f;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = true;
            this.SumRelevant = false;
            this.Unit = "";
            this.VoxelsPerEntry = 40;
            this.Category = WorldLayerCategory.Animal;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";

        }
    }
}
