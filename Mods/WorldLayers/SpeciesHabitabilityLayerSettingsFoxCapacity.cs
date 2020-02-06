namespace Eco.Mods.WorldLayers
{
    using System;
    using System.Collections.Generic;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Components.Habitability;
    using Eco.Simulation.WorldLayers.Layers;

    public class SpeciesHabitabilityLayerSettingsFoxCapacity : SpeciesHabitabilityLayerSettings
    {
        public SpeciesHabitabilityLayerSettingsFoxCapacity() : base()
        {
            this.Species = "Fox";
            this.HabitabilityComponents.Add( new ConsumerComponent() { CalorieConsumption = 150, Prey = new List<Type> { typeof(AnimalLayerSettingsHare), typeof(AnimalLayerSettingsTurkey) } } );
            this.Name = "FoxCapacity";
            this.DisplayName = Localizer.DoStr("Fox Capacity");
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
