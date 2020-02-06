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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]      
    public partial class SteelRecipe : Recipe
    {
        public SteelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CoalItem>(2),
                new CraftingElement<QuicklimeItem>(1), 
                new CraftingElement<IronIngotItem>(typeof(AdvancedSmeltingSkill), 8, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelRecipe), Item.Get<SteelItem>().UILink(), 3, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Steel"), typeof(SteelRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [Serialized]
    [Weight(2500)]      
    [Currency]              
    public partial class SteelItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A strong alloy of iron and other elements."); } }
    }
}