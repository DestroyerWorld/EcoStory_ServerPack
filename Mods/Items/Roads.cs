// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using Eco.Core.Utils;
using Eco.Gameplay.Interactions;
using Eco.Gameplay.Items;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems;
using Eco.Mods.TechTree;
using Eco.Shared.Localization;
using Eco.Shared.Math;
using Eco.Shared.Serialization;
using Eco.Shared.Utils;
using Eco.World;
using Eco.World.Blocks;

[Serialized]
[Solid, Wall, Road(1f)]
public class StoneRoadWorldObjectBlock : WorldObjectBlock
{
    protected StoneRoadWorldObjectBlock() { }
    public StoneRoadWorldObjectBlock(WorldObject obj) : base(obj) { }
}

[Serialized]
[Solid, Wall, Road(1f)]
public class DirtRoadWorldObjectBlock : WorldObjectBlock
{
    protected DirtRoadWorldObjectBlock() { }
    public DirtRoadWorldObjectBlock(WorldObject obj) : base(obj) { }
}

[Serialized]
[Solid, Wall, Road(1f)]
public class AsphaltRoadWorldObjectBlock : WorldObjectBlock
{
    protected AsphaltRoadWorldObjectBlock() { }
    public AsphaltRoadWorldObjectBlock(WorldObject obj) : base(obj) { }
}

[Serialized]
public abstract class BaseRampObject : WorldObject
{
    // No UI
    public override InteractResult OnActInteract(InteractionContext context)
    {
        return InteractResult.NoOp;
    }
}

[Serialized]
public class DirtRampObject : BaseRampObject
{
    public override LocString DisplayName { get { return Localizer.DoStr("Dirt Ramp"); } }

    private DirtRampObject() { }

    protected override void Initialize()
    {
        base.Initialize();

        // destroy the object first - as destroy deletes blocks based on occupancy
        this.Destroy();

        // migration - delete the old object and replace with blocks.
        var placementDir = this.Rotation.RotateVector(Vector3i.Left).Round;
        var offset = this.Rotation.RotateVector(Vector3i.Forward).Round;
        var blockTypes = Item.Get<DirtRampItem>().GetBlockTypesForDirection(placementDir);
        var position = this.Position.Round;
        foreach (var blockType in blockTypes)
        {
            // dirt ramps were 2 wide
            World.SetBlock(blockType, position);
            World.SetBlock(blockType, position + offset);
            position += placementDir;
        }
    }
}

[Serialized]
[ItemGroup("Road Items")]
[Tag("Road")]
[Weight(60000)]
public class DirtRampItem : RampItem<DirtRampObject>
{
    public override LocString DisplayName { get { return Localizer.DoStr("Dirt Ramp"); } }
    public override LocString DisplayDescription  { get { return Localizer.DoStr("4 x 1 Dirt Ramp."); } }

    public override Dictionary<Vector3i, Type[]> BlockTypes { get { return new Dictionary<Vector3i, Type[]>
    {
        {Vector3i.Left,    new [] { typeof(DirtRampABlock), typeof(DirtRampBBlock), typeof(DirtRampCBlock), typeof(DirtRampDBlock) }},
        {Vector3i.Forward, new [] { typeof(DirtRampA90Block), typeof(DirtRampB90Block), typeof(DirtRampC90Block), typeof(DirtRampD90Block) }},
        {Vector3i.Right,   new [] { typeof(DirtRampA180Block), typeof(DirtRampB180Block), typeof(DirtRampC180Block), typeof(DirtRampD180Block) }},
        {Vector3i.Back,    new [] { typeof(DirtRampA270Block), typeof(DirtRampB270Block), typeof(DirtRampC270Block), typeof(DirtRampD270Block) }},
    };}}
}

[Serialized]
public class StoneRampObject : BaseRampObject
{
    public override LocString DisplayName { get { return Localizer.DoStr("Stone Ramp"); } }

    private StoneRampObject() { }

    protected override void Initialize()
    {
        base.Initialize();

        // destroy the object first - as destroy deletes blocks based on occupancy
        this.Destroy();

        // migration - delete the old object and replace with blocks.
        var placementDir = this.Rotation.RotateVector(Vector3i.Left).Round;
        var offset = this.Rotation.RotateVector(Vector3i.Forward).Round;
        var blockTypes = Item.Get<StoneRampItem>().GetBlockTypesForDirection(placementDir);
        var position = this.Position.Round;
        foreach (var blockType in blockTypes)
        {
            // stone ramps were 2 wide
            World.SetBlock(blockType, position);
            World.SetBlock(blockType, position + offset);
            position += placementDir;
        }
    }
}

[Serialized]
[ItemGroup("Road Items")]
[Tag("Road")]
[Weight(60000)]
public class StoneRampItem : RampItem<StoneRampObject>
{
    public override LocString DisplayName { get { return Localizer.DoStr("Stone Ramp"); } }
    public override LocString DisplayDescription  { get { return Localizer.DoStr("4 x 1 Stone Ramp."); } }

    public override Dictionary<Vector3i, Type[]> BlockTypes { get { return new Dictionary<Vector3i, Type[]>
    {
        {Vector3i.Left,    new [] { typeof(StoneRampABlock), typeof(StoneRampBBlock), typeof(StoneRampCBlock), typeof(StoneRampDBlock) }},
        {Vector3i.Forward, new [] { typeof(StoneRampA90Block), typeof(StoneRampB90Block), typeof(StoneRampC90Block), typeof(StoneRampD90Block) }},
        {Vector3i.Right,   new [] { typeof(StoneRampA180Block), typeof(StoneRampB180Block), typeof(StoneRampC180Block), typeof(StoneRampD180Block) }},
        {Vector3i.Back,    new [] { typeof(StoneRampA270Block), typeof(StoneRampB270Block), typeof(StoneRampC270Block), typeof(StoneRampD270Block) }},
    };}}
}

[Serialized]
public class AsphaltRampObject : BaseRampObject
{
    public override LocString DisplayName { get { return Localizer.DoStr("Asphalt Ramp"); } }

    private AsphaltRampObject() { }

    protected override void Initialize()
    {
        base.Initialize();

        // destroy the object first - as destroy deletes blocks based on occupancy
        this.Destroy();

        // migration - delete the old object and replace with blocks.
        var placementDir = this.Rotation.RotateVector(Vector3i.Left).Round;
        var offset = this.Rotation.RotateVector(Vector3i.Forward).Round;
        var blockTypes = Item.Get<AsphaltRampItem>().GetBlockTypesForDirection(placementDir);
        var position = this.Position.Round;
        foreach (var blockType in blockTypes)
        {
            // asphalt ramps were 4 wide
            World.SetBlock(blockType, position);
            World.SetBlock(blockType, position + offset);
            World.SetBlock(blockType, position + offset * 2);
            World.SetBlock(blockType, position + offset * 3);
            position += placementDir;
        }
    }
}

[Serialized]
[ItemGroup("Road Items")]
[Tag("Road")]
[Weight(60000)]
public class AsphaltRampItem : RampItem<AsphaltRampObject>
{
    public override LocString DisplayName { get { return Localizer.DoStr("Asphalt Ramp"); } }
    public override LocString DisplayDescription  { get { return Localizer.DoStr("4 x 1 Asphalt Ramp."); } }

    public override Dictionary<Vector3i, Type[]> BlockTypes { get { return new Dictionary<Vector3i, Type[]>
    {
        {Vector3i.Left,    new [] { typeof(AsphaltRampABlock), typeof(AsphaltRampBBlock), typeof(AsphaltRampCBlock), typeof(AsphaltRampDBlock) }},
        {Vector3i.Forward, new [] { typeof(AsphaltRampA90Block), typeof(AsphaltRampB90Block), typeof(AsphaltRampC90Block), typeof(AsphaltRampD90Block)}},
        {Vector3i.Right,   new [] { typeof(AsphaltRampA180Block), typeof(AsphaltRampB180Block), typeof(AsphaltRampC180Block), typeof(AsphaltRampD180Block) }},
        {Vector3i.Back,    new [] { typeof(AsphaltRampA270Block), typeof(AsphaltRampB270Block), typeof(AsphaltRampC270Block), typeof(AsphaltRampD270Block) }},
    };}}
}

#region DirtRampBlocks
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampABlock : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampBBlock : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampCBlock : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampDBlock : DirtRoadBlock { }

[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampA90Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampB90Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampC90Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampD90Block : DirtRoadBlock { }

[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampA180Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampB180Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampC180Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampD180Block : DirtRoadBlock { }

[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampA270Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampB270Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampC270Block : DirtRoadBlock { }
[Road(typeof(DirtRoadBlock)), Ramp(typeof(DirtRampItem))]
[Serialized, Solid, Constructed] public partial class DirtRampD270Block : DirtRoadBlock { }
#endregion

#region StoneRampBlocks
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampABlock : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampBBlock : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampCBlock : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampDBlock : StoneRoadBlock { }

[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampA90Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampB90Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampC90Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampD90Block : StoneRoadBlock { }

[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampA180Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampB180Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampC180Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampD180Block : StoneRoadBlock { }

[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampA270Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampB270Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampC270Block : StoneRoadBlock { }
[Road(typeof(StoneRoadBlock)), Ramp(typeof(StoneRampItem))]
[Serialized, Solid, Constructed] public partial class StoneRampD270Block : StoneRoadBlock { }
#endregion

#region AsphaltRampBlocks
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampABlock : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampBBlock : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampCBlock : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampDBlock : AsphaltRoadBlock { }

[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampA90Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampB90Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampC90Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampD90Block : AsphaltRoadBlock { }

[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampA180Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampB180Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampC180Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampD180Block : AsphaltRoadBlock { }

[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampA270Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampB270Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampC270Block : AsphaltRoadBlock { }
[Road(typeof(AsphaltRoadBlock)), Ramp(typeof(AsphaltRampItem))]
[Serialized, Solid, Constructed] public partial class AsphaltRampD270Block : AsphaltRoadBlock { }
#endregion