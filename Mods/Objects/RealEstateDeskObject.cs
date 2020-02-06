// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Objects;

    [RequireComponent(typeof(DeedManagementComponent))]
    [RequireComponent(typeof(DeedSalesComponent))]
    public partial class RealEstateDeskObject : WorldObject
    {
        public RealEstateDeskObject() { }

        protected override void PostInitialize()
        {
            if (this.isFirstInitialization)
            {
                this.GetComponent<AuthComponent>().SetPublic();
            }            
        }
    }
}
