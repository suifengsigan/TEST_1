namespace NXOpen.Utilities
{
    using Microsoft.CSharp;
    using Microsoft.VisualBasic;
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Specialized;
    using System.Diagnostics;
    using System.IO;
    using System.Reflection;

    internal class CodeDomCompiler
    {
        private static string baseDirName = Path.GetTempPath();
        private string dirName = Path.Combine(baseDirName, "NXJournals" + Process.GetCurrentProcess().Id.ToString());
        private string languageType;
        private string leafName = "journal";
        private CompilerParameters options;
        private CodeDomProvider provider;
        private StringCollection references;
        private CompilerResults results;

        internal CodeDomCompiler(string langType)
        {
            this.languageType = langType;
            if (string.Compare(this.languageType, "CS") == 0)
            {
                this.provider = new CSharpCodeProvider();
            }
            else
            {
                this.provider = new VBCodeProvider();
            }
            this.options = new CompilerParameters();
            this.options.GenerateExecutable = false;
            this.options.GenerateInMemory = false;
            this.options.OutputAssembly = Path.Combine(this.dirName, Path.ChangeExtension(this.leafName, "dll"));
            this.options.IncludeDebugInformation = true;
            if (string.Compare(this.languageType, "VB") == 0)
            {
                this.options.CompilerOptions = "/imports:Microsoft.VisualBasic";
            }
            this.options.TreatWarningsAsErrors = false;
            if (!Directory.Exists(this.dirName))
            {
                Directory.CreateDirectory(this.dirName);
            }
            this.options.TempFiles = new TempFileCollection(this.dirName, false);
            this.references = this.options.ReferencedAssemblies;
        }

        public void AddReferenceItem(string name, string assemblyName)
        {
            this.references.Add(assemblyName);
        }

        public bool Compile(string script)
        {
            string str;
            if (this.languageType == "CS")
            {
                str = Path.ChangeExtension(this.options.OutputAssembly, "cs");
            }
            else
            {
                str = Path.ChangeExtension(this.options.OutputAssembly, "vb");
            }
            using (StreamWriter writer = new StreamWriter(str))
            {
                writer.Write(script);
            }
            try
            {
                this.results = this.provider.CompileAssemblyFromFile(this.options, new string[] { str });
            }
            catch (Exception exception)
            {
                Trace.WriteLine("Trouble running the journal file: " + exception.ToString());
                throw new Exception("Unable to run the journal file, refer to syslog for details", exception);
            }
            finally
            {
                File.Delete(str);
            }
            return !this.results.Errors.HasErrors;
        }

        public string[] GetCompileErrors()
        {
            CompilerErrorCollection errors = this.results.Errors;
            int num = 0;
            int num2 = 0;
            foreach (CompilerError error in errors)
            {
                if (error.IsWarning)
                {
                    num2++;
                }
                else
                {
                    num++;
                }
            }
            string[] strArray = new string[1 + (2 * num)];
            strArray[0] = "COMPILE";
            int num3 = 1;
            foreach (CompilerError error2 in errors)
            {
                if (!error2.IsWarning)
                {
                    strArray[num3++] = error2.ErrorText;
                    strArray[num3++] = error2.Line.ToString();
                    Console.WriteLine("Line {0}: {1}", strArray[num3 - 1], strArray[num3 - 2]);
                }
                else
                {
                    Console.WriteLine("Warning: Line {0}: {1}", error2.Line.ToString(), error2.ErrorText);
                }
            }
            return strArray;
        }

        public void TidyUp()
        {
            try
            {
                File.Delete(this.options.OutputAssembly);
                File.Delete(Path.ChangeExtension(this.options.OutputAssembly, "pdb"));
                if (Directory.GetFileSystemEntries(this.dirName).Length == 0)
                {
                    Directory.Delete(this.dirName);
                }
            }
            catch (Exception exception)
            {
                Trace.WriteLine("Trouble tidying up journal files: " + exception.ToString());
            }
        }

        public System.Reflection.Assembly Assembly
        {
            get
            {
                return this.results.CompiledAssembly;
            }
        }
    }
}

