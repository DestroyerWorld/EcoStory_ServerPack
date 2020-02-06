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

    [RequiresSkill(typeof(OilDrillingSkill), 0)]      
    public partial class EpoxyRecipe : Recipe
    {
        public EpoxyRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<EpoxyItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(OilDrillingSkill), 8, OilDrillingSkill.MultiplicativeStrategy, typeof(OilDrillingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(EpoxyRecipe), Item.Get<EpoxyItem>().UILink(), 2, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Epoxy"), typeof(EpoxyRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class EpoxyItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Epoxy"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Epoxy"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A useful material for hardening, curing, and other various uses."); } }
    }
}