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
    public partial class NotchBeltItem :
        ClothingItem        
    {

        public override LocString DisplayName         { get { return Localizer.DoStr("Notch Belt"); } }
        public override LocString DisplayDescription  { get { return Localizer.DoStr("Belt with a slightly V-shaped buckle."); } }
        public override string Slot             { get { return ClothingSlot.Belt; } }             
        public override bool Starter            { get { return true ; } }                       

    }

    
    [RequiresSkill(typeof(TailoringSkill), 0)]
    public class NotchBeltRecipe : Recipe
    {
        public NotchBeltRecipe()
        {
            this.Products = new CraftingElement[]
            {
                new CraftingElement<NotchBeltItem>(),
            };
            this.Ingredients = new CraftingElement[]
            {
                new CraftingElement<LeatherHideItem>(typeof(TailoringSkill), 2, TailoringSkill.MultiplicativeStrategy, typeof(TailoringLavishResourcesTalent))
            };
            this.CraftMinutes = CreateCraftTimeValue(typeof(NotchBeltRecipe), Item.Get<NotchBeltItem>().UILink(), 1, typeof(TailoringSkill), typeof(TailoringFocusedSpeedTalent), typeof(TailoringParallelSpeedTalent)); 
            this.Initialize(Localizer.DoStr("Notch Belt"), typeof(NotchBeltRecipe));
            CraftingComponent.AddRecipe(typeof(TailoringTableObject), this);
        }
    } 
}