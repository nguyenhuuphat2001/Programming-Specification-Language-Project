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
            return "\n\t\t\tConsole.WriteLine(\"Nhap " + this.name + " : \");" +
                 "\n\t\t\t" + this.name + " = " + this.identifyDataTypeCSharp() + ".Parse(Console.ReadLine());";
        }

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

    }
}
