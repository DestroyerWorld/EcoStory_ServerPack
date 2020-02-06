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
    public partial class FlatbreadItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Flatbread"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Without any leavening the flatbread isn't very puffy. But it's still tasty."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 17, Fat = 3, Protein = 8, Vitamins = 0};
        public override float Calories                          { get { return 500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BakingSkill), 1)]    
    public partial class FlatbreadRecipe : Recipe
    {
        public FlatbreadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FlatbreadItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(BakingSkill), 10, BakingSkill.MultiplicativeStrategy, typeof(BakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FlatbreadRecipe), Item.Get<FlatbreadItem>().UILink(), 5, typeof(BakingSkill), typeof(BakingFocusedSpeedTalent), typeof(BakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Flatbread"), typeof(FlatbreadRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}