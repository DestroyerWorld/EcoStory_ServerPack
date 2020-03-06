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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 1)]      
    public partial class CorrugatedSteelRecipe : Recipe
    {
        public CorrugatedSteelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CorrugatedSteelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 4, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(CorrugatedSteelRecipe), Item.Get<CorrugatedSteelItem>().UILink(), 2, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Corrugated Steel"), typeof(CorrugatedSteelRecipe));

            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed]
    [Tier(3)]                                          
    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]   
    public partial class CorrugatedSteelBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(CorrugatedSteelItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(3)]                                      
    public partial class CorrugatedSteelItem :
    BlockItem<CorrugatedSteelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Corrugated Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Especially useful for industrial buildings."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(CorrugatedSteelStacked1Block),
            typeof(CorrugatedSteelStacked2Block),
            typeof(CorrugatedSteelStacked3Block),
            typeof(CorrugatedSteelStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class CorrugatedSteelStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class CorrugatedSteelStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class CorrugatedSteelStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class CorrugatedSteelStacked4Block : PickupableBlock { } //Only a wall if it's all 4 CorrugatedSteel
}