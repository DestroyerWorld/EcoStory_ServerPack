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
    public class CleanTroutRecipe : Recipe
    {
        public CleanTroutRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(2f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TroutItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Clean Trout"), typeof(CleanTroutRecipe));
            this.CraftMinutes = new ConstantValue(1.5f);
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}