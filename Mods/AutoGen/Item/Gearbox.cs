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
    [RequiresSkill(typeof(MechanicsSkill), 0)]      
    public partial class GearboxRecipe : Recipe
    {
        public GearboxRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<GearboxItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 8, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<GearItem>(typeof(MechanicsSkill), 8, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(GearboxRecipe), Item.Get<GearboxItem>().UILink(), 5, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Gearbox"), typeof(GearboxRecipe));

            CraftingComponent.AddRecipe(typeof(ShaperObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class GearboxItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Gearbox"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Provides speed and torque conversions from a rotating power source to another device"); } }
    }
}