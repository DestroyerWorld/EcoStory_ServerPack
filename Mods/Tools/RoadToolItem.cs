// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Shared.Math;
    using Eco.Simulation;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Skills;
    using Shared.Localization;

    [Category("Tools")]
    [RepairRequiresSkill(typeof(BasicEngineeringSkill), 1)]
    public partial class RoadToolItem : ToolItem
    {
        static IDynamicValue caloriesBurn = new ConstantValue(15);
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }

        public override Item RepairItem { get { return Item.Get<LogItem>(); } }
        public override int FullRepairAmount { get { return 10; } }

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (!context.HasBlock)
                return InteractResult.NoOp;

            Type blockType = this.GetRoadBlock(context.Block);

            if (blockType != null)
            {
                if (TreeEntity.TreeRootsBlockDigging(context))
                    return InteractResult.Failure(Localizer.DoStr("You attempt to make a road, but the roots are too strong!"));
                IAtomicAction destroyAction = UsableItemUtils.TryDestroyPlant(
                    context.Player, context.BlockPosition.Value + Vector3i.Up, DeathType.Construction);
                return (InteractResult)this.PlayerPlaceBlock(blockType, context.BlockPosition.Value, context.Player, true, 1, destroyAction);
            }
            else
                return InteractResult.NoOp;
        }

        private Type GetRoadBlock(Block currentBlock)
        {
            if (currentBlock is DirtBlock || currentBlock is MudBlock || currentBlock is GrassBlock || currentBlock is TilledDirtBlock || currentBlock is RockySoilBlock)
                return typeof(DirtRoadBlock);
            else
                return null;
        }
    }
}