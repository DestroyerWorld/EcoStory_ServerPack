// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System.ComponentModel;
    using System.Linq;
    using Eco.Core.Utils;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Property;
    using Eco.Shared.Items;
    using Eco.Shared.Math;

    [Category("Tools")]
    [CarryTypesLimited]
    public partial class PropertyToolItem : ToolItem
    {

        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

        public override float InteractDistance { get { return DefaultInteractDistance.Placement; } }
        public override float DurabilityRate { get { return 0f; } }

        // Place to claim
        public override InteractResult OnActRight(InteractionContext context)
        {
            Vector2i? position = this.GetPosition(context);
            User actor = context.Player.User;

            if (!position.HasValue)
                return InteractResult.NoOp;
            
            Deed deed = PropertyManager.FindNearbyDeedOrCreate(actor, position.Value);
            Result claimResult = PropertyManager.Claim(deed.Id, actor, context.Player.User.Inventory, position.Value);

            if (!claimResult.Success && !deed.OwnedObjects.Any())
                PropertyManager.TryRemoveDeed(deed);

            return (InteractResult)claimResult;
        }

        // Hit to unclaim
        public override InteractResult OnActLeft(InteractionContext context)
        {
            Vector2i? position = this.GetPosition(context);
            Player player = context.Player;

            if (!position.HasValue)
                return InteractResult.NoOp;

            return (InteractResult)PropertyManager.Unclaim(player.User, player.User.Inventory, position.Value);
        }

        // Interact to examine
        public override InteractResult OnActInteract(InteractionContext context)
        {
            Vector2i? position = this.GetPosition(context);

            if (!position.HasValue)
                return InteractResult.NoOp;

            Deed deed = PropertyManager.GetDeed(position.Value);

            if (deed == null)
                return InteractResult.NoOp;
            else
            {
                deed.OpenAuthorizationMenuOn(context.Player);
                return InteractResult.Success;
            }
        }

        private Vector2i? GetPosition(InteractionContext context)
        {
            if (context.BlockPosition.HasValue)
                return context.BlockPosition.Value.XZ;
            else if (context.HitPosition.HasValue)
                return context.HitPosition.Value.XZi;
            else
                return null;
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
}
