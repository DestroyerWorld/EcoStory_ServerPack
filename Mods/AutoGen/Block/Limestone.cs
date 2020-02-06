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
    [Solid, Wall, Cliff, Minable(2)]
    public partial class LimestoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(LimestoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class LimestoneItem :
    BlockItem<LimestoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Limestone"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Limestone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hard rock useful for construction and industrial processes. Limestone is sedimentary, forming mostly from the fallen compacted remains of marine organisms."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(LimestoneStacked1Block),
            typeof(LimestoneStacked2Block),
            typeof(LimestoneStacked3Block),
            typeof(LimestoneStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class LimestoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class LimestoneStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class LimestoneStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class LimestoneStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Limestone
}