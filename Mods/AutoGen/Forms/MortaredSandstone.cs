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
    [IsForm("Floor", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }
    }



    [RotatedVariants(typeof(MortaredSandstoneStairsBlock), typeof(MortaredSandstoneStairs90Block), typeof(MortaredSandstoneStairs180Block), typeof(MortaredSandstoneStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(MortaredSandstoneItem))]
    public partial class MortaredSandstoneStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredSandstoneStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredSandstoneStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredSandstoneStairs270Block : Block
    { }

}
