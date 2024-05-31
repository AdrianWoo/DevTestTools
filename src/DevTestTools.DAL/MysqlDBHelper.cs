using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTestTools.DAL
{
    public class MysqlDBHelper
    {
        public MySqlConnection Connection;
        public MysqlDBHelper(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }
        public void Dispose()
        {
            Connection.Close();
        }
    }
}
