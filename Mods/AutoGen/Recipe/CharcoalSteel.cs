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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)] 
    public class CharcoalSteelRecipe : Recipe
    {
        public CharcoalSteelRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<SteelItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CharcoalItem>(2),
                new CraftingElement<QuicklimeItem>(1), 
                new CraftingElement<IronIngotItem>(typeof(AdvancedSmeltingSkill), 8, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Charcoal Steel"), typeof(CharcoalSteelRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CharcoalSteelRecipe), this.UILink(), 3, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}