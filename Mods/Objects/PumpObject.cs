// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Eco.Core.Controller;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Wires;
    using Eco.Shared.Math;
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Systems;
    using Eco.World;
    using Eco.World.Blocks;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Gameplay.Pipes;
    using System.Linq;
    using System.Collections.Generic;
    using Eco.Shared.Utils;

    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PumpComponent))]
    public partial class MechanicalWaterPumpObject : WorldObject
    {
    }
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(PumpComponent))]
    public partial class ElectricWaterPumpObject : WorldObject
    {
    }


    [Serialized, AutogenClass, ViewDisplayName("Pump")]
    [Tag("Economy")]
    [RequireComponent(typeof(LiquidProducerComponent))]
    [RequireComponent(typeof(ChunkSubscriberComponent))]
    public class PumpComponent : WorldObjectComponent, IChunkSubscriber, IWireContainer
    {
        public override bool Enabled { get { return this.connectedToWater; } }

        WireOutput inputPipe;
        StatusElement status;
        bool connectedToWater;

        public IEnumerable<WireConnection> Wires { get { return (this.inputPipe as WireConnection).SingleItemAsEnumerable(); } }

        public override void Initialize()
        {
            base.Initialize();
            this.status = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            this.Parent.GetComponent<LiquidProducerComponent>().Setup(typeof(WaterItem), 1000, this.Parent.NamedOccupancyOffset("OutputPort"));

            // this is a special case, we really want to pull water but pipes are built around the assumption of pushing
            // input pipes don't trace pipe networks so for now inputPipe is a WireOutput purely for the network tracing
            // as this pipe doesn't actually output anything we need to manually update it, using chunk subscriber to handle updates
            // TODO: refactor pipes so pipe networks are their own thing which inputs and outputs get linked to 
            this.inputPipe = new WireOutput(this.Parent, typeof(PipeBlock), this.Parent.NamedOccupancyOffset("InputPort"), Localizer.DoStr("Input"));

            this.ChunksChanged();
            this.UpdateStatus();
        }

        public IEnumerable<Vector3i> RelevantChunkPositions() { return this.inputPipe.CachedChunks; }

        // Called when any RelevantChunk changes or the chunk below changes
        public void ChunksChanged()
        {
            this.inputPipe.UpdateIfNeeded();

            // input pipe network may have changed which chunks we need to watch
            ChunkSubscriberComponent.UpdateSubscriptions(this);
            var supplied = this.inputPipe.CachedOpenEnds.Count > 0 && this.inputPipe.CachedOpenEnds.All(x => World.GetBlock(x.FirstPos).Is<UnderWater>());
            if (supplied != this.connectedToWater)
            {
                this.connectedToWater = supplied;
                this.UpdateStatus();
                this.Parent.UpdateEnabledAndOperating();
                this.Parent.SetDirty();
            }
        }

        public void UpdateStatus()
        {
            this.status.SetStatusMessage(this.connectedToWater, Localizer.DoStr("Connected to water source."), Localizer.DoStr("Not connected to water source."));
        }
    }
}