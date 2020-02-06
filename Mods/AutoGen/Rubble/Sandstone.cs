namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(SandstoneRubbleSet1Chunk1Object), typeof(SandstoneRubbleSet1Chunk2Object), typeof(SandstoneRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(SandstoneRubbleSet2Chunk1Object), typeof(SandstoneRubbleSet2Chunk2Object), typeof(SandstoneRubbleSet2Chunk3Object), typeof(SandstoneRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(SandstoneRubbleSet3Chunk1Object), typeof(SandstoneRubbleSet3Chunk2Object), typeof(SandstoneRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(SandstoneRubbleSet4Chunk1Object), typeof(SandstoneRubbleSet4Chunk2Object), typeof(SandstoneRubbleSet4Chunk3Object))]
    public partial class SandstoneBlock : Block { }

    [Serialized] public partial class SandstoneRubbleSet1Chunk1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet1Chunk2Object : RubbleObject<SandstoneItem> { }

    [BecomesRubble(typeof(SandstoneRubbleSet1Chunk3Split1Object), typeof(SandstoneRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class SandstoneRubbleSet1Chunk3Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet1Chunk3Split1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet1Chunk3Split2Object : RubbleObject<SandstoneItem> { }

    [Serialized] public partial class SandstoneRubbleSet2Chunk1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet2Chunk2Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet2Chunk3Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet2Chunk4Object : RubbleObject<SandstoneItem> { }

    [Serialized] public partial class SandstoneRubbleSet3Chunk1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet3Chunk2Object : RubbleObject<SandstoneItem> { }
    [BecomesRubble(typeof(SandstoneRubbleSet3Chunk3Split1Object), typeof(SandstoneRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class SandstoneRubbleSet3Chunk3Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet3Chunk3Split1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet3Chunk3Split2Object : RubbleObject<SandstoneItem> { }

    [BecomesRubble(typeof(SandstoneRubbleSet4Chunk1Split1Object), typeof(SandstoneRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class SandstoneRubbleSet4Chunk1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet4Chunk1Split1Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet4Chunk1Split2Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet4Chunk2Object : RubbleObject<SandstoneItem> { }
    [Serialized] public partial class SandstoneRubbleSet4Chunk3Object : RubbleObject<SandstoneItem> { }
}