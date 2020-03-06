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
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(75)]                          
    [RequireRoomMaterialTier(2.8f, typeof(ElectronicsLavishReqTalent), typeof(ElectronicsFrugalReqTalent))]   
    public partial class RoboticAssemblyLineObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Robotic Assembly Line"); } } 

        public virtual Type RepresentedItemType { get { return typeof(RoboticAssemblyLineItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                
            this.GetComponent<PowerConsumptionComponent>().Initialize(1000);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        
            this.GetComponent<HousingComponent>().Set(RoboticAssemblyLineItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class RoboticAssemblyLineItem :
        WorldObjectItem<RoboticAssemblyLineObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Robotic Assembly Line"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A complex set of machinery for creating equally complex things."); } }

        static RoboticAssemblyLineItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(1000))); } }  
    }

    [RequiresSkill(typeof(ElectronicsSkill), 1)]      
    public partial class RoboticAssemblyLineRecipe : Recipe
    {
        public RoboticAssemblyLineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RoboticAssemblyLineItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(ElectronicsSkill), 70, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<ElectricMotorItem>(typeof(ElectronicsSkill), 12, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<ServoItem>(typeof(ElectronicsSkill), 28, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 60, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 30;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(RoboticAssemblyLineRecipe), Item.Get<RoboticAssemblyLineItem>().UILink(), 180, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Robotic Assembly Line"), typeof(RoboticAssemblyLineRecipe));
            CraftingComponent.AddRecipe(typeof(ElectronicsAssemblyObject), this);
        }
    }
}