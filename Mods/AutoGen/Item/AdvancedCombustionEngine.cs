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

    [RequiresSkill(typeof(IndustrySkill), 0)]      
    public partial class AdvancedCombustionEngineRecipe : Recipe
    {
        public AdvancedCombustionEngineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<PistonItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<ValveItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<ServoItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<CircuitItem>(typeof(IndustrySkill), 16, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<RadiatorItem>(typeof(IndustrySkill), 6, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 20;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(AdvancedCombustionEngineRecipe), Item.Get<AdvancedCombustionEngineItem>().UILink(), 10, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Advanced Combustion Engine"), typeof(AdvancedCombustionEngineRecipe));

            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class AdvancedCombustionEngineItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Advanced Combustion Engine"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A more advanced version of the normal combustion engine that produces a greater output."); } }
    }
}