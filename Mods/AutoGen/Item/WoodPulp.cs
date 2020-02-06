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
    [Weight(10)]      
    [Currency]              
    public partial class WoodPulpItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wood Pulp"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Wood Pulp"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A byproduct of processing lumber, wood pulp can be burned for Mortar or pressed into paper."); } }
    }
}