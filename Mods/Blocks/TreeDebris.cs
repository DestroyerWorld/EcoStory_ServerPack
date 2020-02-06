namespace Eco.Mods.TechTree
{
    using Eco.Shared.Serialization;
    using Eco.World.Blocks;

    [Serialized, TreeDebris("Oak"), MoveEfficiency(0.3f)]
    public partial class OakTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Cedar"), MoveEfficiency(0.3f)]
    public partial class CedarTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Fir"), MoveEfficiency(0.3f)]
    public partial class FirTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Birch"), MoveEfficiency(0.3f)]
    public partial class BirchTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("OldGrowthRedwood"), MoveEfficiency(0.3f)]
    public partial class OldGrowthRedwoodTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Redwood"), MoveEfficiency(0.3f)]
    public partial class RedwoodTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Palm"), MoveEfficiency(0.3f)]
    public partial class PalmTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("SaguaroCactus"), MoveEfficiency(0.3f)]
    public partial class SaguaroCactusDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Ceiba"), MoveEfficiency(0.3f)]
    public partial class CeibaTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Joshua"), MoveEfficiency(0.3f)]
    public partial class JoshuaTreeDebrisBlock : TreeDebrisBlock { }
    [Serialized, TreeDebris("Spruce"), MoveEfficiency(0.3f)]
    public partial class SpruceTreeDebrisBlock : TreeDebrisBlock { }

}