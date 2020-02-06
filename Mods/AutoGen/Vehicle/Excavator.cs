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
    [Weight(30000)]  
    public class ExcavatorItem : WorldObjectItem<ExcavatorObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Excavator"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("I EAT DIRT!"); } }
    }

    [RequiresSkill(typeof(IndustrySkill), 1)] 
    public class ExcavatorRecipe : Recipe
    {
        public ExcavatorRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<ExcavatorItem>(),
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
            this.CraftMinutes = CreateCraftTimeValue(typeof(ExcavatorRecipe), Item.Get<ExcavatorItem>().UILink(), 50, typeof(IndustrySkill), typeof(IndustryFocusedSpeedTalent), typeof(IndustryParallelSpeedTalent));    

            this.Initialize(Localizer.DoStr("Excavator"), typeof(ExcavatorRecipe));
            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}