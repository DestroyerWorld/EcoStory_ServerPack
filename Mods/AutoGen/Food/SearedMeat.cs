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
    public partial class SearedMeatItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Seared Meat"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Seared Meat"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A cut of perfectly seared steak."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 4, Fat = 17, Protein = 19, Vitamins = 7};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedCookingSkill), 1)]    
    public partial class SearedMeatRecipe : Recipe
    {
        public SearedMeatRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SearedMeatItem>(),
               
               new CraftingElement<TallowItem>(1) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PrimeCutItem>(typeof(AdvancedCookingSkill), 5, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)),
                new CraftingElement<InfusedOilItem>(typeof(AdvancedCookingSkill), 4, AdvancedCookingSkill.MultiplicativeStrategy, typeof(AdvancedCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SearedMeatRecipe), Item.Get<SearedMeatItem>().UILink(), 8, typeof(AdvancedCookingSkill), typeof(AdvancedCookingFocusedSpeedTalent), typeof(AdvancedCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Seared Meat"), typeof(SearedMeatRecipe));
            CraftingComponent.AddRecipe(typeof(StoveObject), this);
        }
    }
}