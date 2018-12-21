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
        var box=face.BoxEx();
        var point = face.GetCenterPointEx();
        var line = Snap.Create.Line(box.MaxXYZ, point + (Snap.Position.Distance(box.MaxXYZ, point) * (Snap.Vector.Unit(point - box.MaxXYZ))));
        line.Layer = face.Layer;
        line.Color = System.Drawing.Color.Blue;
    }
}