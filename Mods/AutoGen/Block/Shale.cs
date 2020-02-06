namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Gameplay.Pipes;


    [Serialized]
    [Solid, Wall, Cliff, Minable(1)]
    public partial class ShaleBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(ShaleItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class ShaleItem :
    BlockItem<ShaleBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Shale"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Shale"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A soft rock, with few potential uses. Shale is a sedimentary rock formed by thin layers of compacted clay or mud."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(ShaleStacked1Block),
            typeof(ShaleStacked2Block),
            typeof(ShaleStacked3Block),
            typeof(ShaleStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class ShaleStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class ShaleStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class ShaleStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class ShaleStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Shale
}