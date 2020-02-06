// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.ComponentModel;
using Eco.Gameplay.Auth;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;
using Eco.World;
using Eco.World.Blocks;

[Serialized]
[Category("Hidden")]
[CanMakeBlockForm(new[] {"Wall", "Floor", "Roof", "Stairs", "Window", "Fence", "Aqueduct", "Cube", "Column", "Ladder"})]
public class HammerItem : ToolItem
{
    public override ToolCategory ToolCategory { get { return ToolCategory.Hammer; } }

    public override LocString DisplayDescription                    { get { return Localizer.DoStr("Destroys constructed materials."); } }
    public override LocString DisplayName                   { get { return Localizer.DoStr("Hammer"); } }

    private static IDynamicValue skilledRepairCost = new ConstantValue(1);
    public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

    public override ClientPredictedBlockAction LeftAction { get { return ClientPredictedBlockAction.PickupBlock; } }
    public override LocString LeftActionDescription          { get { return Localizer.DoStr("Pick Up"); } }

    static IDynamicValue tier = new ConstantValue(0);
    public override IDynamicValue Tier { get { return tier; } }

    static IDynamicValue caloriesBurn = new ConstantValue(1);
    public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }

    public override InteractResult OnActLeft(InteractionContext context)
    {
        if (context.HasBlock)
        {
            if (context.Block.Is<Constructed>())
                return (InteractResult)this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, true, 1);
            else if (context.Block is WorldObjectBlock)
                return this.TryPickUp(((WorldObjectBlock)context.Block).WorldObjectHandle.Object, context);
            else
                return InteractResult.NoOp;
        }
        else if (context.Target is WorldObject)
            return this.TryPickUp((WorldObject)context.Target, context);

        return InteractResult.NoOp;
    }

    public override bool ShouldHighlight(Type block)
    {
        return Block.Is<Constructed>(block);
    }

    private InteractResult TryPickUp(WorldObject obj, InteractionContext context)
    {
        var basicResult = BasicToolOnWorldObjectCheck(context);
        if (context.Modifier == InteractionModifier.Ctrl ? basicResult.IsFailure : !basicResult.IsNoOp) return basicResult;

        var authResult = AuthManager.IsAuthorized(obj, context.Player.User, AccessType.FullAccess);
        if (!authResult.Success)
            return (InteractResult)authResult;

        var pickupResult = obj.TryPickUp(context.Player);
        if (pickupResult.Success)
            this.BurnCalories(context.Player);
        return (InteractResult)pickupResult;
    }

    public override LocString GetNoSuitablePickupTargetFailureMessage(Inventory inventory)
    {
        if (!inventory.IsEmpty) return Localizer.DoStr("Object storage must be empty to pick up.");
        return base.GetNoSuitablePickupTargetFailureMessage(inventory);
    }
}
