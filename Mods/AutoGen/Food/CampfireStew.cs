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
    public partial class CampfireStewItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Campfire Stew"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A thick stew chock-full of meat, camas, and corn. A suprisingly good combination."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 5, Fat = 9, Protein = 12, Vitamins = 4};
        public override float Calories                          { get { return 1200; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 3)]    
    public partial class CampfireStewRecipe : Recipe
    {
        public CampfireStewRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CampfireStewItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ScrapMeatItem>(typeof(AdvancedCampfireCookingSkill), 30, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<CharredCamasBulbItem>(typeof(AdvancedCampfireCookingSkill), 10, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<CharredCornItem>(typeof(AdvancedCampfireCookingSkill), 10, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<TallowItem>(typeof(AdvancedCampfireCookingSkill), 3, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CampfireStewRecipe), Item.Get<CampfireStewItem>().UILink(), 10, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Campfire Stew"), typeof(CampfireStewRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}