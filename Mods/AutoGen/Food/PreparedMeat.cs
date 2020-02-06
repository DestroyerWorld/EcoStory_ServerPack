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
    public partial class PreparedMeatItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Prepared Meat"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Carefully butchered meat, ready to cook."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 6, Protein = 4, Vitamins = 0};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(ButcherySkill), 1)]    
    public partial class PreparedMeatRecipe : Recipe
    {
        public PreparedMeatRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PreparedMeatItem>(),
               
               new CraftingElement<ScrapMeatItem>(2) 
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(ButcherySkill), 10, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PreparedMeatRecipe), Item.Get<PreparedMeatItem>().UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Prepared Meat"), typeof(PreparedMeatRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}