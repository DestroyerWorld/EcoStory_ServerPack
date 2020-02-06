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

    [RequiresSkill(typeof(CuttingEdgeCookingSkill), 0)] 
    public class RefineKelpRecipe : Recipe
    {
        public RefineKelpRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<HydrocolloidsItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<KelpItem>(typeof(CuttingEdgeCookingSkill), 8, CuttingEdgeCookingSkill.MultiplicativeStrategy, typeof(CuttingEdgeCookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Refine Kelp"), typeof(RefineKelpRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RefineKelpRecipe), this.UILink(), 5, typeof(CuttingEdgeCookingSkill), typeof(CuttingEdgeCookingFocusedSpeedTalent), typeof(CuttingEdgeCookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(LaboratoryObject), this);
        }
    }
}