using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.DB
{
    public abstract class MySqlRepository<T>
    {
        protected MySqlConnection connection;

        public MySqlRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }
        
        
        public abstract void DeleteById(int id);
        public abstract List<T> FindAll();
        public abstract T FindById(int id);
        public abstract void Save(T entity);
    }
}
