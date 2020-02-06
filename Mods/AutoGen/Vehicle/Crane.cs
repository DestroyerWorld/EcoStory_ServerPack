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
    [Weight(25000)]  
    public class CraneItem : WorldObjectItem<CraneObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Crane"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Allows the placement and transport of materials in an area."); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class CraneRecipe : Recipe
    {
        public CraneRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<CraneItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<AdvancedCombustionEngineItem>(1),
                new CraftingElement<RubberWheelItem>(4),
                new CraftingElement<RadiatorItem>(2),
                new CraftingElement<SteelAxleItem>(1), 
                new CraftingElement<GearboxItem>(typeof(IndustrySkill), 10, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<CelluloseFiberItem>(typeof(IndustrySkill), 20, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent)),
                new CraftingElement<SteelPlateItem>(typeof(IndustrySkill), 40, IndustrySkill.MultiplicativeStrategy, typeof(IndustryLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(CraneRecipe), Item.Get<CraneItem>().UILink(), 25, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Crane"), typeof(CraneRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}