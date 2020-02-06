namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(CoalRubbleSet1Chunk1Object), typeof(CoalRubbleSet1Chunk2Object), typeof(CoalRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(CoalRubbleSet2Chunk1Object), typeof(CoalRubbleSet2Chunk2Object), typeof(CoalRubbleSet2Chunk3Object), typeof(CoalRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(CoalRubbleSet3Chunk1Object), typeof(CoalRubbleSet3Chunk2Object), typeof(CoalRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(CoalRubbleSet4Chunk1Object), typeof(CoalRubbleSet4Chunk2Object), typeof(CoalRubbleSet4Chunk3Object))]
    public partial class CoalBlock : Block { }

    [Serialized] public partial class CoalRubbleSet1Chunk1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet1Chunk2Object : RubbleObject<CoalItem> { }

    [BecomesRubble(typeof(CoalRubbleSet1Chunk3Split1Object), typeof(CoalRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class CoalRubbleSet1Chunk3Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet1Chunk3Split1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet1Chunk3Split2Object : RubbleObject<CoalItem> { }

    [Serialized] public partial class CoalRubbleSet2Chunk1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet2Chunk2Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet2Chunk3Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet2Chunk4Object : RubbleObject<CoalItem> { }

    [Serialized] public partial class CoalRubbleSet3Chunk1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet3Chunk2Object : RubbleObject<CoalItem> { }
    [BecomesRubble(typeof(CoalRubbleSet3Chunk3Split1Object), typeof(CoalRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class CoalRubbleSet3Chunk3Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet3Chunk3Split1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet3Chunk3Split2Object : RubbleObject<CoalItem> { }

    [BecomesRubble(typeof(CoalRubbleSet4Chunk1Split1Object), typeof(CoalRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class CoalRubbleSet4Chunk1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet4Chunk1Split1Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet4Chunk1Split2Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet4Chunk2Object : RubbleObject<CoalItem> { }
    [Serialized] public partial class CoalRubbleSet4Chunk3Object : RubbleObject<CoalItem> { }
}