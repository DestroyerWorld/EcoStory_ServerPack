// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using Eco.Gameplay.Items;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

[Serialized]
[MaxStackSize(10)]
[RequiresTool(typeof(ShovelItem))]
public partial class GarbageItem : BlockItem<GarbageBlock>
{
    public override LocString DisplayName { get { return Localizer.DoStr("Garbage"); } }
    public override LocString DisplayNamePlural { get { return Localizer.DoStr("Garbage"); } }
    public override LocString DisplayDescription { get { return Localizer.DoStr("A disgusting pile of garbage."); } }
    public override bool CanStickToWalls { get { return false; } }
}
[Serialized]
[MaxStackSize(1)]
public partial class TrashItem : Item { }
[Serialized]
[MaxStackSize(1)]
public partial class CompostablesItem : Item { }
