using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace License
{
    public class Sense4Dev
    {
        #region   Sense4 API

        // ctlCode definition for S4Control
        static public uint S4_LED_UP = 0x00000004;  // LED up
        static public uint S4_LED_DOWN = 0x00000008;  // LED down
        static public uint S4_LED_WINK = 0x00000028;  // LED wink
        static public uint S4_GET_DEVICE_TYPE = 0x00000025;	//get device type
        static public uint S4_GET_SERIAL_NUMBER = 0x00000026;	//get device serial
        static public uint S4_GET_VM_TYPE = 0x00000027;  // get VM type
        static public uint S4_GET_DEVICE_USABLE_SPACE = 0x00000029;  // get total space
        static public uint S4_SET_DEVICE_ID = 0x0000002a;  // set device ID
        static public uint S4_GET_LICENSE = 0x00000020;  // GET_LICENSE	 
        static public uint S4_FREE_LICENSE = 0x00000021;  // free license 
        static public uint S4_MODIFY_TIMOUT = 0x00000022;  // change the timeout value 

        // device type definition 
        static public uint S4_LOCAL_DEVICE = 0x00;		// local device 
        static public uint S4_MASTER_DEVICE = 0x80;		// net master device
        static public uint S4_SLAVE_DEVICE = 0xc0;		// net slave device

        // vm type definiton 
        static public uint S4_VM_51 = 0x00;		// VM51
        static public uint S4_VM_251_BINARY = 0x01;		// VM251 binary mode
        static public uint S4_VM_251_SOURCE = 0x02;		// VM251 source mode


        // PIN type definition 
        static public uint S4_USER_PIN = 0x000000a1;		// user PIN
        static public uint S4_DEV_PIN = 0x000000a2;		// dev PIN
        static public uint S4_AUTHEN_PIN = 0x000000a3;		// autheticate Key


        // file type definition 
        static public uint S4_RSA_PUBLIC_FILE = 0x00000006;		// RSA public file
        static public uint S4_RSA_PRIVATE_FILE = 0x00000007;		// RSA private file 
        static public uint S4_EXE_FILE = 0x00000008;		// VM file
        static public uint S4_DATA_FILE = 0x00000009;		// data file

        // dwFlag definition for S4WriteFile
        static public uint S4_CREATE_NEW = 0x000000a5;		// create new file
        static public uint S4_UPDATE_FILE = 0x000000a6;		// update file
        static public uint S4_KEY_GEN_RSA_FILE = 0x000000a7;		// produce RSA key pair
        static public uint S4_SET_LICENCES = 0x000000a8;		// set the license number for modle,available for net device only
        static public uint S4_CREATE_ROOT_DIR = 0x000000ab;		// create root directory, available for empty device only
        static public uint S4_CREATE_SUB_DIR = 0x000000ac;		// create child directory
        static public uint S4_CREATE_MODULE = 0x000000ad;		// create modle, available for net device only

        // the three parameters below must be bitwise-inclusive-or with S4_CREATE_NEW, only for executive file
        static public uint S4_FILE_READ_WRITE = 0x00000000;      // can be read and written in executive file,default
        static public uint S4_FILE_EXECUTE_ONLY = 0x00000100;      // can NOT be read or written in executive file
        static public uint S4_CREATE_PEDDING_FILE = 0x00002000;		// create padding file


        /* return value*/
        static public uint S4_SUCCESS = 0x00000000;		// succeed
        static public uint S4_UNPOWERED = 0x00000001;
        static public uint S4_INVALID_PARAMETER = 0x00000002;
        static public uint S4_COMM_ERROR = 0x00000003;
        static public uint S4_PROTOCOL_ERROR = 0x00000004;
        static public uint S4_DEVICE_BUSY = 0x00000005;
        static public uint S4_KEY_REMOVED = 0x00000006;
        static public uint S4_INSUFFICIENT_BUFFER = 0x00000011;
        static public uint S4_NO_LIST = 0x00000012;
        static public uint S4_GENERAL_ERROR = 0x00000013;
        static public uint S4_UNSUPPORTED = 0x00000014;
        static public uint S4_DEVICE_TYPE_MISMATCH = 0x00000020;
        static public uint S4_FILE_SIZE_CROSS_7FFF = 0x00000021;
        static public uint S4_DEVICE_UNSUPPORTED = 0x00006a81;
        static public uint S4_FILE_NOT_FOUND = 0x00006a82;
        static public uint S4_INSUFFICIENT_SECU_STATE = 0x00006982;
        static public uint S4_DIRECTORY_EXIST = 0x00006901;
        static public uint S4_FILE_EXIST = 0x00006a80;
        static public uint S4_INSUFFICIENT_SPACE = 0x00006a84;
        static public uint S4_OFFSET_BEYOND = 0x00006B00;
        static public uint S4_PIN_BLOCK = 0x00006983;
        static public uint S4_FILE_TYPE_MISMATCH = 0x00006981;
        static public uint S4_CRYPTO_KEY_NOT_FOUND = 0x00009403;
        static public uint S4_APPLICATION_TEMP_BLOCK = 0x00006985;
        static public uint S4_APPLICATION_PERM_BLOCK = 0x00009303;
        static public int S4_DATA_BUFFER_LENGTH_ERROR = 0x00006700;
        static public uint S4_CODE_RANGE = 0x00010000;
        static public uint S4_CODE_RESERVED_INST = 0x00020000;
        static public uint S4_CODE_RAM_RANGE = 0x00040000;
        static public uint S4_CODE_BIT_RANGE = 0x00080000;
        static public uint S4_CODE_SFR_RANGE = 0x00100000;
        static public uint S4_CODE_XRAM_RANGE = 0x00200000;
        static public uint S4_ERROR_UNKNOWN = 0xffffffff;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SENSE4_CONTEXT
        {
            public int dwIndex;		//device index
            public int dwVersion;		//version		
            public int hLock;			//device handle
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] reserve;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 56)]
            public byte[] bAtr;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] bID;
            public uint dwAtrLen;
        }
        const string DllNameX86 = "Sense4userX86.dll";
        //Assume that Sense4user.dll in c:\, if not, modify the lines below
        [DllImport(DllNameX86)]
        private static extern uint S4Enum([MarshalAs(UnmanagedType.LPArray), Out] SENSE4_CONTEXT[] s4_context, ref uint size);
        [DllImport(DllNameX86)]
        private static extern uint S4Open(ref SENSE4_CONTEXT s4_context);
        [DllImport(DllNameX86)]
        private static extern uint S4Close(ref SENSE4_CONTEXT s4_context);
        [DllImport(DllNameX86)]
        private static extern uint S4Control(ref SENSE4_CONTEXT s4Ctx, uint ctlCode, byte[] inBuff,
            uint inBuffLen, byte[] outBuff, uint outBuffLen, ref uint BytesReturned);
        [DllImport(DllNameX86)]
        private static extern uint S4Execute(ref SENSE4_CONTEXT s4Ctx, string FileID, byte[] InBuffer,
            uint InbufferSize, byte[] OutBuffer, uint OutBufferSize, ref uint BytesReturned);


        const string DllNameX64 = "Sense4userX64.dll";
        //Assume that Sense4user.dll in c:\, if not, modify the lines below
        [DllImport(DllNameX64, EntryPoint = "S4Enum")]
        private static extern uint S4Enum64([MarshalAs(UnmanagedType.LPArray), Out] SENSE4_CONTEXT[] s4_context, ref uint size);
        [DllImport(DllNameX64, EntryPoint = "S4Open")]
        private static extern uint S4Open64(ref SENSE4_CONTEXT s4_context);
        [DllImport(DllNameX64, EntryPoint = "S4Close")]
        private static extern uint S4Close64(ref SENSE4_CONTEXT s4_context);
        [DllImport(DllNameX64, EntryPoint = "S4Control")]
        private static extern uint S4Control64(ref SENSE4_CONTEXT s4Ctx, uint ctlCode, byte[] inBuff,
            uint inBuffLen, byte[] outBuff, uint outBuffLen, ref uint BytesReturned);
        [DllImport(DllNameX64, EntryPoint = "S4Execute")]
        private static extern uint S4Execute64(ref SENSE4_CONTEXT s4Ctx, string FileID, byte[] InBuffer,
            uint InbufferSize, byte[] OutBuffer, uint OutBufferSize, ref uint BytesReturned);


        #endregion

        #region  Verification

        public static bool VerificationX64(out string strMsg)
        {
            try
            {
                strMsg = string.Empty;

                uint size = 0;

                byte[] inBuffer = new byte[512];

                byte[] outBuffer = new byte[256];

                byte[] wModID = new byte[2];

                byte[] wNewtimeouts = new byte[2];

                uint BytesReturned = 0;

                // 查找网络锁，配置文件在同目录下：e4ncli.ini
                uint ret = S4Enum64(null, ref size);
                SENSE4_CONTEXT[] si = new SENSE4_CONTEXT[size / Marshal.SizeOf(typeof(SENSE4_CONTEXT))];
                ret = S4Enum64(si, ref size);
                if (ret != S4_SUCCESS)
                {
                    strMsg = "04 - 加密锁启动失败！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 加密锁启动失败！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }

                int siIndex = -1;
                //验证加密锁的ID
                for (int i = 0; i < si.Length; i++)
                {
                    var siList = new List<byte>(si[i].bAtr);

                    siList.RemoveRange(0, int.Parse(si[i].dwAtrLen.ToString()) - 8);

                    var sSer = Encoding.Default.GetString(siList.ToArray());

                    if (sSer.Trim().IndexOf("EAct") != -1)
                    {
                        siIndex = i;
                        break;
                    }
                }

                if (siIndex == -1)
                {
                    strMsg = "04 - 没有发现EAct系统的加密狗！";
                    //MessageDxUtil.ShowMsg("04 - 没有发现EAct系统的加密狗！", MessageDxUtil.MsgType.Warning);
                    return false;
                }

                // 打开网络锁
                ret = S4Open64(ref si[siIndex]);
                if (ret != S4_SUCCESS)
                {
                    strMsg = "04 - 加密锁网络访问失败！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 加密锁网络访问失败！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }

                // 获取授权
                wModID[0] = 0x01;
                wModID[1] = 0x00;
                ret = S4Control64(ref si[siIndex], S4_GET_LICENSE, wModID, 2, null, 0, ref BytesReturned);
                if (ret != S4_SUCCESS)
                {
                    S4Close64(ref si[siIndex]);
                    strMsg = "04 - 请插入加密锁或检查加密锁服务是否启动！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 请插入加密锁或检查加密锁服务是否启动！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }
                return true;
            }

            catch (Exception ex)
            {
                strMsg = "04 - 加密锁验证失败！错误：" + ex.Message.ToString();
                //MessageDxUtil.ShowMsg("04 - 加密锁验证失败！错误：" + ex.Message.ToString(), MessageDxUtil.MsgType.Warning);
                return false;
            }
        }

        public static bool Verification(out string strMsg)
        {
            if (IntPtr.Size == 8)
            {
                return VerificationX64(out strMsg);
            }
            try
            {
                strMsg=string.Empty;

                uint size = 0;

                byte[] inBuffer = new byte[512];

                byte[] outBuffer = new byte[256];

                byte[] wModID = new byte[2];

                byte[] wNewtimeouts = new byte[2];

                uint BytesReturned = 0;

                // 查找网络锁，配置文件在同目录下：e4ncli.ini
                uint ret = S4Enum(null, ref size);
                SENSE4_CONTEXT[] si = new SENSE4_CONTEXT[size / Marshal.SizeOf(typeof(SENSE4_CONTEXT))];
                ret = S4Enum(si, ref size);
                if (ret != S4_SUCCESS)
                {
                    strMsg = "04 - 加密锁启动失败！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 加密锁启动失败！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }

                int siIndex = -1;
                //验证加密锁的ID
                for (int i = 0; i < si.Length; i++)
                {
                    var siList = new List<byte>(si[i].bAtr);

                    siList.RemoveRange(0, int.Parse(si[i].dwAtrLen.ToString()) - 8);

                    var sSer = Encoding.Default.GetString(siList.ToArray());

                    if (sSer.Trim().IndexOf("EAct") != -1)
                    {
                        siIndex = i;
                        break;
                    }
                }

                if (siIndex == -1)
                {
                    strMsg = "04 - 没有发现EAct系统的加密狗！";
                    //MessageDxUtil.ShowMsg("04 - 没有发现EAct系统的加密狗！", MessageDxUtil.MsgType.Warning);
                    return false;
                }

                // 打开网络锁
                ret = S4Open(ref si[siIndex]);
                if (ret != S4_SUCCESS)
                {
                    strMsg = "04 - 加密锁网络访问失败！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 加密锁网络访问失败！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }

                // 获取授权
                wModID[0] = 0x01;
                wModID[1] = 0x00;
                ret = S4Control(ref si[siIndex], S4_GET_LICENSE, wModID, 2, null, 0, ref BytesReturned);
                if (ret != S4_SUCCESS)
                {
                    S4Close(ref si[siIndex]);
                    strMsg = "04 - 请插入加密锁或检查加密锁服务是否启动！代码：" + ret.ToString();
                    //MessageDxUtil.ShowMsg("04 - 请插入加密锁或检查加密锁服务是否启动！代码：" + ret.ToString(), MessageDxUtil.MsgType.Warning);
                    return false;
                }
                return true;
            }

            catch(Exception ex)
            {
                strMsg = "04 - 加密锁验证失败！错误：" + ex.Message.ToString();
                //MessageDxUtil.ShowMsg("04 - 加密锁验证失败！错误：" + ex.Message.ToString(), MessageDxUtil.MsgType.Warning);
                return false;
            }
        }

        #endregion
    }
}
