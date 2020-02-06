// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using System.Linq;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Shared.Utils;
    using Gameplay.Items.SearchAndSelect;
    using Gameplay.Skills;
    using Shared.Serialization;
    using Eco.Gameplay.DynamicValues;
    using Eco.Shared.Localization;

    public partial class EckoTheDolphinItem : ToolItem
    {
        [Serialized]
        private string wantedItem = string.Empty;
        private static IDynamicValue skilledRepairCost = new ConstantValue(1);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        public override InteractResult OnActRight(InteractionContext context)
        {
            if (this.wantedItem == string.Empty)
                this.wantedItem = SearchAndSelectItemSets.DiscoveredItems.Items.Shuffle().First().DisplayName;

            var itemStack = context.Player.User.Inventory.NonEmptyStacks.Where(stack => stack.Item.DisplayName == this.wantedItem).FirstOrDefault();
            if (itemStack != null)
            {
                var gift = AllItems.Where(x => !(x is Skill) && x.Group != "Actionbar Items").Shuffle().First();
                var result = context.Player.User.Inventory.TryModify(changeSet =>
                {
                    changeSet.RemoveItem(itemStack.Item.Type);
                    changeSet.AddItem(gift.Type);
                }, context.Player.User);

                if (result.Success)
                {
                    
                    context.Player.SendTemporaryMessage(Localizer.Format("Ecko accepts your tribute of {0} and grants you {1} for your devotion.", this.wantedItem, gift.DisplayName));
                    this.wantedItem = SearchAndSelectItemSets.DiscoveredItems.Items.Shuffle().First().DisplayName;
                }
            }
            else
                context.Player.SendTemporaryMessage(Localizer.Format("Ecko demands {0}!", this.wantedItem));

            return InteractResult.Success;
        }
    }
}