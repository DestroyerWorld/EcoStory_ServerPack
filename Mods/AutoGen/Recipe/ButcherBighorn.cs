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
    public class ButcherBighornRecipe : Recipe
    {
        public ButcherBighornRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawMeatItem>(6f),  
               new CraftingElement<LeatherHideItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BighornCarcassItem>(typeof(ButcherySkill), 1, ButcherySkill.MultiplicativeStrategy, typeof(ButcheryLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Butcher Bighorn"), typeof(ButcherBighornRecipe));
            this.ExperienceOnCraft = 8;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ButcherBighornRecipe), this.UILink(), 2, typeof(ButcherySkill), typeof(ButcheryFocusedSpeedTalent), typeof(ButcheryParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(ButcheryTableObject), this);
        }
    }
}