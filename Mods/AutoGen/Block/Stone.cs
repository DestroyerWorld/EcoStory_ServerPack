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
    public partial class StoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(StoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class StoneItem :
    BlockItem<StoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Stone"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Stone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Old stone"); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(StoneStacked1Block),
            typeof(StoneStacked2Block),
            typeof(StoneStacked3Block),
            typeof(StoneStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class StoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class StoneStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class StoneStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class StoneStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Stone
}