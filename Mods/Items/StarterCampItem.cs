// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System;
    using Eco.Core.Utils;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Gameplay.Systems.TextLinks;
    using Shared.Networking;

    [Serialized]
    public class StarterCampItem : WorldObjectItem<StarterCampObject>
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Starter Camp"); } }
        public override LocString DisplayDescription { get { return Localizer.DoStr("A combination of a small tent and a tiny stockpile."); } }

        public override Result OnAreaValid(Player player, Vector3i position, Quaternion rotation)
        {
            Deed deed = PropertyManager.FindNearbyDeedOrCreate(player.User, position.XZ);

            foreach (var plot in WorldObject.GetOccupiedPropertyPositions(typeof(StarterCampObject), position, rotation))
                PropertyManager.Claim(deed.Id, player.User, player.User.Inventory, plot);

            var camp = WorldObjectManager.TryToAdd(typeof(CampsiteObject), player.User, position, rotation, false);
            var stockpile = WorldObjectManager.TryToAdd(typeof(TinyStockpileObject), player.User, position + rotation.RotateVector(Vector3i.Right * 3), rotation, false);
            player.User.OnWorldObjectPlaced.Invoke(camp);
            player.User.Markers.Add(camp.Position3i + Vector3i.Up, camp.UILinkContent());
            player.User.Markers.Add(stockpile.Position3i + Vector3i.Up, stockpile.UILinkContent());
            var storage = camp.GetComponent<PublicStorageComponent>();
            var changeSet = new InventoryChangeSet(storage.Inventory);
            PlayerDefaults.GetDefaultCampsiteInventory().ForEach(x =>
            {
                changeSet.AddItems(x.Key, x.Value, storage.Inventory);
            });
            changeSet.Apply();
            return Result.Succeeded;
        }

        public override bool ShouldCreate { get { return false; } }

        public override bool TryPlaceObject(Player player, Vector3i position, Quaternion rotation)
        {
            bool canClaim;
            return this.TryPlaceObject(player, position, rotation, out canClaim);
        }

        public override void TryPlaceObject(Player player, Vector3i position, Quaternion rotation, Action successCallback)
        {
            bool canClaim;
            if (!this.TryPlaceObject(player, position, rotation, out canClaim))
                return;

            if (canClaim)
            {
                successCallback();
                return;
            }

            player.Client.RPCAsync<bool>("PopupConfirmBox", player.Client, Localizer.Format("Do you want to place {0} on another player's property? The player become an owner of {0} and you can be removed from the property at any moment", this.UILink()))
                .ContinueWith(t => { if (t.Result) successCallback(); });
        }

        private bool TryPlaceObject(Player player, Vector3i position, Quaternion rotation, out bool canClaim)
        {
            canClaim = false;
            if (!TryPlaceObjectOnSolidGround(player, position, rotation))
                return false;

            canClaim = true;
            foreach (var pos in WorldObject.GetOccupiedPropertyPositions(typeof(StarterCampObject), position, rotation))
            {
                var plot = PropertyManager.GetPlot(pos);
                if (plot != null && plot.DeedId != Guid.Empty && plot.Owner != player.User)
                {
                    canClaim = false;
                    if (!plot.IsAuthorized(player.User))
                    {
                        player.SendTemporaryError(Localizer.Format("Can't place {0} on {1}'s property", this.UILink(), plot.Owner.UILink()));
                        return false;
                    }
                }
            }

            return true;
        }

        public override void OnSelected(Player player)
        {
            base.OnSelected(player);
            if (player != null) player.SetShowPropertyState(true);
        }
        
        public override void OnDeselected(Player player)
        {
            base.OnDeselected(player);
            if (player != null) player.SetShowPropertyState(false);
        }
    }

    [Serialized]
    public class StarterCampObject : WorldObject
    {
        public override LocString DisplayName { get { return Localizer.DoStr("Starting Camp"); } }
    }
}