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
    [Weight(800)]                                          
    public partial class CharredMeatItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Meat"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Charred Meat"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("The blackened surface of this unrecognizable meat is 'golden brown'."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 10, Protein = 10, Vitamins = 0};
        public override float Calories                          { get { return 550; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
	[RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]
    public partial class CharredMeatRecipe : Recipe
    {
        public CharredMeatRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredMeatItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(AdvancedCampfireCookingSkill), 1, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CharredMeatRecipe), Item.Get<CharredMeatItem>().UILink(), 3, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Charred Meat"), typeof(CharredMeatRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}