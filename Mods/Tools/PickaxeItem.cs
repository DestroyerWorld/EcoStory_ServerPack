// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Core.Utils;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Stats;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Objects;

    [Category("Hidden")]
    public partial class PickaxeItem : ToolItem
    {
        private static SkillModifiedValue caloriesBurn = CreateCalorieValue(20, typeof(MiningSkill), typeof(PickaxeItem), new PickaxeItem().UILink());
        static PickaxeItem() { }

        public override IDynamicValue CaloriesBurn            { get { return caloriesBurn; } }

        public override ClientPredictedBlockAction LeftAction { get { return ClientPredictedBlockAction.Mine; } }
        public override LocString LeftActionDescription          { get { return Localizer.DoStr("Mine"); } }

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

        static IDynamicValue tier = new ConstantValue(0);
        public override IDynamicValue Tier { get { return tier; } }

        public override Item RepairItem { get { return Item.Get<StoneItem>(); } }
        public override int FullRepairAmount { get { return 1; } }
       
        [Tooltip(200)]
        public TooltipSection MinablesTooltip(Player player)
        {
            var myHardness = this.Tier.GetCurrentValue(player.User);
            var minableBlockTypes = Block.BlockTypesWithAttribute(typeof(Minable)).Select(x => new KeyValuePair<Type, float>(x, Block.Get<Minable>(x).Hardness)).ToList();

            if (!minableBlockTypes.Any()) return null;

            var allBlocks = AllItems.OfType<BlockItem>();

            var resList = new List<LocString>();
            minableBlockTypes.OrderBy(item => item.Value).ForEach(x =>
            {
                var targetItem = allBlocks.FirstOrDefault(item => item.OriginType == x.Key);
                var hitCount = (int)Math.Ceiling(x.Value / myHardness);
                if (targetItem != null) resList.Add(new LocString(string.Format("{0}: {1} {2}", targetItem.UILink(), hitCount, "hit".Pluralize(hitCount))));
            });

            return new TooltipSection(Localizer.DoStr("Can mine"), resList.FoldoutListLoc("item"));
        }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (context.HasBlock && context.Block.Is<Minable>())
            {
                var user = context.Player.User;
                var item = context.Block is IRepresentsItem ? Item.Get((IRepresentsItem)context.Block) : null;

                var totalDamageToTarget = user.BlockHitCache.MemorizeHit(context.Block.GetType(), context.BlockPosition.Value, this.Tier.GetCurrentValue(context.Player.User));
                if (context.Block.Get<Minable>().Hardness <= totalDamageToTarget)
                {
                    Result result;
                    if (item != null)
                    {
                        IAtomicAction lawAction = PlayerActions.PickUp.CreateAtomicAction(context.Player.User, item, context.BlockPosition.Value);
                        result = this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false, 1, null, lawAction);
                    }
                    else
                        result = this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false, 1);

                    if (result.Success)
                    {
                        var forced = context.Player.User.Talentset.HasTalent(typeof(MiningLuckyBreakTalent)) ? 4 : -1;
                        if (RubbleObject.TrySpawnFromBlock(context.Player, context.Block.GetType(), context.BlockPosition.Value, forced))
                        {
                            var addition = item != null ? " " + (item.UILink()) : string.Empty;
                            this.AddExperience(user, 1f, new LocString(Localizer.Format("mining") + addition));
                            user.UserUI.OnCreateRubble.Invoke(item.DisplayName.NotTranslated);
                            user.BlockHitCache.ForgetHit(context.BlockPosition.Value);
                        }
                    }

                    return (InteractResult)result;
                }
                else
                    this.BurnCalories(context.Player);
            }
            else if (context.Target is RubbleObject)
            {
                var rubble = (RubbleObject)context.Target;
                if (rubble.IsBreakable)
                {
                    rubble.Breakup(context.Player);
                    this.BurnCalories(context.Player);
                    return InteractResult.Success;
                }
                else
                    return InteractResult.NoOp;
            }

            if (context.Target is WorldObject) return this.BasicToolOnWorldObjectCheck(context);

            return InteractResult.NoOp;
        }

        public override bool ShouldHighlight(Type block)
        {
            return Block.Is<Minable>(block);
        }

        public override bool CanPickUpItemStack(ItemStack stack)
        {
            var blockitem = stack.Item as BlockItem;
            return stack.Item.IsCarried && blockitem != null && Block.Get<Minable>(blockitem.OriginType) != null;
        }        
    }
}
