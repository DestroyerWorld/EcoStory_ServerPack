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
    public partial class SandstoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(SandstoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [Currency]              
    public partial class SandstoneItem :
    BlockItem<SandstoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Sandstone"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Stone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A varying hardness rock useful for construction, and host to iron ore. Stone is a sedimentary rock that forms when sand is buried deep enough to lithify. Sometimes the forms of dunes and ripples from an ancient desert or beach are preserved!"); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(SandstoneStacked1Block),
            typeof(SandstoneStacked2Block),
            typeof(SandstoneStacked3Block),
            typeof(SandstoneStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class SandstoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class SandstoneStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class SandstoneStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class SandstoneStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Sandstone
}