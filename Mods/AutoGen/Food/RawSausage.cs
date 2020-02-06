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
    public partial class RawSausageItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Raw Sausage"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Ground meat stuffed into an intestine casing."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 8, Protein = 4, Vitamins = 0};
        public override float Calories                          { get { return 500; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(ButcherySkill), 2)]    
    public partial class RawSausageRecipe : Recipe
    {
        public RawSausageRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RawSausageItem>(),
               
               new CraftingElement<TallowItem>(1) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ScrapMeatItem>(typeof(ButcherySkill), 20, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RawSausageRecipe), Item.Get<RawSausageItem>().UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Raw Sausage"), typeof(RawSausageRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}