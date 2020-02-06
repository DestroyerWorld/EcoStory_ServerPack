// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Plants;
    using Eco.Gameplay.Stats;
    using Eco.Shared.Localization;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    using Eco.Simulation;
    using Eco.Simulation.Types;
    using Eco.Shared.Utils;

    [Category("Hidden")]
    public partial class AxeItem
    {
        private static IDynamicValue caloriesBurn;
        private static IDynamicValue damage;
        static AxeItem()
        {
            string axeUiLink = new AxeItem().UILink();
            caloriesBurn = new ConstantValue(0);
            damage = new ConstantValue(100);
        }
        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        public override Type ExperienceSkill { get { return typeof(LoggingSkill); } }

        public override Item RepairItem { get { return Item.Get<StoneItem>(); } }
        public override int FullRepairAmount { get { return 1; } }

        public override IDynamicValue CaloriesBurn   { get { return caloriesBurn; } }
        public override IDynamicValue Damage         { get { return damage; } }

        static IDynamicValue tier = new ConstantValue(0);
        public override IDynamicValue Tier { get { return tier; } }

        public override LocString LeftActionDescription { get { return Localizer.DoStr("Chop"); } }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (context.HasBlock)
            {
                var block = World.GetBlock(context.BlockPosition.Value);
                if (block.Is<TreeDebris>())
                {
                    InventoryChangeSet changes = new InventoryChangeSet(context.Player.User.Inventory, context.Player.User);
                    //TREE DEBRIS REWARDS
                    (EcoSim.GetSpecies(block.Get<TreeDebris>().Species) as TreeSpecies).DebrisResources.ForEach(x => changes.AddItems(x.Key, x.Value.RandInt));
                    IAtomicAction lawAction = PlayerActions.PickUp.CreateAtomicAction(context.Player.User, Get<WoodPulpItem>(), context.BlockPosition.Value);
                    var result = (InteractResult)this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false, context.Player.User.Talentset.HasTalent(typeof(LoggingCleanupCrewTalent)) ? 1 : 3, null, changes, lawAction);
                    if (result.IsSuccess)
                        this.AddExperience(context.Player.User, 0.1f, Localizer.DoStr("removing tree debris"));
                    return result;
                }

                if (block.Is<Chopable>())
                {
                    var plant = EcoSim.PlantSim.GetPlant(context.BlockPosition.Value);
                    if (plant != null) return (InteractResult)this.PlayerDeleteBlock(context.BlockPosition.Value, context.Player, false);
                }
                return InteractResult.NoOp;
            }

            if (context.Target is WorldObject) return BasicToolOnWorldObjectCheck(context);

            return base.OnActLeft(context);
        }

        public override bool ShouldHighlight(Type block)
        {
            return Block.Is<TreeDebris>(block);
        }
    }
}
