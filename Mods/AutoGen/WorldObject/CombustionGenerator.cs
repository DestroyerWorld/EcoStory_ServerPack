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
    [RequireComponent(typeof(ChimneyComponent))]                
    [RequireComponent(typeof(LiquidProducerComponent))]         
    [RequireComponent(typeof(AttachmentComponent))]             
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(FuelSupplyComponent))]                      
    [RequireComponent(typeof(FuelConsumptionComponent))]                 
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(LiquidConverterComponent))]        
    public partial class CombustionGeneratorObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Combustion Generator"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CombustionGeneratorItem); } } 


        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
            typeof(PetroleumItem),
            typeof(GasolineItem)
        };

        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Power"));                
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);                           
            this.GetComponent<FuelConsumptionComponent>().Initialize(75);                    
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(3000);                       
            this.GetComponent<HousingComponent>().Set(CombustionGeneratorItem.HousingVal);                                

            this.GetComponent<LiquidProducerComponent>().Setup(typeof(SmogItem), (int)(0.8f * 1000f), this.NamedOccupancyOffset("ChimneyOut"));  
            this.GetComponent<LiquidConverterComponent>().Setup(typeof(WaterItem), typeof(SewageItem), this.NamedOccupancyOffset("WaterInputPort"), this.NamedOccupancyOffset("SewageOutputPort"), 300, 0.9f); 
        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class CombustionGeneratorItem :
        WorldObjectItem<CombustionGeneratorObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Combustion Generator"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Consumes most fuels to produce energy."); } }

        static CombustionGeneratorItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w from fuel"), Text.Info(75))); } } 
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(3000))); } } 
    }

    [RequiresSkill(typeof(MechanicsSkill), 0)]      
    public partial class CombustionGeneratorRecipe : Recipe
    {
        public CombustionGeneratorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CombustionGeneratorItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CombustionEngineItem>(1), 
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 25;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CombustionGeneratorRecipe), Item.Get<CombustionGeneratorItem>().UILink(), 30, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Combustion Generator"), typeof(CombustionGeneratorRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}