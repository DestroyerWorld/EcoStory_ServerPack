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
    public partial class CharredFishItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Charred Fish"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Charred Fish"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("At least it doesn't have any scales any more."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 4, Protein = 9, Vitamins = 0};
        public override float Calories                          { get { return 550; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }
	[RequiresSkill(typeof(AdvancedCampfireCookingSkill), 1)]
    public partial class CharredFishRecipe : Recipe
    {
        public CharredFishRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CharredFishItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawFishItem>(1)    
            };
            this.CraftMinutes = new ConstantValue(3);     
            this.Initialize(Localizer.DoStr("Charred Fish"), typeof(CharredFishRecipe));
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}