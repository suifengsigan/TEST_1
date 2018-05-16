using Snap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

partial class ElecBaseDesignUI:SnapEx.BaseUI
{
    public override void Init()
    {
        //选择面设置
        var faceSelect = Snap.UI.Block.FaceCollector.GetBlock(theDialog, face_select0.Name);
        faceSelect.FaceRules = FaceRules_SingleFace;

        //默认第三象限
        var list = enum0.GetEnumMembers().ToList();
        enum0.ValueAsString = list.Count >= 3 ? list[(int)QuadrantType.Three] : list.FirstOrDefault();

        expressionX.Show = false;
        expressionY.Show = false;
        expressionZ.Show = false;

        elecName.Value = string.Format("{0}-{1}", System.IO.Path.GetFileNameWithoutExtension(theSession.Parts.Work.FullPath), 1);
    }

    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
    }
    public override void Apply()
    {
        var faceSelecteds = face_select0.GetSelectedObjects().ToList();
        var snapFaces = new List<Snap.NX.Face>();
        faceSelecteds.ForEach(u => {
            snapFaces.Add(u as NXOpen.Face);
        });

        CreateElecBase(
            snapFaces
            ,elecName.Value
            ,expressionQingGen.Value
            ,expressionDistance.Value
            ,expressionJizhutai.Value
            ,expressionDaojiao.Value
            ,expressionDaoyuan.Value
            ,(QuadrantType)enum0.GetEnumMembers().ToList().IndexOf(enum0.ValueAsString)
            ,_bodyColor,
            false
            );
    }

    System.Drawing.Color _bodyColor = System.Drawing.Color.Blue;

    public static NXOpen.Body CreateElecBase(List<Snap.NX.Face> snapFaces, string partName, double qingGenValue, double distanceValue, double jizhutaiValue, double daojiaoValue, double daoyuanValue, QuadrantType type, System.Drawing.Color bodyColor, bool isExtract = true) 
    {
        var bodys = new List<Snap.NX.Body>(); 
        var firstFace = snapFaces.FirstOrDefault();

        //Z轴方向(决定坐标系)  TODO后期需修改
        var normal = Snap.Vector.Unit(firstFace.Normal(firstFace.BoxUV.MinU, firstFace.BoxUV.MinV));
        snapFaces.ForEach(u =>
        {
            bodys.Add(u.Body);
        });

        //清根深度
        snapFaces.ForEach(u =>
        {
            if (qingGenValue > 0)
            {
                var tempOrigin = u.Position(u.BoxUV.MinU, u.BoxUV.MinV);
                var tempCorner = u.Position(u.BoxUV.MaxU, u.BoxUV.MaxV);
                var extrude = Snap.Create.Extrude(u.EdgeCurves,normal , qingGenValue);
                bodys.Add(extrude.Body);
            }
        });

        //创建基准台
        var positions = GetBox2dPosition(snapFaces);
        var minUV = positions.First();
        var maxUV = positions.Last();

        var origin = minUV + (distanceValue * Snap.Vector.Unit(minUV - maxUV));
        origin += qingGenValue * Snap.Vector.Unit(normal);

        var corner = maxUV + (distanceValue * Snap.Vector.Unit(maxUV - minUV));
        corner += qingGenValue * Snap.Vector.Unit(normal);

        var orientation = new Snap.Orientation(normal);
        var block = Snap.Create.Block(orientation, origin, corner, jizhutaiValue);

        //设置基准面
        var baseFace = SetBaseFace(block, normal);
        List<Position> d = new List<Position>();
        baseFace.Edges.ToList().ForEach(u => {
            if (!d.Contains(u.StartPoint))
            {
                d.Add(u.StartPoint);
            }
            if (!d.Contains(u.EndPoint))
            {
                d.Add(u.EndPoint);
            }
        });

        //获取中心点、倒角点
        var centerPosition = baseFace.Position((baseFace.BoxUV.MinU + baseFace.BoxUV.MaxU) / 2, (baseFace.BoxUV.MinV + baseFace.BoxUV.MaxV) / 2);
        var quadrantType=type;
        var quadrantPosition = d.FirstOrDefault(u => SnapEx.Helper.GetQuadrantType(u, centerPosition, orientation) == quadrantType);

        var edges=block.Edges.ToList();
        baseFace.Edges.ToList().ForEach(u =>
        {
            edges.RemoveAll(e => e.StartPoint.Equals(u.StartPoint) && e.EndPoint.Equals(u.EndPoint));
        });

        foreach (var e in edges)
        {
            if (d.Contains(e.StartPoint) || d.Contains(e.EndPoint))
            {
                if (e.StartPoint.Equals(quadrantPosition) || e.EndPoint.Equals(quadrantPosition))
                {
                    //倒角
                    Create.Chamfer(e, daojiaoValue, true);
                }
                else
                {
                    //倒圆
                    Create.EdgeBlend(daoyuanValue, e);
                }
            }
        }

        //设置颜色
        block.Body.Color = bodyColor;
        bodys.ForEach(u => {
            u.Color = bodyColor;
        });

        var body = block.Body;

        var path = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Snap.Globals.WorkPart.FullPath), partName);
        if (System.IO.Directory.Exists(path))
        {
            System.IO.Directory.Delete(path,true);
        }
        System.IO.Directory.CreateDirectory(path);

        if (isExtract) 
        {
            for (int i = 0; i < snapFaces.Count; i++) 
            {
                var snapFace = snapFaces[i];
                snapFace.Name = SnapEx.ConstString.SelectFace;
                var tempBody = snapFace.Body;
                var tempFileName = System.IO.Path.Combine(path, string.Format("{0}-{1}", SnapEx.ConstString.AutoElec,i+1));
                tempBody.Name = System.IO.Path.GetFileNameWithoutExtension(tempFileName);
                SnapEx.Create.ExtractBody(new List<NXOpen.Body>() { tempBody }, tempFileName, false);
                //赋属性
                var info = new SnapEx.ElecInfo();
                info.QingGenValue = qingGenValue;
                info.DistanceValue = distanceValue;
                info.JizhutaiValue = jizhutaiValue;
                info.DaojiaoValue = daojiaoValue;
                info.DaoyuanValue = daoyuanValue;
                info.QuadrantType = (int)type;
                info.BodyColor = bodyColor.ToArgb();
                info.ElectName = partName;
                info.Serialize(path);
            }
        }

        //求和
        var result = Create.Unite(block.Body, bodys.ToArray());
        result.Orphan();

        if (isExtract) 
        {
            var fileName = System.IO.Path.Combine(path, string.Format("{0}", partName));
            SnapEx.Create.ExtractBody(new List<NXOpen.Body>() { body }, fileName, true);
            body.Delete();
        }

        return body;
    }



    /// <summary>
    /// 设置基准面
    /// </summary>
    static Snap.NX.Face SetBaseFace(Snap.NX.Block block,Vector normal) 
    {
        Snap.NX.Face face = null;
        foreach (var f in block.Faces)
        {
            var n1 = Vector.Unit(f.Normal(f.BoxUV.MinU, f.BoxUV.MinV));
            var n2 = Vector.Unit(normal);
            var angle = Vector.Angle(n1, n2);
            if (angle == 0)
            {
                f.Name = "BaseFace";
                f.Translucency = 50;
                face = f;
            }
            else if (angle == 180) 
            {
                f.Name = "BottomFace";
            }
        }
        return face;
    }

    static List<Snap.Position> GetBox2dPosition(List<Snap.NX.Face> faceSelecteds)
    {
        var snapFace = faceSelecteds.FirstOrDefault();
        var box = GetBox2d(faceSelecteds);
        var minUV = snapFace.Position(box.MinU, box.MinV);
        var maxUV = snapFace.Position(box.MaxU, box.MaxV);
        return new List<Snap.Position>() { minUV, maxUV };
    }

    static Snap.Geom.Box2d GetBox2d(List<Snap.NX.Face> faceSelecteds)
    {
        var box = new Snap.Geom.Box2d(0, 0, 0, 0);
        var us = new List<double>();
        var vs = new List<double>();
        var firstface = faceSelecteds.FirstOrDefault();
        faceSelecteds.ForEach(u =>
        {
            var face = u;
            var tempBox = face.BoxUV;
            var min = face.Position(tempBox.MinU, tempBox.MinV);
            var max = face.Position(tempBox.MaxU, tempBox.MaxV);
            var minUV = firstface.Parameters(min);
            var maxUV = firstface.Parameters(max);
            us.Add(minUV.First());
            us.Add(maxUV.First());
            vs.Add(minUV.Last());
            vs.Add(maxUV.Last());
        });
        box.MinU = us.Min();
        box.MaxU = us.Max();
        box.MinV = vs.Min();
        box.MaxV = vs.Max();
        return box;
    }
}