using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class AutoElecUI:SnapEx.BaseUI
{
    /// <summary>
    /// 偏置类型
    /// </summary>
    struct OffsetType {
        public const string ALL = "全部";
    }

    public override void Init()
    {
        var faceSelect0 = Snap.UI.Block.FaceCollector.GetBlock(theDialog, face_select0.Name);
        faceSelect0.FaceRules = FaceRules_SingleFace;
    }
    public override void DialogShown()
    {
        bool isShow = true;
        if (enum0.ValueAsString == OffsetType.ALL)
        {
            isShow = false;
        }
        expressionALL.Show = !isShow;

        expressionXNegative.Show = isShow;
        expressionXPositive.Show = isShow;
        expressionYNegative.Show = isShow;
        expressionYPositive.Show = isShow;
        expressionZNegative.Show = isShow;
        expressionZPositive.Show = isShow;
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        DialogShown();
    }

    public override void Apply()
    {
        var faces = new List<NXOpen.Face>();
        face_select0.GetSelectedObjects().ToList().ForEach(u =>
        {
            var face = u as NXOpen.Face;
            if (face != null)
            {
                faces.Add(face);
            }
        });
        var ds = new List<double>();
        if (enum0.ValueAsString == OffsetType.ALL)
        {
            for (int i = 0; i < 6; i++)
            {
                ds.Add(expressionALL.Value);
            }
        }
        else
        {
            ds.Add(expressionXPositive.Value);
            ds.Add(expressionXNegative.Value);
            ds.Add(expressionYPositive.Value);
            ds.Add(expressionYNegative.Value);
            ds.Add(expressionZPositive.Value);
            ds.Add(expressionZNegative.Value);

        }

        var nxObject = SnapEx.Create.Box(faces, ds[0], ds[1], ds[2], ds[3], ds[4], ds[5]);
        Snap.NX.Body body = nxObject.Bodies.FirstOrDefault();
        Snap.NX.Face snapFace = faces.FirstOrDefault();
        var trimBody = Snap.Create.TrimBody(body, snapFace, true);
        trimBody.Orphan();
    }

}