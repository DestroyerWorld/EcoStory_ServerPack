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

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class GoldIngotRecipe : Recipe
    {
        public GoldIngotRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GoldIngotItem>(),          
            new CraftingElement<TailingsItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy)

            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GoldOreItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(GoldIngotRecipe), Item.Get<GoldIngotItem>().UILink(), 4, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Gold Ingot"), typeof(GoldIngotRecipe));

            CraftingComponent.AddRecipe(typeof(BloomeryObject), this);
        }
    }

    [Serialized]
    [Weight(2500)]      
    [Currency]              
    public partial class GoldIngotItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gold Bar"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Gold Bars"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A shiny, refined gold ingot."); } }
    }
}