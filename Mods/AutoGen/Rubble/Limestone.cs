namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(LimestoneRubbleSet1Chunk1Object), typeof(LimestoneRubbleSet1Chunk2Object), typeof(LimestoneRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(LimestoneRubbleSet2Chunk1Object), typeof(LimestoneRubbleSet2Chunk2Object), typeof(LimestoneRubbleSet2Chunk3Object), typeof(LimestoneRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(LimestoneRubbleSet3Chunk1Object), typeof(LimestoneRubbleSet3Chunk2Object), typeof(LimestoneRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(LimestoneRubbleSet4Chunk1Object), typeof(LimestoneRubbleSet4Chunk2Object), typeof(LimestoneRubbleSet4Chunk3Object))]
    public partial class LimestoneBlock : Block { }

    [Serialized] public partial class LimestoneRubbleSet1Chunk1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet1Chunk2Object : RubbleObject<LimestoneItem> { }

    [BecomesRubble(typeof(LimestoneRubbleSet1Chunk3Split1Object), typeof(LimestoneRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class LimestoneRubbleSet1Chunk3Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet1Chunk3Split1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet1Chunk3Split2Object : RubbleObject<LimestoneItem> { }

    [Serialized] public partial class LimestoneRubbleSet2Chunk1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet2Chunk2Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet2Chunk3Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet2Chunk4Object : RubbleObject<LimestoneItem> { }

    [Serialized] public partial class LimestoneRubbleSet3Chunk1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet3Chunk2Object : RubbleObject<LimestoneItem> { }
    [BecomesRubble(typeof(LimestoneRubbleSet3Chunk3Split1Object), typeof(LimestoneRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class LimestoneRubbleSet3Chunk3Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet3Chunk3Split1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet3Chunk3Split2Object : RubbleObject<LimestoneItem> { }

    [BecomesRubble(typeof(LimestoneRubbleSet4Chunk1Split1Object), typeof(LimestoneRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class LimestoneRubbleSet4Chunk1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet4Chunk1Split1Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet4Chunk1Split2Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet4Chunk2Object : RubbleObject<LimestoneItem> { }
    [Serialized] public partial class LimestoneRubbleSet4Chunk3Object : RubbleObject<LimestoneItem> { }
}