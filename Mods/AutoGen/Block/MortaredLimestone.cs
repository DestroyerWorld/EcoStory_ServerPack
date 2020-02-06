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

    [RequiresSkill(typeof(MortaringSkill), 3)]      
    public partial class MortaredLimestoneRecipe : Recipe
    {
        public MortaredLimestoneRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MortaredLimestoneItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LimestoneItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(MortaringSkill), 5, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MortaredLimestoneRecipe), Item.Get<MortaredLimestoneItem>().UILink(), 0.3f, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mortared Limestone"), typeof(MortaredLimestoneRecipe));

            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(1)]                                          
    [RequiresSkill(typeof(MortaringSkill), 3)]   
    public partial class MortaredLimestoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(MortaredLimestoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(15)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(1)]                                      
    public partial class MortaredLimestoneItem :
    BlockItem<MortaredLimestoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortared Limestone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to create tough but rudimentary buildings."); } }


        private static Type[] blockTypes = new Type[] {
            typeof(MortaredLimestoneStacked1Block),
            typeof(MortaredLimestoneStacked2Block),
            typeof(MortaredLimestoneStacked3Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class MortaredLimestoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class MortaredLimestoneStacked2Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class MortaredLimestoneStacked3Block : PickupableBlock { } //Only a wall if it's all 4 MortaredLimestone
}