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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]      
    public partial class SteelPipeRecipe : Recipe
    {
        public SteelPipeRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteelPipeItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 4, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteelPipeRecipe), Item.Get<SteelPipeItem>().UILink(), 4, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Steel Pipe"), typeof(SteelPipeRecipe));

            CraftingComponent.AddRecipe(typeof(BlastFurnaceObject), this);
        }
    }

    [Serialized]
    [Solid, Constructed]
    [Tier(3)]                                          
    [DoesntEncase]                                          
    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]   
    public partial class SteelPipeBlock :
        PipeBlock      
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(SteelPipeItem); } }  
    }

    [Serialized]
    [MaxStackSize(10)]                                       
    [Weight(2000)]      
    [Currency]              
    [ItemTier(3)]                                      
    public partial class SteelPipeItem :
    BlockItem<SteelPipeBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steel Pipe"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A pipe for transporting liquids."); } }

    }

}