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
    [Weight(400)]                                          
    public partial class CrimsonSaladItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Crimson Salad"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Just in case you want to eat red things without eating meat."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 15, Fat = 12, Protein = 9, Vitamins = 20};
        public override float Calories                          { get { return 1100; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 3)]    
    public partial class CrimsonSaladRecipe : Recipe
    {
        public CrimsonSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CrimsonSaladItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BasicSaladItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<InfusedOilItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<CharredBeetItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<TomatoItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CrimsonSaladRecipe), Item.Get<CrimsonSaladItem>().UILink(), 15, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Crimson Salad"), typeof(CrimsonSaladRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}