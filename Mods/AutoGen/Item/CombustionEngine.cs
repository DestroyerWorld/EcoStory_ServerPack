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
    public partial class CombustionEngineRecipe : Recipe
    {
        public CombustionEngineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CombustionEngineItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 16, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<SteelPlateItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<RivetItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 15;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(CombustionEngineRecipe), Item.Get<CombustionEngineItem>().UILink(), 20, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Combustion Engine"), typeof(CombustionEngineRecipe));

            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }

    [Serialized]
    [Weight(5000)]      
    [Currency]              
    public partial class CombustionEngineItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Combustion Engine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("An engine that generates power by combustion of fuel."); } }
    }
}