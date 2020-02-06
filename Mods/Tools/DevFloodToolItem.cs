// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Shared.Serialization;
using Eco.World;
using Eco.World.Blocks;
using Eco.World.Utils;
using Eco.Gameplay.DynamicValues;
using Eco.Shared.Localization;

[Serialized]
[Category("Hidden")]
public class DevFloodToolItem : ToolItem
{
    public override LocString DisplayDescription  { get { return Localizer.DoStr("Flood fixing tool! Left click to remove the top water layer. (Target a wall touching water)."); } }
    public override LocString DisplayName { get { return Localizer.DoStr("Dev Flood Tool"); } }

    private static IDynamicValue skilledRepairCost = new ConstantValue(1);
    public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

    public override InteractResult OnActLeft(InteractionContext context)
    {
        var clickPos = context.BlockPosition + context.Normal;
        if (clickPos.HasValue && World.GetBlock(clickPos.Value).Is<UnderWater>())
            Flooding.DeleteTopWaterLayer(clickPos.Value);
        else
            Flooding.DeleteTopWaterLayer(context.Player.Position.Round);

        return InteractResult.Success;
    }
}