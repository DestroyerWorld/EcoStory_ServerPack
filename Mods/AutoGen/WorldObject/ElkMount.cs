namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(MountComponent))]                  
    public partial class ElkMountObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElkMountItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Misc"));                
            this.GetComponent<HousingComponent>().Set(ElkMountItem.HousingVal);                                
            this.GetComponent<MountComponent>().Initialize(1);                             

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ElkMountItem :
        WorldObjectItem<ElkMountObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Elk Mount"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A hunting trophy for your house."); } }

        static ElkMountItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 10,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.2f    
        };}}
        
    }

    [RequiresSkill(typeof(TailoringSkill), 1)]      
    public partial class ElkMountRecipe : Recipe
    {
        public ElkMountRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElkMountItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ElkCarcassItem>(1), 
                new CraftingElement<BoardItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 5;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElkMountRecipe), Item.Get<ElkMountItem>().UILink(), 15, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Elk Mount"), typeof(ElkMountRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    }
}