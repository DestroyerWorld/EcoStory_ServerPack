namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(ShaleRubbleSet1Chunk1Object), typeof(ShaleRubbleSet1Chunk2Object), typeof(ShaleRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(ShaleRubbleSet2Chunk1Object), typeof(ShaleRubbleSet2Chunk2Object), typeof(ShaleRubbleSet2Chunk3Object), typeof(ShaleRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(ShaleRubbleSet3Chunk1Object), typeof(ShaleRubbleSet3Chunk2Object), typeof(ShaleRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(ShaleRubbleSet4Chunk1Object), typeof(ShaleRubbleSet4Chunk2Object), typeof(ShaleRubbleSet4Chunk3Object))]
    public partial class ShaleBlock : Block { }

    [Serialized] public partial class ShaleRubbleSet1Chunk1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet1Chunk2Object : RubbleObject<ShaleItem> { }

    [BecomesRubble(typeof(ShaleRubbleSet1Chunk3Split1Object), typeof(ShaleRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class ShaleRubbleSet1Chunk3Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet1Chunk3Split1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet1Chunk3Split2Object : RubbleObject<ShaleItem> { }

    [Serialized] public partial class ShaleRubbleSet2Chunk1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet2Chunk2Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet2Chunk3Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet2Chunk4Object : RubbleObject<ShaleItem> { }

    [Serialized] public partial class ShaleRubbleSet3Chunk1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet3Chunk2Object : RubbleObject<ShaleItem> { }
    [BecomesRubble(typeof(ShaleRubbleSet3Chunk3Split1Object), typeof(ShaleRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class ShaleRubbleSet3Chunk3Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet3Chunk3Split1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet3Chunk3Split2Object : RubbleObject<ShaleItem> { }

    [BecomesRubble(typeof(ShaleRubbleSet4Chunk1Split1Object), typeof(ShaleRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class ShaleRubbleSet4Chunk1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet4Chunk1Split1Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet4Chunk1Split2Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet4Chunk2Object : RubbleObject<ShaleItem> { }
    [Serialized] public partial class ShaleRubbleSet4Chunk3Object : RubbleObject<ShaleItem> { }
}