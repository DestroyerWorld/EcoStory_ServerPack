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
    [Tier(3)]
    [IsForm("Floor", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Wall", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Cube", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Roof", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Column", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Window", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }
    }



    [RotatedVariants(typeof(CorrugatedSteelStairsBlock), typeof(CorrugatedSteelStairs90Block), typeof(CorrugatedSteelStairs180Block), typeof(CorrugatedSteelStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Stairs", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelStairs270Block : Block
    { }


    [RotatedVariants(typeof(CorrugatedSteelLadderBlock), typeof(CorrugatedSteelLadder90Block), typeof(CorrugatedSteelLadder180Block), typeof(CorrugatedSteelLadder270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    [IsForm("Ladder", typeof(CorrugatedSteelItem))]
    public partial class CorrugatedSteelLadderBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelLadder90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelLadder180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(3)]
    public partial class CorrugatedSteelLadder270Block : Block
    { }

}
