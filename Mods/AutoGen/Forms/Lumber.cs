namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;
    


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Floor", typeof(LumberItem))]
    public partial class LumberFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Wall", typeof(LumberItem))]
    public partial class LumberWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Cube", typeof(LumberItem))]
    public partial class LumberCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Roof", typeof(LumberItem))]
    public partial class LumberRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Column", typeof(LumberItem))]
    public partial class LumberColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Window", typeof(LumberItem))]
    public partial class LumberWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Fence", typeof(LumberItem))]
    public partial class LumberFenceBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("WindowT2", typeof(LumberItem))]
    public partial class LumberWindowT2Block :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(LumberItem); } }
    }



    [RotatedVariants(typeof(LumberStairsBlock), typeof(LumberStairs90Block), typeof(LumberStairs180Block), typeof(LumberStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Stairs", typeof(LumberItem))]
    public partial class LumberStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberStairs270Block : Block
    { }


    [RotatedVariants(typeof(LumberLadderBlock), typeof(LumberLadder90Block), typeof(LumberLadder180Block), typeof(LumberLadder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    [IsForm("Ladder", typeof(LumberItem))]
    public partial class LumberLadderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberLadder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberLadder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(2)]
    public partial class LumberLadder270Block : Block
    { }

}
