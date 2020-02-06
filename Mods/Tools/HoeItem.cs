// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.World;
using Eco.World.Blocks;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;

[Serialized]
[Category("Hidden")]
[Hoer]
public class HoeItem : ToolItem
{
    public override LocString DisplayName { get { return Localizer.DoStr("Hoe"); } }
    public override LocString DisplayDescription  { get { return Localizer.DoStr("Used to till soil and prepare it for planting."); } }

    private static IDynamicValue skilledRepairCost = new ConstantValue(1);
    public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

    public override Item RepairItem { get { return Item.Get<StoneItem>(); } }
    public override int FullRepairAmount { get { return 1; } }

    public override InteractResult OnActLeft(InteractionContext context)
    {
        if (context.HasBlock)
        {
            var abovePos = context.BlockPosition.Value + Vector3i.Up;
            var aboveBlock = World.GetBlock(abovePos);
            if (!aboveBlock.Is<Solid>() && context.Block.Is<Tillable>() && !(aboveBlock is TreeBlock))
                return (InteractResult)this.PlayerPlaceBlock<TilledDirtBlock>(
                    context.BlockPosition.Value, context.Player, true, 1, UsableItemUtils.TryDestroyPlant(context.Player, abovePos));
            else
                return InteractResult.NoOp;
        }

        return base.OnActLeft(context);
    }
    static IDynamicValue caloriesBurn = new ConstantValue(1);
    public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
    static IDynamicValue tier = new ConstantValue(0);
    public override IDynamicValue Tier { get { return tier; } }

    public override bool ShouldHighlight(Type block)
    {
        return Block.Is<Tillable>(block);
    }
}