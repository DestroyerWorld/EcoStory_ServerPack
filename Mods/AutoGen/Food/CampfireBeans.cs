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
    public partial class CampfireBeansItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Campfire Beans"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Campfire Beans"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A mushy mixture that can serve somewhat as a replacement protein in a meatless diet."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 1, Fat = 3, Protein = 9, Vitamins = 0};
        public override float Calories                          { get { return 500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class CampfireBeansRecipe : Recipe
    {
        public CampfireBeansRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CampfireBeansItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeansItem>(4)    
            };
            this.CraftMinutes = new ConstantValue(2);     
            this.Initialize(Localizer.DoStr("Campfire Beans"), typeof(CampfireBeansRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}