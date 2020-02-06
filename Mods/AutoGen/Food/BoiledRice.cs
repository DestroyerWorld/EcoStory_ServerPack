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
    public partial class BoiledRiceItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Boiled Rice"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Soft and fluffy."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 13, Fat = 0, Protein = 2, Vitamins = 0};
        public override float Calories                          { get { return 210; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class BoiledRiceRecipe : Recipe
    {
        public BoiledRiceRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BoiledRiceItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RiceItem>(typeof(AdvancedCookingSkill), 20, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BoiledRiceRecipe), Item.Get<BoiledRiceItem>().UILink(), 5, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Boiled Rice"), typeof(BoiledRiceRecipe));
            CraftingComponent.AddRecipe(typeof(KitchenObject), this);
        }
    }
}