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
    public partial class BearclawItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Bearclaw"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A sweet pastry with seperated sections that look a bit like a claw."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 12, Fat = 21, Protein = 6, Vitamins = 7};
        public override float Calories                          { get { return 650; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 1)]    
    public partial class BearclawRecipe : Recipe
    {
        public BearclawRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BearclawItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 8, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<SugarItem>(typeof(AdvancedBakingSkill), 8, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<YeastItem>(typeof(AdvancedBakingSkill), 3, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BearclawRecipe), Item.Get<BearclawItem>().UILink(), 8, typeof(AdvancedBakingSkill), typeof(AdvancedBakingFocusedSpeedTalent), typeof(AdvancedBakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Bearclaw"), typeof(BearclawRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}