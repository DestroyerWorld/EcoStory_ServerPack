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

    [RequiresModule(typeof(MachinistTableObject))]        
    [RequiresSkill(typeof(MechanicsSkill), 1)]      
    public partial class HeatSinkRecipe : Recipe
    {
        public HeatSinkRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HeatSinkItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(HeatSinkRecipe), Item.Get<HeatSinkItem>().UILink(), 5, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Heat Sink"), typeof(HeatSinkRecipe));

            CraftingComponent.AddRecipe(typeof(ShaperObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class HeatSinkItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Heat Sink"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A copper plate to draw and disperse heat."); } }
    }
}