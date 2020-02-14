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

    [RequiresSkill(typeof(CookingSkill), 1)] 
    public class MushroomMedleyRecipe : Recipe
    {
        public MushroomMedleyRecipe()
        {
            this.Products = new CraftingElement[]
            {
               new CraftingElement<VegetableMedleyItem>(1f),  

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CriminiMushroomsItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<CookeinaMushroomsItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)),
                new CraftingElement<BoleteMushroomsItem>(typeof(CookingSkill), 10, CookingSkill.MultiplicativeStrategy, typeof(CookingLavishResourcesTalent)), 
            };
            this.Initialize(Localizer.DoStr("Mushroom Medley"), typeof(MushroomMedleyRecipe));
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MushroomMedleyRecipe), this.UILink(), 2, typeof(CookingSkill), typeof(CookingFocusedSpeedTalent), typeof(CookingParallelSpeedTalent));    
            CraftingComponent.AddRecipe(typeof(CastIronStoveObject), this);
        }
    }
}