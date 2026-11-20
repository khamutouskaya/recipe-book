using MySql.Data.MySqlClient;
using CommonEntities.Models;
using ServerApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlX.XDevAPI.Common;

namespace ServerApp.DB
{
    internal class RecipeCategoryRepository : MySqlRepository<RecipeCategory>
    {
        private readonly string createCommand = "INSERT INTO recipe_categories (recipe_id, category_id) VALUES (?,?)";
        private readonly string updateCommand = "UPDATE recipe_categories SET count = ?, recipe_id = ?, category_id = ? WHERE id = ?";
        private readonly string deleteCommand = "DELETE FROM recipe_categories WHERE id = ?";
        private readonly string deleteByRecipeCommand = "DELETE FROM recipe_categories WHERE recipe_id = ?";
        private readonly string deleteByCategoryCommand = "DELETE FROM recipe_categories WHERE category_id = ?";
        private readonly string readCommand = "SELECT * FROM recipe_categories WHERE id = ?";
        private readonly string readByRecipeCommand = "SELECT * FROM recipe_categories WHERE recipe_id = ?";
        private readonly string readByCategoryCommand = "SELECT * FROM recipe_categories WHERE category_id = ?";
        private readonly string readAllCommand = "SELECT * FROM recipe_categories";
        public RecipeCategoryRepository(MySqlConnection connection) : base(connection)
        {
        }
        public override void DeleteById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            command.ExecuteNonQuery();
        }
        public void DeleteByRecipeId(int recipeId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteByRecipeCommand;
            command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = recipeId;

            command.ExecuteNonQuery();
        }
        public void DeleteByCategoryId(int categoryId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteByCategoryCommand;
            command.Parameters.Add("@category_id", MySqlDbType.Int64).Value = categoryId;

            command.ExecuteNonQuery();
        }

        public override List<RecipeCategory> FindAll()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readAllCommand;

            var reader = command.ExecuteReader();

            List<RecipeCategory> result = new List<RecipeCategory>();
            while (reader.Read())
            {
                RecipeCategory recipeCategory = new RecipeCategory();
                recipeCategory.Id = int.Parse(reader["id"].ToString());
                recipeCategory.Recipe.Id = int.Parse(reader["recipe_id"].ToString());
                recipeCategory.Category.Id = int.Parse(reader["category_id"].ToString());

                result.Add(recipeCategory);
            }

            reader.Close();

            return result;
        }

        public override RecipeCategory FindById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            var reader = command.ExecuteReader();
            reader.Read();
            RecipeCategory recipeCategory = new RecipeCategory();
            recipeCategory.Id = int.Parse(reader["id"].ToString());
            recipeCategory.Recipe.Id = int.Parse(reader["recipe_id"].ToString());
            recipeCategory.Category.Id = int.Parse(reader["category_id"].ToString());

            reader.Close();

            return recipeCategory;
        }
        public List<RecipeCategory> FindByRecipeId(int recipeId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readByRecipeCommand;
            command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = recipeId;

            var reader = command.ExecuteReader();

            List <RecipeCategory> result = new List<RecipeCategory>();
            while (reader.Read())
            {
                RecipeCategory recipeCategory = new RecipeCategory ();
                recipeCategory.Id = int.Parse(reader["id"].ToString());
                recipeCategory.Recipe.Id = int.Parse(reader["recipe_id"].ToString());
                recipeCategory.Category.Id = int.Parse(reader["category_id"].ToString());

                result.Add(recipeCategory);
            }
            reader.Close();

            return result;
        }

        public List<RecipeCategory> FindByCategoryId(int categoryId)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readByCategoryCommand;
            command.Parameters.Add("@category_id", MySqlDbType.Int64).Value = categoryId;

            var reader = command.ExecuteReader();

            List<RecipeCategory> result = new List<RecipeCategory>();
            while (reader.Read())
            {
                RecipeCategory recipeCategory = new RecipeCategory();
                recipeCategory.Id = int.Parse(reader["id"].ToString());
                recipeCategory.Recipe.Id = int.Parse(reader["recipe_id"].ToString());
                recipeCategory.Category.Id = int.Parse(reader["category_id"].ToString());

                result.Add(recipeCategory);
            }
            reader.Close();

            return result;
        }

        
        public override void Save(RecipeCategory entity)
        {
            if (entity.Id == 0)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = createCommand;
                command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = entity.Recipe.Id;
                command.Parameters.Add("@category_id", MySqlDbType.Int64).Value = entity.Category.Id;
                
                command.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand command = connection.CreateCommand();
                ///update?
                command.CommandText = updateCommand;
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = entity.Id;
                command.Parameters.Add("@recipe_id", MySqlDbType.Int64).Value = entity.Recipe.Id;
                command.Parameters.Add("@category_id", MySqlDbType.Int64).Value = entity.Category.Id;

                command.ExecuteNonQuery();
            }
        }
    }
}
