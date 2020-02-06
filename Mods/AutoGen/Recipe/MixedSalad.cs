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

    [RequiresSkill(typeof(CookingSkill), 0)] 
    public class MixedSaladRecipe : Recipe
    {
        public MixedSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BasicSaladItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiddleheadsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<TomatoItem>(typeof(CookingSkill), 15, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<FireweedShootsItem>(typeof(CookingSkill), 15, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Mixed Salad"), typeof(MixedSaladRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MixedSaladRecipe), this.UILink(), 2, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}