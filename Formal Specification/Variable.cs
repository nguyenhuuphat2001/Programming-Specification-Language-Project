using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formal_Specification
{
    class Variable
    {
        string name;
        string dataType;

        public Variable() { }

        public Variable(string para)
        {
            string[] words = para.Split(':');
            this.name = words[0];
            this.dataType = words[1];
        }

        public string getName()
        {
            return this.name;
        }

        public string getDataType()
        {
            return this.dataType;
        }

        public string identifyDataTypeCSharp()
        {
            string output = "";
            switch (this.dataType)
            {
                case "N":
                    output = "int";
                    break;

                case "R":
                    output = "double";
                    break;

                case "Z":
                    output = "int";
                    break;

                case "B":
                    output = "bool";
                    break;

                case "char*":
                    output = "string";
                    break;
                case "R*":
                    output = "double[]";
                    break;
            }

            return output;
        }
          
        public string printInputCSharp()
        {
            return "\n\t\t\tConsole.Write(\"Input " + this.name + " : \");" +
                 "\n\t\t\t" + this.name + " = " + this.identifyDataTypeCSharp() + ".Parse(Console.ReadLine());";
        }

        public string printInputArrayCSharp(string nVariable)
        {
            //a = new double[n];
            string output = "";

            string arrayDataType = this.identifyDataTypeCSharp().Replace("[", "").Replace("]", "");

            output += "\n\t\t\t" + this.name + " = new " + arrayDataType + "[" + nVariable + "];";

            output +=
                "\n\t\t\tConsole.WriteLine(\"Input array " + this.name + " \");" +
                "\n\t\t\tfor (int i = 0; i < " + nVariable + "; i++)" +
                "\n\t\t\t{" +
                "\n\t\t\t\tConsole.Write(\"Input " + this.name + "[{0}]: \", i);" +
                "\n\t\t\t\t" + this.name + "[i]" + " = " + arrayDataType + ".Parse(Console.ReadLine());" +
                "\n\t\t\t}";

            return output;
        }
        
   //     Console.WriteLine("Input array : ");
			//for (int i = 0; i<n; i++)
   //         {
			//	Console.Write("Input a[{0}] : ", i);
			//	a[i] = double.Parse(Console.ReadLine());
    //         }

        public string printDeclareCSharp()
        {
            return this.identifyDataTypeCSharp() + " " + this.getName();
        }

        public string printInitializeCSharp()
        {
            string output = "";
            if (this.identifyDataTypeCSharp() == "string")
                output = string.Format("{0} {1} = \"\";", this.identifyDataTypeCSharp(), this.getName());
            else if (this.identifyDataTypeCSharp() == "bool")
                output = string.Format("{0} {1} = false;", this.identifyDataTypeCSharp(), this.getName());
            else
            {
                output = string.Format("{0} {1} = 0;", this.identifyDataTypeCSharp(), this.getName());
            };
            return output;
        }

        public string printArrayInitializeCSharp(string nVariable)
        {
            string output = "";

            string arrayDataType = this.identifyDataTypeCSharp().Replace("[", "").Replace("]", "");

            output += string.Format("{0} {1} = new {2}[{3}];", this.identifyDataTypeCSharp(), this.getName(), arrayDataType, nVariable);

            return output;

        }

        //JAVA CODE

        public string identifyDataTypeJava()
        {
            string output = "";
            switch (this.dataType)
            {
                case "N":
                    output = "int";
                    break;

                case "R":
                    output = "double";
                    break;

                case "Z":
                    output = "int";
                    break;

                case "B":
                    output = "boolean";
                    break;

                case "char*":
                    output = "String";
                    break;
                case "R*":
                    output = "double[]";
                    break;
            }

            return output;
        }

        public string identifyDataTypeUpcaseJava()
        {
            string output = "";
            switch (this.dataType)
            {
                case "N":
                    output = "Int";
                    break;

                case "R":
                    output = "Double";
                    break;

                case "Z":
                    output = "Int";
                    break;

                case "B":
                    output = "Boolean";
                    break;

                case "char*":
                    output = "String";
                    break;
                case "R*":
                    output = "Double[]";
                    break;
            }

            return output;
        }

        public string printDeclareJava()
        {
            return this.identifyDataTypeJava() + " " + this.getName();
        }

        public string printInitializeJava()
        {
            string output = "";
            if (this.identifyDataTypeJava() == "String")
                output = string.Format("{0} {1} = \"\";", this.identifyDataTypeJava(), this.getName());
            else if (this.identifyDataTypeJava() == "boolean")
                output = string.Format("{0} {1} = false;", this.identifyDataTypeJava(), this.getName());
            else
            {
                output = string.Format("{0} {1} = 0;", this.identifyDataTypeJava(), this.getName());
            };
            return output;
        }

        public string printInputJava()
        {
            return "\n\t\tSystem.out.print(\"Input " + this.name + " : \");" +
                 "\n\t\t" +  this.name + " = " + "myObj.next" + this.identifyDataTypeUpcaseJava() + "();";
        }

        public string printArrayInitializeJava(string nVariable)
        {
            string output = "";

            string arrayDataType = this.identifyDataTypeJava().Replace("[", "").Replace("]", "");

            output += string.Format("{0} {1} = new {2}[{3}];", this.identifyDataTypeJava(), this.getName(), arrayDataType, nVariable);

            return output;

        }

        public string printInputArrayJava(string nVariable)
        {
            //a = new double[n];
            string output = "";

            string arrayDataType = this.identifyDataTypeJava().Replace("[", "").Replace("]", "");
            string arrayDataTypeInput = this.identifyDataTypeUpcaseJava().Replace("[", "").Replace("]", "");

            output += "\n\t\t" + this.name + " = new " + arrayDataType + "[" + nVariable + "];";

            output +=
                "\n\t\tSystem.out.println(\"Input array " + this.name + " : \");" +
                "\n\t\tfor (int i = 0; i < " + nVariable + "; i++)" +
                "\n\t\t{" +
                "\n\t\t\tSystem.out.print(\"Input " + this.name + "[\"" + " + i + " + "\"]: " +"\");" +
                "\n\t\t\t" + this.name + "[i]" + " = " + "myObj.next" + arrayDataTypeInput + "();" +
            "\n\t\t}";

            return output;
        }
    }
}
