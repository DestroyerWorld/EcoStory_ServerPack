// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using Eco.Gameplay.Components;
using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Objects;
using Eco.Gameplay.Property;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

[Serialized]
[RequireComponent(typeof(TreasuryComponent))]
[RequireComponent(typeof(PropertyAuthComponent))]
[RequireComponent(typeof(RoomRequirementsComponent))]
[RequireRoomVolume(45)]
[RequireRoomMaterialTier(1.9f)]
public class TreasuryObject : WorldObject
{
    public override LocString DisplayName { get { return Localizer.DoStr("Treasury"); } }

    protected override void PostInitialize()
    {
        if (this.isFirstInitialization)
        {
            this.GetComponent<PropertyAuthComponent>().SetPublic(); //Only the leader can use the component anyway.
        }        
    }
}
