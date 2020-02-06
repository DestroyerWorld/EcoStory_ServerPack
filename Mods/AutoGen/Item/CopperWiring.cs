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

    [RequiresSkill(typeof(MechanicsSkill), 0)]      
    public partial class CopperWiringRecipe : Recipe
    {
        public CopperWiringRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperWiringItem>(2),  
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(CopperWiringRecipe), Item.Get<CopperWiringItem>().UILink(), 1, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Copper Wiring"), typeof(CopperWiringRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(200)]      
    [Currency]              
    public partial class CopperWiringItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Copper Wiring"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Copper Wiring"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A length of conductive wire useful for a variety of purposes."); } }
    }
}