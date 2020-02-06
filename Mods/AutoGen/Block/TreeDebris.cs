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
    [Occupied]
    public partial class TreeDebrisBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(TreeDebrisItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(500)]      
    [Fuel(100)][Tag("Fuel")]          
    [Currency]              
    public partial class TreeDebrisItem :
    BlockItem<TreeDebrisBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Tree Debris"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Clear cut debris that needs to be broken down to be more usable."); } }

    }

}