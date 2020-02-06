namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;

    public partial class IronPipeItem : BlockItem<IronPipeBlock>
    {
        public override bool DisplayCrate { get { return true; } }
    }
    public partial class SteelPipeItem : BlockItem<SteelPipeBlock>
    {
        public override bool DisplayCrate { get { return true; } }
    }
    public partial class CopperPipeItem : BlockItem<CopperPipeBlock>
    {
        public override bool DisplayCrate { get { return true; } }
    }
}