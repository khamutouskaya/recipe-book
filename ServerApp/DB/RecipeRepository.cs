using MySql.Data.MySqlClient;
using ServerApp.DB;
using CommonEntities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.DB
{
    internal class RecipeRepository : MySqlRepository<Recipe>
    {
        private readonly string getMaxIdCommand = "SELECT MAX(id) as id FROM recipes";

        private readonly string createCommand = "INSERT INTO recipes (name, description) VALUES (?, ?)";
        private readonly string updateCommand = "UPDATE recipes SET name = ?, description = ? WHERE id = ?";
        private readonly string deleteCommand = "DELETE FROM recipes WHERE id = ?";
        private readonly string readCommand = "SELECT * FROM recipes WHERE id = ?";
        private readonly string readAllCommand = "SELECT * FROM recipes";

        public RecipeRepository(MySqlConnection connection) : base(connection)
        {
        }

        public override void DeleteById(int id)
        {
            RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository(connection);   
            recipeCategoryRepository.DeleteByRecipeId(id);

            RecipeProductRepository recipeProductRepository = new RecipeProductRepository(connection);
            recipeProductRepository.DeleteByRecipeId(id);

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            command.ExecuteNonQuery();
        }
        public override Recipe FindById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            var reader = command.ExecuteReader();
            reader.Read();
            Recipe recipe = new Recipe();
            recipe.Id = int.Parse(reader["id"].ToString());
            recipe.Name = reader["name"].ToString();
            recipe.Description = reader["description"].ToString();
            reader.Close();

            InitCategories(recipe);
            InitRecipeProducts(recipe);

            return recipe;
        }

        public override List<Recipe> FindAll()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readAllCommand;

            var reader = command.ExecuteReader();

            List<Recipe> result = new List<Recipe>();
            while (reader.Read())
            {
                Recipe recipe = new Recipe();
                recipe.Id = int.Parse(reader["id"].ToString());
                recipe.Name = reader["name"].ToString();
                recipe.Description = reader["description"].ToString();

                result.Add(recipe);
            }

            reader.Close();

            foreach (var recipe in result)
            {
                InitCategories(recipe);
                InitRecipeProducts(recipe);
            }

            return result;
        }
        public override void Save(Recipe entity)
        {
            if (entity.Id == 0)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = createCommand;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;
                command.Parameters.Add("@description", MySqlDbType.String).Value = entity.Description;

                command.ExecuteNonQuery();

                int recipeId = GetMaxId();

                RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository(connection);
                foreach (var category in entity.Categories)
                {
                    category.Recipe = new Recipe { Id = recipeId };
                    recipeCategoryRepository.Save(category);
                }

                RecipeProductRepository recipeProductRepository = new RecipeProductRepository(connection);
                foreach (var ingridient in entity.Ingridients)
                {
                    ingridient.Recipe = new Recipe { Id = recipeId };
                    recipeProductRepository.Save(ingridient);
                }
            }
            else
            {
                RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository(connection);
                recipeCategoryRepository.DeleteByRecipeId(entity.Id);

                foreach (var category in entity.Categories)
                {
                    category.Recipe = new Recipe { Id = entity.Id };
                    category.Id = 0;
                    recipeCategoryRepository.Save(category);
                }

                RecipeProductRepository recipeProductRepository = new RecipeProductRepository(connection);
                recipeProductRepository.DeleteByRecipeId(entity.Id);

                foreach (var ingridient in entity.Ingridients)
                {
                    ingridient.Recipe = new Recipe { Id = entity.Id };
                    ingridient.Id = 0;
                    recipeProductRepository.Save(ingridient);
                }

                MySqlCommand command = connection.CreateCommand();
                command.CommandText = updateCommand;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;
                command.Parameters.Add("@description", MySqlDbType.String).Value = entity.Description;
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = entity.Id;

                command.ExecuteNonQuery();
            }
        }

        private int GetMaxId()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = getMaxIdCommand;

            var reader = command.ExecuteReader();
            reader.Read();
            int id = int.Parse(reader["id"].ToString());

            reader.Close();

            return id;
        }

        private void InitCategories(Recipe recipe)
        {
            RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository(connection);
            recipe.Categories = recipeCategoryRepository.FindByRecipeId(recipe.Id);
            
        }
        private void InitRecipeProducts(Recipe recipe)
        {
            RecipeProductRepository recipeProductRepository = new RecipeProductRepository(connection);
            recipe.Ingridients = recipeProductRepository.FindByRecipeId(recipe.Id);
        }


    }
}
