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
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class WindmillObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Windmill"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WindmillItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Power"));                
            this.GetComponent<PowerGridComponent>().Initialize(10, new MechanicalPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(200);                       
            this.GetComponent<HousingComponent>().Set(WindmillItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WindmillItem :
        WorldObjectItem<WindmillObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Windmill"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Use wind to produce mechanical power."); } }

        static WindmillItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(200))); } } 
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)]      
    public partial class WindmillRecipe : Recipe
    {
        public WindmillRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WindmillItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 40, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 8;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(WindmillRecipe), Item.Get<WindmillItem>().UILink(), 30, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Windmill"), typeof(WindmillRecipe));
            CraftingComponent.AddRecipe(typeof(CarpentryTableObject), this);
        }
    }
}