//==============================================================================
//  WARNING!!  This file is overwritten by the Block Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\PHEactProject\Source\Project\UGUI\CMMProgramUI.hpp
//
//        This file was generated by the NX Block Styler
//        Created by: PENGHUI
//              Version: NX 6
//              Date: 08-06-2018  (Format: mm-dd-yyyy)
//              Time: 13:53
//
//==============================================================================

#ifndef CMMPROGRAMUI_H_INCLUDED
#define CMMPROGRAMUI_H_INCLUDED

//------------------------------------------------------------------------------
//These includes are needed for the following template code
//------------------------------------------------------------------------------
#include <uf_defs.h>
#include <uf_ui_types.h>
#include <iostream>
#include <NXOpen/Session.hxx>
#include <NXOpen/UI.hxx>
#include <NXOpen/NXMessageBox.hxx>
#include <NXOpen/Callback.hxx>
#include <NXOpen/NXException.hxx>
#include <NXOpen/BlockStyler_UIBlock.hxx>
#include <NXOpen/BlockStyler_BlockDialog.hxx>
#include <NXOpen/BlockStyler_PropertyList.hxx>
//------------------------------------------------------------------------------
//Bit Option for Property: SnapPointTypesEnabled
//------------------------------------------------------------------------------
#define              SnapPointTypesEnabled_UserDefined (1 << 0);
#define                 SnapPointTypesEnabled_Inferred (1 << 1);
#define           SnapPointTypesEnabled_ScreenPosition (1 << 2);
#define                 SnapPointTypesEnabled_EndPoint (1 << 3);
#define                 SnapPointTypesEnabled_MidPoint (1 << 4);
#define             SnapPointTypesEnabled_ControlPoint (1 << 5);
#define             SnapPointTypesEnabled_Intersection (1 << 6);
#define                SnapPointTypesEnabled_ArcCenter (1 << 7);
#define            SnapPointTypesEnabled_QuadrantPoint (1 << 8);
#define            SnapPointTypesEnabled_ExistingPoint (1 << 9);
#define             SnapPointTypesEnabled_PointonCurve (1 <<10);
#define           SnapPointTypesEnabled_PointonSurface (1 <<11);
#define         SnapPointTypesEnabled_PointConstructor (1 <<12);
#define     SnapPointTypesEnabled_TwocurveIntersection (1 <<13);
#define             SnapPointTypesEnabled_TangentPoint (1 <<14);
#define                    SnapPointTypesEnabled_Poles (1 <<15);
//------------------------------------------------------------------------------
//Bit Option for Property: SnapPointTypesOnByDefault
//------------------------------------------------------------------------------
#define             SnapPointTypesOnByDefault_EndPoint (1 << 3);
#define             SnapPointTypesOnByDefault_MidPoint (1 << 4);
#define         SnapPointTypesOnByDefault_ControlPoint (1 << 5);
#define         SnapPointTypesOnByDefault_Intersection (1 << 6);
#define            SnapPointTypesOnByDefault_ArcCenter (1 << 7);
#define        SnapPointTypesOnByDefault_QuadrantPoint (1 << 8);
#define        SnapPointTypesOnByDefault_ExistingPoint (1 << 9);
#define         SnapPointTypesOnByDefault_PointonCurve (1 <<10);
#define       SnapPointTypesOnByDefault_PointonSurface (1 <<11);
#define     SnapPointTypesOnByDefault_PointConstructor (1 <<12);
//------------------------------------------------------------------------------
// Namespaces needed for following template
//------------------------------------------------------------------------------
using namespace std;
using namespace NXOpen;
using namespace NXOpen::BlockStyler;

//------------------------------------------------------------------------------
// Forward declaration for Class
//------------------------------------------------------------------------------
class CMMProgramUI;
//
//------------------------------------------------------------------------------
// Declaration of global variables
//------------------------------------------------------------------------------
extern CMMProgramUI *theCMMProgramUI;

class CMMProgramUI
{
    // class members
public:
    static Session *theSession;
    static UI *theUI;
    CMMProgramUI();
    ~CMMProgramUI();
    int Show();
    
    //----------------------- BlockStyler Callback Prototypes ---------------------
    // The following member function prototypes define the callbacks 
    // specified in your BlockStyler dialog.  The empty implementaiton
    // of these prototypes is provided in the CMMProgramUI.cpp file. 
    // You are REQUIRED to write the implementation for these funtions.
    //------------------------------------------------------------------------------
    void initialize_cb();
    void dialogShown_cb();
    int apply_cb();
    int update_cb(NXOpen::BlockStyler::UIBlock* block);
    
private:
    std::string theDialogName;
    NXOpen::BlockStyler::BlockDialog* theDialog;
    NXOpen::BlockStyler::UIBlock* group0;// Block type: Group
    NXOpen::BlockStyler::UIBlock* selectCuprum;// Block type: Selection
    NXOpen::BlockStyler::UIBlock* enumSelectTool;// Block type: Enumeration
    NXOpen::BlockStyler::UIBlock* group;// Block type: Group
    NXOpen::BlockStyler::UIBlock* drawingArea0;// Block type: Drawing Area
    NXOpen::BlockStyler::UIBlock* toggle0;// Block type: Toggle
    NXOpen::BlockStyler::UIBlock* btnAutoSelectPoint;// Block type: Button
    NXOpen::BlockStyler::UIBlock* selectionPoint;// Block type: Selection
    NXOpen::BlockStyler::UIBlock* tree_control0;// Block type: Tree Control
    NXOpen::BlockStyler::UIBlock* btnRemove;// Block type: Button
    NXOpen::BlockStyler::UIBlock* btnUP;// Block type: Button
    NXOpen::BlockStyler::UIBlock* btnDown;// Block type: Button
    NXOpen::BlockStyler::UIBlock* btnExport;// Block type: Button
    
};
#endif //CMMPROGRAMUI_H_INCLUDED