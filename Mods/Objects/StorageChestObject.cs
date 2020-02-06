// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;

    public partial class StorageChestObject : WorldObject
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            this.GetComponent<LinkComponent>().Initialize(5);
            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Storage"));
        }
    }
}
