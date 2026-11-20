using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonEntities.Models;

namespace CommonEntities.Models
{
    [Serializable]
    public class RecipeCategory
    {
        public int Id { get; set; }
        public Recipe Recipe { get; set; }
        public Category Category { get; set; }

        public RecipeCategory()
        {
            Recipe = new Recipe();
            Category = new Category();
        }

        public RecipeCategory(int id, double count, Recipe recipe, Category category)
        {
            Id = id;
            Recipe = recipe;
            Category = category;
        }
    }

}
