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

    [RequiresSkill(typeof(FertilizersSkill), 3)]  
    public partial class BerryExtractFertilizerRecipe : Recipe
    {
        public BerryExtractFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BerryExtractFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CompositeFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<HuckleberryExtractItem>(typeof(FertilizersSkill), 6, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BerryExtractFertilizerRecipe), Item.Get<BerryExtractFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Berry Extract Fertilizer"), typeof(BerryExtractFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class BerryExtractFertilizerItem : FertilizerItem<BerryExtractFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Berry Extract Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static BerryExtractFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 1));
            nutrients.Add(new NutrientElement("Phosphorus", 3));
            nutrients.Add(new NutrientElement("Potassium", 7));
        }
    }
}