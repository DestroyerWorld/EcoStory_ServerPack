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

    [RequiresSkill(typeof(MechanicsSkill), 1)]      
    public partial class ValveRecipe : Recipe
    {
        public ValveRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ValveItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelPipeItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<SteelGearItem>(typeof(MechanicsSkill), 18, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<SteelPlateItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 4;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(ValveRecipe), Item.Get<ValveItem>().UILink(), 8, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Valve"), typeof(ValveRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class ValveItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Valve"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A device that regulates, directs, or controls the flow of fluid."); } }
    }
}