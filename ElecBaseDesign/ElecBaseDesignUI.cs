//==============================================================================
//  WARNING!!  This file is overwritten by the Block UI Styler while generating
//  the automation code. Any modifications to this file will be lost after
//  generating the code again.
//
//       Filename:  H:\PHEactProject\Source\Project\UGUI\ElecBaseDesignUI.cs
//
//        This file was generated by the NX Block UI Styler
//        Created by: PENGHUI
//              Version: NX 9
//              Date: 10-10-2017  (Format: mm-dd-yyyy)
//              Time: 10:13 (Format: hh-mm)
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
public partial class ElecBaseDesignUI
{
    //class members
    private static Session theSession = null;
    private static UI theUI = null;
    private string theDlxFileName;
    private NXOpen.BlockStyler.BlockDialog theDialog;
    private NXOpen.BlockStyler.FaceCollector face_select0;// Block type: Face Collector
    private NXOpen.BlockStyler.Group group;// Block type: Group
    private NXOpen.BlockStyler.StringBlock elecName;// Block type: String
    private NXOpen.BlockStyler.ExpressionBlock expressionQingGen;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionDistance;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionJizhutai;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionDaojiao;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionDaoyuan;// Block type: Expression
    private NXOpen.BlockStyler.Toggle toggleAutoInteger;// Block type: Toggle
    private NXOpen.BlockStyler.ExpressionBlock expressionX;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionY;// Block type: Expression
    private NXOpen.BlockStyler.ExpressionBlock expressionZ;// Block type: Expression
    private NXOpen.BlockStyler.Group group1;// Block type: Group
    private NXOpen.BlockStyler.Enumeration enum0;// Block type: Enumeration
    //------------------------------------------------------------------------------
    //Bit Option for Property: EntityType
    //------------------------------------------------------------------------------
    public static readonly int EntityType_AllowFaces = (1 << 4);
    public static readonly int EntityType_AllowDatums = (1 << 5);
    public static readonly int EntityType_AllowBodies = (1 << 6);
    //------------------------------------------------------------------------------
    //Bit Option for Property: FaceRules
    //------------------------------------------------------------------------------
    public static readonly int FaceRules_SingleFace = (1 << 0);
    public static readonly int FaceRules_RegionFaces = (1 << 1);
    public static readonly int FaceRules_TangentFaces = (1 << 2);
    public static readonly int FaceRules_TangentRegionFaces = (1 << 3);
    public static readonly int FaceRules_BodyFaces = (1 << 4);
    public static readonly int FaceRules_FeatureFaces = (1 << 5);
    public static readonly int FaceRules_AdjacentFaces = (1 << 6);
    public static readonly int FaceRules_ConnectedBlendFaces = (1 << 7);
    public static readonly int FaceRules_AllBlendFaces = (1 << 8);
    public static readonly int FaceRules_RibFaces = (1 << 9);
    public static readonly int FaceRules_SlotFaces = (1 << 10);
    public static readonly int FaceRules_BossandPocketFaces = (1 << 11);
    public static readonly int FaceRules_MergedRibFaces = (1 << 12);
    public static readonly int FaceRules_RegionBoundaryFaces = (1 << 13);
    public static readonly int FaceRules_FaceandAdjacentFaces = (1 << 14);

    //------------------------------------------------------------------------------
    //Constructor for NX Styler class
    //------------------------------------------------------------------------------
    public ElecBaseDesignUI()
    {
        try
        {
            theSession = Session.GetSession();
            theUI = UI.GetUI();
            theDlxFileName = "ElecBaseDesignUI.dlx";
            theDialog = theUI.CreateDialog(theDlxFileName);
            InitEvent(theDialog, initialize_cb);
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
        ElecBaseDesignUI theElecBaseDesignUI = null;
        try
        {
            theElecBaseDesignUI = new ElecBaseDesignUI();
            // The following method shows the dialog immediately
            theElecBaseDesignUI.Show();
        }
        catch (Exception ex)
        {
            //---- Enter your exception handling code here -----
            theUI.NXMessageBox.Show("Block Styler", NXMessageBox.DialogType.Error, ex.ToString());
        }
        finally
        {
            if (theElecBaseDesignUI != null)
                theElecBaseDesignUI.Dispose();
            theElecBaseDesignUI = null;
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
            face_select0 = (NXOpen.BlockStyler.FaceCollector)theDialog.TopBlock.FindBlock("face_select0");
            group = (NXOpen.BlockStyler.Group)theDialog.TopBlock.FindBlock("group");
            elecName = (NXOpen.BlockStyler.StringBlock)theDialog.TopBlock.FindBlock("elecName");
            expressionQingGen = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionQingGen");
            expressionDistance = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionDistance");
            expressionJizhutai = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionJizhutai");
            expressionDaojiao = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionDaojiao");
            expressionDaoyuan = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionDaoyuan");
            toggleAutoInteger = (NXOpen.BlockStyler.Toggle)theDialog.TopBlock.FindBlock("toggleAutoInteger");
            expressionX = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionX");
            expressionY = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionY");
            expressionZ = (NXOpen.BlockStyler.ExpressionBlock)theDialog.TopBlock.FindBlock("expressionZ");
            group1 = (NXOpen.BlockStyler.Group)theDialog.TopBlock.FindBlock("group1");
            enum0 = (NXOpen.BlockStyler.Enumeration)theDialog.TopBlock.FindBlock("enum0");
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
    //Callback Name: update_cb
    //------------------------------------------------------------------------------
    public int update_cb(NXOpen.BlockStyler.UIBlock block)
    {
        try
        {
            if (block == face_select0)
            {
                //---------Enter your code here-----------
            }
            else if (block == elecName)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionQingGen)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionDistance)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionJizhutai)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionDaojiao)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionDaoyuan)
            {
                //---------Enter your code here-----------
            }
            else if (block == toggleAutoInteger)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionX)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionY)
            {
                //---------Enter your code here-----------
            }
            else if (block == expressionZ)
            {
                //---------Enter your code here-----------
            }
            else if (block == enum0)
            {
                //---------Enter your code here-----------
            }
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