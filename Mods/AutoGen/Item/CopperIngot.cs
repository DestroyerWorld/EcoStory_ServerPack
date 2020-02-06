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

    [RequiresSkill(typeof(SmeltingSkill), 0)]      
    public partial class CopperIngotRecipe : Recipe
    {
        public CopperIngotRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(),          
            new CraftingElement<TailingsItem>(typeof(SmeltingSkill), 4, SmeltingSkill.MultiplicativeStrategy)

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperOreItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(CopperIngotRecipe), Item.Get<CopperIngotItem>().UILink(), 4, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Copper Ingot"), typeof(CopperIngotRecipe));

            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }

    [Serialized]
    [Weight(2500)]      
    [Currency]              
    public partial class CopperIngotItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Copper Bar"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Copper Bars"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hefty block of copper."); } }
    }
}