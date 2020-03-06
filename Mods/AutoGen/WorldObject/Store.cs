namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using Eco.Gameplay.Blocks;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Housing;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Minimap;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes.Gases;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Shared;
    using Eco.Shared.Math;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    using Eco.Shared.Items;
    using Eco.Gameplay.Pipes;
    using Eco.World.Blocks;
    
    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(LinkComponent))]                   
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.8f)]   
    public partial class StoreObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Store"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StoreItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Economy"));                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class StoreItem :
        WorldObjectItem<StoreObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Store"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Allows the selling and trading of items."); } }

        static StoreItem()
        {
            
        }

        
    }
	[RequiresSkill(typeof(HewingSkill), 1)]
    public partial class StoreRecipe : Recipe
    {
        public StoreRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StoreItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10),
                new CraftingElement<StoneItem>(10)                                                                     
            };
            this.CraftMinutes = new ConstantValue(15);
            this.Initialize(Localizer.DoStr("Store"), typeof(StoreRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}