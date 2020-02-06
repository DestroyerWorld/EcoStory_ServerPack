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
    [Weight(600)]                                          
    public partial class WildMixItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Wild Mix"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A dressed salad that, with the added sweetness, its pretty tasty."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 11, Fat = 6, Protein = 8, Vitamins = 21};
        public override float Calories                          { get { return 800; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class WildMixRecipe : Recipe
    {
        public WildMixRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WildMixItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BasicSaladItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<HuckleberryExtractItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WildMixRecipe), Item.Get<WildMixItem>().UILink(), 10, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wild Mix"), typeof(WildMixRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}