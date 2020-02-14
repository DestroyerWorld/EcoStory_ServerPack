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
    [RequireComponent(typeof(PublicStorageComponent))]                
    public partial class StorageChestObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Storage Chest"); } } 

        public virtual Type RepresentedItemType { get { return typeof(StorageChestItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Storage"));                
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(16);
            storage.Storage.AddInvRestriction(new NotCarriedRestriction()); // can't store block or large items

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class StorageChestItem :
        WorldObjectItem<StorageChestObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Storage Chest"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A container you can store items in."); } }

        static StorageChestItem()
        {
            
        }

        
    }
	[RequiresSkill(typeof(HewingSkill), 1)]
    public partial class StorageChestRecipe : Recipe
    {
        public StorageChestRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<StorageChestItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(10)                                                                     
            };
            this.CraftMinutes = new ConstantValue(2);
            this.Initialize(Localizer.DoStr("Storage Chest"), typeof(StorageChestRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}