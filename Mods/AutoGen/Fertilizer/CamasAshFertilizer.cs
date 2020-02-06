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
    public partial class CamasAshFertilizerRecipe : Recipe
    {
        public CamasAshFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CamasAshFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiberFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<CharredCamasBulbItem>(typeof(FertilizersSkill), 6, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CamasAshFertilizerRecipe), Item.Get<CamasAshFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Camas Ash Fertilizer"), typeof(CamasAshFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class CamasAshFertilizerItem : FertilizerItem<CamasAshFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Camas Ash Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static CamasAshFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 0.3f));
            nutrients.Add(new NutrientElement("Phosphorus", 0.7f));
            nutrients.Add(new NutrientElement("Potassium", 2));
        }
    }
}