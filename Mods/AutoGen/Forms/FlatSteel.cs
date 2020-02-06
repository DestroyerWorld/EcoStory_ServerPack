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
    [Tier(4)]
    [IsForm("Floor", typeof(FlatSteelItem))]
    public partial class FlatSteelFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Wall", typeof(FlatSteelItem))]
    public partial class FlatSteelWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Cube", typeof(FlatSteelItem))]
    public partial class FlatSteelCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Roof", typeof(FlatSteelItem))]
    public partial class FlatSteelRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Column", typeof(FlatSteelItem))]
    public partial class FlatSteelColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Window", typeof(FlatSteelItem))]
    public partial class FlatSteelWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }
    }



    [RotatedVariants(typeof(FlatSteelStairsBlock), typeof(FlatSteelStairs90Block), typeof(FlatSteelStairs180Block), typeof(FlatSteelStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Stairs", typeof(FlatSteelItem))]
    public partial class FlatSteelStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelStairs270Block : Block
    { }


    [RotatedVariants(typeof(FlatSteelLadderBlock), typeof(FlatSteelLadder90Block), typeof(FlatSteelLadder180Block), typeof(FlatSteelLadder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Ladder", typeof(FlatSteelItem))]
    public partial class FlatSteelLadderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelLadder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelLadder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FlatSteelLadder270Block : Block
    { }

}
