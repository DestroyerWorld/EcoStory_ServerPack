// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Shared.Localization;
    using Eco.Shared.Math;
    using Eco.Simulation.WorldLayers;
    using Eco.World;
    using Eco.World.Blocks;
    using Gameplay.Components;
    using System;
    using System.Collections.Generic;

    [RequireComponent(typeof(AnimalTrapComponent))]
    public partial class FishTrapObject : WorldObject
    {
        protected override void PostInitialize()
        {
            base.PostInitialize();
            this.GetComponent<PublicStorageComponent>().Initialize(4);
            this.GetComponent<PublicStorageComponent>().Inventory.AddInvRestriction(new SpecificItemTypesRestriction(new System.Type[] { typeof(TroutItem), typeof(SalmonItem) }));
            this.GetComponent<AnimalTrapComponent>().Initialize(new List<string>() { "Trout", "Salmon" });
            this.GetComponent<AnimalTrapComponent>().FailStatusMessage = Localizer.DoStr("Wooden fish traps must be placed underwater in fresh water to function.");
            this.GetComponent<AnimalTrapComponent>().EnabledTest = this.WaterTest;
            this.GetComponent<AnimalTrapComponent>().UpdateEnabled();
        }
        public bool WaterTest(Vector3i pos)
        {
            var block = World.GetBlock(pos + Vector3i.Up);
            return (block is WaterBlock && !(block as WaterBlock).PipeSupplied) ? World.GetWaterHeight(pos.XZ) > WorldLayerManager.ClimateSim.State.SeaLevel : false;
        }
    }

    public partial class FishTrapItem : WorldObjectItem<FishTrapObject>
    {
        public override Type[] Blockers { get { return AllowWaterPlacement; } }
    }
}