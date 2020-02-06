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

    [RequiresSkill(typeof(MortaringSkill), 2)]      
    public partial class MortaredGraniteRecipe : Recipe
    {
        public MortaredGraniteRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<MortaredGraniteItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<GraniteItem>(typeof(MortaringSkill), 10, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)),
                new CraftingElement<MortarItem>(typeof(MortaringSkill), 5, MortaringSkill.MultiplicativeStrategy, typeof(MortaringLavishResourcesTalent)), 
            };
            this.ExperienceOnCraft = 0.5f;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(MortaredGraniteRecipe), Item.Get<MortaredGraniteItem>().UILink(), 0.3f, typeof(MortaringSkill), typeof(MortaringFocusedSpeedTalent), typeof(MortaringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Mortared Granite"), typeof(MortaredGraniteRecipe));

            CraftingComponent.AddRecipe(typeof(MasonryTableObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(1)]                                          
    [RequiresSkill(typeof(MortaringSkill), 2)]   
    public partial class MortaredGraniteBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(MortaredGraniteItem); } }  
    }

    [Serialized]
    [MaxStackSize(15)]                           
    [Weight(10000)]      
    [Currency]              
    [ItemTier(1)]                                      
    public partial class MortaredGraniteItem :
    BlockItem<MortaredGraniteBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Mortared Granite"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("Used to create tough but rudimentary buildings."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(MortaredGraniteStacked1Block),
            typeof(MortaredGraniteStacked2Block),
            typeof(MortaredGraniteStacked3Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class MortaredGraniteStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class MortaredGraniteStacked2Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class MortaredGraniteStacked3Block : PickupableBlock { } //Only a wall if it's all 4 MortaredGranite
}