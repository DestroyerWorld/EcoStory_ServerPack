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

    [RequiresSkill(typeof(ElectronicsSkill), 0)]      
    public partial class CircuitRecipe : Recipe
    {
        public CircuitRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CircuitItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SubstrateItem>(typeof(ElectronicsSkill), 6, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<CopperWiringItem>(typeof(ElectronicsSkill), 14, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<GoldFlakesItem>(typeof(ElectronicsSkill), 25, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 4;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(CircuitRecipe), Item.Get<CircuitItem>().UILink(), 2, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Circuit"), typeof(CircuitRecipe));

            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class CircuitItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Circuit"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A complex electrical component used in advanced electronics."); } }
    }
}