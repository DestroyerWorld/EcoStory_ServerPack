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
    public class ShredKelpRecipe : Recipe
    {
        public ShredKelpRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<PlantFibersItem>(7f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<KelpItem>(6)  
            };
            this.Initialize(Localizer.DoStr("Shred Kelp"), typeof(ShredKelpRecipe));
            this.CraftMinutes = new ConstantValue(1);
            CraftingComponent.AddRecipe(typeof(FisheryObject), this);
        }
    }
}