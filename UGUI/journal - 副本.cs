// NX 9.0.0.19
// Journal created by PENGHUI on Mon Jan 29 14:22:08 2018 中国标准时间
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
    //   菜单：插入->尺寸->Ordinate...
    // ----------------------------------------------
    NXOpen.Session.UndoMarkId markId1;
    markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "开始");
    
    NXOpen.Annotations.OrdinateDimension nullAnnotations_OrdinateDimension = null;
    NXOpen.Annotations.OrdinateDimensionBuilder ordinateDimensionBuilder1;
    ordinateDimensionBuilder1 = workPart.Dimensions.CreateOrdinateDimensionBuilder(nullAnnotations_OrdinateDimension);
    
    ordinateDimensionBuilder1.Baseline.ActivateBaseline = true;
    
    ordinateDimensionBuilder1.Origin.SetInferRelativeToGeometry(false);
    
    ordinateDimensionBuilder1.Origin.Anchor = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter;
    
    theSession.SetUndoMarkName(markId1, "坐标尺寸 对话框");
    
    ordinateDimensionBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
    
    ordinateDimensionBuilder1.Origin.SetInferRelativeToGeometry(false);
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits1;
    dimensionlinearunits1 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits2;
    dimensionlinearunits2 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits3;
    dimensionlinearunits3 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits4;
    dimensionlinearunits4 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits5;
    dimensionlinearunits5 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits6;
    dimensionlinearunits6 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    NXOpen.Annotations.DimensionUnit dimensionlinearunits7;
    dimensionlinearunits7 = ordinateDimensionBuilder1.Style.UnitsStyle.DimensionLinearUnits;
    
    ordinateDimensionBuilder1.Origin.SetInferRelativeToGeometry(false);
    
    ordinateDimensionBuilder1.Origin.SetInferRelativeToGeometry(false);
    
    ordinateDimensionBuilder1.Style.DimensionStyle.NarrowDisplayType = NXOpen.Annotations.NarrowDisplayOption.None;
    
    NXOpen.Drawings.DraftingBody draftingBody1 = (NXOpen.Drawings.DraftingBody)workPart.DraftingViews.FindObject("Front@48").DraftingBodies.FindObject("0 UNPARAMETERIZED_FEATURE(2)  0");
    NXOpen.Drawings.DraftingCurve draftingCurve1 = (NXOpen.Drawings.DraftingCurve)draftingBody1.DraftingCurves.FindObject("(Extracted Edge) EDGE * 1 * 51 {(-96,-79.9999999999999,1)(-2,-79.9999999999999,1)(92,-79.9999999999999,1) UNPARAMETERIZED_FEATURE(2)}");
    NXOpen.Drawings.BaseView baseView1 = (NXOpen.Drawings.BaseView)workPart.DraftingViews.FindObject("Front@48");
    Point3d point1_1 = new Point3d(-2.0, -79.9999999999999, 1.0);
    View nullView = null;
    Point3d point2_1 = new Point3d(0.0, 0.0, 0.0);
    ordinateDimensionBuilder1.OrdinateOrigin.SetValue(NXOpen.InferSnapType.SnapType.Mid, draftingCurve1, baseView1, point1_1, null, nullView, point2_1);
    
    NXOpen.Annotations.OrdinateMargin nullAnnotations_OrdinateMargin = null;
    ordinateDimensionBuilder1.ActiveHorizontalMargin = nullAnnotations_OrdinateMargin;
    
    ordinateDimensionBuilder1.ActiveVerticalMargin = nullAnnotations_OrdinateMargin;
    
    NXOpen.Annotations.OrdinateOriginDimension ordinateOriginDimension1 = (NXOpen.Annotations.OrdinateOriginDimension)workPart.FindObject("ENTITY 26 1 1");
    int nErrs1;
    nErrs1 = theSession.UpdateManager.AddToDeleteList(ordinateOriginDimension1);
    
    ordinateDimensionBuilder1.Destroy();
    
    theSession.UndoToMark(markId1, null);
    
    theSession.DeleteUndoMark(markId1, null);
    
    // ----------------------------------------------
    //   菜单：工具->操作记录->停止录制
    // ----------------------------------------------
    
  }
  public static int GetUnloadOption(string dummy) { return (int)Session.LibraryUnloadOption.Immediately; }
}
