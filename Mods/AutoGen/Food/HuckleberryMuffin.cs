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
    public partial class HuckleberryMuffinItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Huckleberry Muffin"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A soft, slightly sweet bread studded with juicy huckleberries."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 10, Fat = 4, Protein = 5, Vitamins = 11};
        public override float Calories                          { get { return 450; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(BakingSkill), 1)]    
    public partial class HuckleberryMuffinRecipe : Recipe
    {
        public HuckleberryMuffinRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HuckleberryMuffinItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FlourItem>(typeof(BakingSkill), 10, BakingSkill.MultiplicativeStrategy, typeof(BakingLavishResourcesTalent)),
                new CraftingElement<HuckleberriesItem>(typeof(BakingSkill), 20, BakingSkill.MultiplicativeStrategy, typeof(BakingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HuckleberryMuffinRecipe), Item.Get<HuckleberryMuffinItem>().UILink(), 5, typeof(BakingSkill), typeof(BakingFocusedSpeedTalent), typeof(BakingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Huckleberry Muffin"), typeof(HuckleberryMuffinRecipe));
            CraftingComponent.AddRecipe(typeof(BakeryOvenObject), this);
        }
    }
}