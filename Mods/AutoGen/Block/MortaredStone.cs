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

    [RequiresSkill(typeof(MortaringSkill), 1)]      
    public partial class MortaredStoneRecipe : Recipe
    {
        public MortaredStoneRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MortaredStoneItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<StoneItem>(typeof(MortaringSkill), 14, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(MortaringSkill), 5, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MortaredStoneRecipe), Item.Get<MortaredStoneItem>().UILink(), 0.3f, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mortared Stone"), typeof(MortaredStoneRecipe));

            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(1)]                                          
    [RequiresSkill(typeof(MortaringSkill), 0)]   
    public partial class MortaredStoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(MortaredStoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(15)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(1)]                                      
    public partial class MortaredStoneItem :
    BlockItem<MortaredStoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortared Stone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Old stone"); } }


        private static Type[] blockTypes = new Type[] {
            typeof(MortaredStoneStacked1Block),
            typeof(MortaredStoneStacked2Block),
            typeof(MortaredStoneStacked3Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class MortaredStoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class MortaredStoneStacked2Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class MortaredStoneStacked3Block : PickupableBlock { } //Only a wall if it's all 4 MortaredStone
}