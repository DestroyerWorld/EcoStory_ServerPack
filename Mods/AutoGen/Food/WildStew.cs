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
    [Weight(500)]                                          
    public partial class WildStewItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Wild Stew"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A thick stew made with a variety of vegetables."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 8, Fat = 5, Protein = 5, Vitamins = 12};
        public override float Calories                          { get { return 1200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 3)]    
    public partial class WildStewRecipe : Recipe
    {
        public WildStewRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WildStewItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HuckleberriesItem>(typeof(AdvancedCampfireCookingSkill), 30, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<CharredBeetItem>(typeof(AdvancedCampfireCookingSkill), 10, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<CampfireBeansItem>(typeof(AdvancedCampfireCookingSkill), 10, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WildStewRecipe), Item.Get<WildStewItem>().UILink(), 10, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wild Stew"), typeof(WildStewRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}