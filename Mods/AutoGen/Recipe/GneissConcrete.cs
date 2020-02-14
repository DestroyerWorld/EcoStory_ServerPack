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

    [RequiresSkill(typeof(CementSkill), 1)] 
    public class GneissConcreteRecipe : Recipe
    {
        public GneissConcreteRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<ConcreteItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GneissItem>(typeof(CementSkill), 20, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)),
                new CraftingElement<GraniteItem>(typeof(CementSkill), 20, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Gneiss Concrete"), typeof(GneissConcreteRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(GneissConcreteRecipe), this.UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent), typeof(CementParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }
}