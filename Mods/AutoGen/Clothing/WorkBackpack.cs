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
    public partial class WorkBackpackItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Work Backpack"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Lighter pack that causes lower calorie consumption."); } }
        public override string Slot             { get { return ClothingSlot.Back; } }             
        public override bool Starter            { get { return false ; } }                       

        private static Dictionary<UserStatType, float> flatStats = new Dictionary<UserStatType, float>()
    {
                { UserStatType.MaxCarryWeight, 5000 },
                { UserStatType.CalorieRate, -0.1f }
    };
public override Dictionary<UserStatType, float> GetFlatStats() { return flatStats; }
    }

    
    [RequiresSkill(typeof(TailoringSkill), 3)]
    public class WorkBackpackRecipe : Recipe
    {
        public WorkBackpackRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<WorkBackpackItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LeatherHideItem>(typeof(TailoringSkill), 10, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent)),
                new CraftingElement<CelluloseFiberItem>(typeof(TailoringSkill), 20, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(WorkBackpackRecipe), Item.Get<WorkBackpackItem>().UILink(), 1, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Work Backpack"), typeof(WorkBackpackRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    } 
}