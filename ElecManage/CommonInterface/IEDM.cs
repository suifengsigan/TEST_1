using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommonInterface
{
    /// <summary>
    /// EDM图纸接口
    /// </summary>
    public interface IEDM
    {
        void CreateDrawingSheet(List<ElecManage.PositioningInfo> ps, Snap.NX.Body steel);
    }
}
