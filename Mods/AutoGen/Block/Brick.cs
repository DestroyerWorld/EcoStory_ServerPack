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

    [RequiresSkill(typeof(BricklayingSkill), 0)]      
    public partial class BrickRecipe : Recipe
    {
        public BrickRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<BrickItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ClayItem>(typeof(BricklayingSkill), 5, BricklayingSkill.MultiplicativeStrategy, typeof(BricklayingLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(BricklayingSkill), 8, BricklayingSkill.MultiplicativeStrategy, typeof(BricklayingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(BrickRecipe), Item.Get<BrickItem>().UILink(), 1, typeof(BricklayingSkill), typeof(BricklayingFocusedSpeedTalent), typeof(BricklayingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Brick"), typeof(BrickRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(2)]                                          
    [RequiresSkill(typeof(BricklayingSkill), 0)]   
    public partial class BrickBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(BrickItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(2)]                                      
    public partial class BrickItem :
    BlockItem<BrickBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Brick"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Durable building material made from fired blocks and mortar."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(BrickStacked1Block),
            typeof(BrickStacked2Block),
            typeof(BrickStacked3Block),
            typeof(BrickStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class BrickStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class BrickStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class BrickStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class BrickStacked4Block : PickupableBlock { } //Only a wall if it's all 4 Brick
}