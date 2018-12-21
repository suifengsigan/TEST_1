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
        var MaxXYZPoint = face.Position(uBoxUV.MaxU , uBoxUV.MaxV);
        var line = Snap.Create.Line(centerPoint - (Snap.Position.Distance(MaxXYZPoint, centerPoint) * (Snap.Vector.Unit(centerPoint - MaxXYZPoint))),
            centerPoint + (Snap.Position.Distance(MaxXYZPoint, centerPoint) * (Snap.Vector.Unit(centerPoint - MaxXYZPoint))));
        line.Layer = face.Layer;
        line.Color = System.Drawing.Color.Blue;
    }
}