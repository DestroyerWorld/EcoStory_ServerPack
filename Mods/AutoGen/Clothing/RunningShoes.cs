namespace Eco.Mods.TechTree
{
    // [DoNotLocalize]
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using Eco.Gameplay.Components;
    using Eco.Gameplay.DynamicValues;
    using Eco.Gameplay.Items;
    using Eco.Gameplay.Players;
    using Eco.Gameplay.Skills;
    using Eco.Gameplay.Systems.TextLinks;
    using Eco.Mods.TechTree;
    using Eco.Shared.Items;
    using Eco.Shared.Localization;
    using Eco.Shared.Serialization;
    using Eco.Shared.Utils;
    using Eco.Shared.View;
    
    [Serialized]
    [Weight(100)]      
    public partial class RunningShoesItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Running Shoes"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr("FASTER SHOES???"); } }
        public override string Slot             { get { return ClothingSlot.Shoes; } }             
        public override bool Starter            { get { return false ; } }                       

        private static Dictionary<UserStatType, float> flatStats = new Dictionary<UserStatType, float>()
    {
                { UserStatType.MovementSpeed, 0.5f }
    };
public override Dictionary<UserStatType, float> GetFlatStats() { return flatStats; }
    }

    
    [RequiresSkill(typeof(TailoringSkill), 1)]
    public class RunningShoesRecipe : Recipe
    {
        public RunningShoesRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<RunningShoesItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LeatherHideItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent)),
                new CraftingElement<ClothItem>(typeof(TailoringSkill), 5, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(RunningShoesRecipe), Item.Get<RunningShoesItem>().UILink(), 1, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Running Shoes"), typeof(RunningShoesRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    } 
}