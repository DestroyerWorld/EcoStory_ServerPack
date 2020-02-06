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

    [RequiresSkill(typeof(BasicEngineeringSkill), 0)]      
    public partial class StoneRampRecipe : Recipe
    {
        public StoneRampRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoneRampItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(BasicEngineeringSkill), 30, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(BasicEngineeringSkill), 16, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(StoneRampRecipe), Item.Get<StoneRampItem>().UILink(), 1, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Stone Ramp"), typeof(StoneRampRecipe));

            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [Constructed]
    [Road(1.2f)]                                          
    [RequiresSkill(typeof(BasicEngineeringSkill), 0)]   
    public partial class StoneRampBlock :
        Block            
    {
    }

}