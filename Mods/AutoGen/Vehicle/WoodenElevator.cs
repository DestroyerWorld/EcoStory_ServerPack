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
    [Weight(10000)]  
    public class WoodenElevatorItem : WorldObjectItem<WoodenElevatorObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Wooden Elevator"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("An elevator for transporting loads vertically."); } }
    }

    [RequiresSkill(typeof(MechanicsSkill), 1)] 
    public class WoodenElevatorRecipe : Recipe
    {
        public WoodenElevatorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WoodenElevatorItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<PortableSteamEngineItem>(1), 
                new CraftingElement<GearboxItem>(typeof(MechanicsSkill), 10, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<CelluloseFiberItem>(typeof(MechanicsSkill), 20, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent)),
                new CraftingElement<LumberItem>(typeof(MechanicsSkill), 30, MechanicsSkill.MultiplicativeStrategy, typeof(MechanicsLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WoodenElevatorRecipe), Item.Get<WoodenElevatorItem>().UILink(), 25, typeof(MechanicsSkill), typeof(MechanicsFocusedSpeedTalent), typeof(MechanicsParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Wooden Elevator"), typeof(WoodenElevatorRecipe));
            CraftingComponent.AddRecipe(typeof(AssemblyLineObject), this);
        }
    }

}