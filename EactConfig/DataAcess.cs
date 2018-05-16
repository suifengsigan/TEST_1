using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace EactConfig
{
    public class DataAcess
    {
        string _MSSQLConnStr = "Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
        string _conn = string.Empty;

        public DataAcess(string ip, string name, string user, string password)
        {
            _conn = string.Format(_MSSQLConnStr, ip, name, user, password);
        }

        /// <summary>
        /// 获取所有的属性
        /// </summary>
        public List<EACT_UG_ELECATTR> GetAttrs()
        {
            //using (var conn = new SqlConnection(_conn))
            //{
            //    return conn.Query<EACT_UG_ELECATTR>("select * from EACT_UG_ELECATTR").ToList();
            //}

            return null;
        }
    }
}
