using Eco.Gameplay.Components.Auth;
using Eco.Gameplay.Economy;
using Eco.Gameplay.Objects;

namespace Eco.Mods.TechTree
{
    [RequireComponent(typeof(ContractBoardComponent))]
    public partial class ContractBoardObject : WorldObject
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
