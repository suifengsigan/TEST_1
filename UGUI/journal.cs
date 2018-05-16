// NX 9.0.0.19
// Journal created by PENGHUI on Mon Apr 23 15:35:54 2018 中国标准时间
//
using System;
using NXOpen;

public class NXJournal
{
  public static void Main(string[] args)
  {
    Session theSession = Session.GetSession();
    Part workPart = theSession.Parts.Work;
    Part displayPart = theSession.Parts.Display;
    // ----------------------------------------------
    //   菜单：工具->重复命令->2 计算面积
    // ----------------------------------------------
    // ----------------------------------------------
    //   菜单：工具->特定于工艺->注塑模向导->注塑模工具->Calculate Area...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");
    
    NXOpen.Tooling.CalculateAreaBuilder calculateAreaBuilder1;
    calculateAreaBuilder1 = workPart.ToolingManager.CalculateAreas.CreateBuilder();
    
    Point3d origin1 = new Point3d(0.0, 0.0, 0.0);
    Vector3d normal1 = new Vector3d(0.0, 0.0, 1.0);
    Plane plane1;
    plane1 = workPart.Planes.CreatePlane(origin1, normal1, NXOpen.SmartObject.UpdateOption.WithinModeling);
    
    calculateAreaBuilder1.PlaneDefine = plane1;
    
    Unit unit1;
    unit1 = calculateAreaBuilder1.DimTolerance.Units;
    
    Expression expression1;
    expression1 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    Expression expression2;
    expression2 = workPart.Expressions.CreateSystemExpressionWithUnits("0", unit1);
    
    calculateAreaBuilder1.DimTolerance.RightHandSide = "0.1";
    
    calculateAreaBuilder1.AngularAccuracy.RightHandSide = "1";
    
    theSession.SetUndoMarkName(markId1, "计算面积 对话框");
    
    plane1.SetMethod(NXOpen.PlaneTypes.MethodType.Coincident);
    
    Body body1 = (Body)workPart.Bodies.FindObject("BLOCK(28)");
    bool added1;
    added1 = calculateAreaBuilder1.SelectionTarget.Add(body1);
    
    calculateAreaBuilder1.PlaneDefine = plane1;
    
    plane1.SetMethod(NXOpen.PlaneTypes.MethodType.Coincident);
    
    plane1.SetMethod(NXOpen.PlaneTypes.MethodType.Coincident);
    
    NXObject[] geom1 = new NXObject[1];
    NXOpen.Features.Brep brep1 = (NXOpen.Features.Brep)workPart.Features.FindObject("UNPARAMETERIZED_FEATURE(0)");
    Face face1 = (Face)brep1.FindObject("FACE 19 {(0.0003095624247,-80.0000041545987,-19.2730391767314) UNPARAMETERIZED_FEATURE(0)}");
    geom1[0] = face1;
    plane1.SetGeometry(geom1);
    
    plane1.SetAlternate(NXOpen.PlaneTypes.AlternateType.One);
    
    plane1.Evaluate();
    
    calculateAreaBuilder1.PlaneDefine = plane1;
    
    NXOpen.Session.UndoMarkId markId2;
    markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "计算面积");
    
    theSession.DeleteUndoMark(markId2, null);
    
    NXOpen.Session.UndoMarkId markId3;
    markId3 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "计算面积");
    
    NXOpen.Session.UndoMarkId markId4;
    markId4 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Calculate Area");
    
    NXOpen.Session.UndoMarkId id1;
    id1 = theSession.NewestVisibleUndoMark;
    
    NXObject[] objects1 = new NXObject[1];
    NXOpen.Facet.FacetedBody facetedBody1 = (NXOpen.Facet.FacetedBody)workPart.FacetedBodies.FindObject("ENTITY 139 1 1");
    objects1[0] = facetedBody1;
    int nErrs1;
    nErrs1 = theSession.UpdateManager.AddToDeleteList(objects1);
    
    int nErrs2;
    nErrs2 = theSession.UpdateManager.DoUpdate(id1);
    
    theSession.DeleteUndoMarksUpToMark(markId4, "Calculate Area", true);
    
    NXObject nXObject1;
    nXObject1 = calculateAreaBuilder1.Commit();
    
    theSession.DeleteUndoMark(markId3, null);
    
    theSession.SetUndoMarkName(id1, "计算面积");
    
    calculateAreaBuilder1.Destroy();
    
    try
    {
      // 表达式仍然在使用中。
      workPart.Expressions.Delete(expression2);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    try
    {
      // 表达式仍然在使用中。
      workPart.Expressions.Delete(expression1);
    }
    catch (NXException ex)
    {
      ex.AssertErrorCode(1050029);
    }
    
    plane1.DestroyPlane();
    
    // ----------------------------------------------
    //   菜单：工具->操作记录->停止录制
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)Session.LibraryUnloadOption.Immediately; }
}
