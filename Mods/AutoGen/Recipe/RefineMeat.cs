namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 1)] 
    public class RefineMeatRecipe : Recipe
    {
        public RefineMeatRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<HydrocolloidsItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<RawMeatItem>(typeof(CuttingEdgeCookingSkill), 16, CuttingEdgeCookingSkill.MultiplicativeStrategy, typeof(CuttingEdgeCookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Refine Meat"), typeof(RefineMeatRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RefineMeatRecipe), this.UILink(), 5, typeof(CuttingEdgeCookingSkill), typeof(CuttingEdgeCookingFocusedSpeedTalent), typeof(CuttingEdgeCookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}