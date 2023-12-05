namespace Capstone.Models
{
    public class Ingredient
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public string IngredientQuantity {get; set; } = "1 ea";
        public int Calories { get; set; }
    }



}
