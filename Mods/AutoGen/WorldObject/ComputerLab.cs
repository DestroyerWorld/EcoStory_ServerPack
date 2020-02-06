namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    

    [Serialized]
    public partial class ComputerLabItem :
        WorldObjectItem<ComputerLabObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Computer Lab"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A place where you can sit all day and play video games! Or work on expert-level research."); } }

        static ComputerLabItem()
        {
            
        }

        
    }

    [RequiresSkill(typeof(ElectronicsSkill), 0)]      
    public partial class ComputerLabRecipe : Recipe
    {
        public ComputerLabRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ComputerLabItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 100, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<CircuitItem>(typeof(ElectronicsSkill), 60, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<ConcreteItem>(typeof(ElectronicsSkill), 80, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 40;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ComputerLabRecipe), Item.Get<ComputerLabItem>().UILink(), 260, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Computer Lab"), typeof(ComputerLabRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}