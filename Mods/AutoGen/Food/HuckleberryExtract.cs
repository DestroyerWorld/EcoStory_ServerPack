namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Gameplay.Objects;

    [Serialized]
    [Weight(200)]                                          
    public partial class HuckleberryExtractItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Huckleberry Extract"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A concentrated blast of huckleberry goodness."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 15};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class HuckleberryExtractRecipe : Recipe
    {
        public HuckleberryExtractRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HuckleberryExtractItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HuckleberriesItem>(typeof(AdvancedCookingSkill), 50, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HuckleberryExtractRecipe), Item.Get<HuckleberryExtractItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Huckleberry Extract"), typeof(HuckleberryExtractRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}