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
    [Weight(200)]                                          
    public partial class SimpleSyrupItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Simple Syrup"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A simple water and suger combination heated until the sugar dissolves."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 3, Protein = 0, Vitamins = 0};
        public override float Calories                          { get { return 400; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class SimpleSyrupRecipe : Recipe
    {
        public SimpleSyrupRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SimpleSyrupItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SugarItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SimpleSyrupRecipe), Item.Get<SimpleSyrupItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Simple Syrup"), typeof(SimpleSyrupRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}