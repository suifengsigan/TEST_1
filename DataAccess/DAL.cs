using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class DAL
    {
        public static IDbConnection GetConn()
        {
            return new SqlConnection(Entry.Instance.GetConnStr());
        }
    }
}
