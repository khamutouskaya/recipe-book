using CommonEntities.Models;
using MySql.Data.MySqlClient;
using ServerApp.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.DB
{
    public class CategoryRepository : MySqlRepository<Category>
    {
        private readonly string createCommand = "INSERT INTO categories (name) VALUES (?)";
        private readonly string updateCommand = "UPDATE categories SET name = ? WHERE id = ?";
        private readonly string deleteCommand = "DELETE FROM categories WHERE id = ?";
        private readonly string readCommand = "SELECT * FROM categories WHERE id = ?";
        private readonly string readAllCommand = "SELECT * FROM categories";
        public CategoryRepository(MySqlConnection connection) : base(connection) { }

        public override void DeleteById(int id)
        {
            RecipeCategoryRepository recipeCategoryRepository = new RecipeCategoryRepository(connection);
            recipeCategoryRepository.DeleteByRecipeId(id);

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = deleteCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            command.ExecuteNonQuery();
        }

        public override Category FindById(int id)
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readCommand;
            command.Parameters.Add("@id", MySqlDbType.Int64).Value = id;

            var reader = command.ExecuteReader();
            reader.Read();
            Category category = new Category();
            category.Id = int.Parse(reader["id"].ToString());
            category.Name = reader["name"].ToString();

            reader.Close();

            RecipeCategoryRepository repository = new RecipeCategoryRepository(connection);
            category.Recipes = repository.FindByCategoryId(id);

            return category;
        }

        public override List<Category> FindAll()
        {
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = readAllCommand;

            var reader = command.ExecuteReader();

            List<Category> result = new List<Category>();
            while (reader.Read())
            {
                Category category = new Category();
                category.Id = int.Parse(reader["id"].ToString());
                category.Name = reader["name"].ToString();

                result.Add(category);
            }

            reader.Close();

            RecipeCategoryRepository repository = new RecipeCategoryRepository(connection);
            foreach (var category in result)
            {
                category.Recipes = repository.FindByCategoryId(category.Id);
            }

            return result;
        }

       
        public override void Save(Category entity)
        {
            if (entity.Id == 0)
            {
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = createCommand;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;

                command.ExecuteNonQuery();
            }
            else
            {
                MySqlCommand command = connection.CreateCommand();
                ///update?
                command.CommandText = updateCommand;
                command.Parameters.Add("@id", MySqlDbType.Int64).Value = entity.Id;
                command.Parameters.Add("@name", MySqlDbType.String).Value = entity.Name;

                command.ExecuteNonQuery();
            }
        }
        
    }
}
