namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.World.Blocks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Items.SearchAndSelect;

    [Serialized, Weight(30), StartsDiscovered]
    [MaxStackSize(10)]
    [RequiresTool(typeof(ShovelItem))]
    public class TailingsItem : BlockItem<TailingsBlock>
    {
        public override LocString DisplayName       { get { return Localizer.DoStr("Tailings"); } }
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Tailings"); } }
        public override LocString DisplayDescription        { get { return Localizer.DoStr("Waste product from smelting.  When left on soil the run-off will create pollution; killing nearby plants and seeping into the water supply.  Contain in buildings or bury in rock to neutralize."); } }
        public override bool CanStickToWalls      { get { return false; } }
    }
}