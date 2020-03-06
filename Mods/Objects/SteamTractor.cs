namespace Eco.Mods.TechTree
{
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Skills;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Systems.TextLinks;

    [Serialized]
    [Weight(25000)]  
    public class SteamTractorItem : WorldObjectItem<SteamTractorObject>
    {
        public override LocString DisplayName         { get { return Localizer.DoStr("Steam Tractor"); } }
        public override LocString DisplayDescription          { get { return Localizer.DoStr("A tractor powered through steam."); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)] 
    public class SteamTractorRecipe : Recipe
    {
        public SteamTractorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<SteamTractorItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(1),
                new CraftingElement<IronWheelItem>(4),
                new CraftingElement<IronAxleItem>(1), 
                new CraftingElement<IronPlateItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<IronPipeItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<ScrewsItem>(typeof(MechanicsSkill), 40, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy),
                new CraftingElement<LeatherHideItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy),
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(SteamTractorRecipe), Item.Get<SteamTractorItem>().UILink(), 25, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));

            this.Initialize(Localizer.DoStr("Steam Tractor"), typeof(SteamTractorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]              
    [RequireComponent(typeof(FuelConsumptionComponent))]         
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    [RequireComponent(typeof(ModularVehicleComponent))]     
    public partial class SteamTractorObject : PhysicsWorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Steam Tractor"); } }

        static SteamTractorObject()
        {
            WorldObject.AddOccupancy<SteamTractorObject>(new List<BlockOccupancy>(0));
        }

        private static Type[] fuelTypeList = new Type[]
        {
            typeof(LogItem),
            typeof(LumberItem),
            typeof(CharcoalItem),
            typeof(ArrowItem),
            typeof(BoardItem),
            typeof(CoalItem),
        };

        private static Type[] segmentTypeList = new Type[] { };
        private static Type[] attachmentTypeList = new Type[]
        {
            typeof(SteamTractorPloughItem), typeof(SteamTractorHarvesterItem), typeof(SteamTractorSowerItem)
        };

        private SteamTractorObject() { }
        
        private LocString ControlHints = Localizer.DoStr("Q - use module");

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(12, 2500000);           
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);           
            this.GetComponent<FuelConsumptionComponent>().Initialize(25);    
            this.GetComponent<VehicleComponent>().Initialize(15, 1, 2, ControlHints);
            this.GetComponent<ModularVehicleComponent>().Initialize(0, 1, segmentTypeList, attachmentTypeList);
        }
    }
}
