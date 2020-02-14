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
    public partial class BiodieselRecipe : Recipe
    {
        public BiodieselRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BiodieselItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TallowItem>(typeof(OilDrillingSkill), 20, OilDrillingSkill.MultiplicativeStrategy, typeof(OilDrillingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(BiodieselRecipe), Item.Get<BiodieselItem>().UILink(), 2, typeof(OilDrillingSkill), typeof(OilDrillingFocusedSpeedTalent), typeof(OilDrillingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Biodiesel"), typeof(BiodieselRecipe));

            CraftingComponent.AddRecipe(typeof(OilRefineryObject), this);
        }
    }

    [Serialized]
    [Solid]
    [RequiresSkill(typeof(OilDrillingSkill), 0)]   
    public partial class BiodieselBlock :
        PickupableBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(BiodieselItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(30000)]      
    [Fuel(80000)][Tag("Fuel")]          
    [Currency]              
    public partial class BiodieselItem :
    BlockItem<BiodieselBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Biodiesel"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Biodiesel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A vegetable or animal fat-based diesel fuel."); } }

    }

}