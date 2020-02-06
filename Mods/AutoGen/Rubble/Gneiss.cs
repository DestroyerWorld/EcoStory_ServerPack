namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(GneissRubbleSet1Chunk1Object), typeof(GneissRubbleSet1Chunk2Object), typeof(GneissRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(GneissRubbleSet2Chunk1Object), typeof(GneissRubbleSet2Chunk2Object), typeof(GneissRubbleSet2Chunk3Object), typeof(GneissRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(GneissRubbleSet3Chunk1Object), typeof(GneissRubbleSet3Chunk2Object), typeof(GneissRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(GneissRubbleSet4Chunk1Object), typeof(GneissRubbleSet4Chunk2Object), typeof(GneissRubbleSet4Chunk3Object))]
    public partial class GneissBlock : Block { }

    [Serialized] public partial class GneissRubbleSet1Chunk1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet1Chunk2Object : RubbleObject<GneissItem> { }

    [BecomesRubble(typeof(GneissRubbleSet1Chunk3Split1Object), typeof(GneissRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class GneissRubbleSet1Chunk3Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet1Chunk3Split1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet1Chunk3Split2Object : RubbleObject<GneissItem> { }

    [Serialized] public partial class GneissRubbleSet2Chunk1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet2Chunk2Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet2Chunk3Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet2Chunk4Object : RubbleObject<GneissItem> { }

    [Serialized] public partial class GneissRubbleSet3Chunk1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet3Chunk2Object : RubbleObject<GneissItem> { }
    [BecomesRubble(typeof(GneissRubbleSet3Chunk3Split1Object), typeof(GneissRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class GneissRubbleSet3Chunk3Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet3Chunk3Split1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet3Chunk3Split2Object : RubbleObject<GneissItem> { }

    [BecomesRubble(typeof(GneissRubbleSet4Chunk1Split1Object), typeof(GneissRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class GneissRubbleSet4Chunk1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet4Chunk1Split1Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet4Chunk1Split2Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet4Chunk2Object : RubbleObject<GneissItem> { }
    [Serialized] public partial class GneissRubbleSet4Chunk3Object : RubbleObject<GneissItem> { }
}