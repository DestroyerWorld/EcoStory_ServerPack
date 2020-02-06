namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(StoneRubbleSet1Chunk1Object), typeof(StoneRubbleSet1Chunk2Object), typeof(StoneRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(StoneRubbleSet2Chunk1Object), typeof(StoneRubbleSet2Chunk2Object), typeof(StoneRubbleSet2Chunk3Object), typeof(StoneRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(StoneRubbleSet3Chunk1Object), typeof(StoneRubbleSet3Chunk2Object), typeof(StoneRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(StoneRubbleSet4Chunk1Object), typeof(StoneRubbleSet4Chunk2Object), typeof(StoneRubbleSet4Chunk3Object))]
    public partial class StoneBlock : Block { }

    [Serialized] public partial class StoneRubbleSet1Chunk1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet1Chunk2Object : RubbleObject<StoneItem> { }

    [BecomesRubble(typeof(StoneRubbleSet1Chunk3Split1Object), typeof(StoneRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class StoneRubbleSet1Chunk3Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet1Chunk3Split1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet1Chunk3Split2Object : RubbleObject<StoneItem> { }

    [Serialized] public partial class StoneRubbleSet2Chunk1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet2Chunk2Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet2Chunk3Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet2Chunk4Object : RubbleObject<StoneItem> { }

    [Serialized] public partial class StoneRubbleSet3Chunk1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet3Chunk2Object : RubbleObject<StoneItem> { }
    [BecomesRubble(typeof(StoneRubbleSet3Chunk3Split1Object), typeof(StoneRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class StoneRubbleSet3Chunk3Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet3Chunk3Split1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet3Chunk3Split2Object : RubbleObject<StoneItem> { }

    [BecomesRubble(typeof(StoneRubbleSet4Chunk1Split1Object), typeof(StoneRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class StoneRubbleSet4Chunk1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet4Chunk1Split1Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet4Chunk1Split2Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet4Chunk2Object : RubbleObject<StoneItem> { }
    [Serialized] public partial class StoneRubbleSet4Chunk3Object : RubbleObject<StoneItem> { }
}