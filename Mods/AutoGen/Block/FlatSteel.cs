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

    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]      
    public partial class FlatSteelRecipe : Recipe
    {
        public FlatSteelRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FlatSteelItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(AdvancedSmeltingSkill), 6, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)),
                new CraftingElement<EpoxyItem>(typeof(AdvancedSmeltingSkill), 3, AdvancedSmeltingSkill.MultiplicativeStrategy, typeof(AdvancedSmeltingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 1;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(FlatSteelRecipe), Item.Get<FlatSteelItem>().UILink(), 5, typeof(AdvancedSmeltingSkill), typeof(AdvancedSmeltingFocusedSpeedTalent), typeof(AdvancedSmeltingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Flat Steel"), typeof(FlatSteelRecipe));

            CraftingComponent.AddRecipe(typeof(RollingMillObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(4)]                                          
    [RequiresSkill(typeof(AdvancedSmeltingSkill), 0)]   
    public partial class FlatSteelBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(FlatSteelItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(4)]                                      
    public partial class FlatSteelItem :
    BlockItem<FlatSteelBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Flat Steel"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Coated with a layer of epoxy, this steel refuses to rust."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(FlatSteelStacked1Block),
            typeof(FlatSteelStacked2Block),
            typeof(FlatSteelStacked3Block),
            typeof(FlatSteelStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class FlatSteelStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class FlatSteelStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class FlatSteelStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class FlatSteelStacked4Block : PickupableBlock { } //Only a wall if it's all 4 FlatSteel
}