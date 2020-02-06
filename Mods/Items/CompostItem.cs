// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;

    [Serialized]
    [Weight(30000)]
    [MaxStackSize(10)]
    [RequiresTool(typeof(ShovelItem))]
    [MakesRoads]
    public class CompostItem : BlockItem<CompostBlock>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Compost"); } }
        public override LocString DisplayNamePlural  { get { return Localizer.DoStr("Compost"); } }

        public override LocString DisplayDescription { get { return Localizer.DoStr("Delicious decomposed organic matter."); }  }
    }
}