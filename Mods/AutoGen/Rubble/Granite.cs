namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(GraniteRubbleSet1Chunk1Object), typeof(GraniteRubbleSet1Chunk2Object), typeof(GraniteRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(GraniteRubbleSet2Chunk1Object), typeof(GraniteRubbleSet2Chunk2Object), typeof(GraniteRubbleSet2Chunk3Object), typeof(GraniteRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(GraniteRubbleSet3Chunk1Object), typeof(GraniteRubbleSet3Chunk2Object), typeof(GraniteRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(GraniteRubbleSet4Chunk1Object), typeof(GraniteRubbleSet4Chunk2Object), typeof(GraniteRubbleSet4Chunk3Object))]
    public partial class GraniteBlock : Block { }

    [Serialized] public partial class GraniteRubbleSet1Chunk1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet1Chunk2Object : RubbleObject<GraniteItem> { }

    [BecomesRubble(typeof(GraniteRubbleSet1Chunk3Split1Object), typeof(GraniteRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class GraniteRubbleSet1Chunk3Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet1Chunk3Split1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet1Chunk3Split2Object : RubbleObject<GraniteItem> { }

    [Serialized] public partial class GraniteRubbleSet2Chunk1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet2Chunk2Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet2Chunk3Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet2Chunk4Object : RubbleObject<GraniteItem> { }

    [Serialized] public partial class GraniteRubbleSet3Chunk1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet3Chunk2Object : RubbleObject<GraniteItem> { }
    [BecomesRubble(typeof(GraniteRubbleSet3Chunk3Split1Object), typeof(GraniteRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class GraniteRubbleSet3Chunk3Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet3Chunk3Split1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet3Chunk3Split2Object : RubbleObject<GraniteItem> { }

    [BecomesRubble(typeof(GraniteRubbleSet4Chunk1Split1Object), typeof(GraniteRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class GraniteRubbleSet4Chunk1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet4Chunk1Split1Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet4Chunk1Split2Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet4Chunk2Object : RubbleObject<GraniteItem> { }
    [Serialized] public partial class GraniteRubbleSet4Chunk3Object : RubbleObject<GraniteItem> { }
}