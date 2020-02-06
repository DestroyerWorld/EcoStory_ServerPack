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
    [RequireComponent(typeof(RepairComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class RepairStationObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Repair Station"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RepairStationItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class RepairStationItem :
        WorldObjectItem<RepairStationObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Repair Station"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A place to fix up broken tools."); } }

        static RepairStationItem()
        {
            
        }

        
    }

    public partial class RepairStationRecipe : Recipe
    {
        public RepairStationRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RepairStationItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(20),
                new CraftingElement<StoneItem>(10)                                                                     
            };
            this.CraftMinutes = new ConstantValue(1);
            this.Initialize(Localizer.DoStr("Repair Station"), typeof(RepairStationRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}