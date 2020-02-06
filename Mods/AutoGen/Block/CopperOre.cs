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
    [Minable(2), Solid,Wall]
    public partial class CopperOreBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(CopperOreItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(30000)]      
    [ResourcePile]                                          
    [Currency]              
    public partial class CopperOreItem :
    BlockItem<CopperOreBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Copper Ore"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Copper Ore"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Unrefined ore with traces of copper."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(CopperOreStacked1Block),
            typeof(CopperOreStacked2Block),
            typeof(CopperOreStacked3Block),
            typeof(CopperOreStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CopperOreStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CopperOreStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CopperOreStacked4Block : PickupableBlock { } //Only a wall if it's all 4 CopperOre
}