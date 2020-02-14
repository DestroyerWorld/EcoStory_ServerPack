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
    public class CleanSalmonRecipe : Recipe
    {
        public CleanSalmonRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RawFishItem>(3f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SalmonItem>(1)  
            };
            this.Initialize(Localizer.DoStr("Clean Salmon"), typeof(CleanSalmonRecipe));
            this.CraftMinutes = new ConstantValue(1);
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}