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
    [Solid, Wall, Constructed]
    public partial class LogBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(LogItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Fuel(4000)][Tag("Fuel")]          
    [ResourcePile]                                          
    [Currency]              
    public partial class LogItem :
    BlockItem<LogBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Log"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A stack of logs."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(LogStacked1Block),
            typeof(LogStacked2Block),
            typeof(LogStacked3Block),
            typeof(LogStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class LogStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class LogStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class LogStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class LogStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Log
}