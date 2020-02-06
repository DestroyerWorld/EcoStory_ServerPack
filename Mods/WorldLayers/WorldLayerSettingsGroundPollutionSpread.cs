namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public class WorldLayerSettingsGroundPollutionSpread : WorldLayerSettings
    {
        public WorldLayerSettingsGroundPollutionSpread() : base()
        {
            this.Name = "GroundPollutionSpread";
            this.DisplayName = Localizer.DoStr("Ground Pollution");
            this.InitMultiplier = 1f;
            this.SyncToClient = true;
            this.Range = new Range(0f, 1f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 0f, 1f);
            this.Percent = true;
            this.SumRelevant = false;
            this.Unit = "";
            this.VoxelsPerEntry = 5;
            this.Category = WorldLayerCategory.Pollution;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";

        }
    }
}
