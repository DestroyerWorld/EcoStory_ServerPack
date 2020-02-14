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
    public class RainforestSaladRecipe : Recipe
    {
        public RainforestSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BasicSaladItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TaroRootItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<BoleteMushroomsItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<PapayaItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Rainforest Salad"), typeof(RainforestSaladRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RainforestSaladRecipe), this.UILink(), 10, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}