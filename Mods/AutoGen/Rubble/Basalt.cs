namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(BasaltRubbleSet1Chunk1Object), typeof(BasaltRubbleSet1Chunk2Object), typeof(BasaltRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(BasaltRubbleSet2Chunk1Object), typeof(BasaltRubbleSet2Chunk2Object), typeof(BasaltRubbleSet2Chunk3Object), typeof(BasaltRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(BasaltRubbleSet3Chunk1Object), typeof(BasaltRubbleSet3Chunk2Object), typeof(BasaltRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(BasaltRubbleSet4Chunk1Object), typeof(BasaltRubbleSet4Chunk2Object), typeof(BasaltRubbleSet4Chunk3Object))]
    public partial class BasaltBlock : Block { }

    [Serialized] public partial class BasaltRubbleSet1Chunk1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet1Chunk2Object : RubbleObject<BasaltItem> { }

    [BecomesRubble(typeof(BasaltRubbleSet1Chunk3Split1Object), typeof(BasaltRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class BasaltRubbleSet1Chunk3Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet1Chunk3Split1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet1Chunk3Split2Object : RubbleObject<BasaltItem> { }

    [Serialized] public partial class BasaltRubbleSet2Chunk1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet2Chunk2Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet2Chunk3Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet2Chunk4Object : RubbleObject<BasaltItem> { }

    [Serialized] public partial class BasaltRubbleSet3Chunk1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet3Chunk2Object : RubbleObject<BasaltItem> { }
    [BecomesRubble(typeof(BasaltRubbleSet3Chunk3Split1Object), typeof(BasaltRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class BasaltRubbleSet3Chunk3Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet3Chunk3Split1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet3Chunk3Split2Object : RubbleObject<BasaltItem> { }

    [BecomesRubble(typeof(BasaltRubbleSet4Chunk1Split1Object), typeof(BasaltRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class BasaltRubbleSet4Chunk1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet4Chunk1Split1Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet4Chunk1Split2Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet4Chunk2Object : RubbleObject<BasaltItem> { }
    [Serialized] public partial class BasaltRubbleSet4Chunk3Object : RubbleObject<BasaltItem> { }
}