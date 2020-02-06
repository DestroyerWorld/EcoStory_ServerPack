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
    public partial class BreadItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Bread"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Bread"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A delicious, crispy crust hides the soft interior."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 20, Fat = 10, Protein = 5, Vitamins = 5};
        public override float Calories                          { get { return 750; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 1)]    
    public partial class BreadRecipe : Recipe
    {
        public BreadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BreadItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 6, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<YeastItem>(typeof(AdvancedBakingSkill), 3, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BreadRecipe), Item.Get<BreadItem>().UILink(), 8, typeof(AdvancedBakingSkill), typeof(AdvancedBakingFocusedSpeedTalent), typeof(AdvancedBakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Bread"), typeof(BreadRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}