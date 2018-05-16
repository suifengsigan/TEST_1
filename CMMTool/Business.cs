using Snap;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SnapEx;

partial class CMMToolUI : SnapEx.BaseUI
{
    readonly string _probeDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CMM_INSPECTION", "ProbeData.json");
    const string _propertyName = "PROPERTYNAME";
    public override void Update(NXOpen.BlockStyler.UIBlock block)
    {
        if (block == btnNew) //新建
        {
            InitUI();
            tree_control0.SelectNodes(tree_control0.GetSelectedNodes(), false, true);
        }
        else if (block == btnSave) //保存
        {
            var data = GetProbeData();
            if (tree_control0.GetSelectedNodes().Count() > 0) //保存
            {
                tree_control0.GetSelectedNodes().ToList().ForEach(u => {
                    if (CreateProbe(data))
                    {
                        u.GetNodeData().SetString(_propertyName, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                    } 
                });
            }
            else //新建
            {
                //检测名称是否重复
                foreach (var node in GetNodes()) 
                {
                    if (node.DisplayText == data.ProbeName) 
                    {
                        System.Windows.Forms.MessageBox.Show("名字不能重复");
                        return;
                    }
                }

                if (CreateProbe(data))
                {
                    tree_control0.SelectNode(AddNode(data), true, true);
                }
            }
        }
        else if (block == btnDelete) //删除
        {
            tree_control0.GetSelectedNodes().ToList().ForEach(u =>
            {
                DeleteProbe(GetProbeData(u));
                tree_control0.DeleteNode(u);
            });

            InitUI();
        }

        var probeDatas = new List<ProbeData>();
        GetNodes().ForEach(u =>
        {
            probeDatas.Add(GetProbeData(u));
        });

        File.WriteAllText(_probeDataPath, Newtonsoft.Json.JsonConvert.SerializeObject(probeDatas));
    }

    public override void Init()
    {
        tree_control0.SetOnSelectHandler(new NXOpen.BlockStyler.Tree.OnSelectCallback(OnSelectcallback));
    }

    public override void DialogShown()
    {
        InitUI();
        tree_control0.InsertColumn(0, "探针名称", 200);

        tree_control0.SetColumnResizePolicy(0, NXOpen.BlockStyler.Tree.ColumnResizePolicy.ResizeWithTree);

        var probeDatas = new List<ProbeData>();

        if (File.Exists(_probeDataPath))
        {
            var json = File.ReadAllText(_probeDataPath);
            if (!string.IsNullOrEmpty(json))
            {
                probeDatas = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ProbeData>>(json);
                for (int i = 0; i < probeDatas.Count; i++)
                {
                    var data = probeDatas[i];
                    AddNode(data);
                }
            }
        }
    }

    void OnSelectcallback(NXOpen.BlockStyler.Tree tree, NXOpen.BlockStyler.Node node, int columnID, bool selected)
    {
        if (selected) 
        {
            var data = GetProbeData(node);
            bool isInit = false;
            strProbeName.Enable = isInit;
            btnDelete.Enable = !isInit;
            strProbeName.Value = node.DisplayText;
            expSphereRadius.Value = data.SphereRadius;
            expArrowRadius.Value = data.ArrowRadius;
            expArrowLength.Value = data.ArrowLength;
            expExtensionBarRadius.Value = data.ExtensionBarRadius;
            expExtensionBarLength.Value = data.ExtensionBarLength;
            expHeadRadius.Value = data.HeadRadius;
            expHeadLength.Value = data.HeadLength;
            strProbeAB.SetValue(new List<string>() { data.ProbeAB }.ToArray());
        }
    }

    List<NXOpen.BlockStyler.Node> GetNodes() 
    {
        var nodes = new List<NXOpen.BlockStyler.Node>();
        if (tree_control0.RootNode != null)
        {
            var node = tree_control0.RootNode;
            nodes.Add(node);

            while (node.NextNode != null)
            {
                node = node.NextNode;
                nodes.Add(node);
            }
        }
        return nodes;
    }

    NXOpen.BlockStyler.Node AddNode(ProbeData data) 
    {
        var node = tree_control0.CreateNode(data.ProbeName);
        node.GetNodeData().AddString(_propertyName, Newtonsoft.Json.JsonConvert.SerializeObject(data));
        tree_control0.InsertNode(node, null, null, NXOpen.BlockStyler.Tree.NodeInsertOption.AlwaysLast);
        return node;
    }

    void InitUI()
    {
        bool isInit = false;
        strProbeName.Enable = !isInit;
        btnDelete.Enable = isInit;
        strProbeName.Value = string.Empty;
        expSphereRadius.Value = 0;
        expArrowRadius.Value = 0;
        expArrowLength.Value = 0;
        expExtensionBarRadius.Value = 0;
        expExtensionBarLength.Value = 0;
        expHeadRadius.Value = 0;
        expHeadLength.Value = 0;
        strProbeAB.SetValue(new List<string>().ToArray());
    }

    /// <summary>
    /// 删除探针
    /// </summary>
    void DeleteProbe(ProbeData data) 
    {
        var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CMM_INSPECTION", data.ProbeName);
        if (File.Exists(fileName + ".prt"))
        {
            File.Delete(fileName + ".prt");
        }
    }

    /// <summary>
    /// 创建探针模型
    /// </summary>
    bool CreateProbe(ProbeData data)
    {
        bool result = true;
        var mark = Globals.SetUndoMark(Globals.MarkVisibility.Visible, "IsIntervene");
        try
        {
            Snap.NX.CoordinateSystem wcs = Globals.WorkPart.NXOpenPart.WCS.CoordinateSystem;
            var vector = wcs.AxisZ;
            var position = new Snap.Position(0, 0, data.SphereRadius);
            //创建探球
            var body = Snap.Create.Sphere(position, data.SphereRadius * 2).Body;
            body.IsHidden = true;
            body.Faces.ToList().ForEach(u =>
            {
                u.Name = SnapEx.ConstString.CMM_INSPECTION_SPHERE;
            });
            //创建测针
            var body1 = Snap.Create.Cylinder(position, position + (data.ArrowLength * vector), data.ArrowRadius * 2).Body;
            body1.IsHidden = true;
            position = position + (data.ArrowLength * vector);
            //创建加长杆
            var body2 = Snap.Create.Cylinder(position, position + (data.ExtensionBarLength * vector), data.ExtensionBarRadius * 2).Body;
            body2.IsHidden = true;
            //创建测头
            position = position + (data.ExtensionBarLength * vector);
            var body3 = Snap.Create.Cylinder(position, position + (data.HeadLength * vector), data.HeadRadius * 2).Body;
            body3.IsHidden = true;
            body3.Faces.ToList().ForEach(u =>
            {
                if (SnapEx.Helper.Equals(u.GetFaceDirection(), vector) && u.ObjectSubType == Snap.NX.ObjectTypes.SubType.FacePlane)
                {
                    u.Name = SnapEx.ConstString.CMM_INSPECTION_AXISPOINT;
                }
            });
            var r = Snap.Create.Unite(body, body1, body2, body3);
            r.Orphan();

            body.Name = data.ProbeName;

            var fileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CMM_INSPECTION", data.ProbeName);
            if (File.Exists(fileName + ".prt"))
            {
                File.Delete(fileName + ".prt");
            }
            var dir = Path.GetDirectoryName(fileName);
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            SnapEx.Create.ExtractBody(new List<NXOpen.Body> { body }, fileName, false, true);
        }
        catch (Exception ex)
        {
            theUI.NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, ex.Message);
            result = false;
        }
        Globals.UndoToMark(mark, null);
        return result;
    }

    ProbeData GetProbeData(NXOpen.BlockStyler.Node node) 
    {
        return  Newtonsoft.Json.JsonConvert.DeserializeObject<ProbeData>(node.GetNodeData().GetString(_propertyName));
    }

    ProbeData GetProbeData()
    {
        ProbeData data = new ProbeData();
        data.ProbeName = strProbeName.Value;
        data.SphereRadius = expSphereRadius.Value;
        data.ArrowRadius = expArrowRadius.Value;
        data.ArrowLength = expArrowLength.Value;
        data.ExtensionBarRadius = expExtensionBarRadius.Value;
        data.ExtensionBarLength = expExtensionBarLength.Value;
        data.HeadRadius = expHeadRadius.Value;
        data.HeadLength = expHeadLength.Value;
        data.ProbeAB = strProbeAB.GetValue().FirstOrDefault();
        return data;
    }
}

public class ProbeData
{
    /// <summary>
    /// 名称
    /// </summary>
    public string ProbeName { get; set; }
    /// <summary>
    /// 探针角度
    /// </summary>
    public string ProbeAB { get; set; }
    /// <summary>
    /// 球半径
    /// </summary>
    public double SphereRadius { get; set; }
    /// <summary>
    /// 测针半径
    /// </summary>
    public double ArrowRadius { get; set; }
    /// <summary>
    /// 测针长度
    /// </summary>
    public double ArrowLength { get; set; }
    /// <summary>
    /// 加长杆半径
    /// </summary>
    public double ExtensionBarRadius { get; set; }
    /// <summary>
    /// 加长杆长度
    /// </summary>
    public double ExtensionBarLength { get; set; }
    /// <summary>
    /// 测头半径
    /// </summary>
    public double HeadRadius { get; set; }
    /// <summary>
    /// 测头长度
    /// </summary>
    public double HeadLength { get; set; }
}
