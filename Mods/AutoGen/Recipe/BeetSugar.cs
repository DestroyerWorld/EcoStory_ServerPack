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

    [RequiresSkill(typeof(MillingSkill), 1)] 
    public class BeetSugarRecipe : Recipe
    {
        public BeetSugarRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SugarItem>(3f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BeetItem>(typeof(MillingSkill), 15, MillingSkill.MultiplicativeStrategy, typeof(MillingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Beet Sugar"), typeof(BeetSugarRecipe));
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(BeetSugarRecipe), this.UILink(), 5, typeof(MillingSkill), typeof(MillingFocusedSpeedTalent), typeof(MillingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(MillObject), this);
        }
    }
}