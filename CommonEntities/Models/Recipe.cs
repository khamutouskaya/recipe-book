using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities.Models;

namespace CommonEntities.Models
{
    [Serializable]
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<RecipeProduct> Ingridients { get; set; }
        public List<RecipeCategory> Categories { get; set; }
        static public double getCalories()
        {
            return 0;
        }
        //protein fats cabs , добавитьь проверк на ноль

        public Recipe() {
            Id = 0;
            Name = string.Empty;
            Description = string.Empty;
            Ingridients = new List<RecipeProduct>();
            Categories = new List<RecipeCategory>();
        }

        public Recipe(int id, string name, string description, List<RecipeProduct> recipeProducts, List<RecipeCategory> recipeCategory)
        {
            Id = id;
            Name = name;
            Description = description;
            Ingridients = recipeProducts;
            Categories = recipeCategory;
        }

    }
}
