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
    public class GrindGraniteToSandRecipe : Recipe
    {
        public GrindGraniteToSandRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SandItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GraniteItem>(typeof(MortaringSkill), 12, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Grind Granite To Sand"), typeof(GrindGraniteToSandRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(GrindGraniteToSandRecipe), this.UILink(), 5, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}