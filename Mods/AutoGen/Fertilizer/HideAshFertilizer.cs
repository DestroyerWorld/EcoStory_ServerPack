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
    public partial class HideAshFertilizerRecipe : Recipe
    {
        public HideAshFertilizerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HideAshFertilizerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PulpFillerItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<LeatherHideItem>(typeof(FertilizersSkill), 3, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HideAshFertilizerRecipe), Item.Get<HideAshFertilizerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Hide Ash Fertilizer"), typeof(HideAshFertilizerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class HideAshFertilizerItem : FertilizerItem<HideAshFertilizerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Hide Ash Fertilizer"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static HideAshFertilizerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 5));
            nutrients.Add(new NutrientElement("Phosphorus", 0.5f));
            nutrients.Add(new NutrientElement("Potassium", 0.5f));
        }
    }
}