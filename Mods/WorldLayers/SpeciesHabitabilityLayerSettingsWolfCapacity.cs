namespace Eco.Mods.WorldLayers
{
    using System;
    using System.Collections.Generic;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Components.Habitability;
    using Eco.Simulation.WorldLayers.Layers;

    public class SpeciesHabitabilityLayerSettingsWolfCapacity : SpeciesHabitabilityLayerSettings
    {
        public SpeciesHabitabilityLayerSettingsWolfCapacity() : base()
        {
            this.Species = "Wolf";
            this.HabitabilityComponents.Add( new ConsumerComponent() { CalorieConsumption = 250, Prey = new List<Type> { typeof(AnimalLayerSettingsElk), typeof(AnimalLayerSettingsHare), typeof(AnimalLayerSettingsMountainGoat), typeof(AnimalLayerSettingsDeer) } } );
            this.Name = "WolfCapacity";
            this.DisplayName = Localizer.DoStr("Wolf Capacity");
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
