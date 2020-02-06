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
    [Weight(150)]                                          
    public partial class PineappleFriendRiceItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Pineapple Friend Rice"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A homely recipe made from a mix of cooked rice and fruit. Not only is it friendly, but it also happens to be pan fried."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 20, Fat = 12, Protein = 12, Vitamins = 9};
        public override float Calories                          { get { return 620; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class PineappleFriendRiceRecipe : Recipe
    {
        public PineappleFriendRiceRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PineappleFriendRiceItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CharredPineappleItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<BoiledRiceItem>(typeof(AdvancedCookingSkill), 10, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<ScrapMeatItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PineappleFriendRiceRecipe), Item.Get<PineappleFriendRiceItem>().UILink(), 10, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Pineapple Friend Rice"), typeof(PineappleFriendRiceRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}