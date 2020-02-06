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
    [Weight(5000)]  
    public class WheelbarrowItem : WorldObjectItem<WheelbarrowObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Wheelbarrow"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Small wheelbarrow for hauling minimal loads."); } }
    }

    [RequiresSkill(typeof(HewingSkill), 1)] 
    public class WheelbarrowRecipe : Recipe
    {
        public WheelbarrowRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WheelbarrowItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<HewnLogItem>(typeof(HewingSkill), 10, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent)),
                new CraftingElement<BoardItem>(typeof(HewingSkill), 15, HewingSkill.MultiplicativeStrategy, typeof(HewingLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WheelbarrowRecipe), Item.Get<WheelbarrowItem>().UILink(), 5, typeof(HewingSkill), typeof(HewingFocusedSpeedTalent), typeof(HewingParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Wheelbarrow"), typeof(WheelbarrowRecipe));
            CraftingComponent.AddRecipe(typeof(WorkbenchObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]      
    [RequireComponent(typeof(MovableLinkComponent))]        
    [RequireComponent(typeof(VehicleComponent))]
    [RequireComponent(typeof(TailingsReportComponent))]     
    public partial class WheelbarrowObject : PhysicsWorldObject, IRepresentsItem
    {
        static WheelbarrowObject()
        {
            WorldObject.AddOccupancy<WheelbarrowObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Wheelbarrow"); } }
        public Type RepresentedItemType { get { return typeof(WheelbarrowItem); } }


        private WheelbarrowObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<PublicStorageComponent>().Initialize(8, 1400000);           
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(0.5f);           
        }
    }
}