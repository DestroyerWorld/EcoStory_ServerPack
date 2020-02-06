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
    public partial class CornmealItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Cornmeal"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Cornmeal"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Dried and ground corn; it's like a courser flour."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 9, Fat = 3, Protein = 3, Vitamins = 0};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 1)]    
    public partial class CornmealRecipe : Recipe
    {
        public CornmealRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CornmealItem>(),
               
               new CraftingElement<CerealGermItem>(2) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornItem>(typeof(MillingSkill), 10, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CornmealRecipe), Item.Get<CornmealItem>().UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Cornmeal"), typeof(CornmealRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}