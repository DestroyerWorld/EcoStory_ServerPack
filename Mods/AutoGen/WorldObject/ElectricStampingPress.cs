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
    [RequireRoomMaterialTier(2.7f, typeof(MechanicsLavishReqTalent), typeof(MechanicsFrugalReqTalent))]   
    public partial class ElectricStampingPressObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electric Stamping Press"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElectricStampingPressItem); } } 



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
    public partial class ElectricStampingPressItem :
        WorldObjectItem<ElectricStampingPressObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electric Stamping Press"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr(""); } }

        static ElectricStampingPressItem()
        {
            
        }

        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(100))); } }  
    }

    [RequiresSkill(typeof(IndustrySkill), 1)]      
    public partial class ElectricStampingPressRecipe : Recipe
    {
        public ElectricStampingPressRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectricStampingPressItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 20;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElectricStampingPressRecipe), Item.Get<ElectricStampingPressItem>().UILink(), 20, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Electric Stamping Press"), typeof(ElectricStampingPressRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}