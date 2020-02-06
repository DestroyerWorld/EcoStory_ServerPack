// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods
{
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Players;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Shared.Math;
    using Eco.Simulation.WorldLayers;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Items;
    using System.Linq;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Garbage;
    using System.Threading.Tasks;
    using Eco.Core.Tests;

    public class PollutionCommands : IChatCommandHandler
    {
        [ChatCommand("Just incase you like living in TRASH CITY", ChatAuthorizationLevel.Admin)]
        public static void TrashCity(User user)
        {
            var randItems = Item.AllItems.Where(x => !x.IsCarried && !(x is TrashItem) && x.Category != "Hidden").ToList();

            // Needs to be in dedicated task, to prevent main thread hanging, wich causes timeout
            Task.Run(() =>
            {
                for (var i = 0; i < 100000; i++)
                {
                    var pos = World.GetTopPos(World.GetRandomLandPos().XZ) + Vector3i.Up;
                    var block = World.SetBlock<GarbageBagBlock>(pos, pos);
                    GarbageManager.Obj.RegisterDecayingInventory(pos, new DecayingInventory(4, pos));
                    for (var j = 0; j < 4; j++)
                    {
                        block.Inventory.AddItem(randItems.Random());
                        var rand = RandomUtil.Range(50, 43200);
                        ((DecayingInventory) block.Inventory).StartTimers[j] = rand;
                        ((DecayingInventory) block.Inventory).EndTimers[j] = rand + 43200;
                    }
                }
            });
        }

        [ChatCommand("Creates x PPM of air pollution", ChatAuthorizationLevel.Admin)]
        public static void PolluteCO2(User user, float ppm)
        {
            WorldLayerManager.ClimateSim.AddAirPollution(new WorldPosition3i(user.Position.XYZi), ppm);
        }

        [CITest]
        [ChatCommand("Rains tailings from the heavens to ruin the world", ChatAuthorizationLevel.Admin)]
        public static void PolluteWorld(User user)
        {
            for (var i = 0; i < 1000; i++)
                World.SetBlock<TailingsBlock>(World.GetTopPos(World.GetRandomLandPos().XZ) + Vector3i.Up);
        }

        [CITest]
        [ChatCommand("Creates AIR POLLUTION MACHINES OF DOOM", ChatAuthorizationLevel.Admin)]
        public static void PolluteAir(User user)
        {
            for (var i = 0; i < 50; i++)
                WorldObjectManager.TryToAdd(WorldObjectManager.GetTypeFromName("APGenObject"), user, World.GetTopPos(World.GetRandomLandPos().XZ) + Vector3i.Up, Quaternion.Identity);
        }
    }
}