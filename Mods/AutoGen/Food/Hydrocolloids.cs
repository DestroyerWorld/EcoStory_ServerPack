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
    [Weight(100)]                                          
    public partial class HydrocolloidsItem :
        FoodItem            
    {
        public override LocString DisplayName                   { get { return Localizer.DoStr("Hydrocolloids"); } }
        public override LocString DisplayDescription            { get { return Localizer.DoStr("Used to make collids for bursts of flavor."); } }

        private static Nutrients nutrition = new Nutrients()    { Carbs = 0, Fat = 0, Protein = 0, Vitamins = 0};
        public override float Calories                          { get { return 10; } }
        public override Nutrients Nutrition                     { get { return nutrition; } }
    }

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)]    
    public partial class HydrocolloidsRecipe : Recipe
    {
        public HydrocolloidsRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HydrocolloidsItem>(),
               
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CornStarchItem>(typeof(CuttingEdgeCookingSkill), 20, CuttingEdgeCookingSkill.MultiplicativeStrategy, typeof(CuttingEdgeCookingLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HydrocolloidsRecipe), Item.Get<HydrocolloidsItem>().UILink(), 20, typeof(CuttingEdgeCookingSkill), typeof(CuttingEdgeCookingFocusedSpeedTalent), typeof(CuttingEdgeCookingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Hydrocolloids"), typeof(HydrocolloidsRecipe));
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}