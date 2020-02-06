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
    public partial class WheatPorridgeItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Wheat Porridge"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Wheat Porridge"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A thick gruel of wheat, wheat, more wheat, and a dash of huckleberry flavor."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat = 0, Protein = 4, Vitamins = 10};
        public override float Calories                          { get { return 510; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]    
    public partial class WheatPorridgeRecipe : Recipe
    {
        public WheatPorridgeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WheatPorridgeItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(AdvancedCampfireCookingSkill), 15, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<HuckleberriesItem>(typeof(AdvancedCampfireCookingSkill), 20, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WheatPorridgeRecipe), Item.Get<WheatPorridgeItem>().UILink(), 3, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wheat Porridge"), typeof(WheatPorridgeRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}