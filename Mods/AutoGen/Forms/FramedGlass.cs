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
    [IsForm("Floor", typeof(FramedGlassItem))]
    public partial class FramedGlassFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Wall", typeof(FramedGlassItem))]
    public partial class FramedGlassWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Cube", typeof(FramedGlassItem))]
    public partial class FramedGlassCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Roof", typeof(FramedGlassItem))]
    public partial class FramedGlassRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Column", typeof(FramedGlassItem))]
    public partial class FramedGlassColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Window", typeof(FramedGlassItem))]
    public partial class FramedGlassWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }
    }



    [RotatedVariants(typeof(FramedGlassStairsBlock), typeof(FramedGlassStairs90Block), typeof(FramedGlassStairs180Block), typeof(FramedGlassStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    [IsForm("Stairs", typeof(FramedGlassItem))]
    public partial class FramedGlassStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FramedGlassStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FramedGlassStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(4)]
    public partial class FramedGlassStairs270Block : Block
    { }

}
