namespace NXOpen.Utilities
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using System.Text.RegularExpressions;

    public class ScriptHost
    {
        private CodeDomCompiler compiler;
        private string languageType;
        private static string myDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        private ScriptHost(string langType)
        {
            this.languageType = langType;
            this.compiler = new CodeDomCompiler(this.languageType);
        }

        private void AddReferenceItem(string name, string assemblyName)
        {
            this.compiler.AddReferenceItem(name, assemblyName);
        }

        private void AddReferenceItemsForHostApp(string dllNames)
        {
            if (dllNames != "")
            {
                foreach (string str in dllNames.Split(new char[] { ',' }))
                {
                    this.AddReferenceItem(str, this.GetLibraryLocation(str));
                }
            }
        }

        private string GetLibraryLocation(string library)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + Path.DirectorySeparatorChar + library;
            if (File.Exists(path))
            {
                Trace.WriteLine("Adding " + path + " as a reference item");
                return path;
            }
            path = myDirectory + Path.DirectorySeparatorChar + library;
            if (!File.Exists(path))
            {
                throw new Exception("Could not find a version of " + library + " to add as reference");
            }
            Trace.WriteLine("Adding " + path + " as a reference item");
            return path;
        }

        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_DisplayAutotest();
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern void JAM_lprintf(string msg);
        [DllImport("libjam", CallingConvention=CallingConvention.Cdecl)]
        private static extern int JAM_presence_check_snap_author_license();
        private string[] Run(string[] args, bool withUI)
        {
            try
            {
                string str2;
                string script = args[0];
                this.AddReferenceItem("System", "System.dll");
                this.AddReferenceItem("SystemWindowsForms", "System.Windows.Forms.dll");
                this.AddReferenceItem("SystemDrawing", "System.Drawing.dll");
                this.AddReferenceItem("SystemXml", "System.Xml.dll");
                this.AddReferenceItem("SystemData", "System.Data.dll");
                this.AddReferenceItem("NXOpenUtils", this.GetLibraryLocation("NXOpen.Utilities.dll"));
                this.AddReferenceItem("NXOpen", this.GetLibraryLocation("NXOpen.dll"));
                this.AddReferenceItem("NXOpenUF", this.GetLibraryLocation("NXOpen.UF.dll"));
                this.AddReferenceItem("MiniSnap", this.GetLibraryLocation("MiniSnap.dll"));
                try
                {
                    JAM_lprintf("Evaluating whether to add Snap library: \n");
                    if (JAM_presence_check_snap_author_license() != 0)
                    {
                        JAM_lprintf("Cannot obtain authoring license from server \n");
                    }
                    else
                    {
                        JAM_lprintf("Snap Author license found, adding reference to Snap.dll \n");
                        this.AddReferenceItem("Snap", this.GetLibraryLocation("Snap.dll"));
                    }
                }
                catch (Exception exception)
                {
                    JAM_lprintf("Could not add Snap library: " + exception.Message);
                }
                if (JAM_DisplayAutotest() == 1)
                {
                    try
                    {
                        this.AddReferenceItem("DebugSession", this.GetLibraryLocation("DebugSession.dll"));
                    }
                    catch (Exception exception2)
                    {
                        JAM_lprintf("Could not add DebugSession library: " + exception2.Message);
                    }
                }
                if (withUI)
                {
                    this.AddReferenceItem("NXOpenUI", this.GetLibraryLocation("NXOpenUI.dll"));
                }
                this.AddReferenceItemsForHostApp(args[2]);
                if (this.languageType == "CS")
                {
                    str2 = "//";
                }
                else
                {
                    str2 = "'";
                }
                if (script.StartsWith(str2 + "#reference "))
                {
                    int startIndex = 0;
                    Regex regex = new Regex(@"^(\w:|/|\\)");
                    while (script.Substring(startIndex).StartsWith(str2 + "#reference "))
                    {
                        int index = script.IndexOf("\n", startIndex);
                        string input = script.Substring(startIndex, index - startIndex).Replace("\r", "").Substring((str2 + "#reference ").Length).Trim();
                        if (!regex.Match(input).Success)
                        {
                            input = this.GetLibraryLocation(input);
                        }
                        this.AddReferenceItem("ExternRef" + startIndex, input);
                        startIndex = index + 1;
                    }
                }
                try
                {
                    if (!this.compiler.Compile(script))
                    {
                        return this.compiler.GetCompileErrors();
                    }
                }
                catch (Exception exception3)
                {
                    return new string[] { "SCRIPT_RUN", (exception3.GetType().ToString() + ": " + exception3.Message), exception3.StackTrace };
                }
                Type[] types = this.compiler.Assembly.GetTypes();
                bool flag = false;
                string[] destinationArray = null;
                if (args.Length > 3)
                {
                    destinationArray = new string[args.Length - 3];
                    Array.Copy(args, 3, destinationArray, 0, args.Length - 3);
                }
                for (int i = 0; i < types.Length; i++)
                {
                    MethodInfo method = types[i].GetMethod("Main");
                    if (method != null)
                    {
                        ParameterInfo[] parameters = method.GetParameters();
                        flag = true;
                        if (parameters.Length == 0)
                        {
                            method.Invoke(null, null);
                        }
                        else if (parameters.Length == 1)
                        {
                            if (destinationArray == null)
                            {
                                destinationArray = new string[0];
                            }
                            method.Invoke(null, new object[] { destinationArray });
                        }
                        else
                        {
                            flag = false;
                        }
                        break;
                    }
                }
                if (!flag)
                {
                    return new string[] { "NO_MAIN" };
                }
            }
            catch (TargetInvocationException exception4)
            {
                Exception innerException = exception4.InnerException;
                Exception exception6 = innerException.InnerException;
                if (exception6 != null)
                {
                    return new string[] { "SCRIPT_RUN", (innerException.GetType().ToString() + ": " + innerException.Message), innerException.StackTrace, exception6.ToString() };
                }
                return new string[] { "SCRIPT_RUN", (innerException.GetType().ToString() + ": " + innerException.Message), innerException.StackTrace };
            }
            catch (Exception exception7)
            {
                Exception exception8 = exception7.InnerException;
                if (exception8 != null)
                {
                    return new string[] { "SCRIPT_INVOKE", (exception7.GetType().ToString() + ": " + exception7.Message), exception7.StackTrace, exception8.ToString() };
                }
                return new string[] { "SCRIPT_INVOKE", (exception7.GetType().ToString() + ": " + exception7.Message), exception7.StackTrace };
            }
            return null;
        }

        public static string[] RunInternalNXJournal(string[] args)
        {
            string[] strArray;
            ScriptHost host = new ScriptHost(args[1]);
            try
            {
                strArray = host.Run(args, true);
            }
            finally
            {
                host.TidyUp();
            }
            return strArray;
        }

        public static string[] RunNXJournal(string[] args)
        {
            string[] strArray;
            ScriptHost host = new ScriptHost(args[1]);
            try
            {
                strArray = host.Run(args, false);
            }
            finally
            {
                host.TidyUp();
            }
            return strArray;
        }

        public void TidyUp()
        {
            this.compiler.TidyUp();
            GC.Collect();
        }
    }
}

