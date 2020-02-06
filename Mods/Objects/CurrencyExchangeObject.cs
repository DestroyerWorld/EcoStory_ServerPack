// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Objects;
    using Gameplay.Components;

    [RequireComponent(typeof(ExchangeComponent))]
    public partial class CurrencyExchangeObject : WorldObject
    {
        protected override void PostInitialize()
        {
            if (this.isFirstInitialization)
            {
                this.GetComponent<PropertyAuthComponent>().SetPublic();
            }            
        }
    }
}