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

    [RequiresSkill(typeof(SmeltingSkill), 1)] 
    public class SmeltIronRecipe : Recipe
    {
        public SmeltIronRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<IronIngotItem>(1f),  
               new CraftingElement<TailingsItem>(typeof(SmeltingSkill), 10f, SmeltingSkill.MultiplicativeStrategy),  
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronOreItem>(typeof(SmeltingSkill), 10, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Smelt Iron"), typeof(SmeltIronRecipe));
            this.ExperienceOnCraft = 2;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(SmeltIronRecipe), this.UILink(), 0.5f, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }
}