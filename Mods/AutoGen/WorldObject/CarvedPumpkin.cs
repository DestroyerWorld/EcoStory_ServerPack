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
    [RequireComponent(typeof(HousingComponent))]                  
    public partial class CarvedPumpkinObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carved Pumpkin"); } } 

        public virtual Type RepresentedItemType { get { return typeof(CarvedPumpkinItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Misc"));                
            this.GetComponent<HousingComponent>().Set(CarvedPumpkinItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    [Category("Hidden")] 
	[RequiresSkill(typeof(MortaringSkill), 1)]
    public partial class CarvedPumpkinItem :
        WorldObjectItem<CarvedPumpkinObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Carved Pumpkin"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Spooky pumpkin that emits a mystical light."); } }

        static CarvedPumpkinItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "General",
                                                    Val = 1,                                   
                                                    TypeForRoomLimit = "Decoration", 
                                                    DiminishingReturnPercent = 0.8f    
        };}}
        
    }

    public partial class CarvedPumpkinRecipe : Recipe
    {
        public CarvedPumpkinRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CarvedPumpkinItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<TallowCandleItem>(1),
                new CraftingElement<PumpkinItem>(1)                                                                     
            };
            this.CraftMinutes = new ConstantValue(10);
            this.Initialize(Localizer.DoStr("Carved Pumpkin"), typeof(CarvedPumpkinRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }
}