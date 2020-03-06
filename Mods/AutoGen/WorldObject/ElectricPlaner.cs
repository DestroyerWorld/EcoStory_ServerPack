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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(45)]                          
    [RequireRoomMaterialTier(2.7f, typeof(IndustryLavishReqTalent), typeof(IndustryFrugalReqTalent))]   
    public partial class ElectricPlanerObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electric Planer"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElectricPlanerItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                
            this.GetComponent<PowerConsumptionComponent>().Initialize(100);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ElectricPlanerItem :
        WorldObjectItem<ElectricPlanerObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electric Planer"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr(""); } }

        static ElectricPlanerItem()
        {
            
        }

        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }

    [RequiresModule(typeof(ElectricLatheObject))]           
    [RequiresSkill(typeof(IndustrySkill), 1)]      
    public partial class ElectricPlanerRecipe : Recipe
    {
        public ElectricPlanerRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectricPlanerItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<RivetItem>(typeof(IndustrySkill), 30, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 20;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElectricPlanerRecipe), Item.Get<ElectricPlanerItem>().UILink(), 20, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Electric Planer"), typeof(ElectricPlanerRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}