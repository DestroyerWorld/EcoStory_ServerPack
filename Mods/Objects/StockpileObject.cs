// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Objects;
    using Gameplay.Components.Auth;
    using Gameplay.Players;
    using Shared.Math;
    using Gameplay.Items;
    using Shared.Serialization;
    using System;
    using Eco.Shared.Localization;

    public partial class StockpileItem : WorldObjectItem<StockpileObject>
    {
        public override bool TryPlaceObject(Player player, Vector3i position, Quaternion rotation)
        {
            return TryPlaceObjectOnSolidGround(player, position, rotation);
        }
    }

    [Serialized]
    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(PublicStorageComponent))]
    [RequireComponent(typeof(StockpileComponent))]
    [RequireComponent(typeof(WorldStockpileComponent))]
    [RequireComponent(typeof(LinkComponent))]
    public partial class StockpileObject : WorldObject, IRepresentsItem
    {
        public static readonly Vector3i DefaultDim = new Vector3i(5, 5, 5);

        public override LocString DisplayName { get { return Localizer.DoStr("Stockpile"); } }
        public virtual Type RepresentedItemType { get { return typeof(StockpileItem); } }

        protected override void OnCreate(User creator)
        {
            base.OnCreate(creator);
            StockpileComponent.ClearPlacementArea(creator, this.Position3i, DefaultDim, this.Rotation);
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            var storage = this.GetComponent<PublicStorageComponent>();
            storage.Initialize(DefaultDim.x * DefaultDim.z);
            storage.Storage.AddInvRestriction(new StockpileStackRestriction(DefaultDim.y)); // limit stack sizes to the y-height of the stockpile
            
            this.GetComponent<LinkComponent>().Initialize(7);
        }
    }
}
