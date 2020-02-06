// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(ElevatorComponent))]
    public class WoodenElevatorObject : PhysicsWorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wooden Elevator"); } }

        private WoodenElevatorObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            var elevatorComponent = this.GetComponent<ElevatorComponent>();
            elevatorComponent.Initialize();            
        }
    }
}