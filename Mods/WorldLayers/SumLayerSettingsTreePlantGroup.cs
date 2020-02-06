namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public class SumLayerSettingsTreePlantGroup : SumLayerSettings
    {
        public SumLayerSettingsTreePlantGroup() : base()
        {
            this.Name = "TreePlantGroup";
            this.DisplayName = Localizer.DoStr("Trees");
            this.InitMultiplier = 1f;
            this.SyncToClient = false;
            this.Range = new Range(0f, 16f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = true;
            this.SumRelevant = false;
            this.Unit = "";
            this.VoxelsPerEntry = 20;
            this.Category = WorldLayerCategory.PlantGroup;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
            this.Layers = new string[] {"Birch", "Cedar", "Redwood"};

        }
    }
}
