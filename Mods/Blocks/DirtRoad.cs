namespace Eco.Mods.TechTree
{
    using System;
    using Gameplay.Items;

    public partial class DirtRoadBlock : IRepresentsItem {
        public Type RepresentedItemType { get { return typeof(DirtItem); } }
    }
}