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

    [RequiresSkill(typeof(IndustrySkill), 1)]      
    public partial class RubberWheelRecipe : Recipe
    {
        public RubberWheelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RubberWheelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SyntheticRubberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 2;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(RubberWheelRecipe), Item.Get<RubberWheelItem>().UILink(), 5, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Rubber Wheel"), typeof(RubberWheelRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Weight(2000)]      
    [Currency]              
    public partial class RubberWheelItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Rubber Wheel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr(""); } }
    }
}