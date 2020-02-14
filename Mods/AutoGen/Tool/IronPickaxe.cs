namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(SmeltingSkill), 1)]   
    [RepairRequiresSkill(typeof(SmeltingSkill), 1)] 
    public partial class IronPickaxeRecipe : Recipe
    {
        public IronPickaxeRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<IronPickaxeItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(IronPickaxeRecipe), Item.Get<IronPickaxeItem>().UILink(), 0.5f, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Iron Pickaxe"), typeof(IronPickaxeRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
    [Serialized]
    [ItemTier(2)] 
    [Weight(1000)]
    [Category("Tool")]
    public partial class IronPickaxeItem : PickaxeItem
    {

        public override LocString DisplayName { get { return Localizer.DoStr("Iron Pickaxe"); } }
        private static IDynamicValue caloriesBurn = CreateCalorieValue(17, typeof(MiningSkill), typeof(IronPickaxeItem), new IronPickaxeItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Type ExperienceSkill { get { return typeof(MiningSkill); } }
        private static IDynamicValue exp = new ConstantValue(1);
        public override IDynamicValue ExperienceRate { get { return exp; } }
        private static IDynamicValue tier = new MultiDynamicValue(MultiDynamicOps.Sum, new ConstantValue(2), new TalentModifiedValue(typeof(MiningToolStrengthTalent), 0)); 
        public override IDynamicValue Tier { get { return tier; } }


        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(8, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));        
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }


        public override float DurabilityRate { get { return DurabilityMax / 1500f; } }
        
        public override Item RepairItem         {get{ return Item.Get<IronIngotItem>(); } }
        public override int FullRepairAmount    {get{ return 8; } }
    }
}