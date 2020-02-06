// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

[Serialized]
[RequireComponent(typeof(MinimapComponent))]
[RequireComponent(typeof(PowerGridComponent))]
[RequireComponent(typeof(PowerConsumptionComponent))]
[RequireComponent(typeof(ChargingComponent))]
[RequireComponent(typeof(PowerGridNetworkComponent))]
[RequireComponent(typeof(PropertyAuthComponent))]
public class LaserObject : WorldObject
{
    public float MinimapYaw { get { return 0.0f; } }
    public bool HideOnMinimap { get { return false; } }

    protected override void Initialize()
    {
        base.Initialize();
        this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Laser"));
        this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
        this.GetComponent<PowerConsumptionComponent>().Initialize(6000);
        this.GetComponent<ChargingComponent>().Initialize(30, 30);
        this.GetComponent<PowerGridNetworkComponent>().Initialize(new Dictionary<Type, int> { { typeof(LaserObject), 4 }, { typeof(ComputerLabObject), 1 } }, false);
    }

    static LaserObject()
    {
    }
}