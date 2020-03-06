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
    public partial class ServoRecipe : Recipe
    {
        public ServoRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ServoItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CircuitItem>(typeof(MechanicsSkill), 6, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<FiberglassItem>(typeof(MechanicsSkill), 12, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(ServoRecipe), Item.Get<ServoItem>().UILink(), 8, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Servo"), typeof(ServoRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class ServoItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Servo"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A rotary actuator that allows for control over angular position."); } }
    }
}