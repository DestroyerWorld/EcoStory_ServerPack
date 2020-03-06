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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)] 
    public class SteelRivetsRecipe : Recipe
    {
        public SteelRivetsRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<RivetItem>(3f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 2, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Steel Rivets"), typeof(SteelRivetsRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelRivetsRecipe), this.UILink(), 2, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}