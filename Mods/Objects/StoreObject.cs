namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Components.Auth;
    using Eco.Gameplay.Objects;

    [RequireComponent(typeof(StoreComponent))] public partial class StoreObject : WorldObject 
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