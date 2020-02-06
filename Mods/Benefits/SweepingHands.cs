namespace Eco.Mods.TechTree
{
    using System.Collections.Generic;
    using System.Linq;
    using Core.Utils.AtomicAction;
    using Gameplay.Items;
    using Gameplay.Objects;
    using Gameplay.Players;
    using Gameplay.Stats;
    using Shared.Networking;
    using Shared.Utils;

    public partial class MiningSweepingHandsTalent
    {
        public readonly int PickUpRange = 4;

        public override void RegisterTalent(User user)
        {
            base.RegisterTalent(user);
            user.OnPickupObject.Add((target, tool, actions) =>
            {
                // only apply talent when object picked up by hands, not with tool like excavator or skid steer
                if (tool != null) return;
                var rubble = target as RubbleObject;
                if (rubble != null)
                    this.ApplyTalent(user, rubble, actions);
            });
        }

        private void ApplyTalent(User user, RubbleObject target, List<IAtomicAction> actions)
        {
            var representsItem = target as IRepresentsItem;
            if (representsItem == null)
                return;

            var itemType = representsItem.RepresentedItemType;
            // max stack size minus currently picking item
            var numToTake = Item.GetMaxStackSize(itemType) - 1;
            if (numToTake <= 0)
                return;

            var carrying = user.Carrying;
            if (!carrying.Empty)
            {
                if (carrying.Item.Type != itemType || carrying.Quantity >= numToTake)
                    return;
                // adjust to currently carrying item count
                numToTake -= carrying.Quantity;
            }

            var inventoryChangeSet = (InventoryChangeSet)actions.FirstOrDefault(a => a is InventoryChangeSet);
            DebugUtils.Assert(inventoryChangeSet != null, "No inventory change set for PickUp Rubble event");
            if (inventoryChangeSet == null)
                return;

            var item = Item.Get(itemType);
            var numTaken = 0;
            foreach (var rubble in NetObjectManager.GetObjectsWithin(target.Position, this.PickUpRange).OfType<RubbleObject>())
            {
                if (rubble == target || rubble.IsBreakable) continue;
                var rubbleRepresentsItem = rubble as IRepresentsItem;
                if (rubbleRepresentsItem == null || rubbleRepresentsItem.RepresentedItemType != itemType) continue;
                actions.Add(PlayerActions.PickUp.CreateAtomicAction(user, item, rubble.Position.Round));
                actions.Add(new DestroyRubbleAction(rubble));
                if (++numTaken == numToTake)
                    break;
            }
            inventoryChangeSet.AddItems(itemType, numTaken);
        }
    }
}