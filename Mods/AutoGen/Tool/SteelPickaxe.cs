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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]   
    [RepairRequiresSkill(typeof(AdvancedSmeltingSkill), 1)] 
    public partial class SteelPickaxeRecipe : Recipe
    {
        public SteelPickaxeRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<SteelPickaxeItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(AdvancedSmeltingSkill), 10, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 20, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelPickaxeRecipe), Item.Get<SteelPickaxeItem>().UILink(), 0.5f, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Steel Pickaxe"), typeof(SteelPickaxeRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
    [Serialized]
    [ItemTier(4)] 
    [Weight(1000)]
    [Category("Tool")]
    public partial class SteelPickaxeItem : PickaxeItem
    {

        public override LocString DisplayName { get { return Localizer.DoStr("Steel Pickaxe"); } }
        private static IDynamicValue caloriesBurn = CreateCalorieValue(15, typeof(MiningSkill), typeof(SteelPickaxeItem), new SteelPickaxeItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Type ExperienceSkill { get { return typeof(MiningSkill); } }
        private static IDynamicValue exp = new ConstantValue(1);
        public override IDynamicValue ExperienceRate { get { return exp; } }
        private static IDynamicValue tier = new MultiDynamicValue(MultiDynamicOps.Sum, new ConstantValue(4), new TalentModifiedValue(typeof(MiningToolStrengthTalent), 0)); 
        public override IDynamicValue Tier { get { return tier; } }


        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(8, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));        
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }


        public override float DurabilityRate { get { return DurabilityMax / 1000f; } }
        
        public override Item RepairItem         {get{ return Item.Get<SteelItem>(); } }
        public override int FullRepairAmount    {get{ return 8; } }
    }
}