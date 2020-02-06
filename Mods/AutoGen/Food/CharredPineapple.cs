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
    public partial class CharredPineappleItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Pineapple"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr(""); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 9, Fat = 0, Protein = 1, Vitamins = 3};
        public override float Calories                          { get { return 540; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class CharredPineappleRecipe : Recipe
    {
        public CharredPineappleRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredPineappleItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PineappleItem>(1)    
            };
            this.CraftMinutes = new ConstantValue(3);     
            this.Initialize(Localizer.DoStr("Charred Pineapple"), typeof(CharredPineappleRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}