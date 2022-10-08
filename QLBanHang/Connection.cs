using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace QLBanHang
{
    internal class Connection
    {
        private static string stringConnection = (@"Data Source=(local)\SQLEXPRESS;Initial Catalog=QLBanHang;Integrated Security=True");

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(stringConnection);
        }
    }
}
