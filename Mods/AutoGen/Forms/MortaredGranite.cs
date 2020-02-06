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
    [IsForm("Floor", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteFloorBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Wall", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteWallBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Cube", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteCubeBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Roof", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteRoofBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Column", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteColumnBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }


    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Window", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteWindowBlock :
        Block, IRepresentsItem
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }
    }



    [RotatedVariants(typeof(MortaredGraniteStairsBlock), typeof(MortaredGraniteStairs90Block), typeof(MortaredGraniteStairs180Block), typeof(MortaredGraniteStairs270Block))]
    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    [IsForm("Stairs", typeof(MortaredGraniteItem))]
    public partial class MortaredGraniteStairsBlock : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredGraniteStairs90Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredGraniteStairs180Block : Block
    { }

    [Serialized]
    [Wall, Constructed, Solid, BuildRoomMaterialOption]
    [Tier(1)]
    public partial class MortaredGraniteStairs270Block : Block
    { }

}
