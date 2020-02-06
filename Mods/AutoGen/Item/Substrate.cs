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
    public partial class SubstrateRecipe : Recipe
    {
        public SubstrateRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SubstrateItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<FiberglassItem>(typeof(ElectronicsSkill), 8, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<EpoxyItem>(typeof(ElectronicsSkill), 8, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(SubstrateRecipe), Item.Get<SubstrateItem>().UILink(), 5, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Substrate"), typeof(SubstrateRecipe));

            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class SubstrateItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Substrate"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("The foundation material for complex electronics."); } }
    }
}