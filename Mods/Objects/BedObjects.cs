// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Objects;
    using Eco.Shared.Math;
    using Eco.Shared.Networking;
    using Eco.Shared.Serialization;
    using Eco.Core.Controller;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Systems;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Gameplay.Property;
    using System.Threading;
    using Eco.Core.Tests;
    using Eco.Gameplay.Interactions;
    using Eco.Shared.Items;

    //Add Bed component to beds
    [RequireComponent(typeof(BedComponent))] public partial class WoodenFabricBedObject { }
    [RequireComponent(typeof(BedComponent))] public partial class WoodenStrawBedObject { }
    [RequireComponent(typeof(BedComponent))] public partial class CampsiteObject { }

    //Bed component just has a button to open the sleep manager.
    [Serialized, AutogenClass, ViewDisplayNameAttribute("Bed")]
    public class BedComponent : WorldObjectComponent, IChatCommandHandler, IInteractable
    {
        public override WorldObjectComponentClientAvailability Availability { get { return WorldObjectComponentClientAvailability.UI; } }

        [RPC, Autogen("BigButton")]
        public void Sleep(Player player) { SleepManager.Obj.PlayerSleep(player, this.Parent); }

        public InteractResult OnActLeft(InteractionContext context) { return InteractResult.NoOp; }
        public InteractResult OnActRight(InteractionContext context) { return InteractResult.NoOp; }

        public InteractResult OnActInteract(InteractionContext context)
        {
            if (context.Parameters != null && context.Parameters.ContainsKey("sleep")) { Sleep(context.Player); return InteractResult.Success; }
            return InteractResult.NoOp;
        }

        public InteractionDesc GetActDesc(InteractionContext context) { return InteractionDesc.None; }

        [CITest(clientDependent: true)]
        [ChatCommand("Spawn a bed and sleep in it.", ChatAuthorizationLevel.Developer)]
        public static void SpawnBed(User user)
        {
            RoomChecker.SpawnBuilding(user, 1, new Vector3i(5, 5, 5), user.Player.Position.Round + Vector3i.Down - new Vector3i(2,0,2));
            var bed = WorldObjectUtil.SpawnAndClaim<BedComponent>("WoodenFabricBedObject", user, user.Position.Round + Vector3i.Forward);
            Thread.Sleep(2000);
            bed.Sleep(user.Player);
        }
    }
}
