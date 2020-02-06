namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Components.Auth;

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))] 
    [RequireComponent(typeof(MovableLinkComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(VehicleToolComponent))]
    public class ExcavatorObject : PhysicsWorldObject
    {
        protected ExcavatorObject() { }
        public override LocString DisplayName                     { get { return Localizer.DoStr("Excavator"); } }

        static ExcavatorObject()
        {
            WorldObject.AddOccupancy<ExcavatorObject>(new List<BlockOccupancy>(0));
        }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(PetroleumItem),
            typeof(GasolineItem),
            typeof(BiodieselItem),
        };

        private Player Driver { get { return this.GetComponent<VehicleComponent>().Driver; } }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(50);
            this.GetComponent<AirPollutionComponent>().Initialize(0.2f);
            this.GetComponent<VehicleComponent>().Initialize(30, 1);
            this.GetComponent<VehicleToolComponent>().Initialize(6, 0, new DirtItem(), 100, 200);
        }
    }
}
