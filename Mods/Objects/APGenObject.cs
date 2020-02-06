// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Gameplay.Objects;
    using Gameplay.Components;

    [RequireComponent(typeof(AirPollutionComponent))]
    public partial class APGenObject : WorldObject
    {
        protected override void PostInitialize()
        {
            this.GetComponent<AirPollutionComponent>().Initialize(1.4f);
        }
    }
}
