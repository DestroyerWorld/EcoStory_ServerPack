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
    public class SkidSteerObject : PhysicsWorldObject
    {
        protected SkidSteerObject() { }
        public override LocString DisplayName                     { get { return Localizer.DoStr("Skid Steer"); } }

        static SkidSteerObject()
        {
            WorldObject.AddOccupancy<SkidSteerObject>(new List<BlockOccupancy>(0));
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
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);
            this.GetComponent<AirPollutionComponent>().Initialize(0.1f);
            this.GetComponent<VehicleComponent>().Initialize(20, 1);
            this.GetComponent<VehicleToolComponent>().Initialize(4, 0, new DirtItem(), 100, 200, toolOnMount:true);
        }
    }
}
