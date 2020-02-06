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
    public partial class FlourItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Flour"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Flour"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A fine, milled wheat product that's useful for all baking."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 15, Fat = 0, Protein = 0, Vitamins = 0};
        public override float Calories                          { get { return 50; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 1)]    
    public partial class FlourRecipe : Recipe
    {
        public FlourRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(),
               
               new CraftingElement<CerealGermItem>(1) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WheatItem>(typeof(MillingSkill), 20, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(FlourRecipe), Item.Get<FlourItem>().UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Flour"), typeof(FlourRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}