namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(CopperOreRubbleSet1Chunk1Object), typeof(CopperOreRubbleSet1Chunk2Object), typeof(CopperOreRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(CopperOreRubbleSet2Chunk1Object), typeof(CopperOreRubbleSet2Chunk2Object), typeof(CopperOreRubbleSet2Chunk3Object), typeof(CopperOreRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(CopperOreRubbleSet3Chunk1Object), typeof(CopperOreRubbleSet3Chunk2Object), typeof(CopperOreRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(CopperOreRubbleSet4Chunk1Object), typeof(CopperOreRubbleSet4Chunk2Object), typeof(CopperOreRubbleSet4Chunk3Object))]
    public partial class CopperOreBlock : Block { }

    [Serialized] public partial class CopperOreRubbleSet1Chunk1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet1Chunk2Object : RubbleObject<CopperOreItem> { }

    [BecomesRubble(typeof(CopperOreRubbleSet1Chunk3Split1Object), typeof(CopperOreRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class CopperOreRubbleSet1Chunk3Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet1Chunk3Split1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet1Chunk3Split2Object : RubbleObject<CopperOreItem> { }

    [Serialized] public partial class CopperOreRubbleSet2Chunk1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet2Chunk2Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet2Chunk3Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet2Chunk4Object : RubbleObject<CopperOreItem> { }

    [Serialized] public partial class CopperOreRubbleSet3Chunk1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet3Chunk2Object : RubbleObject<CopperOreItem> { }
    [BecomesRubble(typeof(CopperOreRubbleSet3Chunk3Split1Object), typeof(CopperOreRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class CopperOreRubbleSet3Chunk3Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet3Chunk3Split1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet3Chunk3Split2Object : RubbleObject<CopperOreItem> { }

    [BecomesRubble(typeof(CopperOreRubbleSet4Chunk1Split1Object), typeof(CopperOreRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class CopperOreRubbleSet4Chunk1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet4Chunk1Split1Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet4Chunk1Split2Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet4Chunk2Object : RubbleObject<CopperOreItem> { }
    [Serialized] public partial class CopperOreRubbleSet4Chunk3Object : RubbleObject<CopperOreItem> { }
}