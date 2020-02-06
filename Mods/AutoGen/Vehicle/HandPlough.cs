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
    public class HandPloughItem : WorldObjectItem<HandPloughObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Hand Plough"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A tool that tills the field for farming."); } }
    }

    [RequiresSkill(typeof(BasicEngineeringSkill), 1)] 
    public class HandPloughRecipe : Recipe
    {
        public HandPloughRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<HandPloughItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<WoodenWheelItem>(1), 
                new CraftingElement<HewnLogItem>(typeof(BasicEngineeringSkill), 10, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)),
                new CraftingElement<BoardItem>(typeof(BasicEngineeringSkill), 50, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent)),
                new CraftingElement<IronIngotItem>(typeof(BasicEngineeringSkill), 20, BasicEngineeringSkill.MultiplicativeStrategy, typeof(BasicEngineeringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(HandPloughRecipe), Item.Get<HandPloughItem>().UILink(), 5, typeof(BasicEngineeringSkill), typeof(BasicEngineeringFocusedSpeedTalent), typeof(BasicEngineeringParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Hand Plough"), typeof(HandPloughRecipe));
            CraftingComponent.AddRecipe(typeof(WainwrightTableObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(StandaloneAuthComponent))]
    [RequireComponent(typeof(VehicleComponent))]
    public partial class HandPloughObject : PhysicsWorldObject, IRepresentsItem
    {
        static HandPloughObject()
        {
            WorldObject.AddOccupancy<HandPloughObject>(new List<BlockOccupancy>(0));
        }

        public override LocString DisplayName { get { return Localizer.DoStr("Hand Plough"); } }
        public Type RepresentedItemType { get { return typeof(HandPloughItem); } }


        private HandPloughObject() { }

        protected override void Initialize()
        {
            base.Initialize();
            
            this.GetComponent<VehicleComponent>().Initialize(10, 1, 1);
            this.GetComponent<VehicleComponent>().HumanPowered(1);           
        }
    }
}