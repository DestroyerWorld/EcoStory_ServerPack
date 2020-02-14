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
    public class RainforestFruitSaladRecipe : Recipe
    {
        public RainforestFruitSaladRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<FruitSaladItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PapayaItem>(typeof(CookingSkill), 20, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<PineappleItem>(typeof(CookingSkill), 15, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Rainforest Fruit Salad"), typeof(RainforestFruitSaladRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RainforestFruitSaladRecipe), this.UILink(), 10, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}