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
    [Weight(300)]                                          
    public partial class BakedAgaveItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Baked Agave"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Charred agave leaves are too fiberous to eat entirely, but you can certainly chew them."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 6, Protein = 2, Vitamins = 8};
        public override float Calories                          { get { return 350; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BakingSkill), 1)]    
    public partial class BakedAgaveRecipe : Recipe
    {
        public BakedAgaveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BakedAgaveItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AgaveLeavesItem>(typeof(BakingSkill), 15, BakingSkill.MultiplicativeStrategy, typeof(BakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BakedAgaveRecipe), Item.Get<BakedAgaveItem>().UILink(), 5, typeof(BakingSkill), typeof(BakingFocusedSpeedTalent), typeof(BakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Baked Agave"), typeof(BakedAgaveRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}