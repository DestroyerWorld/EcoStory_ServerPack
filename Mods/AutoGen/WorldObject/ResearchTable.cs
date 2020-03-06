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
    [RequireComponent(typeof(CraftingComponent))]               
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(0.5f)]   
    public partial class ResearchTableObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Research Table"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ResearchTableItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Research"));                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ResearchTableItem :
        WorldObjectItem<ResearchTableObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Research Table"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A basic table for researching new technologies and skills."); } }

        static ResearchTableItem()
        {
            
        }

        
    }
	
    public partial class ResearchTableRecipe : Recipe
    {
        public ResearchTableRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ResearchTableItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LogItem>(30),
                new CraftingElement<StoneItem>(40),
                new CraftingElement<PlantFibersItem>(30)                                                                     
            };
            this.CraftMinutes = new ConstantValue(5);
            this.Initialize(Localizer.DoStr("Research Table"), typeof(ResearchTableRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}