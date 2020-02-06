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
    public partial class CampfireRoastItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Campfire Roast"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("The uneven flame might be mediocre for cooking, but the open flame imparts a great flavor."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 12, Protein = 16, Vitamins = 0};
        public override float Calories                          { get { return 1000; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 2)]    
    public partial class CampfireRoastRecipe : Recipe
    {
        public CampfireRoastRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CampfireRoastItem>(),
               
               new CraftingElement<TallowItem>(1) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawRoastItem>(typeof(AdvancedCampfireCookingSkill), 3, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CampfireRoastRecipe), Item.Get<CampfireRoastItem>().UILink(), 10, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Campfire Roast"), typeof(CampfireRoastRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}