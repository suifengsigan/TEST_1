using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        var uv = face.BoxUV;
    }
}