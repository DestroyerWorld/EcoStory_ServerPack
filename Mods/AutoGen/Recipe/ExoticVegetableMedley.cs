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

    [RequiresSkill(typeof(CookingSkill), 1)] 
    public class ExoticVegetableMedleyRecipe : Recipe
    {
        public ExoticVegetableMedleyRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<VegetableMedleyItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeansItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<TomatoItem>(typeof(CookingSkill), 15, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<BeetItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Exotic Vegetable Medley"), typeof(ExoticVegetableMedleyRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ExoticVegetableMedleyRecipe), this.UILink(), 2, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}