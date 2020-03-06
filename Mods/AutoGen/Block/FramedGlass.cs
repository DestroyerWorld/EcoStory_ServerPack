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

    [RequiresSkill(typeof(GlassworkingSkill), 1)]      
    public partial class FramedGlassRecipe : Recipe
    {
        public FramedGlassRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<FramedGlassItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GlassItem>(typeof(GlassworkingSkill), 15, GlassworkingSkill.MultiplicativeStrategy, typeof(GlassworkingLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(GlassworkingSkill), 8, GlassworkingSkill.MultiplicativeStrategy, typeof(GlassworkingLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(FramedGlassRecipe), Item.Get<FramedGlassItem>().UILink(), 5, typeof(GlassworkingSkill), typeof(GlassworkingFocusedSpeedTalent), typeof(GlassworkingParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Framed Glass"), typeof(FramedGlassRecipe));

            CraftingComponent.AddRecipe(typeof(KilnObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(4)]                                          
    [RequiresSkill(typeof(GlassworkingSkill), 0)]   
    public partial class FramedGlassBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(FramedGlassItem); } }  
    }

    [Serialized]
    [MaxStackSize(20)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(4)]                                      
    public partial class FramedGlassItem :
    BlockItem<FramedGlassBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Framed Glass"); } } 
        public override LocString DisplayNamePlural { get { return Localizer.DoStr("Framed Glass"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A transparent, solid material useful for more than just windows."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(FramedGlassStacked1Block),
            typeof(FramedGlassStacked2Block),
            typeof(FramedGlassStacked3Block),
            typeof(FramedGlassStacked4Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class FramedGlassStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class FramedGlassStacked2Block : PickupableBlock { }
    [Serialized, Solid] public class FramedGlassStacked3Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class FramedGlassStacked4Block : PickupableBlock { } //Only a wall if it's all 4 FramedGlass
}