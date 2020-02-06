namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public class SpreadLayerSettingsSaltWaterSpread : SpreadLayerSettings
    {
        public SpreadLayerSettingsSaltWaterSpread() : base()
        {
            this.HeightLayerName = "Height";
            this.Name = "SaltWaterSpread";
            this.DisplayName = Localizer.DoStr("Salt Water Spread");
            this.InitMultiplier = 1f;
            this.SyncToClient = true;
            this.Range = new Range(0f, 1f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 0.8f, 1f);
            this.Percent = true;
            this.SumRelevant = false;
            this.Unit = "";
            this.VoxelsPerEntry = 5;
            this.Category = WorldLayerCategory.World;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
            this.SpreadRate = 0.9f;
            this.ZeroAtHeightDiff = 5f;
            this.SourceInfluenceRate = 1f;
            this.BaseLayerName = "SaltWater";

        }
    }
}
