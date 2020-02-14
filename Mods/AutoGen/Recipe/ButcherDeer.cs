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

    [RequiresSkill(typeof(ButcherySkill), 1)] 
    public class ButcherDeerRecipe : Recipe
    {
        public ButcherDeerRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(5f),  
               new CraftingElement<LeatherHideItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<DeerCarcassItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Butcher Deer"), typeof(ButcherDeerRecipe));
            this.ExperienceOnCraft = 6;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherDeerRecipe), this.UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}