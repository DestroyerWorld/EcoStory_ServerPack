// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Economy;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Gameplay.Systems.Tooltip;
    using Eco.Gameplay.Utils;
    using Shared.Serialization;

    [RequireComponent(typeof(MintComponent))]
    public partial class MintObject : WorldObject { }

    [MaxStackSize(1)]
    public partial class MintItem : WorldObjectItem<MintObject>
    {
        [Serialized] CurrencyHandle currencyHandle = CurrencyHandle.New;
        [Serialized] private BankAccountHandle targetAccountHandle = null;

        public override void OnPickup(WorldObject placed)
        {
            var mintComponent = placed.GetComponent<MintComponent>();
            this.currencyHandle = mintComponent.Currency;
            this.targetAccountHandle = mintComponent.TargetAccount;
        }

        public override void OnWorldObjectPlaced(WorldObject placedObject)
        {
            var mintComponent = placedObject.GetComponent<MintComponent>();
            mintComponent.TargetAccount = this.targetAccountHandle.BankAccount;
            mintComponent.InitializeCurrency(this.currencyHandle);
            base.OnWorldObjectPlaced(placedObject);
        }

        [Tooltip(120)] public string Tooltip()
        {
            Currency currency = this.currencyHandle;
            var currencyText = currency != null ? currency.UILink() : "<None>";
            return "Currency associated with this mint: " + currencyText;
        }
    }
}