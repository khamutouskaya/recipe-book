using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonEntities.Enums
{
    public enum Command
    {
        ReadAllCategories,
        
        ReadAllProducts,
        CreateProduct,
        UpdateProduct,
        DeleteProduct,

        ReadAllRecipesByCategory,
        CreateRecipe,
        UpdateRecipe,
        DeleteRecipe,

        Disconnect,
    }

    public enum ServiceCommands
    {
        ReadyToReceive
    }

    public enum AnswerFromServer
    {
        Successfully,
        Error,
        UnknownCommand
    }
}
