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
    using Eco.Gameplay.Pipes.LiquidComponents; 

    [RequiresSkill(typeof(SmeltingSkill), 1)]      
    public partial class CopperPipeRecipe : Recipe
    {
        public CopperPipeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CopperPipeItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<CopperIngotItem>(typeof(SmeltingSkill), 4, SmeltingSkill.MultiplicativeStrategy, typeof(SmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CopperPipeRecipe), Item.Get<CopperPipeItem>().UILink(), 2, typeof(SmeltingSkill), typeof(SmeltingFocusedSpeedTalent), typeof(SmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Copper Pipe"), typeof(CopperPipeRecipe));

            CraftingComponent.AddRecipe(typeof(AnvilObject), this);
        }
    }

    [Serialized]
    [Solid, Constructed]
    [Tier(2)]                                          
    [DoesntEncase]                                          
    [RequiresSkill(typeof(SmeltingSkill), 1)]   
    public partial class CopperPipeBlock :
        PipeBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(CopperPipeItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(2000)]      
    [Currency]              
    [ItemTier(2)]                                      
    public partial class CopperPipeItem :
    BlockItem<CopperPipeBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Copper Pipe"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A pipe for transporting liquids."); } }

    }

}