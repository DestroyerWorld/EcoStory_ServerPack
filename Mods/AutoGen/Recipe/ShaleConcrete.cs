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
    public class ShaleConcreteRecipe : Recipe
    {
        public ShaleConcreteRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<ConcreteItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ShaleItem>(typeof(CementSkill), 40, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)),
                new CraftingElement<GraniteItem>(typeof(CementSkill), 20, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Shale Concrete"), typeof(ShaleConcreteRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ShaleConcreteRecipe), this.UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent), typeof(CementParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }
}