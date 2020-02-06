// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Eco.Gameplay.Components;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

[Serialized]
[Category("Hidden")]
public class TransmissionPoleItem : WorldObjectItem<TransmissionPoleObject>
{
    public override LocString DisplayName { get { return Localizer.DoStr("Transmission Pole"); } }

    public override LocString DisplayDescription  { get { return Localizer.DoStr("Can link energy."); } }
}

[Serialized]
[RequireComponent(typeof(PowerGridComponent))]
[RequireComponent(typeof(MinimapComponent))]
public class TransmissionPoleObject : WorldObject
{
    public override LocString DisplayName { get { return Localizer.DoStr("Transmission Pole"); } }

    private TransmissionPoleObject()
    { }

    protected override void Initialize()
    {
        base.Initialize();

        this.GetComponent<PowerGridComponent>().Initialize(10f, new ElectricPower());
        this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Power Structures"));
    }
}