using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienttApp
{
    internal class Mock
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Recipe> Recipes { get; set; } = new List<Recipe>();
        public List<RecipeProduct> RecipeProducts { get; set; } = new List<RecipeProduct>();
        public List<RecipeCategory> RecipeCategories { get; set; } = new List<RecipeCategory>();

        public Mock() 
        { 
            Categories.Add(new Category() { Id = 1, Name = "Soup"});
            Categories.Add(new Category() { Id = 2, Name = "desert"});
            Categories.Add(new Category() { Id = 3, Name = "Soup2"});
            Categories.Add(new Category() { Id = 4, Name = "Soup3"});

            Products.Add(new Product() { Id = 1, MetricType = "g", Name = "P1", Cab = 101, Fat = 102, Protein = 103, Calories = 104 });
            Products.Add(new Product() { Id = 2, MetricType = "g", Name = "P2", Cab = 101, Fat = 102, Protein = 103, Calories = 104 });
            Products.Add(new Product() { Id = 3, MetricType = "g", Name = "P3", Cab = 101, Fat = 102, Protein = 103, Calories = 104 });
            Products.Add(new Product() { Id = 4, MetricType = "g", Name = "P4", Cab = 101, Fat = 102, Protein = 103, Calories = 104 });

            Recipes.Add(new Recipe() { Id = 1, Name = "R1", Description = "D", Ingridients = new List<RecipeProduct>(), Categories = new List<RecipeCategory>() { new RecipeCategory() { Id = 1, Category = Categories[0] } } });
            Recipes.Add(new Recipe() { Id = 2, Name = "R2", Description = "D", Ingridients = new List<RecipeProduct>(), Categories = new List<RecipeCategory>() { new RecipeCategory() { Id = 1, Category = Categories[0] } } });
            Recipes.Add(new Recipe() { Id = 3, Name = "R3", Description = "D", Ingridients = new List<RecipeProduct>(), Categories = new List<RecipeCategory>() { new RecipeCategory() { Id = 1, Category = Categories[0] } } });
            Recipes.Add(new Recipe() { Id = 4, Name = "R4", Description = "D", Ingridients = new List<RecipeProduct>(), Categories = new List<RecipeCategory>() { new RecipeCategory() { Id = 1, Category = Categories[0] } } });
        }
    }
}
