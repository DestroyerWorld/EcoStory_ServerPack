namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Objects;
    using Eco.World;

    [BecomesRubble(typeof(IronOreRubbleSet1Chunk1Object), typeof(IronOreRubbleSet1Chunk2Object), typeof(IronOreRubbleSet1Chunk3Object))]
    [BecomesRubble(typeof(IronOreRubbleSet2Chunk1Object), typeof(IronOreRubbleSet2Chunk2Object), typeof(IronOreRubbleSet2Chunk3Object), typeof(IronOreRubbleSet2Chunk4Object))]
    [BecomesRubble(typeof(IronOreRubbleSet3Chunk1Object), typeof(IronOreRubbleSet3Chunk2Object), typeof(IronOreRubbleSet3Chunk3Object))]
    [BecomesRubble(typeof(IronOreRubbleSet4Chunk1Object), typeof(IronOreRubbleSet4Chunk2Object), typeof(IronOreRubbleSet4Chunk3Object))]
    public partial class IronOreBlock : Block { }

    [Serialized] public partial class IronOreRubbleSet1Chunk1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet1Chunk2Object : RubbleObject<IronOreItem> { }

    [BecomesRubble(typeof(IronOreRubbleSet1Chunk3Split1Object), typeof(IronOreRubbleSet1Chunk3Split2Object))]
    [Serialized] public partial class IronOreRubbleSet1Chunk3Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet1Chunk3Split1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet1Chunk3Split2Object : RubbleObject<IronOreItem> { }

    [Serialized] public partial class IronOreRubbleSet2Chunk1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet2Chunk2Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet2Chunk3Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet2Chunk4Object : RubbleObject<IronOreItem> { }

    [Serialized] public partial class IronOreRubbleSet3Chunk1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet3Chunk2Object : RubbleObject<IronOreItem> { }
    [BecomesRubble(typeof(IronOreRubbleSet3Chunk3Split1Object), typeof(IronOreRubbleSet3Chunk3Split2Object))]
    [Serialized] public partial class IronOreRubbleSet3Chunk3Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet3Chunk3Split1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet3Chunk3Split2Object : RubbleObject<IronOreItem> { }

    [BecomesRubble(typeof(IronOreRubbleSet4Chunk1Split1Object), typeof(IronOreRubbleSet4Chunk1Split2Object))]
    [Serialized] public partial class IronOreRubbleSet4Chunk1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet4Chunk1Split1Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet4Chunk1Split2Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet4Chunk2Object : RubbleObject<IronOreItem> { }
    [Serialized] public partial class IronOreRubbleSet4Chunk3Object : RubbleObject<IronOreItem> { }
}