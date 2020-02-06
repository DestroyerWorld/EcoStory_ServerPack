namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

    [RequiresSkill(typeof(FertilizersSkill), 0)]      
    public partial class SoilSamplerRecipe : Recipe
    {
        public SoilSamplerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SoilSamplerItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(FertilizersSkill), 4, FertilizersSkill.MultiplicativeStrategy, typeof(FertilizersLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(SoilSamplerRecipe), Item.Get<SoilSamplerItem>().UILink(), 10, typeof(FertilizersSkill), typeof(FertilizersFocusedSpeedTalent), typeof(FertilizersParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Soil Sampler"), typeof(SoilSamplerRecipe));

            CraftingComponent.AddRecipe(typeof(FarmersTableObject), this);
        }
    }

}