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

    [RequiresSkill(typeof(MortaringSkill), 1)] 
    public class GrindSandstoneRecipe : Recipe
    {
        public GrindSandstoneRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SandItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SandstoneItem>(typeof(MortaringSkill), 6, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Grind Sandstone"), typeof(GrindSandstoneRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(GrindSandstoneRecipe), this.UILink(), 5, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}