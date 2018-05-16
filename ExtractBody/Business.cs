using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class ExtractBodyUI
{
    void Apply() 
    {
        //析出
        var path = nativeFolderBrowser0.Path;
        var name = string0.Value;
        if (string.IsNullOrEmpty(path) || string.IsNullOrEmpty(name))
        {
            NXOpen.UI.GetUI().NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Error, "未选择导出路径或文件名为空");
            return;
        }

        var fileName = string.Format("{0}{1}.prt", path, name);
        var bodys = new List<NXOpen.Body>();
        bodySelect0.GetSelectedObjects().ToList().ForEach(u =>
        {
            bodys.Add(u as NXOpen.Body);
        });
        SnapEx.Create.ExtractBody(bodys, fileName);
    }
}