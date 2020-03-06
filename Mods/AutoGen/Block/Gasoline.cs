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
    using Eco.Gameplay.Pipes.LiquidComponents; 

    [RequiresSkill(typeof(OilDrillingSkill), 1)]      
    public partial class GasolineRecipe : Recipe
    {
        public GasolineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GasolineItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PetroleumItem>(typeof(OilDrillingSkill), 8, OilDrillingSkill.MultiplicativeStrategy, typeof(OilDrillingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(GasolineRecipe), Item.Get<GasolineItem>().UILink(), 2, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Gasoline"), typeof(GasolineRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class GasolineBlock :
        PickupableBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(GasolineItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(30000)]      
    [Fuel(100000)][Tag("Fuel")]          
    [Currency]              
    public partial class GasolineItem :
    BlockItem<GasolineBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gasoline"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Gasoline"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Refined petroleum useful for fueling machines and generators."); } }

    }

}