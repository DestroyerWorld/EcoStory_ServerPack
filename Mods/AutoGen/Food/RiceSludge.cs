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
    public partial class RiceSludgeItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Rice Sludge"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Sometimes when you try and make rice, you just add too much water. Some people might call this porridge, but that would indicate intention."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat = 0, Protein = 1, Vitamins = 2};
        public override float Calories                          { get { return 450; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    public partial class RiceSludgeRecipe : Recipe
    {
        public RiceSludgeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RiceSludgeItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RiceItem>(3)    
            };
            this.CraftMinutes = new ConstantValue(2);     
            this.Initialize(Localizer.DoStr("Rice Sludge"), typeof(RiceSludgeRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}