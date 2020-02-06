// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Pipes.LiquidComponents;

    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(LiquidConverterComponent))]
    public partial class SinkObject : WorldObject
    {
    }
}
