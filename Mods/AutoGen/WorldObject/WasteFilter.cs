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
    [RequireComponent(typeof(PowerConsumptionComponent))]                     
    [RequireComponent(typeof(SolidGroundComponent))]            
    public partial class WasteFilterObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waste Filter"); } } 

        public virtual Type RepresentedItemType { get { return typeof(WasteFilterItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Misc"));                
            this.GetComponent<PowerConsumptionComponent>().Initialize(250);                      
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());        

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class WasteFilterItem :
        WorldObjectItem<WasteFilterObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Waste Filter"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Treats raw sewage."); } }

        static WasteFilterItem()
        {
            
        }

        
        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Consumes: {0}w"), Text.Info(250))); } }  
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)]      
    public partial class WasteFilterRecipe : Recipe
    {
        public WasteFilterRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WasteFilterItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<ConcreteItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<PistonItem>(typeof(MechanicsSkill), 5, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 25;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(WasteFilterRecipe), Item.Get<WasteFilterItem>().UILink(), 50, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Waste Filter"), typeof(WasteFilterRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }
}