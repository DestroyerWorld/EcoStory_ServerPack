// Copyright (c) Strange Loop Games. All rights reserved.
// See LICENSE file in the project root for full license information.
namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Shared.Serialization;
    using Eco.Shared.Networking;
    using Eco.Gameplay.Players;
    using Simulation;
    using Gameplay.Animals;
    using Eco.Shared.Localization;

    //this is going to be a real item at some point

    [Serialized]
    public partial class FishingPoleItem : ToolItem
    {
        private static IDynamicValue caloriesBurn = new ConstantValue(1.0f);

        private static SkillModifiedValue skilledRepairCost = new SkillModifiedValue(10, HuntingSkill.MultiplicativeStrategy, typeof(HuntingSkill), Localizer.DoStr("repair cost"), typeof(Efficiency));
        public override IDynamicValue SkilledRepairCost { get { return skilledRepairCost; } }
        public override LocString DisplayName { get { return Localizer.DoStr("Fishing Pole"); } }

        static FishingPoleItem() { }

        public override IDynamicValue CaloriesBurn { get { return caloriesBurn; } }
       
        [RPC]
        void FinalizeCatch(Player player, INetObject target)
        {
            if (target is AnimalEntity)
                if (player.User.Inventory.TryAddItem(((AnimalEntity)target).Species.ResourceItemType, player.User))
                {
                    ((AnimalEntity)target).Kill(DeathType.Harvesting);
                    ((AnimalEntity)target).Destroy();
                }
        }
    }

    public class LureEntity : NetPhysicsEntity
    {
        public LureEntity() : base("Lure") { }

        public override bool IsNotRelevant(INetObjectViewer viewer)
        {
            bool isNot = base.IsNotRelevant(viewer);
            if (this.Controller == null)
                this.Destroy();

            return isNot;
        }

        public override void ReceiveUpdate(BSONObject bsonObj)
        {
            base.ReceiveUpdate(bsonObj);

            if (this.Position.y <= 0)
                this.Destroy();
        }
    }

}