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

    [RequiresSkill(typeof(MortaringSkill), 0)] 
    public class FiberMortarRecipe : Recipe
    {
        public FiberMortarRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<MortarItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PlantFibersItem>(typeof(MortaringSkill), 20, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Fiber Mortar"), typeof(FiberMortarRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(FiberMortarRecipe), this.UILink(), 0.2f, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CampfireObject), this);
        }
    }
}