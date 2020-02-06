// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Gameplay.Components.Auth;

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))] 
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(AirPollutionComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(CraneToolComponent))]
    [RequireComponent(typeof(PhysicsValueSyncComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    [ObjectCanMakeBlockForm(new[] { "Wall", "Floor", "Roof", "Stairs", "Window", "Fence", "Aqueduct", "Cube", "Column" })]
    public class CraneObject : PhysicsWorldObject
    {
        protected CraneObject() { }
        public override LocString DisplayName                     { get { return Localizer.DoStr("Crane"); } }

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
            this.GetComponent<VehicleComponent>().Initialize(30, 1, 1);
            this.GetComponent<CraneToolComponent>().Initialize(200, 150);
        }
    }
}
