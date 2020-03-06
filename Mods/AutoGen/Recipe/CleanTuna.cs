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
    public class CleanTunaRecipe : Recipe
    {
        public CleanTunaRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(5f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TunaItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Clean Tuna"), typeof(CleanTunaRecipe));
            this.CraftMinutes = new ConstantValue(2);
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}