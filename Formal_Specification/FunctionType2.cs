﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Formal_Specification
{
    class FunctionType2
    {
        string nameFunc;

        List<Variable> listVariable;

        Variable result;

        string preCondition;

        string postCondition;

        string ifCondition;

        List<String> listForCondition;

        public FunctionType2()
        {
            listVariable = new List<Variable>();
            result = new Variable();
            listForCondition = new List<String>();
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
                if (array[i].Contains(this.result.getName() + "="))
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

        public void splitPost(string SLCode)
        {
            //Split line
            // Replace {1..n-1} to {1tn-1}
            string a = SLCode.Replace("..", "t");
            string[] stringSeparators = new string[] { "." };
            string[] words = a.Split(stringSeparators, StringSplitOptions.None);

            // if condition
            ifCondition = words[words.Length - 1];

            // listForCondition - Remove unuseful charater
            for (int i = 0; i <= words.Length - 2; i++)
            {
                String str = words[i];
                str = str.Replace("kq=", "").Replace("(", "").Replace("{", "").Replace("}", "");

                listForCondition.Add(str);
            }

        }

        public string printOneForloopCdtPost(string vmCdt, string cdtType)
        {
            string output = "";
            string comparisonSign = "";
            string reverseComparisonSign = "";

            // identify forLoopRange
            string str = vmCdt.Replace("VM", "").Replace("TT", "").Replace("i", "").Replace("j", "").Replace("TH", "");
            string[] stringSeparators = new string[] { "t" };
            string[] forLoopRange = str.Split(stringSeparators, StringSplitOptions.None);

            // print forLoop
            output += "\n\t\t\tfor (int i = " + forLoopRange[0] + "-1; i <= " + forLoopRange[1] + "-1; i++)\n\t\t\t{\n\t\t\t\t";
            
            // identify ifCondition
            string ifCondition = this.ifCondition;
            ifCondition = ifCondition.Replace("(", "").Replace(")", "").Replace("a", "");


            if (ifCondition.Contains("<="))
            {
                comparisonSign = "<=";
                reverseComparisonSign = ">=";
            }
            else if (ifCondition.Contains(">="))
            {
                comparisonSign = ">=";
                reverseComparisonSign = "<=";
            }
            else if (ifCondition.Contains("<"))
            {
                comparisonSign = "<";
                reverseComparisonSign = ">";
            }
            else if (ifCondition.Contains(">"))
            {
                comparisonSign = ">";
                reverseComparisonSign = "<";
            }

            string[] stringSeparators2 = new string[] { comparisonSign };
            string[] ifCdtArray = ifCondition.Split(stringSeparators2, StringSplitOptions.None);

            // VM condition type
            if (cdtType.Equals("VM"))
            {
                // print if condition
                output += "if (a[" + ifCdtArray[0] + "] " + reverseComparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t{";

                output += "\n\t\t\t\t\treturn false;\n\t\t\t\t}";
                output += "\n\t\t\t}";
                output += "\n\t\t\treturn true;";
                output += "\n\t\t}";
            }

            // TT condition type
            if (cdtType.Equals("TT"))
            {
                // print if condition
                output += "if (a[" + ifCdtArray[0] + "] " + comparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t{";

                output += "\n\t\t\t\t\treturn true;\n\t\t\t\t}";
                output += "\n\t\t\t}";
                output += "\n\t\t\treturn false;";
                output += "\n\t\t}";
            }


            return output;
        }

        public string printTwoForloopCdtPost(string firstCondition, string secondCondition, string firstCdt, string secondCdt)
        {
            string output = "";
            string comparisonSign = "";
            string reverseComparisonSign = "";

            // identify forLoopRange for first condition
            string str1 = firstCondition.Replace("VM", "").Replace("TT", "").Replace("i", "").Replace("TH", "");
            string[] stringSeparators1 = new string[] { "t" };
            string[] forLoopRange1 = str1.Split(stringSeparators1, StringSplitOptions.None);

            // identify forLoopRange for second condition
            string str2 = secondCondition.Replace("VM", "").Replace("TT", "").Replace("j", "").Replace("TH", "");
            string[] stringSeparators2 = new string[] { "t" };
            string[] forLoopRange2 = str2.Split(stringSeparators2, StringSplitOptions.None);


            // print declare variable
            // TT vs VM
            if ((firstCdt.Equals("TT") && secondCdt.Equals("VM")) 
                || (firstCdt.Equals("VM") && secondCdt.Equals("TT")))
            {
                output += "\n\t\t\tint count = 0;";
            }

                // print forLoop first condition
                output += "\n\t\t\tfor (int i = " + forLoopRange1[0] + "-1; i <= " + forLoopRange1[1] + "-1; i++)\n\t\t\t{";

            // print forLoop first condition
            output += "\n\t\t\t\tfor (int j = " + forLoopRange2[0] + "; j <= " + forLoopRange2[1] + "-1; j++)\n\t\t\t\t{";


            // identify ifCondition
            string ifCondition = this.ifCondition;
            ifCondition = ifCondition.Replace("(", "").Replace(")", "").Replace("a", "");


            if (ifCondition.Contains("<="))
            {
                comparisonSign = "<=";
                reverseComparisonSign = ">=";
            }
            else if (ifCondition.Contains(">="))
            {
                comparisonSign = ">=";
                reverseComparisonSign = "<=";
            }
            else if (ifCondition.Contains("<"))
            {
                comparisonSign = "<";
                reverseComparisonSign = ">";
            }
            else if (ifCondition.Contains(">"))
            {
                comparisonSign = ">";
                reverseComparisonSign = "<";
            }

            string[] stringSeparators3 = new string[] { comparisonSign };
            string[] ifCdtArray = ifCondition.Split(stringSeparators3, StringSplitOptions.None);


            // print if condition for TT vs TT
            if(firstCdt.Equals("TT") && secondCdt.Equals("TT"))
            {
                output += "\n\t\t\t\t\tif (a[" + ifCdtArray[0] + "] " + comparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t\t{";
                output += "\n\t\t\t\t\t\treturn true;\n\t\t\t\t\t}";

                output += "\n\t\t\t}";

                output += "\n\t\t\treturn false;";
                output += "\n\t\t}";
            }

            // print if condition for TT vs VM
            else if (firstCdt.Equals("TT") && secondCdt.Equals("VM"))
            {
                output += "\n\t\t\t\t\tif (a[" + ifCdtArray[0] + "] " + comparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t\t{";
                output += "\n\t\t\t\t\t\tcount++;\n\t\t\t\t\t}";

                output += "\n\t\t\t\t\tif (count == " + forLoopRange2[1] + "-1-" + "j+1)" + "\n\t\t\t\t\t{";
                output += "\n\t\t\t\t\t\treturn true;\n\t\t\t\t\t}";
                output += "\n\t\t\t}";

                output += "\n\t\t\treturn false;";
                output += "\n\t\t}";
            }

            // print if condition for VM vs TT
            else if (firstCdt.Equals("VM") && secondCdt.Equals("TT"))
            {
                output += "\n\t\t\t\t\tif (a[" + ifCdtArray[0] + "] " + comparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t\t{";
                output += "\n\t\t\t\t\t\tcount++;";
                output += "\n\t\t\t\t\t\tbreak;\n\t\t\t\t\t}";

                output += "\n\t\t\t\t}";
                output += "\n\t\t\t}";

                output += "\n\t\t\tif (count == " + forLoopRange1[1] + ")\n\t\t\t{";
                output += "\n\t\t\t\treturn true;\n\t\t\t}";
                output += "\n\t\t\treturn false;";

                output += "\n\t\t}";
            }

            // print if condition for VM vs VM
            else if (firstCdt.Equals("VM") && secondCdt.Equals("VM"))
            {
                output += "\n\t\t\t\t\tif (a[" + ifCdtArray[0] + "] " + reverseComparisonSign + " a[" + ifCdtArray[1] + "])\n\t\t\t\t\t{";
                output += "\n\t\t\t\t\t\treturn false;\n\t\t\t\t\t}";

                output += "\n\t\t\t\t}";
                output += "\n\t\t\t}";

                output += "\n\t\t\treturn true;";

                output += "\n\t\t}";
            }

            return output;
        }

        // print and identify ForLoop Form
        public string printForFormPostCdt()
        {
            string output = "";

            // One condition
            if(this.listForCondition.Count == 1)
            {
                string[] array = listForCondition.ToArray();
                string cdtType = array[0];

                if (cdtType.Contains("VM"))
                {
                    output += printOneForloopCdtPost(cdtType, "VM");
                }
                else if (cdtType.Contains("TT"))
                {
                    output += printOneForloopCdtPost(cdtType, "TT");
                }
            }

            // Two condition
            else if (this.listForCondition.Count == 2)
            {
                string[] array = listForCondition.ToArray();
                string firstCdt = array[0];
                string secondeCdt = array[1];

                if(firstCdt.Contains("TT") && secondeCdt.Contains("VM"))
                {
                    output += printTwoForloopCdtPost(firstCdt, secondeCdt, "TT", "VM");
                }
                else if (firstCdt.Contains("TT") && secondeCdt.Contains("TT"))
                {
                    output += printTwoForloopCdtPost(firstCdt, secondeCdt, "TT", "TT");
                }
                else if (firstCdt.Contains("VM") && secondeCdt.Contains("VM"))
                {
                    output += printTwoForloopCdtPost(firstCdt, secondeCdt, "VM", "VM");
                }
                else if (firstCdt.Contains("VM") && secondeCdt.Contains("TT"))
                {
                    output += printTwoForloopCdtPost(firstCdt, secondeCdt, "VM", "TT");
                }
            }

            return output;
        }

        public string printPostConditionCSharp()
        {
            string output = "";

            splitPost(this.postCondition);

            output += printForFormPostCdt();

            //if (postCondition.Contains("||"))
            //{
            //    string[] array = postCondition.Split("||");
            //    for (int i = 0; i < array.Length; i++)
            //    {
            //        output += printIfFormPostCdtCSharp(array[i]);
            //    }
            //}
            //else if (postCondition.Contains("&&"))
            //{
            //    output = printIfFormPostCdtCSharp(postCondition);
            //}
            //else
            //{
            //    output = "\n\t\t\t" + postCondition + ";";
            //}
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

            output += "\n\t\t\t" + this.result.printDeclareCSharp() + " = 0;";

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
            //output += "\n\t\t\t" + this.result.printDeclareCSharp() + ";";
            output += printPostConditionCSharp();
            //output += "\n\t\t\treturn " + this.result.getName() + ";";
            //output += "\n\t\t}";

            //Main function
            output += printMainFunctionCSharp();
            output += "\n\t}" +
                "\n}";
            return output;
        }
    }
}
