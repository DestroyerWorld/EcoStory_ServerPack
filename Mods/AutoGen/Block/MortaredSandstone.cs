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
    public partial class MortaredSandstoneRecipe : Recipe
    {
        public MortaredSandstoneRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MortaredSandstoneItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SandstoneItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(MortaringSkill), 5, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MortaredSandstoneRecipe), Item.Get<MortaredSandstoneItem>().UILink(), 0.3f, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mortared Sandstone"), typeof(MortaredSandstoneRecipe));

            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(1)]                                          
    [RequiresSkill(typeof(MortaringSkill), 0)]   
    public partial class MortaredSandstoneBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(MortaredSandstoneItem); } }  
    }

    [Serialized]
    [MaxStackSize(15)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(1)]                                      
    public partial class MortaredSandstoneItem :
    BlockItem<MortaredSandstoneBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortared Sandstone"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to create tough but rudimentary buildings."); } }


        private static Type[] blockTypes = new Type[] {
            typeof(MortaredSandstoneStacked1Block),
            typeof(MortaredSandstoneStacked2Block),
            typeof(MortaredSandstoneStacked3Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class MortaredSandstoneStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class MortaredSandstoneStacked2Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class MortaredSandstoneStacked3Block : PickupableBlock { } //Only a wall if it's all 4 MortaredSandstone
}