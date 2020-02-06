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
    public partial class MacaroonsItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Macaroons"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Macaroons"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A small circular biscuit with a sweet huckleberry filling."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 16, Fat = 14, Protein = 7, Vitamins = 10};
        public override float Calories                          { get { return 250; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(AdvancedBakingSkill), 2)]    
    public partial class MacaroonsRecipe : Recipe
    {
        public MacaroonsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MacaroonsItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<SimpleSyrupItem>(typeof(AdvancedBakingSkill), 5, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)),
                new CraftingElement<HuckleberryExtractItem>(typeof(AdvancedBakingSkill), 10, AdvancedBakingSkill.MultiplicativeStrategy, typeof(AdvancedBakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(MacaroonsRecipe), Item.Get<MacaroonsItem>().UILink(), 8, typeof(AdvancedBakingSkill), typeof(AdvancedBakingFocusedSpeedTalent), typeof(AdvancedBakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Macaroons"), typeof(MacaroonsRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}