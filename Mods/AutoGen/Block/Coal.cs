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
    [Minable(1), Solid,Wall]
    public partial class CoalBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(CoalItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(30000)]      
    [Fuel(20000)][Tag("Fuel")]          
    [ResourcePile]                                          
    [Currency]              
    public partial class CoalItem :
    BlockItem<CoalBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Coal"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Coal"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A combustible mineral which when used as a fuel provides lots of energy but generates lots of pollution."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(CoalStacked1Block),
            typeof(CoalStacked2Block),
            typeof(CoalStacked3Block),
            typeof(CoalStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CoalStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CoalStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CoalStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CoalStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Coal
}