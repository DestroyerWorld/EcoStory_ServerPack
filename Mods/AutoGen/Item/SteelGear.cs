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

    [RequiresSkill(typeof(IndustrySkill), 1)]      
    public partial class SteelGearRecipe : Recipe
    {
        public SteelGearRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelGearItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 6, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<EpoxyItem>(typeof(IndustrySkill), 3, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelGearRecipe), Item.Get<SteelGearItem>().UILink(), 4, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Steel Gear"), typeof(SteelGearRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricPlanerObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class SteelGearItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Gear"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }
    }
}