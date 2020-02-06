// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.


namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Plants;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation;
    using Eco.World;
    using Eco.World.Blocks;

    [Category("Hidden")]
    [CarryTypesLimited]
    public partial class ShovelItem : ToolItem
    {
        private static SkillModifiedValue caloriesBurn = CreateCalorieValue(20, typeof(SelfImprovementSkill), typeof(ShovelItem), new ShovelItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }

        public override ClientPredictedBlockAction LeftAction { get { return ClientPredictedBlockAction.PickupBlock; } }
        public override LocString LeftActionDescription     { get { return Localizer.DoStr("Dig"); } }

        static IDynamicValue tier = new ConstantValue(0);
        public override IDynamicValue Tier { get { return tier; } }

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

        public override Item RepairItem { get { return Item.Get<StoneItem>(); } }
        public override int FullRepairAmount { get { return 1; } }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (context.HasBlock)
            {
                if (context.Block is PlantBlock)
                {
                    var plant = EcoSim.PlantSim.GetPlant(context.BlockPosition.Value);
                    if (plant != null && plant is IHarvestable)
                    {
                        Result result = ((IHarvestable)plant).TryHarvest(context.Player, true);
                        if (result.Success)
                            this.BurnCalories(context.Player);
                        return (InteractResult)result;
                    }
                    else
                        return (InteractResult)this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false);
                }
                else if (context.Block.Is<Diggable>())
                {
                    if (TreeEntity.TreeRootsBlockDigging(context))
                        return InteractResult.Failure(Localizer.DoStr("You attempt to dig up the soil, but the roots are too strong!"));

                    var destroyAction = UsableItemUtils.TryDestroyPlant(context.Player, context.BlockPosition.Value + Vector3i.Up);

                    return (InteractResult)this.PlayerDeleteBlock(
                        context.BlockPosition.Value, context.Player, true, 1, new DirtItem(), destroyAction);
                }
            }

            if (context.Target is WorldObject) return BasicToolOnWorldObjectCheck(context);

            return InteractResult.NoOp;
        }

        public override int MaxTake                         { get { return 1; } }
        public override bool ShouldHighlight(Type block)    { return Block.Is<Diggable>(block);}
    }
}