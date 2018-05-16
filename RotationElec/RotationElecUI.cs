//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  F:\PHEactProject\Source\Project\UGUI\RotationElecUI.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: PENGHUI
//              Version: NX 9
//              Date: 01-11-2018  (Format: mm-dd-yyyy)
//              Time: 15:58 (Format: hh-mm)
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
public partial class RotationElecUI
{
    //class members
    private static Session theSession = null;
    private static UI theUI = null;
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private Snap.UI.Block.SelectObject selection0;// Block type: Selection
    private Snap.UI.Block.Toggle toggleJiaju;// Block type: Toggle
    private Snap.UI.Block.SelectObject selectionJiaju;// Block type: Selection
    private Snap.UI.Block.Enumeration enum01;// Block type: Enumeration
    private Snap.UI.Block.Group groupRotation;// Block type: Group
    private Snap.UI.Block.Enumeration enumRotation;// Block type: Enumeration
    private Snap.UI.Block.SpecifyPoint pointAxis;// Block type: Specify Point
    private Snap.UI.Block.SpecifyVector vector0;// Block type: Specify Vector
    private Snap.UI.Block.Expression expressionAngle;// Block type: Expression
    private Snap.UI.Block.Toggle toggleRotationMove;// Block type: Toggle
    private Snap.UI.Block.Toggle toggleRotationPatter;// Block type: Toggle
    private Snap.UI.Block.Group groupMove;// Block type: Group
    private Snap.UI.Block.Enumeration enum0;// Block type: Enumeration
    private Snap.UI.Block.SpecifyPoint pointStart;// Block type: Specify Point
    private Snap.UI.Block.SpecifyPoint pointEnd;// Block type: Specify Point
    private Snap.UI.Block.Enumeration enumSelectAxis;// Block type: Enumeration
    private Snap.UI.Block.Expression expressionDistance;// Block type: Expression
    private Snap.UI.Block.Expression expressionDistanceX;// Block type: Expression
    private Snap.UI.Block.Expression expressionDistanceY;// Block type: Expression
    private Snap.UI.Block.Expression expressionDistanceZ;// Block type: Expression
    private Snap.UI.Block.Toggle toggleMoveRotation;// Block type: Toggle
    private Snap.UI.Block.Toggle toggleMovePatter;// Block type: Toggle
    private Snap.UI.Block.Group group;// Block type: Group
    private Snap.UI.Block.SpecifyVector vectorPatter;// Block type: Specify Vector
    private Snap.UI.Block.Expression expressionPatterSum;// Block type: Expression
    private Snap.UI.Block.Expression expressionPatterDistance;// Block type: Expression
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
    public static readonly int SnapPointTypesOnByDefault_UserDefined = (1 << 0);
    public static readonly int SnapPointTypesOnByDefault_Inferred = (1 << 1);
    public static readonly int SnapPointTypesOnByDefault_ScreenPosition = (1 << 2);
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
    public static readonly int SnapPointTypesOnByDefault_TwocurveIntersection = (1 << 13);
    public static readonly int SnapPointTypesOnByDefault_TangentPoint = (1 << 14);
    public static readonly int SnapPointTypesOnByDefault_Poles = (1 << 15);
    public static readonly int SnapPointTypesOnByDefault_BoundedGridPoint = (1 << 16);
    public static readonly int SnapPointTypesOnByDefault_FacetVertexPoint = (1 << 17);

    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public RotationElecUI()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theDlxFileName = "RotationElecUI.dlx";
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
    public static void Main()
    {
        RotationElecUI theRotationElecUI = null;
        try
        {
            theRotationElecUI = new RotationElecUI();
            // The following method shows the dialog immediately
            theRotationElecUI.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            if (theRotationElecUI != null)
                theRotationElecUI.Dispose();
            theRotationElecUI = null;
        }
    }
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
            selection0 = Snap.UI.Block.SelectObject.GetBlock(theDialog,"selection0");
            toggleJiaju = Snap.UI.Block.Toggle.GetBlock(theDialog, "toggleJiaju");
            selectionJiaju = Snap.UI.Block.SelectObject.GetBlock(theDialog, "selectionJiaju");
            enum01 = Snap.UI.Block.Enumeration.GetBlock(theDialog, "enum01");
            groupRotation = Snap.UI.Block.Group.GetBlock(theDialog,"groupRotation");
            enumRotation = Snap.UI.Block.Enumeration.GetBlock(theDialog,"enumRotation");
            pointAxis = Snap.UI.Block.SpecifyPoint.GetBlock(theDialog,"pointAxis");
            vector0 = Snap.UI.Block.SpecifyVector.GetBlock(theDialog,"vector0");
            expressionAngle = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionAngle");
            toggleRotationMove = Snap.UI.Block.Toggle.GetBlock(theDialog,"toggleRotationMove");
            toggleRotationPatter = Snap.UI.Block.Toggle.GetBlock(theDialog,"toggleRotationPatter");
            groupMove = Snap.UI.Block.Group.GetBlock(theDialog,"groupMove");
            enum0 = Snap.UI.Block.Enumeration.GetBlock(theDialog,"enum0");
            pointStart = Snap.UI.Block.SpecifyPoint.GetBlock(theDialog,"pointStart");
            pointEnd = Snap.UI.Block.SpecifyPoint.GetBlock(theDialog,"pointEnd");
            enumSelectAxis = Snap.UI.Block.Enumeration.GetBlock(theDialog,"enumSelectAxis");
            expressionDistance = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionDistance");
            expressionDistanceX = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionDistanceX");
            expressionDistanceY = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionDistanceY");
            expressionDistanceZ = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionDistanceZ");
            toggleMoveRotation = Snap.UI.Block.Toggle.GetBlock(theDialog,"toggleMoveRotation");
            toggleMovePatter = Snap.UI.Block.Toggle.GetBlock(theDialog,"toggleMovePatter");
            group = Snap.UI.Block.Group.GetBlock(theDialog, "group");
            vectorPatter = Snap.UI.Block.SpecifyVector.GetBlock(theDialog,"vectorPatter");
            expressionPatterSum = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionPatterSum");
            expressionPatterDistance = Snap.UI.Block.Expression.GetBlock(theDialog,"expressionPatterDistance");
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
    public int update_cb(NXOpen.BlockStyler.UIBlock block)
    {
        try
        {
            //if (block == selection0)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == toggleJiaju)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == selectionJiaju)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == enum01)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == enumRotation)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == pointAxis)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == vector0)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionAngle)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == toggleRotationMove)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == toggleRotationPatter)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == enum0)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == pointStart)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == pointEnd)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == enumSelectAxis)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionDistance)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionDistanceX)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionDistanceY)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionDistanceZ)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == toggleMoveRotation)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == toggleMovePatter)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == vectorPatter)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionPatterSum)
            //{
            //    //---------Enter your code here-----------
            //}
            //else if (block == expressionPatterDistance)
            //{
            //    //---------Enter your code here-----------
            //}
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        return 0;
    }

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
