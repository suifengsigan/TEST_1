using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Snap;
using NXOpen.Features;
using NXOpen;
using NXOpen.Drawings;

namespace SnapEx
{
    public static class Create
    {
        /// <summary>
        /// 获取投影面积
        /// </summary>
        /// <param name="face"></param>
        /// <param name="max_facet_size"></param>
        /// <returns></returns>
        public static double GetProjectionArea(Snap.NX.Face face, double max_facet_size = 1)
        {
            var area = 0.0;
            try
            {
                var positions = new List<Snap.Position>();
                #region old code
                var parameters = new NXOpen.UF.UFFacet.Parameters();

                var ufSession = NXOpen.UF.UFSession.GetUFSession();
                var facet = ufSession.Facet;
                facet.AskDefaultParameters(out parameters);
                parameters.max_facet_edges = 3;
                parameters.specify_max_facet_size = true;
                parameters.max_facet_size = max_facet_size;

                NXOpen.Tag facet_model = NXOpen.Tag.Null;
                facet.FacetSolid(face.NXOpenTag, ref parameters, out facet_model);

                if (facet_model == NXOpen.Tag.Null) return area;
                NXOpen.Tag solid = NXOpen.Tag.Null;
                facet.AskSolidOfModel(facet_model, out solid);
                if (solid != face.NXOpenTag) return area;

                int facet_id = NXOpen.UF.UFConstants.UF_FACET_NULL_FACET_ID;
                bool isWhile = true;
                while (isWhile)
                {
                    facet.CycleFacets(facet_model, ref facet_id);
                    if (facet_id != NXOpen.UF.UFConstants.UF_FACET_NULL_FACET_ID)
                    {
                        int num_vertices = 0;
                        facet.AskNumVertsInFacet(facet_model, facet_id, out num_vertices);
                        if (num_vertices == 3)
                        {
                            var vertices = new double[num_vertices, 3];
                            facet.AskVerticesOfFacet(facet_model, facet_id, out num_vertices, vertices);
                            double[] vecZ = { 0, 0, 1 };
                            var projectorZAxis = new List<double[]>();
                            projectorZAxis.Add(new double[] { 0, 0, 0 });
                            projectorZAxis.Add(new double[] { 0, 0, 0 });
                            projectorZAxis.Add(new double[] { 0, 0, 0 });
                            double[] projectorZLength = { 0, 0, 0 };
                            var verticesList = new List<double[]>();
                            for (int x = 0; x < 3; x++)
                            {
                                var dList = new List<double>();
                                for (int y = 0; y < 3; y++)
                                {
                                    dList.Add(vertices[x, y]);
                                }
                                verticesList.Add(dList.ToArray());
                            }
                            for (int i = 0; i < num_vertices; i++)
                            {
                                var vertice = verticesList[i];
                                var zAxis = projectorZAxis[i];
                                ufSession.Vec3.Dot(vertice, vecZ, out projectorZLength[i]);
                                ufSession.Vec3.Scale(projectorZLength[i], vecZ, zAxis);
                                ufSession.Vec3.Sub(vertice, zAxis, vertice);
                            }
                            double[] vec1 = { 0, 0, 0 }, vec2 = { .0, .0, .0 }, cross_vec = { .0, .0, .0 };
                            var tempFaceArea = 0.0;
                            ufSession.Vec3.Sub(verticesList[1], verticesList[0], vec1);
                            ufSession.Vec3.Sub(verticesList[2], verticesList[0], vec2);
                            ufSession.Vec3.Cross(vec1, vec2, cross_vec);
                            ufSession.Vec3.Mag(cross_vec, out tempFaceArea);
                            area += tempFaceArea;
                        }
                    }
                    else
                    {
                        isWhile = false;
                    }
                }

                ufSession.Obj.DeleteObject(facet_model);
                positions = positions.Distinct().ToList();
                #endregion
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                area = 0;
            }
            return area;
        }

        public static void ExportStp(Snap.NX.Body body, string path, Func<Snap.Geom.Transform> func = null, params Snap.Geom.Transform[] transfroms)
        {
            var fileName = string.Format("{0}{1}", path, ".stp");
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Invisible, "ExportStp");
            try
            {
                if (body.IsOccurrence)
                {
                    transfroms.ToList().ForEach(u =>
                    {
                        var trans = u.Matrix;
                        Matrix3x3 matrix = new Matrix3x3();
                        matrix.Xx = trans[0]; matrix.Xy = trans[4]; matrix.Xz = trans[8];
                        matrix.Yx = trans[1]; matrix.Yy = trans[5]; matrix.Yz = trans[9];
                        matrix.Zx = trans[2]; matrix.Zy = trans[6]; matrix.Zz = trans[10];
                        workPart.ComponentAssembly.MoveComponent(body.OwningComponent, new Vector3d(trans[3], trans[7], trans[11]), matrix);
                    });
                }
                else
                {
                    var trans = Snap.Geom.Transform.CreateTranslation();
                    transfroms.ToList().ForEach(u =>
                    {
                        trans = Snap.Geom.Transform.Composition(trans, u);
                    });
                    body.Move(trans);
                    if (func != null)
                    {
                        body.Move(func());
                    }
                }

                SnapEx.Create.ExportStp(new List<NXObject> { body }, path);
            }
            catch (Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, ex.Message);
            }

            Snap.Globals.UndoToMark(mark, null);
        }

        public static void ExportPrt(Snap.NX.Body body, string path,Func<Snap.Geom.Transform> func=null, params Snap.Geom.Transform[] transfroms) 
        {
            var fileName = string.Format("{0}{1}", path, ".prt");
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Invisible, "ExportPrt");
            try
            {
                if (body.IsOccurrence)
                {
                    transfroms.ToList().ForEach(u =>
                    {
                        var trans = u.Matrix;
                        Matrix3x3 matrix = new Matrix3x3();
                        matrix.Xx = trans[0]; matrix.Xy = trans[4]; matrix.Xz = trans[8];
                        matrix.Yx = trans[1]; matrix.Yy = trans[5]; matrix.Yz = trans[9];
                        matrix.Zx = trans[2]; matrix.Zy = trans[6]; matrix.Zz = trans[10];
                        workPart.ComponentAssembly.MoveComponent(body.OwningComponent, new Vector3d(trans[3], trans[7], trans[11]), matrix);
                    });
                }
                else
                {
                    var trans = Snap.Geom.Transform.CreateTranslation();
                    transfroms.ToList().ForEach(u =>
                    {
                        trans = Snap.Geom.Transform.Composition(trans, u);
                    });
                    body.Move(trans);
                    if (func != null)
                    {
                        body.Move(func());
                    }
                }

                NXOpen.UF.UFSession.GetUFSession().Csys.SetOrigin(Snap.Globals.Wcs.NXOpenTag, Snap.Position.Origin.Array);
                Snap.Globals.WcsOrientation = Snap.Orientation.Identity;

                NXOpen.UF.UFSession.GetUFSession().Part.Export(path, 1, new Tag[] { body.NXOpenTag });
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Snap.Globals.UndoToMark(mark, null);
            }
        }
        public static void ExportStp(Snap.NX.Body body, string path, params Snap.Geom.Transform[] transfroms) 
        {
            var fileName = string.Format("{0}{1}", path, ".stp");
            if (System.IO.File.Exists(fileName))
            {
                System.IO.File.Delete(fileName);
            }
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            var mark = Snap.Globals.SetUndoMark(Snap.Globals.MarkVisibility.Invisible, "ExportStp");
            try 
            {
                if (body.IsOccurrence)
                {
                    transfroms.ToList().ForEach(u =>
                    {
                        var trans = u.Matrix;
                        Matrix3x3 matrix = new Matrix3x3();
                        matrix.Xx = trans[0]; matrix.Xy = trans[4]; matrix.Xz = trans[8];
                        matrix.Yx = trans[1]; matrix.Yy = trans[5]; matrix.Yz = trans[9];
                        matrix.Zx = trans[2]; matrix.Zy = trans[6]; matrix.Zz = trans[10];
                        workPart.ComponentAssembly.MoveComponent(body.OwningComponent, new Vector3d(trans[3], trans[7], trans[11]), matrix);
                    });
                }
                else 
                {
                    var trans = Snap.Geom.Transform.CreateTranslation();
                    transfroms.ToList().ForEach(u =>
                    {
                        trans = Snap.Geom.Transform.Composition(trans, u);
                    });
                    body.Move(trans);
                }

                SnapEx.Create.ExportStp(new List<NXObject> { body }, path);
            }
            catch(Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("提示", NXOpen.NXMessageBox.DialogType.Information, ex.Message);
            }
           
            Snap.Globals.UndoToMark(mark, null);
        }

        public static void ExportStp(string inFileName,string outFileName)
        {
            //var UGII_BASE_DIR = System.Environment.GetEnvironmentVariable("UGII_BASE_DIR");
            string fileName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            var dir = System.IO.Path.GetDirectoryName(fileName);
            var info = new System.IO.DirectoryInfo(dir);
            var UGII_BASE_DIR = info.Parent.FullName;
            string str = string.Format("\"{0}\\step214ug\\step214ug.exe\" \"{1}\" \"o={2}\" \"d={3}\\step214ug\\ugstep214.def\""
                , UGII_BASE_DIR
                , inFileName
                , outFileName
                , UGII_BASE_DIR
                );

            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.UseShellExecute = false;    //是否使用操作系统shell启动
            p.StartInfo.RedirectStandardInput = true;//接受来自调用程序的输入信息
            p.StartInfo.RedirectStandardOutput = true;//由调用程序获取输出信息
            p.StartInfo.RedirectStandardError = true;//重定向标准错误输出
            p.StartInfo.CreateNoWindow = true;//不显示程序窗口
            p.Start();//启动程序

            //向cmd窗口发送输入信息
            p.StandardInput.WriteLine(str + "&exit");

            p.StandardInput.AutoFlush = true;
            //p.StandardInput.WriteLine("exit");
            //向标准输入写入要执行的命令。这里使用&是批处理命令的符号，表示前面一个命令不管是否执行成功都执行后面(exit)命令，如果不执行exit命令，后面调用ReadToEnd()方法会假死
            //同类的符号还有&&和||前者表示必须前一个命令执行成功才会执行后面的命令，后者表示必须前一个命令执行失败才会执行后面的命令



            //获取cmd窗口的输出信息
            string output = p.StandardOutput.ReadToEnd();

            //StreamReader reader = p.StandardOutput;
            //string line=reader.ReadLine();
            //while (!reader.EndOfStream)
            //{
            //    str += line + "  ";
            //    line = reader.ReadLine();
            //}

            p.WaitForExit();//等待程序执行完退出进程
            p.Close();


            Console.WriteLine(output);
        }

        public static void ExportStp(List<NXOpen.NXObject> list, string path)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            Step214Creator step214Creator1;
            step214Creator1 = theSession.DexManager.CreateStep214Creator();

            step214Creator1.OutputFile = path;

            //step214Creator1.SettingsFile = "I:\\UG\\NX 9.0-64bit\\step214ug\\ugstep214.def";

            step214Creator1.ObjectTypes.Solids = true;

            step214Creator1.ExportSelectionBlock.SelectionScope = ObjectSelector.Scope.SelectedObjects;
            list.ForEach(u =>
            {
                step214Creator1.ExportSelectionBlock.SelectionComp.Add(u);
            });

            step214Creator1.FileSaveFlag =false;
            step214Creator1.ValidationProperties = false;

            step214Creator1.LayerMask = "1-256";

            NXObject nXObject1;
            nXObject1 = step214Creator1.Commit();

            step214Creator1.Destroy();
        }
        public static void ApplicationSwitchRequest(string application) 
        {
#if !NET35
            UI.GetUI().MenuBarManager.ApplicationSwitchRequest(application);
#endif
        }

#if !NET35
        /// <summary>
        /// 创建文本
        /// </summary>
        public static void Text() 
        {
            //只需要对其中 Point3d origin放置原点的位置，和Vector3d xDirection与Vector3d yDirection两个方向的向量值进行传递即可快速创建文本
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            var textBuilder1 = workPart.Features.CreateTextBuilder(null);

            textBuilder1.FrameOnPath.AnchorPosition.Expression.Value = 50;
            textBuilder1.Script = TextBuilder.ScriptOptions.Western;
            textBuilder1.PlanarFrame.AnchorLocation = NXOpen.GeometricUtilities.RectangularFrameBuilder.AnchorLocationType.BottomCenter;
            textBuilder1.PlanarFrame.Length.Value = 5;

            //设置字体的高度
            textBuilder1.PlanarFrame.Height.Value = 2;

            //设置长宽比例                
            textBuilder1.PlanarFrame.WScale = 100.0;
            textBuilder1.PlanarFrame.Shear.Value = 0;
            textBuilder1.FrameOnPath.AnchorPosition.Expression.Value = 50;
            textBuilder1.FrameOnPath.Offset.Value = 0;
            textBuilder1.FrameOnPath.Length.Value = 1;
            textBuilder1.FrameOnPath.Height.Value = 2;

            //设置字体
            textBuilder1.SelectFont("Arial", TextBuilder.ScriptOptions.Western);

            //设置要刻的文本                
            textBuilder1.TextString = "NX90";

            NXObject nXObject1;
            nXObject1 = textBuilder1.Commit();
            textBuilder1.Destroy();
            
        }
#endif

        /// <summary>
        /// 对象显示调整
        /// </summary>
        /// <param name="objects"></param>
        public static void DisplayModification(List<DisplayableObject> objects) 
        {
            var theSession = NXOpen.Session.GetSession();
            var workPart = theSession.Parts.Work;
            NXOpen.DisplayModification displayModification1 = theSession.DisplayManager.NewDisplayModification();
            displayModification1.ApplyToAllFaces = true;
            displayModification1.NewTranslucency = 70;
            displayModification1.Apply(objects.ToArray());
            workPart.ModelingViews.WorkView.RenderingStyle = View.RenderingStyleType.ShadedWithEdges;
        }

#if !NET35

        /// <summary>
        /// 阵列组件
        /// </summary>
        public static NXOpen.Assemblies.ComponentPattern ComponentPattern(Snap.NX.Component component, Snap.Vector vector, double copiesCount, double pitchDistance) 
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            NXOpen.Assemblies.ComponentPattern nullAssemblies_ComponentPattern = null;
            NXOpen.Assemblies.ComponentPatternBuilder componentPatternBuilder1;
            componentPatternBuilder1 = workPart.ComponentAssembly.CreateComponentPatternBuilder(nullAssemblies_ComponentPattern);
            componentPatternBuilder1.ComponentPatternSet.Add(component);

            //矢量
            Point3d origin3 = new Point3d(0.0, 0.0, 0.0);
            Direction direction2;
            direction2 = workPart.Directions.CreateDirection(origin3,vector, NXOpen.SmartObject.UpdateOption.WithinModeling);
            componentPatternBuilder1.PatternService.RectangularDefinition.XDirection = direction2;

            //间距
            componentPatternBuilder1.PatternService.RectangularDefinition.XSpacing.NCopies.Value = copiesCount;
            componentPatternBuilder1.PatternService.RectangularDefinition.XSpacing.PitchDistance.Value = pitchDistance;

            nullAssemblies_ComponentPattern = componentPatternBuilder1.Commit() as NXOpen.Assemblies.ComponentPattern;
            componentPatternBuilder1.Destroy();

            return nullAssemblies_ComponentPattern;
        }
#endif

        /// <summary>
        /// 移动对象
        /// </summary>
        public static void MoveObject(NXOpen.NXObject obj,Snap.Position point,Snap.Vector normal , double distance)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            NXOpen.Features.MoveObjectBuilder moveObjectBuilder1;
            moveObjectBuilder1 = workPart.BaseFeatures.CreateMoveObjectBuilder(null);
            moveObjectBuilder1.ObjectToMoveObject.Add(obj);
            moveObjectBuilder1.TransformMotion.DistanceVector = workPart.Directions.CreateDirection(point, normal,SmartObject.UpdateOption.WithinModeling);
            moveObjectBuilder1.TransformMotion.DistanceValue.Value = distance;
            moveObjectBuilder1.MoveObjectResult = NXOpen.Features.MoveObjectBuilder.MoveObjectResultOptions.MoveOriginal;
            moveObjectBuilder1.TransformMotion.Option = NXOpen.GeometricUtilities.ModlMotion.Options.Distance;
            moveObjectBuilder1.Commit();
            moveObjectBuilder1.Destroy();
        }

        /// <summary>
        /// 移除参数
        /// </summary>
        public static void RemoveParameters(List<NXOpen.Body> bodies) 
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            NXOpen.Features.RemoveParametersBuilder removeParametersBuilder1;
            removeParametersBuilder1 = workPart.Features.CreateRemoveParametersBuilder();
            bodies.ForEach(b =>
            {
                removeParametersBuilder1.Objects.Add(b);
            });

            NXObject nXObject1;
            nXObject1 = removeParametersBuilder1.Commit();
            removeParametersBuilder1.Destroy();
        }

        public static Body Geomcopy(NXOpen.Body body, NXOpen.Plane plane) 
        {
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            return Geomcopy(workPart,body, plane);
        }

        /// <summary>
        /// 创建镜像几何体
        /// </summary>
        public static Body Geomcopy(NXOpen.Part workPart,NXOpen.Body body, NXOpen.Plane plane) 
        {
            NXOpen.Features.Feature nullFeatures_Feature = null;
            NXOpen.Features.GeomcopyBuilder geomcopyBuilder1;
            geomcopyBuilder1 = workPart.Features.CreateGeomcopyBuilder(nullFeatures_Feature);

            //选择对象
            geomcopyBuilder1.GeometryToInstance.Add(body);

            //选择平面
            geomcopyBuilder1.MirrorPlane = plane;

            geomcopyBuilder1.Type = NXOpen.Features.GeomcopyBuilder.TransformTypes.Mirror;

            Geomcopy feature1;
            feature1 = geomcopyBuilder1.CommitFeature() as Geomcopy;
            geomcopyBuilder1.Destroy();
            var b=feature1.GetBodies().FirstOrDefault();
            
            RemoveParameters(new List<Body> { b });
            return b;
        }

        /// <summary>
        /// 简单干涉
        /// </summary>
        public static NXOpen.GeometricAnalysis.SimpleInterference.Result SimpleInterference(NXOpen.TaggedObject firstBody, NXOpen.TaggedObject secondBody) 
        {
            var workPart = NXOpen.Session.GetSession().Parts.Work;
            NXOpen.GeometricAnalysis.SimpleInterference simpleInterference1;
            simpleInterference1 = workPart.AnalysisManager.CreateSimpleInterferenceObject();
            simpleInterference1.InterferenceType = NXOpen.GeometricAnalysis.SimpleInterference.InterferenceMethod.InterferenceSolid;
            simpleInterference1.FaceInterferenceType = NXOpen.GeometricAnalysis.SimpleInterference.FaceInterferenceMethod.FirstPairOnly;
            simpleInterference1.FirstBody.Value = firstBody;
            simpleInterference1.SecondBody.Value = secondBody;

            NXOpen.GeometricAnalysis.SimpleInterference.Result result1;
            result1 = simpleInterference1.PerformCheck();

            NXOpen.NXObject nXObject1;
            nXObject1 = simpleInterference1.Commit();

            simpleInterference1.Destroy();

            return result1;
        }

        /// <summary>
        /// 新建图纸页
        /// </summary>
        public static DrawingSheet DrawingSheet(string name,string template) 
        {
            var workPart = Session.GetSession().Parts.Work;
            DrawingSheet nullDrawingSheet = null;
            var builder = workPart.DrawingSheets.DrawingSheetBuilder(nullDrawingSheet);
            //模板位置(设置模板（可选择）)
            builder.MetricSheetTemplateLocation = template;
            builder.Name = name;

            nullDrawingSheet = builder.Commit() as DrawingSheet;
            builder.Destroy();
            return nullDrawingSheet;
        }

        /// <summary>
        /// 创建基本视图
        /// </summary>
        /// <returns></returns>
        public static BaseView BaseView(NXOpen.Part part,Point2d point) 
        {
            var workPart = Session.GetSession().Parts.Work;
            var modelView = workPart.ModelingViews.FindObject("FRONT");
            var point3d = new Point3d(point.X, point.Y, 0);
            BaseView baseView1 = null;
            var baseViewBuilder1 = workPart.DraftingViews.CreateBaseViewBuilder(baseView1);
            //设置模型视图
            baseViewBuilder1.SelectModelView.SelectedView = modelView;
            //设置工作部件
            baseViewBuilder1.Style.ViewStyleBase.Part = part;
            //定位
            baseViewBuilder1.Placement.Placement.SetValue(null, workPart.Views.WorkView, point3d);
            baseView1 = baseViewBuilder1.Commit() as BaseView;
            baseViewBuilder1.Destroy();
            return baseView1;
        }

        /// <summary>
        /// 创建投影视图
        /// </summary>
        public static ProjectedView ProjectedView(DraftingView view,Point2d point) 
        {
            var workPart = Session.GetSession().Parts.Work;
            ProjectedView nullDrawings_ProjectedView = null;
            var projectedViewBuilder1 = workPart.DraftingViews.CreateProjectedViewBuilder(nullDrawings_ProjectedView);
            //设置基本视图
            projectedViewBuilder1.Parent.View.Value = view;
            //定位
            var p = new Point3d(point.X, point.Y, 0.0);
            projectedViewBuilder1.Placement.Placement.SetValue(null, workPart.Views.WorkView, p);

            nullDrawings_ProjectedView = projectedViewBuilder1.Commit() as ProjectedView;
            projectedViewBuilder1.Destroy();

            return nullDrawings_ProjectedView;
        }

        /// <summary>
        /// 析出体
        /// </summary>
        public static void ExtractBody(List<NXOpen.Body> bodies, string fileName) 
        {
            ExtractBody(bodies, fileName,false);
        }

        public static void ExtractBody(List<NXOpen.Body> bodies, string fileName, bool isAddComponent, bool newPart = true) 
        {
            ExtractObject(Enumerable.Select(bodies, m => m as NXOpen.NXObject).ToList(), fileName, isAddComponent, newPart);
        }

        /// <summary>
        /// 析出体
        /// </summary>
        public static NXOpen.Assemblies.Component ExtractObject(List<NXOpen.NXObject> bodies, string fileName, bool isAddComponent, bool newPart = true, Snap.Geom.Transform trans = null,Snap.Position basePoint=new Snap.Position(),Snap.Orientation orientation=null) 
        {
            NXOpen.Assemblies.Component component = null;
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;
            if (newPart) 
            {
                if (System.IO.File.Exists(fileName)) System.IO.File.Delete(fileName);
            }
            var objs = bodies;
            var tags = new List<NXOpen.Tag>();
            objs.ForEach(u =>
            {
                tags.Add(u.Tag);
            });

            //TODO 后期需优化
            NXOpen.UF.UFPart.ExportOptions options = new NXOpen.UF.UFPart.ExportOptions();
            options.new_part = newPart;
            options.params_mode = NXOpen.UF.UFPart.ExportParamsMode.RemoveParams;
            options.expression_mode = NXOpen.UF.UFPart.ExportExpMode.CopyExpDeeply;
            NXOpen.UF.UFSession.GetUFSession().Part.ExportWithOptions(
                fileName,
                tags.Count, tags.ToArray(),
                ref options
                );

            if (trans != null) 
            {
                PartLoadStatus partLoadStatus1;
                var basePart = theSession.Parts.OpenBase(fileName, out partLoadStatus1) as Part;
                if (basePart != null)
                {
                    Snap.NX.Part snapBasePart = basePart;
                    var partAllObjs = new List<Snap.NX.NXObject>();
                    snapBasePart.Bodies.ToList().ForEach(u=>{
                        partAllObjs.Add(u);
                    });

                    snapBasePart.Lines.ToList().ForEach(u => {
                        partAllObjs.Add(u);
                    });

                    snapBasePart.Points.ToList().ForEach(u => {
                        partAllObjs.Add(u);
                    });
                   
                    partAllObjs.ForEach(u =>
                    {
                        u.Move(trans);
                    });
                    snapBasePart.Save();
                    snapBasePart.Close(false, true);
                }
            }

            if (isAddComponent)
            {
                PartLoadStatus partLoadStatus1;
                //后期需优化
                component=workPart.ComponentAssembly.AddComponent(fileName, SnapEx.ConstString.ReferenceSetName, System.IO.Path.GetFileNameWithoutExtension(fileName), basePoint, orientation ?? new Orientation(), -1, out partLoadStatus1, true);
            }

            return component;
        }

        /// <summary>
        /// 创建包容块
        /// </summary>
        public static Snap.NX.Block Box(List<Face> faces, double positiveX, double negativeX, double positiveY, double negativeY, double positiveZ, double negativeZ)
        {
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            var box = GetBox3d(faces);
            box = new Snap.Geom.Box3d(box.MinX - negativeX, box.MinY - negativeY, box.MinZ - negativeZ, box.MaxX + positiveX, box.MaxY + positiveY, box.MaxZ + positiveZ);
            var block = Snap.Create.Block(box.MinXYZ, box.MaxX - box.MinX, box.MaxY - box.MinY, box.MaxZ - box.MinZ);
            return block;
        }

        static Snap.Geom.Box3d GetBox3d(List<Face> faces)
        {
            double minX = 0, maxX = 0, minY = 0, maxY = 0, minZ = 0, maxZ = 0;
            if (faces.Count > 0)
            {
                Snap.NX.Face snapFace = faces.FirstOrDefault();
                var box = snapFace.Box;
                minX = box.MinX;
                maxX = box.MaxX;
                minY = box.MinY;
                maxY = box.MaxY;
                minZ = box.MinZ;
                maxZ = box.MaxZ;

                faces.ForEach(u =>
                {
                    Snap.NX.Face face = u;
                    var b = face.Box;
                    if (minX > b.MinX)
                    {
                        minX = b.MinX;
                    }

                    if (minY > b.MinY)
                    {
                        minY = b.MinY;
                    }

                    if (minZ > b.MinZ)
                    {
                        minZ = b.MinZ;
                    }

                    if (maxX < b.MaxX)
                    {
                        maxX = b.MaxX;
                    }

                    if (maxY < b.MaxY)
                    {
                        maxY = b.MaxY;
                    }

                    if (maxZ < b.MaxZ)
                    {
                        maxZ = b.MaxZ;
                    }
                });
            }

            return new Snap.Geom.Box3d(minX, minY, minZ, maxX, maxY, maxZ);
        }


        /// <summary>
        /// 创建拉伸体
        /// </summary>
        public static Extrude Extrude(Snap.NX.Face face, double height)
        {
            var curves = face.EdgeCurves;
            Session theSession = Session.GetSession();
            Part workPart = theSession.Parts.Work;
            Part displayPart = theSession.Parts.Display;

            var normal = face.Normal(face.BoxUV.MinU, face.BoxUV.MinV);
            var point = face.Position(face.BoxUV.MinU, face.BoxUV.MinV);

            var curveList = new List<NXOpen.Curve>();
            curves.ToList().ForEach(u =>
            {
                curveList.Add(u);
            });

            //创建拉伸
            NXOpen.Features.Feature nullFeatures_Feature = null;
            ExtrudeBuilder extrudeBuilder1;
            extrudeBuilder1 = workPart.Features.CreateExtrudeBuilder(nullFeatures_Feature);

            //方向
            extrudeBuilder1.Direction = workPart.Directions.CreateDirection(point,normal,SmartObject.UpdateOption.WithinModeling);

            //曲面
            Section section1;
            section1 = workPart.Sections.CreateSection();
            extrudeBuilder1.Section = section1;
            var curveDumbRule1 = workPart.ScRuleFactory.CreateRuleCurveDumb(curveList.ToArray());
            SelectionIntentRule[] rules1 = new SelectionIntentRule[1];
            rules1[0] = curveDumbRule1;
            section1.AddToSection(rules1, curves.FirstOrDefault(), null, null, new Point3d(), NXOpen.Section.Mode.Create);

            //开始值、结束值
            extrudeBuilder1.Limits.StartExtend.Value.Value = 0;
            extrudeBuilder1.Limits.EndExtend.Value.Value = height;

            Extrude feature1;
            feature1 = (Extrude)extrudeBuilder1.CommitFeature();
            extrudeBuilder1.Destroy();
            return feature1;
        }
    }
  
    public class ApplicationType
    {
        /// <summary>
        /// 建模
        /// </summary>
        public const string MODELING = "UG_APP_MODELING";
        /// <summary>
        /// 加工
        /// </summary>
        public const string MANUFACTURING = "UG_APP_MANUFACTURING";
        /// <summary>
        /// 检测
        /// </summary>
        public const string INSPECTION = "UG_APP_INSPECTION";
    }
}
