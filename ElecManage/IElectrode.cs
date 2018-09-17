using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElecManage
{
    public interface IElectrode
    {
    }

    /// <summary>
    /// 电极类型
    /// </summary>
    public enum ElectrodeType
    {
        /// <summary>
        /// 未知
        /// </summary>
        UNKOWN,
        /// <summary>
        /// //星空电极
        /// </summary>
        XK,
        /// <summary>
        /// 进玉
        /// </summary>
        JY,
        /// <summary>
        /// Eact
        /// </summary>
        EACT,
        /// <summary>
        /// 优品
        /// </summary>
        UP
    }
}
