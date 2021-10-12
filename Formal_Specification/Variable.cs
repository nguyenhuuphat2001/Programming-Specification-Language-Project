﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Formal_Specification
{
    class Variable
    {
        string name;
        string dataType;

        public Variable() { }

        public Variable(string para)
        {
            string[] words = para.Split(":");
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
            string dataType = "";
            switch (this.dataType)
            {
                case "N":
                    dataType = "double";
                    break;

                case "R":
                    dataType = "float";
                    break;

                case "Z":
                    dataType = "double";
                    break;

                case "B":
                    dataType = "bool";
                    break;

            }

            return dataType;
        }
        
        public string printInputCSharp()
        {
            return "\n\t\t\tConsole.WriteLine(\"Nhap "+this.name +" : \");" + 
                 "\n\t\t\t" + this.name+" = "+this.identifyDataTypeCSharp()+".Parse(Console.ReadLine());";
        }

        public string printDeclareCSharp()
        {
            return this.identifyDataTypeCSharp() + " " + this.getName();
        }

       
    }
}