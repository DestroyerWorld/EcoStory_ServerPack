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
    [Weight(0)]      
    [Category("Hidden")]    
    [Currency]              
    public partial class EckoTheDolphinItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ecko The Dolphin"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Ecko, the dolphin god of Eco bugs. Handle with care and lightly sprinkle with water every so often to keep moist."); } }
    }
}