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
    public partial class CompostFertilizerRecipe : Recipe
    {
        public CompostFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CompostFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CompostItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<FiberFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CompostFertilizerRecipe), Item.Get<CompostFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Compost Fertilizer"), typeof(CompostFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class CompostFertilizerItem : FertilizerItem<CompostFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Compost Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static CompostFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 5));
            nutrients.Add(new NutrientElement("Phosphorus", 5));
            nutrients.Add(new NutrientElement("Potassium", 5));
        }
    }
}