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
    public partial class FriedHareHaunchesItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Fried Hare Haunches"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Everything is better deep fried."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 6, Fat = 27, Protein = 15, Vitamins = 4};
        public override float Calories                          { get { return 700; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 2)]    
    public partial class FriedHareHaunchesRecipe : Recipe
    {
        public FriedHareHaunchesRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FriedHareHaunchesItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PreparedMeatItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<FlourItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<OilItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FriedHareHaunchesRecipe), Item.Get<FriedHareHaunchesItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Fried Hare Haunches"), typeof(FriedHareHaunchesRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}