namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using System.ComponentModel;

    [RequiresSkill(typeof(FertilizersSkill), 1)]  
    public partial class BloodMealFertilizerRecipe : Recipe
    {
        public BloodMealFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BloodMealFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PulpFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<ScrapMeatItem>(typeof(FertilizersSkill), 28, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BloodMealFertilizerRecipe), Item.Get<BloodMealFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Blood Meal Fertilizer"), typeof(BloodMealFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class BloodMealFertilizerItem : FertilizerItem<BloodMealFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Blood Meal Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static BloodMealFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 3));
            nutrients.Add(new NutrientElement("Phosphorus", 0.4f));
            nutrients.Add(new NutrientElement("Potassium", 0.4f));
        }
    }
}