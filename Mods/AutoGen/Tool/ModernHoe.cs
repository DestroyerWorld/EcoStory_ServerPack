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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]   
    [RepairRequiresSkill(typeof(AdvancedSmeltingSkill), 3)] 
    public partial class ModernHoeRecipe : Recipe
    {
        public ModernHoeRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<ModernHoeItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiberglassItem>(typeof(AdvancedSmeltingSkill), 20, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 30, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ModernHoeRecipe), Item.Get<ModernHoeItem>().UILink(), 0.5f, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Modern Hoe"), typeof(ModernHoeRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
    [Serialized]
    [ItemTier(6)] 
    [Weight(1000)]
    [Category("Tool")]
    public partial class ModernHoeItem : HoeItem
    {

        public override LocString DisplayName { get { return Localizer.DoStr("Modern Hoe"); } }
        private static IDynamicValue caloriesBurn = CreateCalorieValue(10, typeof(FarmingSkill), typeof(ModernHoeItem), new ModernHoeItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Type ExperienceSkill { get { return typeof(FarmingSkill); } }
        private static IDynamicValue exp = new ConstantValue(1);
        public override IDynamicValue ExperienceRate { get { return exp; } }
        private static IDynamicValue tier = new ConstantValue(6); 
        public override IDynamicValue Tier { get { return tier; } }


        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(15, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));        
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }


        public override float DurabilityRate { get { return DurabilityMax / 7500f; } }
        
        public override Item RepairItem         {get{ return Item.Get<SteelItem>(); } }
        public override int FullRepairAmount    {get{ return 15; } }
    }
}