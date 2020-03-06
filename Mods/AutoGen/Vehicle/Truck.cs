namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    
    [Serialized]
    [Weight(25000)]  
    public class TruckItem : WorldObjectItem<TruckObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Truck"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Modern truck for hauling sizable loads."); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class TruckRecipe : Recipe
    {
        public TruckRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<TruckItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CombustionEngineItem>(1),
                new CraftingElement<IronWheelItem>(4),
                new CraftingElement<RadiatorItem>(1), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(TruckRecipe), Item.Get<TruckItem>().UILink(), 25, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Truck"), typeof(TruckRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(AirPollutionComponent))]       
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class TruckObject : PhysicsWorldObject, IRepresentsItem
    {
        static TruckObject()
        {
            WorldObject.AddOccupancy<TruckObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Truck"); } }
        public Type RepresentedItemType { get { return typeof(TruckItem); } }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(PetroleumItem),
typeof(GasolineItem)
        };

        private TruckObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(36, 8000000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<AirPollutionComponent>().Initialize(0.25f);            
            this.GetComponent<VehicleComponent>().Initialize(20, 2, 2);
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,2,3));  
        }
    }
}