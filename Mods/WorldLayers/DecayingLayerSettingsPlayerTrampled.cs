namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public class DecayingLayerSettingsPlayerTrampled : DecayingLayerSettings
    {
        public DecayingLayerSettingsPlayerTrampled() : base()
        {
            this.Name = "PlayerTrampled";
            this.DisplayName = Localizer.DoStr("Player Trampled");
            this.InitMultiplier = 1f;
            this.SyncToClient = false;
            this.Range = new Range(0f, 1f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(1f, 0.5f, 0.5f);
            this.Percent = true;
            this.SumRelevant = false;
            this.Unit = "";
            this.VoxelsPerEntry = 5;
            this.Category = WorldLayerCategory.Pollution;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
            this.DecayRateFlat = 0f;
            this.DecayRatePercent = 0.001f;

        }
    }
}
