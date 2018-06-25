﻿//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\PHEactProject\Source\Project\UGUI\SelectSteelUI.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: PENGHUI
//              Version: NX 9
//              Date: 03-14-2018  (Format: mm-dd-yyyy)
//              Time: 13:33 (Format: hh-mm)
//
//==============================================================================

//==============================================================================
//  Purpose:  This TEMPLATE file contains C# source to guide you in the
//  construction of your Block application dialog. The generation of your
//  dialog file (.dlx extension) is the first step towards dialog construction
//  within NX.  You must now create a NX Open application that
//  utilizes this file (.dlx).
//
//  The information in this file provides you with the following:
//
//  1.  Help on how to load and display your Block UI Styler dialog in NX
//      using APIs provided in NXOpen.BlockStyler namespace
//  2.  The empty callback methods (stubs) associated with your dialog items
//      have also been placed in this file. These empty methods have been
//      created simply to start you along with your coding requirements.
//      The method name, argument list and possible return values have already
//      been provided for you.
//==============================================================================

//------------------------------------------------------------------------------
//These imports are needed for the following template code
//------------------------------------------------------------------------------
using System;
using NXOpen;
using NXOpen.BlockStyler;

//------------------------------------------------------------------------------
//Represents Block Styler application class
//------------------------------------------------------------------------------
public partial class SelectSteelUI
{
    //class members
    private static Session theSession = null;
    private static UI theUI = null;
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private Snap.UI.Block.Group group1;// Block type: Group
    private Snap.UI.Block.SelectObject selectSteel;// Block type: Selection
    private Snap.UI.Block.Group group;// Block type: Group
    private Snap.UI.Block.String sMODELNUMBER;// Block type: String
    private Snap.UI.Block.String sMRNUMBER;// Block type: String
    private Snap.UI.Block.Enumeration eMATERAL;// Block type: Enumeration
    private Snap.UI.Block.Enumeration enum0;// Block type: Enumeration
    private Snap.UI.Block.SpecifyCsys coord_system0;// Block type: Specify Csys
    private Snap.UI.Block.Enumeration enumSelectedXX;// Block type: Enumeration
    private Snap.UI.Block.Toggle toggleSInsert;// Block type: Toggle
    private Snap.UI.Block.SelectObject selectionSInsert;// Block type: Selection
    //------------------------------------------------------------------------------
    //Bit Option for Property: SnapPointTypesEnabled
    //------------------------------------------------------------------------------
    public static readonly int SnapPointTypesEnabled_UserDefined = (1 << 0);
    public static readonly int SnapPointTypesEnabled_Inferred = (1 << 1);
    public static readonly int SnapPointTypesEnabled_ScreenPosition = (1 << 2);
    public static readonly int SnapPointTypesEnabled_EndPoint = (1 << 3);
    public static readonly int SnapPointTypesEnabled_MidPoint = (1 << 4);
    public static readonly int SnapPointTypesEnabled_ControlPoint = (1 << 5);
    public static readonly int SnapPointTypesEnabled_Intersection = (1 << 6);
    public static readonly int SnapPointTypesEnabled_ArcCenter = (1 << 7);
    public static readonly int SnapPointTypesEnabled_QuadrantPoint = (1 << 8);
    public static readonly int SnapPointTypesEnabled_ExistingPoint = (1 << 9);
    public static readonly int SnapPointTypesEnabled_PointonCurve = (1 << 10);
    public static readonly int SnapPointTypesEnabled_PointonSurface = (1 << 11);
    public static readonly int SnapPointTypesEnabled_PointConstructor = (1 << 12);
    public static readonly int SnapPointTypesEnabled_TwocurveIntersection = (1 << 13);
    public static readonly int SnapPointTypesEnabled_TangentPoint = (1 << 14);
    public static readonly int SnapPointTypesEnabled_Poles = (1 << 15);
    public static readonly int SnapPointTypesEnabled_BoundedGridPoint = (1 << 16);
    public static readonly int SnapPointTypesEnabled_FacetVertexPoint = (1 << 17);
    //------------------------------------------------------------------------------
    //Bit Option for Property: SnapPointTypesOnByDefault
    //------------------------------------------------------------------------------
    public static readonly int SnapPointTypesOnByDefault_EndPoint = (1 << 3);
    public static readonly int SnapPointTypesOnByDefault_MidPoint = (1 << 4);
    public static readonly int SnapPointTypesOnByDefault_ControlPoint = (1 << 5);
    public static readonly int SnapPointTypesOnByDefault_Intersection = (1 << 6);
    public static readonly int SnapPointTypesOnByDefault_ArcCenter = (1 << 7);
    public static readonly int SnapPointTypesOnByDefault_QuadrantPoint = (1 << 8);
    public static readonly int SnapPointTypesOnByDefault_ExistingPoint = (1 << 9);
    public static readonly int SnapPointTypesOnByDefault_PointonCurve = (1 << 10);
    public static readonly int SnapPointTypesOnByDefault_PointonSurface = (1 << 11);
    public static readonly int SnapPointTypesOnByDefault_PointConstructor = (1 << 12);
    public static readonly int SnapPointTypesOnByDefault_BoundedGridPoint = (1 << 16);

    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public SelectSteelUI()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theDlxFileName = "SelectSteelUI.dlx";
            InitEvent(theDlxFileName, initialize_cb, (s) =>
            {
                return theDialog = theUI.CreateDialog(s);
            });
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            throw ex;
        }
    }
    //------------------------------- DIALOG LAUNCHING ---------------------------------
    //
    //    Before invoking this application one needs to open any part/empty part in NX
    //    because of the behavior of the blocks.
    //
    //    Make sure the dlx file is in one of the following locations:
    //        1.) From where NX session is launched
    //        2.) $UGII_USER_DIR/application
    //        3.) For released applications, using UGII_CUSTOM_DIRECTORY_FILE is highly
    //            recommended. This variable is set to a full directory path to a file 
    //            containing a list of root directories for all custom applications.
    //            e.g., UGII_CUSTOM_DIRECTORY_FILE=$UGII_ROOT_DIR\menus\custom_dirs.dat
    //
    //    You can create the dialog using one of the following way:
    //
    //    1. Journal Replay
    //
    //        1) Replay this file through Tool->Journal->Play Menu.
    //
    //    2. USER EXIT
    //
    //        1) Create the Shared Library -- Refer "Block UI Styler programmer's guide"
    //        2) Invoke the Shared Library through File->Execute->NX Open menu.
    //
    //------------------------------------------------------------------------------
    //public static void Main()
    //{
    //    SelectSteelUI theSelectSteelUI = null;
    //    try
    //    {
    //        theSelectSteelUI = new SelectSteelUI();
    //        // The following method shows the dialog immediately
    //        theSelectSteelUI.Show();
    //    }
    //    catch (Exception ex)
    //    {
    //        //---- Enter your exception handling code here -----
    //        theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
    //    }
    //    finally
    //    {
    //        if (theSelectSteelUI != null)
    //            theSelectSteelUI.Dispose();
    //        theSelectSteelUI = null;
    //    }
    //}
    //------------------------------------------------------------------------------
    // This method specifies how a shared image is unloaded from memory
    // within NX. This method gives you the capability to unload an
    // internal NX Open application or user  exit from NX. Specify any
    // one of the three constants as a return value to determine the type
    // of unload to perform:
    //
    //
    //    Immediately : unload the library as soon as the automation program has completed
    //    Explicitly  : unload the library from the "Unload Shared Image" dialog
    //    AtTermination : unload the library when the NX session terminates
    //
    //
    // NOTE:  A program which associates NX Open applications with the menubar
    // MUST NOT use this option since it will UNLOAD your NX Open application image
    // from the menubar.
    //------------------------------------------------------------------------------
    public static int GetUnloadOption(string arg)
    {
        //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
        return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
        // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
    }

    //------------------------------------------------------------------------------
    // Following method cleanup any housekeeping chores that may be needed.
    // This method is automatically called by NX.
    //------------------------------------------------------------------------------
    public static void UnloadLibrary(string arg)
    {
        try
        {
            //---- Enter your code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    //------------------------------------------------------------------------------
    //This method shows the dialog on the screen
    //------------------------------------------------------------------------------
    public NXOpen.UIStyler.DialogResponse Show()
    {
        try
        {
            theDialog.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }

    //------------------------------------------------------------------------------
    //Method Name: Dispose
    //------------------------------------------------------------------------------
    public void Dispose()
    {
        if (theDialog != null)
        {
            theDialog.Dispose();
            theDialog = null;
        }
    }

    //------------------------------------------------------------------------------
    //---------------------Block UI Styler Callback Functions--------------------------
    //------------------------------------------------------------------------------

    //------------------------------------------------------------------------------
    //Callback Name: initialize_cb
    //------------------------------------------------------------------------------
    public void initialize_cb()
    {
        try
        {
            group1 = Snap.UI.Block.Group.GetBlock(theDialog, "group1");
            selectSteel = Snap.UI.Block.SelectObject.GetBlock(theDialog, "selectSteel");
            group = Snap.UI.Block.Group.GetBlock(theDialog, "group");
            sMODELNUMBER = Snap.UI.Block.String.GetBlock(theDialog, "sMODELNUMBER");
            sMRNUMBER = Snap.UI.Block.String.GetBlock(theDialog, "sMRNUMBER");
            eMATERAL = Snap.UI.Block.Enumeration.GetBlock(theDialog, "eMATERAL");
            enumSelectedXX = Snap.UI.Block.Enumeration.GetBlock(theDialog, "enumSelectedXX");
            enum0 = Snap.UI.Block.Enumeration.GetBlock(theDialog, "enum0");
            coord_system0 = Snap.UI.Block.SpecifyCsys.GetBlock(theDialog, "coord_system0");
            toggleSInsert = Snap.UI.Block.Toggle.GetBlock(theDialog, "toggleSInsert");
            selectionSInsert = Snap.UI.Block.SelectObject.GetBlock(theDialog, "selectionSInsert");
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    //------------------------------------------------------------------------------
    //Callback Name: dialogShown_cb
    //This callback is executed just before the dialog launch. Thus any value set 
    //here will take precedence and dialog will be launched showing that value. 
    //------------------------------------------------------------------------------
    public void dialogShown_cb()
    {
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
    }

    //------------------------------------------------------------------------------
    //Callback Name: apply_cb
    //------------------------------------------------------------------------------
    public int apply_cb()
    {
        int errorCode = 0;
        try
        {
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }

    //------------------------------------------------------------------------------
    //Callback Name: update_cb  
    //------------------------------------------------------------------------------
    //public int update_cb(NXOpen.BlockStyler.UIBlock block)
    //{
    //    try
    //    {
    //        if (block == selectSteel)
    //        {
    //            //---------Enter your code here-----------
    //        }
    //        else if (block == sMODELNUMBER)
    //        {
    //            //---------Enter your code here-----------
    //        }
    //        else if (block == sMRNUMBER)
    //        {
    //            //---------Enter your code here-----------
    //        }
    //        else if (block == eMATERAL)
    //        {
    //            //---------Enter your code here-----------
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        //---- Enter your exception handling code here -----
    //        theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
    //    }
    //    return 0;
    //}

    //------------------------------------------------------------------------------
    //Callback Name: ok_cb
    //------------------------------------------------------------------------------
    public int ok_cb()
    {
        int errorCode = 0;
        try
        {
            errorCode = apply_cb();
            //---- Enter your callback code here -----
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            errorCode = 1;
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return errorCode;
    }

    //------------------------------------------------------------------------------
    //Function Name: GetBlockProperties
    //Returns the propertylist of the specified BlockID
    //------------------------------------------------------------------------------
    public PropertyList GetBlockProperties(string blockID)
    {
        PropertyList plist = null;
        try
        {
            plist = theDialog.GetBlockProperties(blockID);
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return plist;
    }

}
