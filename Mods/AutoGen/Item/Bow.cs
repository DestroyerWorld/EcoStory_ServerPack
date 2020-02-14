namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;

	[RequiresSkill(typeof(HuntingSkill), 1)]
    public partial class BowRecipe : Recipe
    {
        public BowRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BowItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(8),
                new CraftingElement<PlantFibersItem>(30)    
            };

            this.CraftMinutes = new ConstantValue(0.5f);
            this.Initialize(Localizer.DoStr("Bow"), typeof(BowRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

}