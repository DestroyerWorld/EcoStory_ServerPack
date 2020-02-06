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

    [RequiresSkill(typeof(IndustrySkill), 0)]      
    public partial class SteelPlateRecipe : Recipe
    {
        public SteelPlateRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelPlateItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 8, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelPlateRecipe), Item.Get<SteelPlateItem>().UILink(), 4, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Steel Plate"), typeof(SteelPlateRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricStampingPressObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class SteelPlateItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Plate"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }
    }
}