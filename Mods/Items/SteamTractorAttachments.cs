// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Gameplay.Auth;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Plants;
    using Eco.Shared.Math;
    using Eco.Simulation;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Core.Utils.AtomicAction;
    using Eco.Gameplay.Stats;

    public partial class SteamTractorPloughItem : VehicleToolItem
    {
        private static Vector3i[] area = new Vector3i[] { new Vector3i(0, 0, -2), new Vector3i(1, 0, -2), new Vector3i(-1, 0, -2) };
        public override void BlockInteraction(Vector3i pos, Quaternion rot, VehicleComponent vehicle, Inventory inv = null)
        {
            if (this.enabled)
            {
                foreach (var offset in area)
                {
                    var targetPos = (rot.RotateVector(offset) + pos).XYZi;
                    Result authResult = AuthManager.IsAuthorized(targetPos, vehicle.Driver.User);
                    if (World.GetBlock(targetPos + Vector3i.Down).Is<Tillable>() && authResult.Success)
                    {
                        if (UsableItemUtils.TryDestroyPlant(vehicle.Driver, vehicle.Parent.Position3i).TryApply())
                            World.SetBlock<TilledDirtBlock>(targetPos + Vector3i.Down);
                    }
                }
            }
        }
    }
    
    public partial class SteamTractorHarvesterItem : VehicleToolItem
    {
        private static Vector3i[] area = new Vector3i[] { new Vector3i(0, 0, 3), new Vector3i(1, 0, 3), new Vector3i(-1, 0, 3) };
        public override void BlockInteraction(Vector3i pos, Quaternion rot, VehicleComponent vehicle, Inventory inv = null)
        {
            foreach (var offset in area)
            {
                var targetPos = (rot.RotateVector(offset) + pos).XYZi;
                Result authResult = AuthManager.IsAuthorized(targetPos, vehicle.Driver.User);
                if (authResult.Success)
                {
                    var plant = PlantBlock.GetPlant(targetPos);
                    if (!(plant is IHarvestable)) continue;
                    if (plant.Alive)
                        ((IHarvestable)plant).TryHarvest(vehicle.Driver, false, inv);
                    else
                        World.DeleteBlock(targetPos);
                }
            }
        }
    }

    
    public partial class SteamTractorSowerItem : VehicleToolItem
    {
        private static Vector3i[] area = new Vector3i[] { new Vector3i(0, 0, 3), new Vector3i(1, 0, 3), new Vector3i(-1, 0, 3) };
        public override void BlockInteraction(Vector3i pos, Quaternion rot, VehicleComponent vehicle, Inventory inv = null)
        {
            if (inv == null)
                return;
            foreach (var offset in area)
            {
                var stack = inv.GroupedStacks.Where(x => x.Item is SeedItem).FirstOrDefault();
                if (stack == null)
                    return;
                SeedItem seed = stack.Item as SeedItem;
                var targetPos = (rot.RotateVector(offset) + pos).XYZi;
                Result authResult = AuthManager.IsAuthorized(targetPos, vehicle.Driver.User);
                if (authResult.Success)
                {
                    if (World.GetBlock(targetPos + Vector3i.Down).Is<Tilled>() && World.GetBlock(targetPos).Is<Empty>())
                    {
                        var changes = new InventoryChangeSet(inv, vehicle.Driver.User);
                        changes.RemoveItem(seed.Type);
                        IAtomicAction plantAction = PlayerActions.Plant.CreateAtomicAction(vehicle.Driver.User, targetPos, seed.Species);

                        if (new MultiAtomicAction(changes, plantAction).TryApply())
                        {
                            var plant = EcoSim.PlantSim.SpawnPlant(seed.Species, targetPos);
                            plant.Tended = true;
                        }
                    }
                }
            }
        }
    }
}
