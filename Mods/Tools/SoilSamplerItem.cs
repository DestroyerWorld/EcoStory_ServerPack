// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using System.ComponentModel;
    using Eco.Gameplay.Interactions;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Plants;
    using Eco.Shared.Math;
    using Eco.Simulation;
    using Shared.Serialization;
    using Simulation.WorldLayers;
    using System.Text;
    using Eco.Gameplay.DynamicValues;
    using Eco.Shared.Localization;

    [Serialized]
    [Category("Tools")]
    public class SoilSamplerItem : ToolItem
    {
        public override LocString DisplayName  { get { return Localizer.DoStr("Soil Sampler"); } }
        public override LocString DisplayDescription   { get { return Localizer.DoStr("Beaker and measuring tools for detecting the factors influencing plants in the environment."); } }
        public override float DurabilityRate { get { return 0; } }

        private static IDynamicValue skilledRepairCost = new ConstantValue(4);
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }

        public override InteractResult OnActLeft(InteractionContext context)
        {
            if (!context.HasBlock)
                return InteractResult.NoOp;
            
            var target = context.BlockPosition.Value + context.Normal;
            var plant = EcoSim.PlantSim.GetPlant(context.BlockPosition.Value + Vector3i.Up);
            if (context.Block is PlantBlock)
                plant = EcoSim.PlantSim.GetPlant(context.BlockPosition.Value);
            StringBuilder title = new StringBuilder();
            StringBuilder text = new StringBuilder();
            if (plant != null)
            {
                title.Append(plant.Species.DisplayName + " " + context.BlockPosition.ToString());
                text.Append(plant.GetEcosystemInfo() + "\n" + text);
            }
            else
            {
                title.Append(context.Block.GetType().Name + " " + context.BlockPosition.ToString());
            }
            text.AppendLine(WorldLayerManager.DescribePos(target.Value.XZ));
            context.Player.OpenInfoPanel(title.ToString(), text.ToString());

            this.BurnCalories(context.Player);
            return InteractResult.Success;
        }
    }
}