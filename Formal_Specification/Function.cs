using System;
using System.Collections.Generic;
namespace Formal_Specification
{
    class Function
    {
        string nameFunc;

        List<Variable> listVariable;

        Variable result;

        string preCondition;

        string postCondition;

        public Function() {
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
            string[] words3 = words2[1].Split(",");
            foreach (var word in words3)
            {
                listVariable.Add(new Variable(word));
            }
            this.result = new Variable(words2[2]);

            //Second line
            this.preCondition = words[1];   

            //Third line
            this.postCondition = words[2];

        }
        public string printIfFormPreCdtCSharp(string cdt)
        {
            cdt = cdt.Replace("(", "").Replace(")", "");
            string output = "\n\t\t\tif (";
            string[] array = cdt.Split("&&");

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
            string[] array = cdt.Split("&&");
            string returnResult = "return 0";
            for (int i = 0; i < array.Length; i++)
            {               
                if (array[i].Contains(this.result.getName()+"="))
                {
                    returnResult = array[i];
                }
                else
                {
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
                string[] array = preCondition.Split("||");
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
                output = "\n\t\t\tif (" + preCondition + ")" +
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
                string[] array = postCondition.Split("||");
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
                output = "\n\t\t\t" +postCondition + ";";
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
                output += "\n\t\t\t" + listVariable[i].printDeclareCSharp() + " = 0;";
            }
            output += "\n\t\t\t" + this.result.printDeclareCSharp() + " =0;";
            output += "\n\t\t\tprogram.Input_" + this.nameFunc + "(";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "ref " +  listVariable[i].getName();
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

        public string printFunction()
        {
            //Open
            string output = "using System;" +
                "\nnamespace test" +
                "\n{" +
                "\n\tclass Program" +
                "\n\t{";

            //Input function
            output += "\n\t\tpublic void Input_" + this.nameFunc + "(";
            for (int i = 0; i < listVariable.Count; i++)
            {
                output += "ref "+listVariable[i].identifyDataTypeCSharp()+" "+ listVariable[i].getName();
                if(i< listVariable.Count - 1)
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
            output += "\n\t\tpublic void Output_" + this.nameFunc + "(" + this.result.identifyDataTypeCSharp() + " " + this.result.getName()+ ")" + 
                "\n\t\t{" + 
                "\n\t\t\tConsole.WriteLine(\"Result: {0} \","+ this.result.getName() + ");" +
                "\n\t\t}";

            //Pre function
            output += "\n\t\tpublic int Check_" + this.nameFunc + "(";
            output += printListVariableDeclareCSharp();
            output += printPreConditionCSharp();
            output += "\n\t\t}";

            //Post Function
            output += "\n\t\tpublic " + this.result.identifyDataTypeCSharp() + " " + this.nameFunc + "(";
            output += printListVariableDeclareCSharp();
            output += "\n\t\t\t" + this.result.printDeclareCSharp() + ";";
            output += printPostConditionCSharp();
            output += "\n\t\t\treturn " + this.result.getName() + ";";
            output += "\n\t\t}";

            //Main function
            output += printMainFunctionCSharp();
            output += "\n\t}" +
                "\n}";
            return output;
        }
    }
}
