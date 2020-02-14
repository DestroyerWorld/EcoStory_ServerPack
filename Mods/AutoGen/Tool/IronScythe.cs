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
    public partial class IronScytheRecipe : Recipe
    {
        public IronScytheRecipe()
        {
            this.Products = new CraftingElement[] { new CraftingElement<IronScytheItem>() };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(IronScytheRecipe), Item.Get<IronScytheItem>().UILink(), 0.5f, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Iron Scythe"), typeof(IronScytheRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
    [Serialized]
    [ItemTier(2)] 
    [Weight(1000)]
    [Category("Tool")]
    public partial class IronScytheItem : ScytheItem
    {

        public override LocString DisplayName { get { return Localizer.DoStr("Iron Scythe"); } }
        private static IDynamicValue caloriesBurn = CreateCalorieValue(17, typeof(GatheringSkill), typeof(IronScytheItem), new IronScytheItem().UILink());
        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
        public override Type ExperienceSkill { get { return typeof(GatheringSkill); } }
        private static IDynamicValue exp = new ConstantValue(1);
        public override IDynamicValue ExperienceRate { get { return exp; } }
        private static IDynamicValue tier = new MultiDynamicValue(MultiDynamicOps.Sum, new ConstantValue(2), new TalentModifiedValue(typeof(GatheringToolStrengthTalent), 0)); 
        public override IDynamicValue Tier { get { return tier; } }


        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(8, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));        
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }


        public override float DurabilityRate { get { return DurabilityMax / 1500f; } }
        
        public override Item RepairItem         {get{ return Item.Get<IronIngotItem>(); } }
        public override int FullRepairAmount    {get{ return 8; } }
    }
}