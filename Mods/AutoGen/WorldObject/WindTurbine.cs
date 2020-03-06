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
    [RequireComponent(typeof(OnOffComponent))]                   
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]                
    [RequireComponent(typeof(PowerGridComponent))]              
    [RequireComponent(typeof(PowerGeneratorComponent))]         
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class WindTurbineObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wind Turbine"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WindTurbineItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Power"));                
            this.GetComponent<PowerGridComponent>().Initialize(30, new ElectricPower());        
            this.GetComponent<PowerGeneratorComponent>().Initialize(1000);                       
            this.GetComponent<HousingComponent>().Set(WindTurbineItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WindTurbineItem :
        WorldObjectItem<WindTurbineObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Wind Turbine"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Generates electrical power from the wind."); } }

        static WindTurbineItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
        [Tooltip(8)] private LocString PowerProductionTooltip  { get { return new LocString(string.Format(Localizer.DoStr("Produces: {0}w"), Text.Info(1000))); } } 
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]      
    public partial class WindTurbineRecipe : Recipe
    {
        public WindTurbineRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WindTurbineItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<CircuitItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 15;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(WindTurbineRecipe), Item.Get<WindTurbineItem>().UILink(), 50, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Wind Turbine"), typeof(WindTurbineRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }
}