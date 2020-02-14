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
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                          
    [RequireRoomMaterialTier(1.8f, typeof(CookingLavishReqTalent), typeof(CookingFrugalReqTalent))]   
    public partial class CastIronStoveObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cast Iron Stove"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CastIronStoveItem); } } 


        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem)
        };

        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Cooking"));                
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(10);                    
            this.GetComponent<HousingComponent>().Set(CastIronStoveItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class CastIronStoveItem :
        WorldObjectItem<CastIronStoveObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Cast Iron Stove"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("The perfect stove for the fledgling chef."); } }

        static CastIronStoveItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Kitchen",
                                                    Val = 2,                                   
                                                    TypeForRoomLimit = "Cooking", 
                                                    DiminishingReturnPercent = 0.3f    
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(10))); } } 
    }

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class CastIronStoveRecipe : Recipe
    {
        public CastIronStoveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CastIronStoveItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),
                new CraftingElement<LumberItem>(typeof(SmeltingSkill), 20, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 15;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CastIronStoveRecipe), Item.Get<CastIronStoveItem>().UILink(), 15, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Cast Iron Stove"), typeof(CastIronStoveRecipe));
            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }
}