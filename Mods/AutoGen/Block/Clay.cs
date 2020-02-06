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
    [Solid, Wall, Diggable]
    public partial class ClayBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(ClayItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(7000)]      
    [Currency]              
    public partial class ClayItem :
    BlockItem<ClayBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Clay"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Clay"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A very fine grained deposit of weathered bits of rock. Plastic when wet and brittle when dry, clay is impermeable to many liquids and useful for many industrial and environmental purposes."); } }

    }

}