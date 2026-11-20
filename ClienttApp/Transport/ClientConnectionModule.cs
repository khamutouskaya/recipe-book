using CommonEntities.Models;
using CommonEntities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClienttApp.Transport
{
    public class ClientConnectionModule
    {
        protected static TCPClientProtocol protocol;
        public bool Connected { get; }

        public ClientConnectionModule()
        {
            if (protocol == null)
            {
                protocol = new TCPClientProtocol();
                Connected = protocol.Connect();
            }
        }

        public void Disconnect()
        {
            protocol.Disconnect();
        }

        public List<Category> ReadAllCategories()
        {
            protocol.SendCommand(Command.ReadAllCategories);
            return protocol.ReceiveCollection<Category>();
        }

        public List<Product> ReadAllProducts()
        {
            protocol.SendCommand(Command.ReadAllProducts);
            return protocol.ReceiveCollection<Product>();
        }

        public void CreateProduct(Product product)
        {
            protocol.SendCommand(Command.CreateProduct);
            protocol.SendObject<Product>(product);
        }

        public void UpdateProduct(Product product)
        {
            protocol.SendCommand(Command.UpdateProduct);
            protocol.SendObject<Product>(product);
        }

        public void DeleteProduct(int id)
        {
            protocol.SendCommand(Command.DeleteProduct);
            protocol.SendString(id.ToString());
        }

        public List<Recipe> ReadAllRecipesByCategory(Category category)
        {
            protocol.SendCommand(Command.ReadAllRecipesByCategory);
            protocol.SendObject<Category>(category);
            return protocol.ReceiveCollection<Recipe>();
        }

        public void CreateRecipe(Recipe recipe)
        {
            protocol.SendCommand(Command.CreateRecipe);
            protocol.SendObject<Recipe>(recipe);
        }

        public void UpdateRecipe(Recipe recipe)
        {
            protocol.SendCommand(Command.UpdateRecipe);
            protocol.SendObject<Recipe>(recipe);
        }

        public void DeleteRecipe(int id)
        {
            protocol.SendCommand(Command.DeleteRecipe);
            protocol.SendString(id.ToString());
        }
    }
}
