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
    public partial class CharredTaroItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Taro"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr(""); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 11, Fat = 0, Protein = 1, Vitamins = 1};
        public override float Calories                          { get { return 490; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
	[RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]
    public partial class CharredTaroRecipe : Recipe
    {
        public CharredTaroRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredTaroItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TaroRootItem>(2)    
            };
            this.CraftMinutes = new ConstantValue(3);     
            this.Initialize(Localizer.DoStr("Charred Taro"), typeof(CharredTaroRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}