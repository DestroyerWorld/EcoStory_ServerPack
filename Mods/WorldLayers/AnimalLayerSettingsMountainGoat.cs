namespace Eco.Mods.WorldLayers
{
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Utils;
    using Eco.Simulation.WorldLayers.Layers;

    public class AnimalLayerSettingsMountainGoat : AnimalLayerSettings
    {
        public AnimalLayerSettingsMountainGoat() : base()
        {
            this.Name = "MountainGoat";
            this.DisplayName = Localizer.Format("{0} Population", Localizer.DoStr(this.Name));
            this.InitMultiplier = 1f;
            this.SyncToClient = false;
            this.Range = new Range(0f, 2.1f);
            this.RenderRange = null;
            this.MinColor = new Color(1f, 1f, 1f);
            this.MaxColor = new Color(0f, 1f, 0f);
            this.Percent = false;
            this.SumRelevant = true;
            this.Unit = "Mountain Goats";
            this.VoxelsPerEntry = 40;
            this.Category = WorldLayerCategory.Animal;
            this.ValueType = WorldLayerValueType.Percent;
            this.AreaDescription = "";
            this.HabitabilityLayer = "MountainGoatCapacity";
            this.SpreadRate = 0.5f;
            this.SpeciesChangeRate = 0.02f;

        }
    }
}
