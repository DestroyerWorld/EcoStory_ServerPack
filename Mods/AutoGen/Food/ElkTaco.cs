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
    public partial class ElkTacoItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Elk Taco"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A tasty treat made from corn tortillas and meat."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 10, Protein = 15, Vitamins = 14};
        public override float Calories                          { get { return 650; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 2)]    
    public partial class ElkTacoRecipe : Recipe
    {
        public ElkTacoRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElkTacoItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ScrapMeatItem>(typeof(AdvancedCookingSkill), 30, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<TortillaItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<WildMixItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElkTacoRecipe), Item.Get<ElkTacoItem>().UILink(), 15, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Elk Taco"), typeof(ElkTacoRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}