using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SnapEx;

public partial class FaceDiagonalUI : SnapEx.BaseUI
{
    public Snap.NX.Face GetFace()
    {
        return selection0.SelectedObjects.FirstOrDefault() as Snap.NX.Face;
    }
    public override void Init()
    {
        selection0.AllowMultiple = false;
        selection0.SetFilter(Snap.NX.ObjectTypes.Type.Face, Snap.NX.ObjectTypes.SubType.FacePlane);
    }

    public override void Apply()
    {
        var face = GetFace();
        var faceDirOri = new Snap.Orientation(face.GetFaceDirection());
        var uBoxUV = face.BoxUV;
        var centerPoint = face.Position((uBoxUV.MaxU + uBoxUV.MinU) / 2, (uBoxUV.MaxV + uBoxUV.MinV) / 2);
        var point = face.Position(uBoxUV.MaxU, uBoxUV.MaxV);
        var point1 = face.Position(uBoxUV.MaxU, uBoxUV.MinV);

        var lstPoint = new List<Snap.Position>();
        var rPoint_01 = centerPoint - (Snap.Position.Distance(point, centerPoint) * (Snap.Vector.Unit(centerPoint - point)));
        var rPoint_02 = centerPoint + (Snap.Position.Distance(point, centerPoint) * (Snap.Vector.Unit(centerPoint - point)));

        var rPoint1 = centerPoint - (Snap.Position.Distance(point1, centerPoint) * (Snap.Vector.Unit(centerPoint - point1)));
        var rPoint2 = centerPoint + (Snap.Position.Distance(point1, centerPoint) * (Snap.Vector.Unit(centerPoint - point1)));
        if (Snap.Compute.Distance(rPoint1, face) + Snap.Compute.Distance(rPoint2, face) < Snap.Compute.Distance(rPoint_01, face)+ Snap.Compute.Distance(rPoint_02, face))
        {
            rPoint_01 = rPoint1;
            rPoint_02 = rPoint2;
        }
        var line = Snap.Create.Line(rPoint_01, rPoint_02);
        line.Layer = face.Layer;
        line.Color = System.Drawing.Color.Blue;
    }
}