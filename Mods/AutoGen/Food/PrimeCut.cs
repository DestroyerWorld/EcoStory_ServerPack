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
    public partial class PrimeCutItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Prime Cut"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("A perfectly marbled piece of meat."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 4, Protein = 9, Vitamins = 0};
        public override float Calories                          { get { return 600; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(ButcherySkill), 3)]    
    public partial class PrimeCutRecipe : Recipe
    {
        public PrimeCutRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PrimeCutItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(ButcherySkill), 40, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PrimeCutRecipe), Item.Get<PrimeCutItem>().UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Prime Cut"), typeof(PrimeCutRecipe));
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}