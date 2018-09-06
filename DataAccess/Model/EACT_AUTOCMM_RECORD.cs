using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Model
{
    /// <summary>
    /// 自动取点工具记录
    /// </summary>
    public class EACT_AUTOCMM_RECORD
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 模号
        /// </summary>
        public string MODELNO { get; set; }
        /// <summary>
        /// 件号
        /// </summary>
        public string PARTNO { get; set; }
        /// <summary>
        /// 部件名
        /// </summary>
        public string PARTNAME { get; set; }
        /// <summary>
        /// CMM取点是否成功 0表示未上传  1表示上传成功 2表示上传失败
        /// </summary>
        public int CMMRESULT { get; set; }
        /// <summary>
        /// 表示CMM取点信息
        /// </summary>
        public string CMMINFO { get; set; }
        /// <summary>
        /// CMM取点的日期
        /// </summary>
        public DateTime? CMMDATE { get; set; }
    }
}
