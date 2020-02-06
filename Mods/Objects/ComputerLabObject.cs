// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerGridNetworkComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    public partial class ComputerLabObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Computer Lab"); } }

        protected override void Initialize()
        {
            base.Initialize();

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Computer Lab"));
            this.GetComponent<LinkComponent>().Initialize(5);
            this.GetComponent<PowerGridComponent>().Initialize(10.0f, new ElectricPower());
            this.GetComponent<PowerGridNetworkComponent>().Initialize(new Dictionary<Type, int> { { typeof(LaserObject), 4 }, { typeof(ComputerLabObject), 1 } }, true);
        }
    }
}