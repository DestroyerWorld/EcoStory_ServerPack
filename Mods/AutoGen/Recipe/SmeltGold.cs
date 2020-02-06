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

    [RequiresSkill(typeof(SmeltingSkill), 0)] 
    public class SmeltGoldRecipe : Recipe
    {
        public SmeltGoldRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<GoldIngotItem>(1f),  
               new CraftingElement<TailingsItem>(typeof(SmeltingSkill), 2f, SmeltingSkill.MultiplicativeStrategy),  
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldOreItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Smelt Gold"), typeof(SmeltGoldRecipe));
            this.ExperienceOnCraft = 2;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmeltGoldRecipe), this.UILink(), 0.5f, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}