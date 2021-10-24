using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using System.IO;

namespace Formal_Specification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            btnBuild.Click += BtnBuild_Click;
            btnRun.Click += BtnRun_Click;

            
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = "Out.exe";
            Button ButtonObject = (Button)sender;

            //textBox2.Text = "";
            System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();
            //Make sure we generate an EXE, not a DLL
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            CompilerResults results = codeProvider.CompileAssemblyFromSource(parameters, richTextBox2.Text);

            if (results.Errors.Count > 0)
            {
                /*textBox2.ForeColor = Color.Red;
                foreach (CompilerError CompErr in results.Errors)
                {
                    textBox2.Text = textBox2.Text +
                                "Line number " + CompErr.Line +
                                ", Error Number: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";" +
                                Environment.NewLine + Environment.NewLine;
                }*/
                
            }
            else
            {
                //Successful Compile
    /*            textBox2.ForeColor = Color.Blue;
                textBox2.Text = "Success!";
                //If we clicked run then launch our EXE
                if (ButtonObject.Text == "Run") Process.Start(Output);*/
                Process.Start(Output);
            }

        }

        private void BtnBuild_Click(object sender, EventArgs e)
        {

            if (rdBtnType1.Checked)
            {
                Function function = new Function();
                function.splitFunction(richTextBox1.Text);
                richTextBox2.Text = function.printFunction();
                string[] words = { "public", "void", "int", "float", "double", "ref" };
                foreach (string word in words)
                {
                    int startIndex = 0;
                    while (startIndex < richTextBox2.TextLength)
                    {
                        int wordStartIndex = richTextBox2.Find(word, startIndex, RichTextBoxFinds.None);
                        if (wordStartIndex != -1)
                        {
                            richTextBox2.SelectionStart = wordStartIndex;
                            richTextBox2.SelectionLength = word.Length;
                            richTextBox2.SelectionColor = Color.Blue;
                        }
                        else
                            break;
                        startIndex += wordStartIndex + 1;
                    }
                }
            }

            else if (rdBtnType2.Checked)
            {
               
                FunctionType2 functionType2 = new FunctionType2();

                functionType2.splitFunction(richTextBox1.Text);
                richTextBox2.Text = functionType2.printFunction();

            }
            
        }


    }
}
