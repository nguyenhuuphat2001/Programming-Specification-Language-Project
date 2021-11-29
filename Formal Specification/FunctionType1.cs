using System;
using System.Collections.Generic;

namespace Formal_Specification
{
    class FunctionType1
    {
        string nameFunc;

        List<Variable> listVariable;

        Variable result;

        string preCondition;

        string postCondition;

        public FunctionType1()
        {
            listVariable = new List<Variable>();
            result = new Variable();
        }

        public void splitFunction(string SLCode)
        {
            //Split line
            string a = SLCode.Replace("\n", "").Replace("\t", "").Replace(" ", "");
            string[] stringSeparators = new string[] { "pre", "post" };
            string[] words = a.Split(stringSeparators, StringSplitOptions.None);

            //First line
            string[] stringSeparators2 = new string[] { "(", ")" };
            string[] words2 = words[0].Split(stringSeparators2, StringSplitOptions.None);
            this.nameFunc = words2[0];
            string[] words3 = words2[1].Split(',');
            foreach (var word in words3)
            {
                listVariable.Add(new Variable(word));
            }
            this.result = new Variable(words2[2]);

            //Second line
            this.preCondition = words[1];

            //Third line
            this.postCondition = words[2].Replace("TRUE","true").Replace("FALSE","false");

        }
        public string printIfFormPreCdtCSharp(string cdt)
        {
            cdt = cdt.Replace("(", "").Replace(")", "");
            string output = "\n\t\t\tif (";
            string[] array = cdt.Split(new[] { "&&" }, StringSplitOptions.None);

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    output += array[i] + " && ";
                }
                else
                {
                    output += array[i] + ")" +
                        "\n\t\t\t{" +
                        "\n\t\t\t\treturn 1;" +
                        "\n\t\t\t}";
                }
            }
            return output;
        }

        public string printIfFormPostCdtCSharp(string cdt)
        {
            cdt = cdt.Replace("(", "").Replace(")", "");
            string output = "\n\t\t\tif (";
            string[] array = cdt.Split(new[] { "&&" }, StringSplitOptions.None);
            string returnResult = "return 0";
            for (int i = 0; i < array.Length; i++)
            {
              
                if (array[i].Contains(this.result.getName() + "="))
                {
                    returnResult = array[i];
                }
                else
                {

                    if (array[i].Contains("=") && !array[i].Contains("!=") && !array[i].Contains("<=") && !array[i].Contains(">=") && !array[i].Contains("=="))
                        array[i]=array[i].Replace("=", "==");
                    if (i < array.Length - 1)
                    {
                        output += array[i] + " && ";
                    }
                    else
                    {
                        output += array[i] + ")" +
                            "\n\t\t\t{" +
                            "\n\t\t\t\t" + returnResult + ";" +
                            "\n\t\t\t}";
                    }
                }
            }
            return output;
        }

        public string printPreConditionCSharp()
        {
            string output = "";
            if (preCondition == "")
            {
                output = "\n\t\t\treturn 1;";
            }
            else if (preCondition.Contains("||"))
            {
                string[] array = preCondition.Split(new[] { "||" }, StringSplitOptions.None);
                for (int i = 0; i < array.Length; i++)
                {
                    output += printIfFormPreCdtCSharp(array[i]);
                }
                output += "\n\t\t\treturn 0; ";
            }
            else if (preCondition.Contains("&&"))
            {
                output = printIfFormPreCdtCSharp(preCondition);
                output += "\n\t\t\treturn 0; ";
            }
            else
            {
                output = "\n\t\t\tif (" + preCondition.Replace("(","").Replace(")","") + ")" +
                         "\n\t\t\t\treturn 1;" +
                         "\n\t\t\treturn 0;";
            }
            return output;
        }

        public string printPostConditionCSharp()
        {
            string output = "";
            if (postCondition.Contains("||"))
            {
                string[] array = postCondition.Split(new[] { "||" }, StringSplitOptions.None);
                for (int i = 0; i < array.Length; i++)
                {
                    output += printIfFormPostCdtCSharp(array[i]);
                }
            }
            else if (postCondition.Contains("&&"))
            {
                output = printIfFormPostCdtCSharp(postCondition);
            }
            else
            {
                output = "\n\t\t\t" + postCondition.Replace("(","").Replace(")","") + ";";
            }
            return output;
        }

        public string printListVariableDeclareCSharp()
        {
            string output = "";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += listVariable[i].printDeclareCSharp();
                if (i < listVariable.Count - 1)
                {
                    output += ", ";
                }
                else
                {
                    output += ")\n\t\t{";
                }
            }
            return output;
        }

        public string printListVariableCSharp()
        {
            string output = "";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += listVariable[i].getName();
                if (i < listVariable.Count - 1)
                {
                    output += ", ";
                }
                else
                {
                    output += ")";
                }
            }
            return output;
        }


        public string printMainFunctionCSharp()
        {
            string output = "\n\t\tstatic void Main(string[] args)" +
                "\n\t\t{" +
                "\n\t\t\tProgram program = new Program();";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "\n\t\t\t" + listVariable[i].printInitializeCSharp();
            }

            output += "\n\t\t\t" + this.result.printInitializeCSharp();

            output += "\n\t\t\tprogram.Input_" + this.nameFunc + "(";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "ref " + listVariable[i].getName();
                if (i < listVariable.Count - 1)
                {
                    output += ", ";
                }
                else
                {
                    output += ");";
                }
            }
            output += "\n\t\t\t if (program.Check_" + this.nameFunc + "(" + printListVariableCSharp() + " == 1)";
            output += "\n\t\t\t{" +
                "\n\t\t\t\t" + this.result.getName() + " = program." + this.nameFunc + "(" + printListVariableCSharp() + ";" +
                "\n\t\t\t}";
            output += "\n\t\t\tprogram.Output_" + this.nameFunc + "(" + this.result.getName() + ");";
            output += "\n\t\t\tConsole.ReadLine();";
            output += "\n\t\t}";

            return output;
        }

        public string printFunctionCSharp()
        {
            //Open
            string output = "using System;" +
                "\nnamespace Formular_Specification" +
                "\n{" +
                "\n\tclass Program" +
                "\n\t{";

            //Input function
            output += "\n\t\tpublic void Input_" + this.nameFunc + "(";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "ref " + listVariable[i].identifyDataTypeCSharp() + " " + listVariable[i].getName();
                if (i < listVariable.Count - 1)
                {
                    output += ", ";
                }
                else
                {
                    output += ")\n\t\t{";
                }
            }
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += listVariable[i].printInputCSharp();
            }
            output += "\n\t\t}";

            //Output function
            output += "\n\t\tpublic void Output_" + this.nameFunc + "(" + this.result.identifyDataTypeCSharp() + " " + this.result.getName() + ")" +
                "\n\t\t{" +
                "\n\t\t\tConsole.WriteLine(\"Result: {0} \"," + this.result.getName() + ");" +
                "\n\t\t}";

            //Pre function
            output += "\n\t\tpublic int Check_" + this.nameFunc + "(";
            output += printListVariableDeclareCSharp();
            output += printPreConditionCSharp();
            output += "\n\t\t}";

            //Post Function
            output += "\n\t\tpublic " + this.result.identifyDataTypeCSharp() + " " + this.nameFunc + "(";
            output += printListVariableDeclareCSharp();
            output += "\n\t\t\t" + this.result.printInitializeCSharp();
            output += printPostConditionCSharp();
            output += "\n\t\t\treturn " + this.result.getName() + ";";
            output += "\n\t\t}";

            //Main function
            output += printMainFunctionCSharp();
            output += "\n\t}" +
                "\n}";
            return output;
        }

        //
        //JAVA CODE
        //

        public string printIfFormPreCdtJava(string cdt)
        {
            cdt = cdt.Replace("(", "").Replace(")", "");
            string output = "\n\t\tif (";
            string[] array = cdt.Split(new[] { "&&" }, StringSplitOptions.None);

            for (int i = 0; i < array.Length; i++)
            {
                if (i < array.Length - 1)
                {
                    output += array[i] + " && ";
                }
                else
                {
                    output += array[i] + ")" +
                        "\n\t\t{" +
                        "\n\t\t\treturn 1;" +
                        "\n\t\t}";
                }
            }
            return output;
        }

        public string printIfFormPostCdtJava(string cdt)
        {
            cdt = cdt.Replace("(", "").Replace(")", "");
            string output = "\n\t\tif (";
            string[] array = cdt.Split(new[] { "&&" }, StringSplitOptions.None);
            string returnResult = "return 0";
            for (int i = 0; i < array.Length; i++)
            {

                if (array[i].Contains(this.result.getName() + "="))
                {
                    returnResult = array[i];
                }
                else
                {

                    if (array[i].Contains("=") && !array[i].Contains("!=") && !array[i].Contains("<=") && !array[i].Contains(">=") && !array[i].Contains("=="))
                        array[i] = array[i].Replace("=", "==");
                    if (i < array.Length - 1)
                    {
                        output += array[i] + " && ";
                    }
                    else
                    {
                        output += array[i] + ")" +
                            "\n\t\t{" +
                            "\n\t\t\t" + returnResult + ";" +
                            "\n\t\t}";
                    }
                }
            }
            return output;
        }

        public string printPreConditionJava()
        {
            string output = "";
            if (preCondition == "")
            {
                output = "\n\t\treturn 1;";
            }
            else if (preCondition.Contains("||"))
            {
                string[] array = preCondition.Split(new[] { "||" }, StringSplitOptions.None);
                for (int i = 0; i < array.Length; i++)
                {
                    output += printIfFormPreCdtJava(array[i]);
                }
                output += "\n\t\treturn 0; ";
            }
            else if (preCondition.Contains("&&"))
            {
                output = printIfFormPreCdtJava(preCondition);
                output += "\n\t\treturn 0; ";
            }
            else
            {
                output = "\n\t\tif (" + preCondition.Replace("(", "").Replace(")", "") + ")" +
                         "\n\t\t\treturn 1;" +
                         "\n\t\treturn 0;";
            }
            return output;
        }

        public string printPostConditionJava()
        {
            string output = "";
            if (postCondition.Contains("||"))
            {
                string[] array = postCondition.Split(new[] { "||" }, StringSplitOptions.None);
                for (int i = 0; i < array.Length; i++)
                {
                    output += printIfFormPostCdtJava(array[i]);
                }
            }
            else if (postCondition.Contains("&&"))
            {
                output = printIfFormPostCdtJava(postCondition);
            }
            else
            {
                output = "\n\t\t" + postCondition.Replace("(", "").Replace(")", "") + ";";
            }
            return output;
        }

        public string printFunctionJava()
        {
            //Open
            string className = "Program";
            string variableClass = "program1";
            string output = "import java.util.Scanner;" + 
                "\npublic class " + className +" {";

            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "\n\t" + listVariable[i].printInitializeJava();
            }
            output += "\n\t" + this.result.printInitializeJava();

            //Input
            output += "\n\tpublic void Input_" + this.nameFunc + "(){" +
                "\n\t\tScanner myObj = new Scanner(System.in);";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += listVariable[i].printInputJava();
            }
            output += "\n\t}";

            //Output
            output += "\n\tpublic void Output_" + this.nameFunc + "(){" +
                "\n\t\tSystem.out.println(\"Result: \"+ " + this.result.getName() + ");" +
                "\n\t}";

            //Pre function
            output += "\n\tpublic int Check_" + this.nameFunc + "(){";
            output += printPreConditionJava();
            output += "\n\t}";

            //Post Function
            output += "\n\tpublic " + this.result.identifyDataTypeJava() + " " + this.nameFunc + "(){";
            output += "\n\t\t" + this.result.printInitializeJava();
            output += printPostConditionJava();
            output += "\n\t\treturn " + this.result.getName() + ";";
            output += "\n\t}";

            //Main function
            output += "\n\tpublic static void main(String[] args) {" + 
                "\n\t\tProgram program1 = new Program();" + 
                "\n\t\t" + variableClass + ".Input_" + this.nameFunc + "();" + 
                "\n\t\tif(" + variableClass + ".Check_" + this.nameFunc + "() == 1){" + 
                "\n\t\t\t" + variableClass + "." + this.result.getName() + " = " + variableClass + "." + this.nameFunc + "();" +
                "\n\t\t}" + 
                "\n\t\t" + variableClass + "." + "Output_" + this.nameFunc + "();" + 
                "\n\t}" + 
                "\n}";

            return output;
        }

    }
}
