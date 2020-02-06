// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Objects;
    using Eco.Shared.Serialization;
    using Gameplay.Components;

    [RequireComponent(typeof(CustomTextComponent))]
    public partial class WoodSignObject : WorldObject
    { }

    [Serialized]
    public partial class SmallWoodSignObject : WoodSignObject
    { }
}


