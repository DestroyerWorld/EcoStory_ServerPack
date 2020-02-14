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
    public class ButcherWolfRecipe : Recipe
    {
        public ButcherWolfRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(2f),  
               new CraftingElement<FurPeltItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WolfCarcassItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Butcher Wolf"), typeof(ButcherWolfRecipe));
            this.ExperienceOnCraft = 6;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherWolfRecipe), this.UILink(), 1.5f, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}