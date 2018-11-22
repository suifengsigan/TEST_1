using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AssemblyLoader
{
    public class Entry
    {
        private static bool isInitAssembly = false;
        public static void InitAssembly()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.AssemblyResolve -= new ResolveEventHandler(MyResolveEventHandler);
            if (!isInitAssembly)
            {
                currentDomain.AssemblyResolve += new ResolveEventHandler(MyResolveEventHandler);
                isInitAssembly = true;
            }

        }

        private static Assembly MyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            if (assemblyName.Name.Contains("PHSnap"))
            {
                Console.WriteLine("Resolving...");
                var assemblbyName = args.Name.Split(',').FirstOrDefault();
                var ufSession = NXOpen.UF.UFSession.GetUFSession();
                NXOpen.UF.SystemInfo info;
                ufSession.UF.AskSystemInfo(out info);
                var version = "UG9.0";
                if (info.program_name.Contains("NX 6")
                    || info.program_name.Contains("NX 7")
                    )
                {
                    version = "UG6.0";
                }
                var path = string.Format("{0}\\SnapDll\\{1}", AppDomain.CurrentDomain.BaseDirectory, version);
                var file = System.IO.Directory.GetFiles(path).FirstOrDefault(u => u == string.Format("{0}.dll", System.IO.Path.Combine(path, assemblbyName)));
                return Assembly.LoadFile(file);
            }

            return null;
        }
    }
}
