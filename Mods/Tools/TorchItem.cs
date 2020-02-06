// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.DynamicValues;

    public partial class TorchItem : ToolItem
    {
        public override bool ShouldHighlight(Type block) { return false; }

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
    }
}