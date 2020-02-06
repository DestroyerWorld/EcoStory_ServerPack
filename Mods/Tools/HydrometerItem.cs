// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.

using System.ComponentModel;
using Eco.Gameplay.DynamicValues;
using Eco.Gameplay.Items;
using Eco.Shared.Localization;
using Eco.Shared.Serialization;

[Serialized]
[Category("Hidden")]
public class HydrometerItem : ToolItem
{
    static IDynamicValue caloriesBurn = new ConstantValue(1);

    public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }

    public override LocString DisplayName { get { return Localizer.DoStr("Hydrometer"); } }
    public override LocString DisplayDescription { get { return Localizer.DoStr("Measures the water content of the environment."); } }
        
    private static IDynamicValue skilledRepairCost = new ConstantValue(1);
    public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
}