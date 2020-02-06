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
    public partial class PulpFillerRecipe : Recipe
    {
        public PulpFillerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<PulpFillerItem>()
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodPulpItem>(typeof(FertilizersSkill), 30, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)),
                new CraftingElement<DirtItem>(typeof(FertilizersSkill), 2, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)) 
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(PulpFillerRecipe), Item.Get<PulpFillerItem>().UILink(), 0.3f, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Pulp Filler"), typeof(PulpFillerRecipe));
            CraftingComponent.AddRecipe(typeof(FarmersTableObject),this);
        }
    }
    
    [Serialized]
    [Weight(500)]  
    [Category("Tool")]
    public partial class PulpFillerItem : FertilizerItem<PulpFillerItem>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Pulp Filler"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }

        static PulpFillerItem()
        {
            nutrients = new List<NutrientElement>();
            nutrients.Add(new NutrientElement("Nitrogen", 0.3f));
            nutrients.Add(new NutrientElement("Phosphorus", 0.3f));
            nutrients.Add(new NutrientElement("Potassium", 0.3f));
        }
    }
}