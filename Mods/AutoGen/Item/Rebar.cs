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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]      
    public partial class RebarRecipe : Recipe
    {
        public RebarRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RebarItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 8, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(RebarRecipe), Item.Get<RebarItem>().UILink(), 0.5f, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Rebar"), typeof(RebarRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [Serialized]
    [Weight(3000)]      
    [Currency]              
    public partial class RebarItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rebar"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Ribbed steel bars for reinforcing stuctures."); } }
    }
}