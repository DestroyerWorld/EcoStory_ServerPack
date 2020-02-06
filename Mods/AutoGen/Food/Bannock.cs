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
    public partial class BannockItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Bannock"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A dense whole wheat unleavened bread."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 15, Fat = 8, Protein = 3, Vitamins = 0};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]    
    public partial class BannockRecipe : Recipe
    {
        public BannockRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BannockItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(AdvancedCampfireCookingSkill), 30, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)),
                new CraftingElement<TallowItem>(typeof(AdvancedCampfireCookingSkill), 3, AdvancedCampfireCookingSkill.MultiplicativeStrategy, typeof(AdvancedCampfireCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BannockRecipe), Item.Get<BannockItem>().UILink(), 5, typeof(AdvancedCampfireCookingSkill), typeof(AdvancedCampfireCookingFocusedSpeedTalent), typeof(AdvancedCampfireCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Bannock"), typeof(BannockRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}