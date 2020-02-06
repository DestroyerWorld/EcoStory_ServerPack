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
    public partial class CharredBeetItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Beet"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Perhaps not the best raw vegetable to char, this beet seems to have held up well enough."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 3, Protein = 0, Vitamins = 7};
        public override float Calories                          { get { return 470; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class CharredBeetRecipe : Recipe
    {
        public CharredBeetRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredBeetItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeetItem>(1)    
            };
            this.CraftMinutes = new ConstantValue(2);     
            this.Initialize(Localizer.DoStr("Charred Beet"), typeof(CharredBeetRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}