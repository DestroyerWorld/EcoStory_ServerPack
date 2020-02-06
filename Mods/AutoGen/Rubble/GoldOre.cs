namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(GoldOreRubbleSet1Chunk1Object), typeof(GoldOreRubbleSet1Chunk2Object), typeof(GoldOreRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(GoldOreRubbleSet2Chunk1Object), typeof(GoldOreRubbleSet2Chunk2Object), typeof(GoldOreRubbleSet2Chunk3Object), typeof(GoldOreRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(GoldOreRubbleSet3Chunk1Object), typeof(GoldOreRubbleSet3Chunk2Object), typeof(GoldOreRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(GoldOreRubbleSet4Chunk1Object), typeof(GoldOreRubbleSet4Chunk2Object), typeof(GoldOreRubbleSet4Chunk3Object))]
    public partial class GoldOreBlock : Block { }

    [Serialized] public partial class GoldOreRubbleSet1Chunk1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet1Chunk2Object : RubbleObject<GoldOreItem> { }

    [BecomesRubble(typeof(GoldOreRubbleSet1Chunk3Split1Object), typeof(GoldOreRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class GoldOreRubbleSet1Chunk3Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet1Chunk3Split1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet1Chunk3Split2Object : RubbleObject<GoldOreItem> { }

    [Serialized] public partial class GoldOreRubbleSet2Chunk1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet2Chunk2Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet2Chunk3Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet2Chunk4Object : RubbleObject<GoldOreItem> { }

    [Serialized] public partial class GoldOreRubbleSet3Chunk1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet3Chunk2Object : RubbleObject<GoldOreItem> { }
    [BecomesRubble(typeof(GoldOreRubbleSet3Chunk3Split1Object), typeof(GoldOreRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class GoldOreRubbleSet3Chunk3Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet3Chunk3Split1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet3Chunk3Split2Object : RubbleObject<GoldOreItem> { }

    [BecomesRubble(typeof(GoldOreRubbleSet4Chunk1Split1Object), typeof(GoldOreRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class GoldOreRubbleSet4Chunk1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet4Chunk1Split1Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet4Chunk1Split2Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet4Chunk2Object : RubbleObject<GoldOreItem> { }
    [Serialized] public partial class GoldOreRubbleSet4Chunk3Object : RubbleObject<GoldOreItem> { }
}