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
    public partial class TortillaItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Tortilla"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A thin, unleavened flatbread."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 20, Fat = 0, Protein = 10, Vitamins = 0};
        public override float Calories                          { get { return 350; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class TortillaRecipe : Recipe
    {
        public TortillaRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TortillaItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornmealItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TortillaRecipe), Item.Get<TortillaItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Tortilla"), typeof(TortillaRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}