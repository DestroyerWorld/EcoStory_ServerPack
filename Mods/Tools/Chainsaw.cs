namespace Eco.Mods.TechTree
{
    using Eco.Shared.Localization;

    public partial class ChainsawItem : AxeItem
    {
        public override LocString DisplayDescription { get { return Localizer.DoStr("A motorized saw used for chopping trees."); } }
    }
}