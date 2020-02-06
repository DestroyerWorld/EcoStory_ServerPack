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

    [RequiresSkill(typeof(CementSkill), 0)]      
    public partial class ConcreteRecipe : Recipe
    {
        public ConcreteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(CementSkill), 50, CementSkill.MultiplicativeStrategy, typeof(CementLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(ConcreteRecipe), Item.Get<ConcreteItem>().UILink(), 2, typeof(CementSkill), typeof(CementFocusedSpeedTalent), typeof(CementParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Concrete"), typeof(ConcreteRecipe));

            CraftingComponent.AddRecipe(typeof(CementKilnObject), this);
        }
    }

    [Serialized]
    [Weight(10000)]      
    [Currency]              
    public partial class ConcreteItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Concrete"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Concrete"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A material made from cement and an aggregate like crushed stone. In order to be usable it needs to be reinforced."); } }
    }
}