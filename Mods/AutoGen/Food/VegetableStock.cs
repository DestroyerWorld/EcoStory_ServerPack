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
    [Weight(800)]                                          
    public partial class VegetableStockItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Vegetable Stock"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A hearty stock full of assorted vegetables."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 11, Fat = 2, Protein = 1, Vitamins = 11};
        public override float Calories                          { get { return 700; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 1)]    
    public partial class VegetableStockRecipe : Recipe
    {
        public VegetableStockRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<VegetableStockItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<VegetableMedleyItem>(typeof(CookingSkill), 4, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(VegetableStockRecipe), Item.Get<VegetableStockItem>().UILink(), 20, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Vegetable Stock"), typeof(VegetableStockRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}