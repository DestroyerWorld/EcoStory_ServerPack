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
    public partial class CamasPasteItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Camas Paste"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Camas Paste"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Pulverized camas works as an excellent thickener or flavour enhancer."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 10, Protein = 2, Vitamins = 0};
        public override float Calories                          { get { return 60; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 1)]    
    public partial class CamasPasteRecipe : Recipe
    {
        public CamasPasteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CamasPasteItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CamasBulbItem>(typeof(MillingSkill), 10, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CamasPasteRecipe), Item.Get<CamasPasteItem>().UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Camas Paste"), typeof(CamasPasteRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}