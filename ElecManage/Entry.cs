using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ElecManage
{
    /// <summary>
    /// 入口
    /// </summary>
    public class Entry
    {
        private Entry() { }
        public static Entry Instance = new Entry();

        public QuadrantType DefaultQuadrantType = QuadrantType.Three;

        /// <summary>
        /// 获取电极信息
        /// </summary>
        public Electrode GetElectrode(Snap.NX.Body body) 
        {
            return null;
        }
    }
}
