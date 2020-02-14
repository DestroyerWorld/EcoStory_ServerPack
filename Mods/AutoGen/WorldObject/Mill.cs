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
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.8f, typeof(MillingLavishReqTalent), typeof(MillingFrugalReqTalent))]   
    public partial class MillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(MillItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Cooking"));                
            this.GetComponent<PowerConsumptionComponent>().Initialize(75);                      
            this.GetComponent<PowerGridComponent>().Initialize(5, new MechanicalPower());        
            this.GetComponent<HousingComponent>().Set(MillItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class MillItem :
        WorldObjectItem<MillObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mill"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Refines food resources by crushing them under a stone millstone."); } }

        static MillItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 3,                                   
                                                    TypeForRoomLimit = "Food Preparation", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(75))); } }  
    }

    [RequiresSkill(typeof(MortaringSkill), 1)]      
    public partial class MillRecipe : Recipe
    {
        public MillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 80, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),
                new CraftingElement<LogItem>(typeof(MortaringSkill), 40, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 5;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MillRecipe), Item.Get<MillItem>().UILink(), 20, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mill"), typeof(MillRecipe));
            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }
}