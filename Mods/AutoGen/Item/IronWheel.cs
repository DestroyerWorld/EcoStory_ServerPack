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
    public partial class IronWheelRecipe : Recipe
    {
        public IronWheelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<IronWheelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<IronIngotItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(IronWheelRecipe), Item.Get<IronWheelItem>().UILink(), 5, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Iron Wheel"), typeof(IronWheelRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Currency]              
    public partial class IronWheelItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Iron Wheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }
    }
}