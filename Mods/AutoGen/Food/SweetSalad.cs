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
    [Weight(400)]                                          
    public partial class SweetSaladItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Sweet Salad"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("The sweetness of the fruits happens to work well with the salad."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 18, Fat = 7, Protein = 9, Vitamins = 22};
        public override float Calories                          { get { return 1100; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 3)]    
    public partial class SweetSaladRecipe : Recipe
    {
        public SweetSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SweetSaladItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BasicSaladItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<FruitSaladItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<SimpleSyrupItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SweetSaladRecipe), Item.Get<SweetSaladItem>().UILink(), 15, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Sweet Salad"), typeof(SweetSaladRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}