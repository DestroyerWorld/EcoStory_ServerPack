// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

namespace Eco.Mods.TechTree
{
    using Core.Utils;
    using Eco.Core.Controller;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Objects;
    using Eco.Gameplay.Systems.Chat;
    using Eco.Shared.Serialization;
    using Eco.Gameplay.Components;
    using Eco.Shared.Localization;
    using Eco.Gameplay.Pipes.LiquidComponents;
    using Eco.Shared.Utils;
    using Eco.Gameplay.Systems.TextLinks;

    public partial class WasteFilterItem : WorldObjectItem<WasteFilterObject>
    {
        [Serialized] float ProcessedWater { get; set; }
        public const float WaterPerCompostBlock = 100f;

        public override void OnPickup(WorldObject placed)                   { this.ProcessedWater = placed.GetComponent<FilterComponent>().ProcessedWater; }
        public override void OnWorldObjectPlaced(WorldObject placedObject)  { placedObject.GetComponent<FilterComponent>().ProcessedWater = ProcessedWater; }
    }

    [RequireComponent(typeof(PropertyAuthComponent))]
    [RequireComponent(typeof(OnOffComponent))]
    [RequireComponent(typeof(FilterComponent))]
    [RequireComponent(typeof(AttachmentComponent))]
    public partial class WasteFilterObject : WorldObject
    {
    }

    [Serialized]
    [RequireComponent(typeof(LiquidConverterComponent))]
    [RequireComponent(typeof(LinkComponent))]
    [RequireComponent(typeof(StatusComponent))]
    [RequireComponent(typeof(MustBeOwnedComponent))]
    [Priority(Priority)]
    public class FilterComponent : WorldObjectComponent, IOperatingWorldObjectComponent
    {
        // Ensure initialized after PowerGridComponent to setup accumulator
        public const int Priority = PowerGridComponent.Priority + 1;
        [Serialized] public float ProcessedWater { get; set; }
        [Serialized] bool shutdownFromFullInv;

        private LiquidConverterComponent converter;
        StatusElement status;
        
        PeriodicUpdateRealTime updateThrottle = new PeriodicUpdateRealTime(5);

        public override void Initialize()
        {
            this.status   = this.Parent.GetComponent<StatusComponent>().CreateStatusElement();
            this.converter = this.Parent.GetComponent<LiquidConverterComponent>();
            this.converter.Setup(typeof(SewageItem), typeof(WaterItem), this.Parent.NamedOccupancyOffset("InputPort"), this.Parent.NamedOccupancyOffset("OutputPort"), 2000, 0f);
            // buffer will accept liquid even if filter isn't operating and as soon as buffer won't be empty it will become operational
            this.converter.In.BufferSize = 1000;
            this.converter.OnConvert += Converted;
            this.Parent.GetComponent<LinkComponent>().OnInventoryContentsChanged.Add(TurnOnIfRoom);
        }

        string DisplayStatus { get { return Localizer.Format("{0} of {1} currently filtered.", Text.StyledPercent(ProcessedWater / WasteFilterItem.WaterPerCompostBlock), Item.Get("CompostItem").UILink()); } }

        void Converted(float rawamount)
        {
            var amount = rawamount / 1000f;
            ProcessedWater += amount;
            while (ProcessedWater > WasteFilterItem.WaterPerCompostBlock)
            {
                var invs = this.Parent.GetComponent<LinkComponent>().GetSortedLinkedInventories(this.Parent.OwnerUser);
                if (!invs.TryAddItem<CompostItem>())
                {
                    this.Parent.GetComponent<OnOffComponent>().On = false;
                    ChatManager.ServerMessageToPlayer(Localizer.Format("{0} disabled, no room left for filtered waste.", this.Parent.UILink()), this.Parent.OwnerUser, false);
                    this.status.SetStatusMessage(false, Localizer.DoStr("No room for filtered waste."));
                    this.Changed("DisplayStatus");
                    this.Parent.UpdateEnabledAndOperating();
                    this.shutdownFromFullInv = true;
                    return;
                }
                else
                { 
                    ProcessedWater -= WasteFilterItem.WaterPerCompostBlock;
                    this.status.SetStatusMessage(true, DisplayStatus);
                }
            }

            if (updateThrottle.DoUpdate)
                this.status.SetStatusMessage(true, DisplayStatus);
        }

        void TurnOnIfRoom()
        {
            if (shutdownFromFullInv)
            {
                this.Parent.GetComponent<OnOffComponent>().On = true;
                this.shutdownFromFullInv = false;
            }
        }
        public bool Operating { get { return this.converter.In.BufferAmount > 0 || this.converter.In.LastTickReceived > 0; } }
    }
}