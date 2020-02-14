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

    [RequiresSkill(typeof(BricklayingSkill), 1)] 
    public class ShaleBrickRecipe : Recipe
    {
        public ShaleBrickRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<BrickItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ShaleItem>(typeof(BricklayingSkill), 16, BricklayingSkill.MultiplicativeStrategy, typeof(BricklayingLavishResourcesTalent)),
                new CraftingElement<ClayItem>(typeof(BricklayingSkill), 2, BricklayingSkill.MultiplicativeStrategy, typeof(BricklayingLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(BricklayingSkill), 8, BricklayingSkill.MultiplicativeStrategy, typeof(BricklayingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Shale Brick"), typeof(ShaleBrickRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ShaleBrickRecipe), this.UILink(), 1, typeof(BricklayingSkill), typeof(BricklayingFocusedSpeedTalent), typeof(BricklayingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }
}