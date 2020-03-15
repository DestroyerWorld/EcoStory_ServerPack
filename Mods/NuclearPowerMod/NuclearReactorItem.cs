namespace Eco.Mods.TechTree
{
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
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(FuelSupplyComponent))]
    [RequireComponent(typeof(FuelConsumptionComponent))]
    [RequireComponent(typeof(PowerGridComponent))]
    [RequireComponent(typeof(PowerGeneratorComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    public partial class NuclearReactorObject :
        WorldObject,
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ядерный реактор"); } }

        public virtual Type RepresentedItemType { get { return typeof(NuclearReactorItem); } }

        private static Type[] fuelTypeList = new Type[]
       {
            typeof(NuclearFuelCellItem),
       };

        protected override void Initialize()
        {
            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Электричество"));
            this.GetComponent<FuelSupplyComponent>().Initialize(2, fuelTypeList);
            this.GetComponent<FuelConsumptionComponent>().Initialize(10000);
            this.GetComponent<PowerGridComponent>().Initialize(10, new ElectricPower());
            this.GetComponent<PowerGeneratorComponent>().Initialize(25000);
        }

        public override void Destroy()
        {
            base.Destroy();
        }

    }

    [Serialized]
    public partial class NuclearReactorItem :
        WorldObjectItem<NuclearReactorObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Ядерный реактор"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Ядерный реактор — устройство, предназначенное для организации управляемой, самоподдерживающейся цепной реакции деления, которая всегда сопровождается выделением энергии."); } }

        static NuclearReactorItem()
        {

        }

        [Tooltip(7)] private LocString PowerConsumptionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Потребляет: {0}Вт от топлива"), Text.Info(10000))); } }
        [Tooltip(8)] private LocString PowerProductionTooltip { get { return new LocString(string.Format(Localizer.DoStr("Производит: {0}Вт"), Text.Info(25000))); } }
    }

    [Serialized]
    [RequiresModule(typeof(ComputerLabObject))]
    [RequiresSkill(typeof(NuclearTechnitionSkill), 1)]
    public class NuclearReactorItemRecipe : Recipe
    {
        public NuclearReactorItemRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<NuclearReactorItem>(1), 
            };

            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<ConcreteItem>(typeof(NuclearTechnitionSkill), 2000f, NuclearTechnitionSkill.MultiplicativeStrategy),
                new CraftingElement<SteelPlateItem>(typeof(NuclearTechnitionSkill), 700f, NuclearTechnitionSkill.MultiplicativeStrategy),
                new CraftingElement<CircuitItem>(typeof(NuclearTechnitionSkill), 150f, NuclearTechnitionSkill.MultiplicativeStrategy),
                new CraftingElement<ServoItem>(typeof(NuclearTechnitionSkill), 50f, NuclearTechnitionSkill.MultiplicativeStrategy),
                new CraftingElement<HeatSinkItem>(typeof(NuclearTechnitionSkill), 50f, NuclearTechnitionSkill.MultiplicativeStrategy),
            };

            this.CraftMinutes = CreateCraftTimeValue(typeof(NuclearReactorItemRecipe), Item.Get<NuclearReactorItem>().UILink(), 120f, typeof(NuclearTechnitionSkill)); 

            this.Initialize(Localizer.DoStr("Ядерный реактор"), typeof(NuclearReactorItemRecipe)); 

            CraftingComponent.AddRecipe(typeof(RoboticAssemblyLineObject), this);
        }
    }

}