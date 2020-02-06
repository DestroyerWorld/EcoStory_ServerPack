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

    [RequiresSkill(typeof(FertilizersSkill), 2)]  
    public partial class PeltFertilizerRecipe : Recipe
    {
        public PeltFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PeltFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiberFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<FurPeltItem>(typeof(FertilizersSkill), 3, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PeltFertilizerRecipe), Item.Get<PeltFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Pelt Fertilizer"), typeof(PeltFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class PeltFertilizerItem : FertilizerItem<PeltFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Pelt Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static PeltFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 4));
            nutrients.Add(new NutrientElement("Phosphorus", 2));
            nutrients.Add(new NutrientElement("Potassium", 2));
        }
    }
}