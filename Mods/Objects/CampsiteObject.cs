// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;

    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(MountComponent))]
    public partial class CampsiteObject : WorldObject
    {
        protected override void PostInitialize()
        {
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(10);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items
            this.GetComponent<MountComponent>().Initialize(1);
        }
    }
}
