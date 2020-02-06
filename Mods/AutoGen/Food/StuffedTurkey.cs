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
    [Weight(1000)]                                          
    public partial class StuffedTurkeyItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Stuffed Turkey"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("To give thanks for fact that this food items gives two nutrients more than other food at the same tier."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 9, Fat = 12, Protein = 16, Vitamins = 7};
        public override float Calories                          { get { return 1500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CookingSkill), 3)]    
    public partial class StuffedTurkeyRecipe : Recipe
    {
        public StuffedTurkeyRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StuffedTurkeyItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PrimeCutItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<BreadItem>(typeof(CookingSkill), 5, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<VegetableMedleyItem>(typeof(CookingSkill), 4, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(StuffedTurkeyRecipe), Item.Get<StuffedTurkeyItem>().UILink(), 30, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Stuffed Turkey"), typeof(StuffedTurkeyRecipe));
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}