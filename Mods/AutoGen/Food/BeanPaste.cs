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
    public partial class BeanPasteItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Bean Paste"); } }
        public override LocString DisplayNamePlural             { get { return Localizer.DoStr("Bean Paste"); } } 
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Smashed beans can work as a thickener or flavour enhancer."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 3, Fat = 7, Protein = 5, Vitamins = 0};
        public override float Calories                          { get { return 40; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(MillingSkill), 2)]    
    public partial class BeanPasteRecipe : Recipe
    {
        public BeanPasteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BeanPasteItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeansItem>(typeof(MillingSkill), 15, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BeanPasteRecipe), Item.Get<BeanPasteItem>().UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Bean Paste"), typeof(BeanPasteRecipe));
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}