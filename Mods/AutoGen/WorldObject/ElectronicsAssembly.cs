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
    [RequireComponent(typeof(HousingComponent))]                  
    [RequireComponent(typeof(SolidGroundComponent))]            
    [RequireComponent(typeof(RoomRequirementsComponent))]
    [RequireRoomContainment]
    [RequireRoomVolume(25)]                          
    [RequireRoomMaterialTier(1.8f, typeof(ElectronicsLavishReqTalent), typeof(ElectronicsFrugalReqTalent))]   
    public partial class ElectronicsAssemblyObject : 
        WorldObject,    
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 

        public virtual Type RepresentedItemType { get { return typeof(ElectronicsAssemblyItem); } } 



        protected override void Initialize()
        {

            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));                
            this.GetComponent<HousingComponent>().Set(ElectronicsAssemblyItem.HousingVal);                                

        }

        public override void Destroy()
        {
            base.Destroy();
        }
       
    }

    [Serialized]
    public partial class ElectronicsAssemblyItem :
        WorldObjectItem<ElectronicsAssemblyObject> 
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Electronics Assembly"); } } 
        public override LocString DisplayDescription  { get { return Localizer.DoStr("A set of machinery to create electronics."); } }

        static ElectronicsAssemblyItem()
        {
            
        }

        [TooltipChildren] public HousingValue HousingTooltip { get { return HousingVal; } }
        [TooltipChildren] public static HousingValue HousingVal { get { return new HousingValue() 
                                                {
                                                    Category = "Industrial",
                                                    TypeForRoomLimit = "", 
        };}}
        
    }

    [RequiresSkill(typeof(ElectronicsSkill), 1)]      
    public partial class ElectronicsAssemblyRecipe : Recipe
    {
        public ElectronicsAssemblyRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ElectronicsAssemblyItem>(),
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<SteelItem>(typeof(ElectronicsSkill), 30, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),
                new CraftingElement<RivetItem>(typeof(ElectronicsSkill), 20, ElectronicsSkill.MultiplicativeStrategy, typeof(ElectronicsLavishResourcesTalent)),          
            };
            this.ExperienceOnCraft = 20;  
            this.CraftMinutes = CreateCraftTimeValue(typeof(ElectronicsAssemblyRecipe), Item.Get<ElectronicsAssemblyItem>().UILink(), 60, typeof(ElectronicsSkill), typeof(ElectronicsFocusedSpeedTalent), typeof(ElectronicsParallelSpeedTalent));    
            this.Initialize(Localizer.DoStr("Electronics Assembly"), typeof(ElectronicsAssemblyRecipe));
            CraftingComponent.AddRecipe(typeof(ElectricMachinistTableObject), this);
        }
    }
}