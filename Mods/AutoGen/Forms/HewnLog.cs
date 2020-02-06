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
    [Tier(1)]
    [IsForm("Floor", typeof(HewnLogItem))]
    public partial class HewnLogFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(HewnLogItem))]
    public partial class HewnLogWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(HewnLogItem))]
    public partial class HewnLogCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(HewnLogItem))]
    public partial class HewnLogRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(HewnLogItem))]
    public partial class HewnLogColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(HewnLogItem))]
    public partial class HewnLogWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("PeatRoof", typeof(HewnLogItem))]
    public partial class HewnLogPeatRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }
    }



    [RotatedVariants(typeof(HewnLogStairsBlock), typeof(HewnLogStairs90Block), typeof(HewnLogStairs180Block), typeof(HewnLogStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(HewnLogItem))]
    public partial class HewnLogStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogStairs270Block : Block
    { }


    [RotatedVariants(typeof(HewnLogLadderBlock), typeof(HewnLogLadder90Block), typeof(HewnLogLadder180Block), typeof(HewnLogLadder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Ladder", typeof(HewnLogItem))]
    public partial class HewnLogLadderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogLadder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogLadder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class HewnLogLadder270Block : Block
    { }

}
