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
    public partial class CharredPapayaItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Papaya"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr(""); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 0, Protein = 1, Vitamins = 6};
        public override float Calories                          { get { return 460; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class CharredPapayaRecipe : Recipe
    {
        public CharredPapayaRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredPapayaItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PapayaItem>(1)    
            };
            this.CraftMinutes = new ConstantValue(3);     
            this.Initialize(Localizer.DoStr("Charred Papaya"), typeof(CharredPapayaRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}