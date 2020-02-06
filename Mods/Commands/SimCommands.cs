// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using Eco.Core.Tests;
using Eco.Gameplay.Players;
using Eco.Gameplay.Systems.Chat;
using Eco.Shared.Localization;
using Eco.Shared.Utils;
using Eco.Simulation.WorldLayers;

namespace Eco.Mods
{
    public class SimCommands : IChatCommandHandler
    {
        [CITest]
        [ChatCommand("Rasies the sea level by a passed in amount.  Careful with this one!", ChatAuthorizationLevel.Developer)]
        public static void RaiseSeaLevel(User user, float val = 1.5f)
        {
            var seaLevel = WorldLayerManager.ClimateSim.State.SeaLevel;
            WorldLayerManager.ClimateSim.SetSeaLevel(seaLevel + val);
            ChatManager.ServerMessageToAll(Localizer.Format("{0} has raised the seas by {1}!", user.Name, Text.StyledNum(val)), false);
        }

        [CITest]
        [ChatCommand("Displays the current sea level and how much it has risen.", ChatAuthorizationLevel.User)]
        public static void SeaLevel(User user) 
        {
            ChatManager.ServerMessageToPlayer(Localizer.Format("Current sea level: {0}  Amount raised so far: {1}", Text.StyledNum(WorldLayerManager.ClimateSim.State.SeaLevel), Text.StyledNum(WorldLayerManager.ClimateSim.State.SeaLevel - WorldLayerManager.ClimateSim.State.InitialSeaLevel)), user);
        }
    }
}