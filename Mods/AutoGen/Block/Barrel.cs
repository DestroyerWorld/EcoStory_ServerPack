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

    [RequiresSkill(typeof(OilDrillingSkill), 0)]      
    public partial class BarrelRecipe : Recipe
    {
        public BarrelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BarrelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(OilDrillingSkill), 2, OilDrillingSkill.MultiplicativeStrategy, typeof(OilDrillingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(BarrelRecipe), Item.Get<BarrelItem>().UILink(), 1, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Barrel"), typeof(BarrelRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class BarrelBlock :
        PickupableBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(BarrelItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(2000)]      
    [Currency]              
    public partial class BarrelItem :
    BlockItem<BarrelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Barrel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A metal barrel for carrying liquids."); } }

    }

}