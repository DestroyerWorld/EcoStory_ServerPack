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
    public class ButcherFoxRecipe : Recipe
    {
        public ButcherFoxRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(2f),  
               new CraftingElement<FurPeltItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FoxCarcassItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Butcher Fox"), typeof(ButcherFoxRecipe));
            this.ExperienceOnCraft = 4;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherFoxRecipe), this.UILink(), 1, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}