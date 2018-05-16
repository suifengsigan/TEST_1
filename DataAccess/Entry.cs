using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace DataAccess
{
    public class Entry
    {
        private Entry() { }
        public static Entry Instance = new Entry();
        string _connStr { get; set; }
        public void Init(string connStr)
        {
            _connStr = connStr;
        }

        public string GetConnStr() 
        {
            return _connStr;
        }
    }
}
