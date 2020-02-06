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
    [IsForm("Floor", typeof(MortaredStoneItem))]
    public partial class MortaredStoneFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(MortaredStoneItem))]
    public partial class MortaredStoneWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(MortaredStoneItem))]
    public partial class MortaredStoneCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(MortaredStoneItem))]
    public partial class MortaredStoneRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(MortaredStoneItem))]
    public partial class MortaredStoneColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(MortaredStoneItem))]
    public partial class MortaredStoneWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }
    }



    [RotatedVariants(typeof(MortaredStoneStairsBlock), typeof(MortaredStoneStairs90Block), typeof(MortaredStoneStairs180Block), typeof(MortaredStoneStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(MortaredStoneItem))]
    public partial class MortaredStoneStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredStoneStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredStoneStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredStoneStairs270Block : Block
    { }

}
