namespace Eco.Mods.TechTree
{
    using System.Linq;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Players;

    public partial class FocusedWorkflowTalent : Talent
    {
        public override bool HasActiveRequirements { get { return true; } }
        public override bool Active(object obj, User user = null)
        {
            var cc = obj as CraftingComponent;
            if (cc != null)
                if (cc.Parent.HasComponent<RoomRequirementsComponent>() && cc.Parent.GetComponent<RoomRequirementsComponent>().RoomStats != null && cc.Parent.GetComponent<RoomRequirementsComponent>().RoomStats.ContainedWorldObjects.Where(x => x.GetType() == cc.Parent.GetType()).Count() > 1)
                    return false;
            return true;
        }
    }
}