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
    [Solid, Wall, Minable(6)]
    public partial class BasaltBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(BasaltItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(7000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class BasaltItem :
    BlockItem<BasaltBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Basalt"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Basalt"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A hard and heavy rock with some uses in construction. Basalt forms directly from lava erupted volcanically, making it an extrusive igneous rock. The basis of all bedrock in the oceans."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(BasaltStacked1Block),
            typeof(BasaltStacked2Block),
            typeof(BasaltStacked3Block),
            typeof(BasaltStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class BasaltStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class BasaltStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class BasaltStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class BasaltStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Basalt
}