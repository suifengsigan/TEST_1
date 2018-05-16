namespace NXOpen.Utilities
{
    using NXOpen;
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class BaseSession : TaggedObject
    {
        private static NXObjectManager objectManager = null;
        private static SortedList sessionList;
        private EventHandler unloadHandler;

        protected BaseSession()
        {
            this.unloadHandler = new EventHandler(this.CurrentDomain_DomainUnload);
            AppDomain.CurrentDomain.DomainUnload += this.unloadHandler;
        }

        private static void AddDLLAuthorization(string filename, ResourceUtilities.SignatureType sigType)
        {
            if (sessionList == null)
            {
                sessionList = new SortedList();
            }
            if (filename != null)
            {
                if (sessionList.Contains(filename))
                {
                    int index = sessionList.IndexOfKey(filename);
                    ResourceUtilities.SignatureType type = (ResourceUtilities.SignatureType) sessionList.GetValueList()[index];
                    if ((type != ResourceUtilities.SignatureType.BOTH_TYPE) && (type != sigType))
                    {
                        sessionList.RemoveAt(index);
                        sessionList.Add(filename, ResourceUtilities.SignatureType.BOTH_TYPE);
                    }
                }
                else
                {
                    sessionList.Add(filename, sigType);
                }
            }
        }

        private static bool CheckDLLAuthorization(string filename, ResourceUtilities.SignatureType sigType)
        {
            if ((sessionList == null) || (filename == null))
            {
                return false;
            }
            if (!sessionList.Contains(filename))
            {
                return false;
            }
            int num = sessionList.IndexOfKey(filename);
            ResourceUtilities.SignatureType type = (ResourceUtilities.SignatureType) sessionList.GetValueList()[num];
            if ((type != ResourceUtilities.SignatureType.BOTH_TYPE) && (type != sigType))
            {
                return false;
            }
            return true;
        }

        public void CloseTestOutput()
        {
            int status = JAUTL_SESSION_close_test_output();
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        public void CompareTestOutput(string originalFile, string newFile)
        {
            int status = JAUTL_SESSION_compare_test_output(originalFile, newFile);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        private void CurrentDomain_DomainUnload(object sender, EventArgs e)
        {
            AppDomain.CurrentDomain.DomainUnload -= this.unloadHandler;
            JAM_SESSION_terminate();
            IntPtr s = JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            if (s != IntPtr.Zero)
            {
                RemoveDllAthorization(JAM.ToStringFromLocale(s));
            }
        }

        public static ResourceUtilities.SignatureType determineSignatureNeeded()
        {
            ResourceUtilities.SignatureType type = ResourceUtilities.SignatureType.NORMAL_TYPE;
            bool flag = false;
            StackTrace trace = new StackTrace();
            if (trace.FrameCount > 0)
            {
                for (int i = 0; (i < trace.FrameCount) && !flag; i++)
                {
                    string location = trace.GetFrame(i).GetMethod().Module.Assembly.Location;
                    string fileName = Path.GetFileName(JAM.ToStringFromLocale(JAM_get_filename("", location)));
                    if (string.Compare(fileName, "Snap.dll", true) == 0)
                    {
                        type = ResourceUtilities.SignatureType.SNAP_TYPE;
                    }
                    if (string.Compare(fileName, "MiniSnap.dll", true) == 0)
                    {
                        flag = true;
                        type = ResourceUtilities.SignatureType.MINI_SNAP;
                    }
                }
            }
            return type;
        }

        public static IntPtr evalualuteCallStack(ref ResourceUtilities.SignatureType sigType)
        {
            StackTrace trace = new StackTrace();
            sigType = ResourceUtilities.SignatureType.NORMAL_TYPE;
            bool flag = false;
            bool flag2 = false;
            SortedList list = GenerateInitialDllList();
            ArrayList c = new ArrayList();
            if (trace.FrameCount > 0)
            {
                for (int i = 0; i < trace.FrameCount; i++)
                {
                    string location = trace.GetFrame(i).GetMethod().Module.Assembly.Location;
                    string path = JAM.ToStringFromLocale(JAM_get_filename("", location));
                    string key = Path.GetFileNameWithoutExtension(path) + ".dll";
                    if (!list.Contains(key) && !c.Contains(path))
                    {
                        c.Add(path);
                    }
                    //if (!flag && (string.Compare(key, "Snap.dll", true) == 0))
                    //{
                    //    flag = true;
                    //    sigType = ResourceUtilities.SignatureType.SNAP_TYPE;
                    //}
                    //if (!flag2 && (string.Compare(key, "MiniSnap.dll", true) == 0))
                    //{
                    //    flag2 = true;
                    //    sigType = ResourceUtilities.SignatureType.MINI_SNAP;
                    //}
                }
                int num2 = 0;
                string str4 = null;
                bool flag3 = false;
                for (int j = 0; j < c.Count; j++)
                {
                    string fileName = Path.GetFileName((string) c[j]);
                    if (string.Compare("journal.dll", fileName, true) == 0)
                    {
                        num2++;
                        if (!flag3)
                        {
                            flag3 = true;
                            str4 = (string) c[j];
                        }
                    }
                }
                if (num2 > 0)
                {
                    if (num2 == 1)
                    {
                        ArrayList list3 = new ArrayList(c);
                        list3.Remove(str4);
                        if (list3.Count == 0)
                        {
                            return IntPtr.Zero;
                        }
                        JAM_lprintf("Detected call to non-NX dll while replaying journal.\n");
                        JAM_lprintf("Validating DLL - " + list3[0] + "\n");
                        return JAM.ToLocaleString((string) list3[0]);
                    }
                    JAM_lprintf("Detected call to non-NX dll while replaying journal.\n");
                    JAM_lprintf("Validating DLL - " + str4 + "\n");
                    return JAM.ToLocaleString(str4);
                }
                if (c.Count > 0)
                {
                    return JAM.ToLocaleString((string) c[0]);
                }
                JAM_lprintf("No journal.dll detected in call stack while replaying journal.\n");
            }
            return IntPtr.Zero;
        }

        ~BaseSession()
        {
        }

        private static SortedList GenerateInitialDllList()
        {
            SortedList list = new SortedList();
            list.Add("NXOpen.dll", "skip");
            list.Add("NXOpenUI.dll", "skip");
            list.Add("ManagedLoader.dll", "skip");
            list.Add("NXOpen.UF.dll", "skip");
            list.Add("NXOpen.Utilities.dll", "skip");
            list.Add("Snap.dll", "skip");
            list.Add("MiniSnap.dll", "skip");
            list.Add("mscorlib.dll", "skip");
            list.Add("System.dll", "skip");
            list.Add("System.Windows.Forms.dll", "skip");
            list.Add("System.Drawing.dll", "skip");
            list.Add("System.Xml.dll", "skip");
            list.Add("System.Data.dll", "skip");
            list.Add("Microsoft.VisualBasic.dll", "skip");
            list.Add("NXAthenaUgmgrUtils.dll", "skip");
            return list;
        }

        public NXObjectManager GetObjectManager()
        {
            return objectManager;
        }

        protected void initialize()
        {
            base.initialize();
        }

        public static void InitLicense()
        {
            bool flag = false;
            try
            {
                flag = verifyAssemblyData(false);
            }
            catch (NeedSNAPAuthorLicenseException exception)
            {
                if (JAM_presence_check_snap_author_license() != 0)
                {
                    throw exception;
                }
                Trace.WriteLine(exception);
                Trace.WriteLine("Validation failed but author license exists - loading library");
                AddDLLAuthorization(JAM.ToStringFromLocale(JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName)), ResourceUtilities.SignatureType.SNAP_TYPE);
            }
            catch (NeedDOTNETAuthorLicenseException exception2)
            {
                if (JAM_presence_check_dotnet_author_license() != 0)
                {
                    throw exception2;
                }
                Trace.WriteLine(exception2);
                Trace.WriteLine("Validation failed but author license exists - loading library");
                AddDLLAuthorization(JAM.ToStringFromLocale(JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName)), ResourceUtilities.SignatureType.NORMAL_TYPE);
            }
            catch (Exception exception3)
            {
                throw exception3;
            }
            if (!flag)
            {
                int status = JAM_check_session_license(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName);
                if (status != 0)
                {
                    Trace.WriteLine("Could not obtain runtime license");
                    throw NXException.Create(status);
                }
            }
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_check_session_license(string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_declare_alliance_context(string context);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_dotnet_verification_override();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern IntPtr JAM_get_filename(string BaseDirectory, string FriendlyName);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_lprintf(string msg);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_presence_check_dotnet_author_license();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_presence_check_snap_author_license();
        [DllImport("libuginit", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_SESSION_finalize();
        [DllImport("libuginit", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_SESSION_initialize();
        [DllImport("libuginit", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_SESSION_terminate();
        [DllImport("libpart", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAUTL_SESSION_close_test_output();
        [DllImport("libpart", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAUTL_SESSION_compare_test_output(string masterFile, string newFile);
        [DllImport("libpart", CallingConvention=CallingConvention.Cdecl, CharSet=CharSet.Ansi)]
        private static extern int JAUTL_SESSION_set_test_output(string outputFile, int version);
        public static bool needToEvaluate()
        {
            ResourceUtilities.SignatureType sigType = ResourceUtilities.SignatureType.NORMAL_TYPE;
            string key = JAM.ToStringFromLocale(evalualuteCallStack(ref sigType));
            if ((key == null) || (sigType == ResourceUtilities.SignatureType.MINI_SNAP))
            {
                return false;
            }
            bool flag = true;
            if ((sessionList == null) || !sessionList.Contains(key))
            {
                return flag;
            }
            int num = sessionList.IndexOfKey(key);
            ResourceUtilities.SignatureType type2 = (ResourceUtilities.SignatureType) sessionList.GetValueList()[num];
            if ((type2 == ResourceUtilities.SignatureType.BOTH_TYPE) || (type2 == sigType))
            {
                return false;
            }
            return true;
        }

        public static bool RefreshSessionTable()
        {
            bool flag = false;
            try
            {
                verifyAssemblyData(true);
                flag = true;
            }
            catch (NeedSNAPAuthorLicenseException exception)
            {
                if (JAM_presence_check_snap_author_license() != 0)
                {
                    throw exception;
                }
                Trace.WriteLine(exception);
                Trace.WriteLine("Validation failed but author license exists - loading library");
                AddDLLAuthorization(JAM.ToStringFromLocale(JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName)), ResourceUtilities.SignatureType.SNAP_TYPE);
                return true;
            }
            catch (NeedDOTNETAuthorLicenseException exception2)
            {
                if (JAM_presence_check_dotnet_author_license() != 0)
                {
                    throw exception2;
                }
                Trace.WriteLine(exception2);
                Trace.WriteLine("Validation failed but author license exists - loading library");
                AddDLLAuthorization(JAM.ToStringFromLocale(JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName)), ResourceUtilities.SignatureType.NORMAL_TYPE);
                return true;
            }
            catch (Exception exception3)
            {
                throw exception3;
            }
            return flag;
        }

        private static void RemoveDllAthorization(string filename)
        {
            if (((filename != null) && (sessionList != null)) && sessionList.Contains(filename))
            {
                int index = sessionList.IndexOfKey(filename);
                sessionList.RemoveAt(index);
            }
        }

        public void SetTestOutput(string outputFile)
        {
            int status = JAUTL_SESSION_set_test_output(outputFile, 1);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        public void SetTestOutput(string outputFile, int version)
        {
            int status = JAUTL_SESSION_set_test_output(outputFile, version);
            if (status != 0)
            {
                throw NXException.Create(status);
            }
        }

        protected static void StaticInitialize()
        {
            int status = JAM_SESSION_initialize();
            if (status != 0)
            {
                throw NXException.Create(status);
            }
            InitLicense();
            if (objectManager == null)
            {
                objectManager = new NXObjectManager();
            }
        }

        private static bool verifyAssembly(string filename, ref bool alliance_mode, ResourceUtilities.SignatureType signTypeNeeded)
        {
            bool flag = true;
            alliance_mode = false;
            try
            {
                string str = "";
                if (signTypeNeeded == ResourceUtilities.SignatureType.NORMAL_TYPE)
                {
                    str = "NXOpen";
                }
                else if (signTypeNeeded == ResourceUtilities.SignatureType.SNAP_TYPE)
                {
                    str = "SNAP";
                }
                if (filename == null)
                {
                    return flag;
                }
                Trace.WriteLine("Verifying " + filename + " for " + str + " signature.");
                FileInfo info = new FileInfo(filename);
                BinaryReader reader = new BinaryReader(info.OpenRead());
                ResourceUtilities utilities = new ResourceUtilities(reader.ReadBytes((int) info.Length));
                byte[] hashValue = utilities.computeHash();
                byte[] signature = utilities.getSignature();
                if (!Decryptor.verifySignature(hashValue, signature))
                {
                    throw new Exception("Invalid NX signature found");
                }
                ASCIIEncoding encoding = new ASCIIEncoding();
                byte[] bytes = utilities.getEmbeddedData();
                string str2 = encoding.GetString(bytes, 1, bytes.Length - 1);
                if (str2.StartsWith("2 "))
                {
                    alliance_mode = true;
                }
                else if (str2.StartsWith("1 "))
                {
                    if ((signTypeNeeded == ResourceUtilities.SignatureType.SNAP_TYPE) || (signTypeNeeded == ResourceUtilities.SignatureType.BOTH_TYPE))
                    {
                        flag = false;
                        Trace.WriteLine("Is signed with NXOpen signature but needs Snap or Both signature type");
                    }
                }
                else if (str2.StartsWith("3 "))
                {
                    if ((signTypeNeeded == ResourceUtilities.SignatureType.BOTH_TYPE) || (signTypeNeeded == ResourceUtilities.SignatureType.NORMAL_TYPE))
                    {
                        flag = false;
                        Trace.WriteLine("Is signed with Snap signature but needs NXOpen or Both signature type");
                    }
                }
                else
                {
                    if (!str2.StartsWith("4 "))
                    {
                        throw new Exception("Corrupted NX signature text " + str2);
                    }
                    if (((signTypeNeeded == ResourceUtilities.SignatureType.SNAP_TYPE) || (signTypeNeeded == ResourceUtilities.SignatureType.BOTH_TYPE)) || (signTypeNeeded == ResourceUtilities.SignatureType.NORMAL_TYPE))
                    {
                        flag = true;
                    }
                }
                Trace.WriteLine("Signed by : " + str2.Substring("1 ".Length));
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception);
                Trace.WriteLine("Validation failed ");
                flag = false;
            }
            return flag;
        }

        public static bool verifyAssemblyData(bool callEvaluateStack)
        {
            return true;
            bool flag = false;
            bool flag2 = false;
            IntPtr p = JAM_get_filename(AppDomain.CurrentDomain.BaseDirectory, AppDomain.CurrentDomain.FriendlyName);
            ResourceUtilities.SignatureType sigType = ResourceUtilities.SignatureType.NORMAL_TYPE;
            if (((p == IntPtr.Zero) && (JAM_dotnet_verification_override() == 0)) || callEvaluateStack)
            {
                p = evalualuteCallStack(ref sigType);
            }
            else
            {
                sigType = determineSignatureNeeded();
            }
            if (sigType != ResourceUtilities.SignatureType.MINI_SNAP)
            {
                if (CheckDLLAuthorization(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName, sigType))
                {
                    JAM.SMFree(p);
                }
                else
                {
                    string filename = JAM.ToStringFromLocale(p);
                    flag2 = verifyAssembly(filename, ref flag, sigType);
                    if (flag2)
                    {
                        AddDLLAuthorization(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName, sigType);
                    }
                    else
                    {
                        StackTrace trace = new StackTrace();
                        SortedList list = GenerateInitialDllList();
                        string key = Path.GetFileNameWithoutExtension(filename) + ".dll";
                        list.Add(key, "notsigned");
                        if (trace.FrameCount > 0)
                        {
                            for (int i = 0; (i < trace.FrameCount) && !flag2; i++)
                            {
                                string location = trace.GetFrame(i).GetMethod().Module.Assembly.Location;
                                string str5 = Path.GetFileNameWithoutExtension(JAM.ToStringFromLocale(JAM_get_filename("", location))) + ".dll";
                                if (list.Contains(str5))
                                {
                                    string strA = (string) list.GetValueList()[0];
                                    if (((string.Compare(strA, "skip", false) == 0) || (string.Compare(strA, "notsigned", false) == 0)) || (string.Compare(strA, "signed", false) == 0))
                                    {
                                        continue;
                                    }
                                }
                                flag2 = verifyAssembly(JAM.ToStringFromLocale(JAM_get_filename("", location)), ref flag, sigType);
                                if (flag2)
                                {
                                    list.Add(str5, "signed");
                                    AddDLLAuthorization(location, sigType);
                                }
                                else
                                {
                                    list.Add(str5, "notsigned");
                                }
                            }
                        }
                        if (!flag2)
                        {
                            if (sigType == ResourceUtilities.SignatureType.SNAP_TYPE)
                            {
                                throw new NeedSNAPAuthorLicenseException("Invalid NX signature found");
                            }
                            throw new NeedDOTNETAuthorLicenseException("Invalid NX signature found");
                        }
                    }
                }
                if (flag)
                {
                    JAM_declare_alliance_context(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName);
                }
            }
            return flag;
        }
    }
}

