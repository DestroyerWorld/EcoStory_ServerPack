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
    public partial class AcornPowderItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Acorn Powder"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Acorn Powder"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Powdered acorn."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 2, Protein = 5, Vitamins = 5};
        public override float Calories                          { get { return 40; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 3)]    
    public partial class AcornPowderRecipe : Recipe
    {
        public AcornPowderRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AcornPowderItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AcornItem>(typeof(MillingSkill), 10, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(AcornPowderRecipe), Item.Get<AcornPowderItem>().UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Acorn Powder"), typeof(AcornPowderRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}