using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    public class EACT_CUPRUM_EXP:EACT_CUPRUM
    {
        /// <summary>
        /// 标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 模号
        /// </summary>
        public string MODELNO{get;set;}
        /// <summary>
        /// 件号
        /// </summary>
        public string PARTNO { get; set; }
        /// <summary>
        /// 跑位X
        /// </summary>
        public string X { get; set; }
        /// <summary>
        /// 跑位Y
        /// </summary>
        public string Y { get; set; }
        /// <summary>
        /// 跑位Z
        /// </summary>
        public string Z { get; set; }
        /// <summary>
        /// 旋转C
        /// </summary>
        public string C { get; set; }
    }
}
