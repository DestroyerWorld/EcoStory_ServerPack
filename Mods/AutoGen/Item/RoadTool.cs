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

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]      
    public partial class RoadToolRecipe : Recipe
    {
        public RoadToolRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RoadToolItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 4, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  

            this.CraftMinutes = CreateCraftTimeValue(typeof(RoadToolRecipe), Item.Get<RoadToolItem>().UILink(), 5, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Road Tool"), typeof(RoadToolRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [Weight(1000)]      
    [Currency]              
    public partial class RoadToolItem :
    ToolItem                        
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Road Tool"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to press roads into dirt and stone rubble."); } }
    }
}