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

    [RequiresSkill(typeof(CementSkill), 0)] 
    public class BasaltConcreteRecipe : Recipe
    {
        public BasaltConcreteRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<ConcreteItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BasaltItem>(typeof(CementSkill), 30, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)),
                new CraftingElement<GraniteItem>(typeof(CementSkill), 20, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Basalt Concrete"), typeof(BasaltConcreteRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(BasaltConcreteRecipe), this.UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent), typeof(CementParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }
}