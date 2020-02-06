namespace Eco.Mods.TechTree
{
    using Gameplay.Components;
    using Gameplay.Objects;
    using Gameplay.Players;

    public partial class FrugalWorkspaceTalent
    {
        public override void OnLearned(User user)
        {
            base.OnLearned(user);
            foreach (var obj in WorldObjectManager.GetOwnedBy(user))
            {
                var requirements = obj.GetComponent<RoomRequirementsComponent>();
                if (requirements != null)
                    requirements.MarkDirty();
            }
        }
    }
}