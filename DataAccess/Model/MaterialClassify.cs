using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    /// <summary>
    /// 物料类型表
    /// </summary>
    public class MaterialClassify
    {
        public string MaterialID { get; set; }
        public string MaterialName { get; set; }
        public string ParentID { get; set; }
    }
}
