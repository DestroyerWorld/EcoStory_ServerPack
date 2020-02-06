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
    [Yield(typeof(PlantFibersItem), typeof(GatheringSkill), new float[] { 1f, 1.4f, 1.5f, 1.6f, 1.7f, 1.8f, 1.9f, 2.0f })]  
    [Currency]              
    public partial class PlantFibersItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Plant Fibers"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Harvested from a number of plants, these fibers are useful for a suprising number of things."); } }
    }
}