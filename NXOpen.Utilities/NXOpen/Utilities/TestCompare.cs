namespace NXOpen.Utilities
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Xml;

    public class TestCompare
    {
        private bool hadErrors;
        private string newFile;
        private string originalFile;
        private bool printed;

        public TestCompare(string originalFile, string newFile)
        {
            this.originalFile = originalFile;
            this.newFile = newFile;
        }

        public void CompareFiles()
        {
            XmlTextReader reader = null;
            XmlTextReader reader2 = null;
            try
            {
                reader = new XmlTextReader(this.originalFile);
                reader2 = new XmlTextReader(this.newFile);
                XmlDocument originalNode = new XmlDocument();
                originalNode.Load(reader);
                XmlDocument newNode = new XmlDocument();
                newNode.Load(reader2);
                this.compareNodes(originalNode, newNode);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (reader2 != null)
                {
                    reader2.Close();
                }
                if (this.hadErrors)
                {
                    throw new Exception("Differences found - please see log file for details\n");
                }
            }
        }

        private void compareMethodNodes(XmlNode originalMethod, XmlNode newMethod)
        {
            for (int i = 0; i < originalMethod.ChildNodes.Count; i++)
            {
                XmlNode originalResult = originalMethod.ChildNodes[i];
                if (originalResult.Name.Equals("result"))
                {
                    for (int j = 0; j < newMethod.ChildNodes.Count; j++)
                    {
                        if (newMethod.ChildNodes[j].Name.Equals("result"))
                        {
                            this.compareResultNodes(originalResult, newMethod.ChildNodes[j]);
                        }
                    }
                }
                else if (originalResult.Name.Equals("output"))
                {
                    string str = this.getAttValue(originalResult, "name");
                    for (int k = 0; k < newMethod.ChildNodes.Count; k++)
                    {
                        XmlNode node = newMethod.ChildNodes[k];
                        string str2 = this.getAttValue(node, "name");
                        if (str.Equals(str2))
                        {
                            this.compareResultNodes(originalResult, node);
                        }
                    }
                }
                else if (!originalResult.Name.Equals("method_error"))
                {
                    this.nodeException(originalMethod, "Unknown method child " + originalResult.Name);
                }
            }
        }

        private void compareNodes(XmlNode originalNode, XmlNode newNode)
        {
            if (originalNode.Name.Equals("object"))
            {
                this.compareObjectNodes(originalNode, newNode);
            }
            else
            {
                if (originalNode.ChildNodes.Count != newNode.ChildNodes.Count)
                {
                    this.nodeException(originalNode, string.Concat(new object[] { "originalNode has ", originalNode.ChildNodes.Count, " children, newNode has ", newNode.ChildNodes.Count }));
                }
                for (int i = 0; i < originalNode.ChildNodes.Count; i++)
                {
                    this.compareNodes(originalNode.ChildNodes[i], newNode.ChildNodes[i]);
                }
            }
        }

        private void compareObjectNodes(XmlNode originalNode, XmlNode newNode)
        {
            string str = this.getAttValue(originalNode, "type");
            string str2 = this.getAttValue(newNode, "type");
            if (!str.Equals(str2))
            {
                this.nodesDiffer(originalNode, newNode, "Different node types");
            }
            string originalValue = this.getAttValue(originalNode, "value");
            string str4 = this.getAttValue(newNode, "value");
            if ((str.Equals("int") || str.Equals("bool")) || (str.Equals("string") || str.Equals("enum")))
            {
                if (!originalValue.Equals(str4))
                {
                    this.nodesDiffer(originalNode, newNode, "originalValue = \"" + originalValue + "\" newValue = \"" + str4 + "\"");
                }
            }
            else if (str.Equals("double"))
            {
                if (!this.doublesEqual(originalValue, str4))
                {
                    this.nodesDiffer(originalNode, newNode, "originalValue = \"" + originalValue + "\" newValue = \"" + str4 + "\"");
                }
            }
            else if (!originalValue.StartsWith("HANDLE R-") || !str4.StartsWith("HANDLE R-"))
            {
                bool flag = false;
                if (!originalValue.Equals(str4))
                {
                    flag = true;
                    string[] strArray = originalValue.Split(new char[] { ' ' });
                    string[] strArray2 = str4.Split(new char[] { ' ' });
                    if ((strArray.Length > 0) && (strArray.Length == strArray2.Length))
                    {
                        flag = false;
                        for (int j = 0; j < strArray.Length; j++)
                        {
                            if (!this.doublesEqual(strArray[j], strArray2[j]))
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                if (flag)
                {
                    this.nodesDiffer(originalNode, newNode, "originalValue = \"" + originalValue + "\" newValue = \"" + str4 + "\"");
                }
            }
            for (int i = 0; i < originalNode.ChildNodes.Count; i++)
            {
                if (originalNode.ChildNodes[i].Name.Equals("method"))
                {
                    string str5 = this.getAttValue(originalNode.ChildNodes[i], "name");
                    for (int k = 0; k < newNode.ChildNodes.Count; k++)
                    {
                        if (newNode.ChildNodes[k].Name.Equals("method"))
                        {
                            string str6 = this.getAttValue(newNode.ChildNodes[k], "name");
                            if (str5.Equals(str6))
                            {
                                this.compareMethodNodes(originalNode.ChildNodes[i], newNode.ChildNodes[k]);
                            }
                        }
                    }
                }
                else if (originalNode.ChildNodes[i].Name.Equals("custom_data"))
                {
                    string str7 = this.getAttValue(originalNode.ChildNodes[i], "name");
                    for (int m = 0; m < newNode.ChildNodes.Count; m++)
                    {
                        if (newNode.ChildNodes[m].Name.Equals("custom_data"))
                        {
                            string str8 = this.getAttValue(newNode.ChildNodes[m], "name");
                            if (str7.Equals(str8))
                            {
                                string str9 = this.getAttValue(originalNode.ChildNodes[i], "value");
                                string str10 = this.getAttValue(newNode.ChildNodes[m], "value");
                                if (!str9.Equals(str10))
                                {
                                    this.nodesDiffer(originalNode.ChildNodes[i], newNode.ChildNodes[m], "originalValue = \"" + str9 + "\" newValue = \"" + str10 + "\"");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void compareResultNodes(XmlNode originalResult, XmlNode newResult)
        {
            if (originalResult.ChildNodes.Count != 1)
            {
                this.nodeException(originalResult, "number of child nodes = " + originalResult.ChildNodes.Count);
            }
            if (newResult.ChildNodes.Count != 1)
            {
                this.nodeException(newResult, "number of child nodes = " + newResult.ChildNodes.Count);
            }
            XmlNode originalNode = originalResult.ChildNodes[0];
            XmlNode newNode = newResult.ChildNodes[0];
            string name = originalNode.Name;
            string str2 = newNode.Name;
            if (name.Equals(str2))
            {
                if (name.Equals("object"))
                {
                    this.compareObjectNodes(originalNode, newNode);
                }
                else if (name.Equals("struct"))
                {
                    this.compareStructNodes(originalNode, newNode);
                }
                else
                {
                    this.nodeException(newResult, "Unknown result type " + name);
                }
            }
            else
            {
                this.nodesDiffer(originalNode, newNode, "Different result types");
            }
        }

        private void compareStructNodes(XmlNode originalStruct, XmlNode newStruct)
        {
            string str = this.getAttValue(originalStruct, "type");
            string str2 = this.getAttValue(newStruct, "type");
            if (!str.Equals(str2))
            {
                this.nodesDiffer(originalStruct, newStruct, "Different types");
            }
            if (originalStruct.ChildNodes.Count != newStruct.ChildNodes.Count)
            {
                this.nodesDiffer(originalStruct, newStruct, "Different number of elements");
            }
            for (int i = 0; i < originalStruct.ChildNodes.Count; i++)
            {
                this.getAttValue(originalStruct.ChildNodes[i], "name");
                this.getAttValue(newStruct.ChildNodes[i], "name");
                this.compareResultNodes(originalStruct.ChildNodes[i], newStruct.ChildNodes[i]);
            }
        }

        private bool doublesEqual(string originalValue, string newValue)
        {
            try
            {
                double num = double.Parse(newValue);
                double num2 = double.Parse(originalValue);
                return (Math.Abs((double) (num - num2)) < 1E-11);
            }
            catch (FormatException)
            {
                return false;
            }
            catch (OverflowException)
            {
                return false;
            }
        }

        private string getAttValue(XmlNode node, string attname)
        {
            XmlAttributeCollection attributes = node.Attributes;
            if (attributes == null)
            {
                this.nodeException(node, "No attributes defined");
            }
            XmlAttribute namedItem = (XmlAttribute) attributes.GetNamedItem(attname);
            if (namedItem == null)
            {
                this.nodeException(node, "No attribute \"" + attname + "\"");
            }
            return namedItem.Value;
        }

        private string getNodeDescription(XmlNode node)
        {
            string name = node.Name;
            if (name.Equals("method"))
            {
                return this.getAttValue(node, "name");
            }
            if (name.Equals("output"))
            {
                return ("param: " + this.getAttValue(node, "name"));
            }
            if (name.Equals("result"))
            {
                return "result";
            }
            if (name.Equals("struct"))
            {
                return ("struct: " + this.getAttValue(node, "type"));
            }
            if (name.Equals("elem"))
            {
                return ("elem: " + this.getAttValue(node, "name"));
            }
            return name;
        }

        private void nodeException(XmlNode node, string message)
        {
            for (XmlNode node2 = node.ParentNode; node2 != null; node2 = node2.ParentNode)
            {
                XmlNode node3 = node2["context"];
                if (node3 != null)
                {
                    Trace.WriteLine("Variable name \"" + this.getAttValue(node3, "variable_name") + "\" line: " + this.getAttValue(node3, "line_number"));
                    break;
                }
                Trace.WriteLine(" " + node.Name);
            }
            throw new Exception("Node " + node.Name + " : " + message);
        }

        private void nodesDiffer(XmlNode originalNode, XmlNode newNode, string message)
        {
            if (!this.printed)
            {
                Trace.WriteLine("=========================================================================================\n");
                Trace.WriteLine("Differences found when comparing '" + this.originalFile + "' and '" + this.newFile + "'\n");
                Trace.WriteLine("=========================================================================================\n");
                this.printed = true;
            }
            this.hadErrors = true;
            XmlNode parentNode = originalNode;
            ArrayList list = new ArrayList();
            while (parentNode != null)
            {
                XmlNode node2 = parentNode["context"];
                if (node2 != null)
                {
                    Trace.WriteLine("Variable " + this.getAttValue(node2, "variable_name") + " line: " + this.getAttValue(node2, "line_number"));
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        Trace.Write(list[i]);
                        Trace.Write(" ");
                    }
                    Trace.WriteLine(message);
                    Trace.WriteLine("");
                    return;
                }
                list.Add(this.getNodeDescription(parentNode));
                parentNode = parentNode.ParentNode;
            }
        }
    }
}

