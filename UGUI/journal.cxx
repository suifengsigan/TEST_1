// NX 9.0.0.19
// Journal created by PENGHUI on Thu May 24 11:20:06 2018 中国标准时间
//
#include <uf_defs.h>
#include <NXOpen/NXException.hxx>
#include <NXOpen/Session.hxx>
#include <NXOpen/BasePart.hxx>
#include <NXOpen/CartesianCoordinateSystem.hxx>
#include <NXOpen/CoordinateSystemCollection.hxx>
#include <NXOpen/Expression.hxx>
#include <NXOpen/ExpressionCollection.hxx>
#include <NXOpen/Offset.hxx>
#include <NXOpen/OffsetCollection.hxx>
#include <NXOpen/Part.hxx>
#include <NXOpen/PartCollection.hxx>
#include <NXOpen/Point.hxx>
#include <NXOpen/PointCollection.hxx>
#include <NXOpen/Scalar.hxx>
#include <NXOpen/ScalarCollection.hxx>
#include <NXOpen/Session.hxx>
#include <NXOpen/SmartObject.hxx>
#include <NXOpen/Unit.hxx>
#include <NXOpen/UnitCollection.hxx>
#include <NXOpen/Update.hxx>
#include <NXOpen/Xform.hxx>
#include <NXOpen/XformCollection.hxx>
using namespace NXOpen;

extern "C" DllExport int ufusr_ask_unload()
{
    return (int)Session::LibraryUnloadOptionImmediately;
}

extern "C" DllExport void ufusr(char *param, int *retCode, int paramLen)
{
    Session *theSession = Session::GetSession();
    Part *workPart(theSession->Parts()->Work());
    Part *displayPart(theSession->Parts()->Display());
    // ----------------------------------------------
    //   Menu: File->Export->Part...
    // ----------------------------------------------
    // ----------------------------------------------
    //   Dialog Begin ѡ�񲿼�
    // ----------------------------------------------
    // ----------------------------------------------
    //   Dialog Begin Export Part
    // ----------------------------------------------
    Session::UndoMarkId markId1;
    markId1 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, NXString("\345\274\200\345\247\213", NXString::UTF8));
    
    theSession->SetUndoMarkName(markId1, NXString("\347\261\273\351\200\211\346\213\251 \345\257\271\350\257\235\346\241\206", NXString::UTF8));
    
    // ----------------------------------------------
    //   Dialog Begin Class Selection
    // ----------------------------------------------
    Session::UndoMarkId markId2;
    markId2 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, NXString("\347\261\273\351\200\211\346\213\251", NXString::UTF8));
    
    theSession->DeleteUndoMark(markId2, NULL);
    
    Session::UndoMarkId markId3;
    markId3 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, NXString("\347\261\273\351\200\211\346\213\251", NXString::UTF8));
    
    theSession->DeleteUndoMark(markId3, NULL);
    
    theSession->SetUndoMarkName(markId1, NXString("\347\261\273\351\200\211\346\213\251", NXString::UTF8));
    
    theSession->DeleteUndoMark(markId1, NULL);
    
    // ----------------------------------------------
    //   Dialog Begin Export Part
    // ----------------------------------------------
    Session::UndoMarkId markId4;
    markId4 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, NXString("\345\274\200\345\247\213", NXString::UTF8));
    
    workPart = theSession->Parts()->Work();
    Unit *unit1(dynamic_cast<Unit *>(workPart->UnitCollection()->FindObject("MilliMeter")));
    Expression *expression1;
    expression1 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit1);
    
    Unit *unit2(dynamic_cast<Unit *>(workPart->UnitCollection()->FindObject("Degrees")));
    Expression *expression2;
    expression2 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit2);
    
    Expression *expression3;
    expression3 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit1);
    
    Expression *expression4;
    expression4 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit2);
    
    Expression *expression5;
    expression5 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit1);
    
    Expression *expression6;
    expression6 = workPart->Expressions()->CreateSystemExpressionWithUnits("0", unit2);
    
    expression1->SetRightHandSide("0");
    
    expression3->SetRightHandSide("0");
    
    expression5->SetRightHandSide("0");
    
    expression2->SetRightHandSide("0");
    
    expression4->SetRightHandSide("0");
    
    expression6->SetRightHandSide("0");
    
    expression1->SetRightHandSide("0");
    
    expression3->SetRightHandSide("0");
    
    expression5->SetRightHandSide("0");
    
    expression2->SetRightHandSide("0");
    
    expression4->SetRightHandSide("0");
    
    expression6->SetRightHandSide("0");
    
    theSession->SetUndoMarkName(markId4, NXString("CSYS \345\257\271\350\257\235\346\241\206", NXString::UTF8));
    
    // ----------------------------------------------
    //   Dialog Begin CSYS
    // ----------------------------------------------
    Point3d origin1(0.0, 0.0, 0.0);
    Matrix3x3 orientation1;
    orientation1.Xx = 1.0;
    orientation1.Xy = 0.0;
    orientation1.Xz = 0.0;
    orientation1.Yx = 0.0;
    orientation1.Yy = 1.0;
    orientation1.Yz = 0.0;
    orientation1.Zx = 0.0;
    orientation1.Zy = 0.0;
    orientation1.Zz = 1.0;
    CartesianCoordinateSystem *cartesianCoordinateSystem1;
    cartesianCoordinateSystem1 = workPart->CoordinateSystems()->CreateCoordinateSystem(origin1, orientation1, true);
    
    Scalar *scalar1;
    scalar1 = workPart->Scalars()->CreateScalarExpression(expression1, Scalar::DimensionalityTypeLength, SmartObject::UpdateOptionAfterModeling);
    
    Scalar *scalar2;
    scalar2 = workPart->Scalars()->CreateScalarExpression(expression3, Scalar::DimensionalityTypeLength, SmartObject::UpdateOptionAfterModeling);
    
    Scalar *scalar3;
    scalar3 = workPart->Scalars()->CreateScalarExpression(expression5, Scalar::DimensionalityTypeLength, SmartObject::UpdateOptionAfterModeling);
    
    Scalar *scalar4;
    scalar4 = workPart->Scalars()->CreateScalarExpression(expression2, Scalar::DimensionalityTypeAngle, SmartObject::UpdateOptionAfterModeling);
    
    Scalar *scalar5;
    scalar5 = workPart->Scalars()->CreateScalarExpression(expression4, Scalar::DimensionalityTypeAngle, SmartObject::UpdateOptionAfterModeling);
    
    Scalar *scalar6;
    scalar6 = workPart->Scalars()->CreateScalarExpression(expression6, Scalar::DimensionalityTypeAngle, SmartObject::UpdateOptionAfterModeling);
    
    Offset *offset1;
    offset1 = workPart->Offsets()->CreateOffsetRectangular(scalar1, scalar2, scalar3, SmartObject::UpdateOptionAfterModeling);
    
    Offset *nullOffset(NULL);
    Xform *xform1;
    xform1 = workPart->Xforms()->CreateXform(cartesianCoordinateSystem1, nullOffset, offset1, scalar4, scalar5, scalar6, 0, SmartObject::UpdateOptionAfterModeling, 1.0);
    
    Session::UndoMarkId markId5;
    markId5 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, "CSYS");
    
    theSession->DeleteUndoMark(markId5, NULL);
    
    Session::UndoMarkId markId6;
    markId6 = theSession->SetUndoMark(Session::MarkVisibilityInvisible, "CSYS");
    
    CartesianCoordinateSystem *cartesianCoordinateSystem2;
    cartesianCoordinateSystem2 = workPart->CoordinateSystems()->CreateCoordinateSystem(xform1, SmartObject::UpdateOptionAfterModeling);
    
    xform1->RemoveParameters();
    
    int nErrs1;
    nErrs1 = theSession->UpdateManager()->AddToDeleteList(cartesianCoordinateSystem1);
    
    theSession->DeleteUndoMark(markId6, NULL);
    
    theSession->SetUndoMarkName(markId4, "CSYS");
    
    theSession->DeleteUndoMark(markId4, NULL);
    
    int nErrs2;
    nErrs2 = theSession->UpdateManager()->AddToDeleteList(cartesianCoordinateSystem2);
    
    // ----------------------------------------------
    //   Menu: Tools->Journal->Stop Recording
    // ----------------------------------------------
}
