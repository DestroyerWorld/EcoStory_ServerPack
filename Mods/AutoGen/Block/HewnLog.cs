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

	[RequiresSkill(typeof(HewingSkill), 1)]
    public partial class HewnLogRecipe : Recipe
    {
        public HewnLogRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(),          
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(2)    
            };
            this.CraftMinutes = new ConstantValue(0.2f);
            this.Initialize(Localizer.DoStr("Hewn Log"), typeof(HewnLogRecipe));

            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [Solid, Wall, Constructed,BuildRoomMaterialOption]
    [Tier(1)]                                          
    public partial class HewnLogBlock :
        Block            
        , IRepresentsItem   
    {
        public Type RepresentedItemType { get { return typeof(HewnLogItem); } }  
    }

    [Serialized]
    [MaxStackSize(15)]                           
    [Weight(10000)]      
    [ResourcePile]                                          
    [Currency]              
    [ItemTier(1)]                                      
    public partial class HewnLogItem :
    BlockItem<HewnLogBlock>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Hewn Log"); } } 
        public override LocString DisplayDescription { get { return Localizer.DoStr("A log hewn and shaped to be a building material. Hewing logs at a workbench will grant no experience."); } }

        public override bool CanStickToWalls { get { return false; } }  

        private static Type[] blockTypes = new Type[] {
            typeof(HewnLogStacked1Block),
            typeof(HewnLogStacked2Block),
            typeof(HewnLogStacked3Block)
        };
        public override Type[] BlockTypes { get { return blockTypes; } }
    }

    [Serialized, Solid] public class HewnLogStacked1Block : PickupableBlock { }
    [Serialized, Solid] public class HewnLogStacked2Block : PickupableBlock { }
    [Serialized, Solid,Wall] public class HewnLogStacked3Block : PickupableBlock { } //Only a wall if it's all 4 HewnLog
}