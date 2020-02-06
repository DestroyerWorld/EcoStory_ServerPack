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
    [Weight(100)]                                          
    public partial class YeastItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Yeast"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A fungus that acts as an amazing leavening agent."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 0, Protein = 8, Vitamins = 7};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class YeastRecipe : Recipe
    {
        public YeastRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<YeastItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SugarItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(YeastRecipe), Item.Get<YeastItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Yeast"), typeof(YeastRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}