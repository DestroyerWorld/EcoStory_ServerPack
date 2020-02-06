namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System;
    using System.Collections.Generic;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Components.VehicleModules;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    
    [Serialized]
    [Weight(15000)]  
    public class WoodCartItem : WorldObjectItem<WoodCartObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Wood Cart"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small cart for hauling small loads."); } }
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)] 
    public class WoodCartRecipe : Recipe
    {
        public WoodCartRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodCartItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(2), 
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 30, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 50, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodCartRecipe), Item.Get<WoodCartItem>().UILink(), 5, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Wood Cart"), typeof(WoodCartRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(ModularStockpileComponent))]   
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class WoodCartObject : PhysicsWorldObject, IRepresentsItem
    {
        static WoodCartObject()
        {
            WorldObject.AddOccupancy<WoodCartObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Wood Cart"); } }
        public Type RepresentedItemType { get { return typeof(WoodCartItem); } }


        private WoodCartObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(12, 2000000);           
            this.GetComponent<VehicleComponent>().Initialize(12, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
            this.GetComponent<StockpileComponent>().Initialize(new Vector3i(2,1,2));  
        }
    }
}