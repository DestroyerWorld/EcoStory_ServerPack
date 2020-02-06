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
    public partial class CamasBreadItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Camas Bread"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Camas Bread"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A bread with a camas twist for a bit of flavor and fun. "); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 15, Fat = 13, Protein = 5, Vitamins = 9};
        public override float Calories                          { get { return 800; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 1)]    
    public partial class CamasBreadRecipe : Recipe
    {
        public CamasBreadRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CamasBreadItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<CamasPasteItem>(typeof(AdvancedBakingSkill), 5, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<YeastItem>(typeof(AdvancedBakingSkill), 3, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CamasBreadRecipe), Item.Get<CamasBreadItem>().UILink(), 8, typeof(AdvancedBakingSkill), typeof(AdvancedBakingFocusedSpeedTalent), typeof(AdvancedBakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Camas Bread"), typeof(CamasBreadRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}