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

    [RequiresSkill(typeof(HewingSkill), 1)]      
    public partial class BoardRecipe : Recipe
    {
        public BoardRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(2),  
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(typeof(HewingSkill), 6, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(BoardRecipe), Item.Get<BoardItem>().UILink(), 0.5f, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Board"), typeof(BoardRecipe));

            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }

    [Serialized]
    [Weight(500)]      
    [Fuel(2000)][Tag("Fuel")]          
    [Currency]              
    public partial class BoardItem :
    Item                                    
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Board"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Can be used in simple crafts, or used to create workbenches."); } }
    }
}