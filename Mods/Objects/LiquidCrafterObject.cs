// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System;
    using System.ComponentModel;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Shared.Serialization;
    using Eco.Shared.Localization;

    //A test object for tables that use liquid to craft.  Should be removed once we get real tables that craft liquid.
    [Category("Hidden")]
    public partial class LiquidRecipe : Recipe
    {
        public LiquidRecipe()
        {
            this.Products     = new CraftingElement[] { new CraftingElement<LogItem>(2),};
            this.Ingredients  = new CraftingElement[] { new CraftingElement<WaterItem>(5000), };
            this.CraftMinutes = CreateCraftTimeValue(typeof(BoardRecipe), Item.Get<BoardItem>().UILink(), 0.5f, typeof(HewingSkill));
            this.Initialize(Localizer.DoStr("Liquid Crafting Test"), typeof(BoardRecipe));

            CraftingComponent.AddRecipe(typeof(LiquidCrafterObject), this);
        }
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(MinimapComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(CraftingComponent))]
    [RequireComponent(typeof(SolidGroundComponent))]
    [RequireComponent(typeof(LiquidConverterComponent))]
    [Category("Hidden")]
    public partial class LiquidCrafterObject :
        WorldObject,
        IRepresentsItem
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Liquid Crafter"); } }
        public virtual Type RepresentedItemType { get { return typeof(LiquidCrafterItem); } }

        protected override void Initialize()
        {
            this.GetComponent<LiquidConverterComponent>().Setup(Item.Get("WaterItem").Type, Item.Get("SewageItem").Type, this.NamedOccupancyOffset("InputPort"), this.NamedOccupancyOffset("OutputPort"), 1000, 0f);
            this.GetComponent<MinimapComponent>().Initialize(Localizer.DoStr("Crafting"));
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }

    [Serialized]
    [Category("Hidden")]
    public partial class LiquidCrafterItem :
        WorldObjectItem<LiquidCrafterObject>
    {
        public override LocString DisplayName        { get { return Localizer.DoStr("Liquid Converter"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("Example crafting table that uses liquids."); } }
    }
}