namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Components;
    using Eco.Simulation.WorldLayers.Layers;
    using Eco.World.Blocks;

    public class SpreadLayerSettingsTrampledSpread : SpreadLayerSettings
    {
        public SpreadLayerSettingsTrampledSpread() : base()
        {
            this.HeightLayerName = "Height";
            this.Name = "TrampledSpread";
            this.DisplayName = Localizer.DoStr("Trampled Spread");
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
            this.Components.Add( new BiomeComponent() { TopBlock = typeof(DirtBlock) });
            this.SpreadRate = 0.5f;
            this.ZeroAtHeightDiff = 3f;
            this.SourceInfluenceRate = 1f;
            this.BaseLayerName = "TrampledSum";

        }
    }
}
