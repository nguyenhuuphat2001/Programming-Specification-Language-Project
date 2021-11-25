using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            if (rdBtnCSharp.Checked)
            {
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                ICodeCompiler icc = codeProvider.CreateCompiler();
                string Output = "Out.exe";
                Button ButtonObject = (Button)sender;


                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();

                parameters.GenerateExecutable = true;
                parameters.OutputAssembly = Output;
                CompilerResults results = icc.CompileAssemblyFromSource(parameters, richTextBox2.Text);


                if (results.Errors.Count > 0)
                {
                    string error_string = "";
                    foreach (CompilerError CompErr in results.Errors)
                    {
                        error_string = "Fail" +
                        "Line number " + CompErr.Line +
                        ", Error Number: " + CompErr.ErrorNumber +
                        ", '" + CompErr.ErrorText + ";" +
                        Environment.NewLine + Environment.NewLine;
                    }
                    DialogResult error = MessageBox.Show(error_string, "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (ButtonObject.Text == "Run") Process.Start(Output);
                }
            }
            else
            {
                /*string path = Environment.CurrentDirectory + "\\Program.java";
                if (!File.Exists(path))
                {
                   *//* File.CreateText(path);*//*
                    File.WriteAllText(path, richTextBox2.Text);
                }*/

                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "cmd.exe";
                /*processStartInfo.Arguments = @"/k java "+path.Replace("\\\\","\\");*/
                processStartInfo.Arguments = @"/k java E:\DTHT\Formal_Specification\CodingProject\Formal Specification\bin\Debug\Program.java";
                process.StartInfo = processStartInfo;
                process.Start();
            }
            


        }


        private void BtnBuild_Click(object sender, EventArgs e)
        {

            if (rdBtnCSharp.Checked)
            {
                // Type 1 CSharp
                if(!CheckType2(richTextBox1.Text))
                {
                    FunctionType1 function = new FunctionType1();
                    function.splitFunction(richTextBox1.Text);
                    richTextBox2.Text = function.printFunctionCSharp();
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

                // Type 2 CSharp
                else
                {
                    FunctionType2 functionType2 = new FunctionType2();

                    functionType2.splitFunction(richTextBox1.Text);
                    richTextBox2.Text = functionType2.printFunctionCSharp();
                }
                
            }

            if (rdBtnJava.Checked)
            {
                // Type 1 Java
                if (!CheckType2(richTextBox1.Text))
                {
                    FunctionType1 function = new FunctionType1();
                    function.splitFunction(richTextBox1.Text);
                    richTextBox2.Text = function.printFunctionJava();
                    string[] words = { "public", "void", "int", "float", "double", " String" };
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

                // Type 2 Java
                else
                {
                    FunctionType2 functionType2 = new FunctionType2();

                    functionType2.splitFunction(richTextBox1.Text);
                    richTextBox2.Text = functionType2.printFunctionJava();

                }
                    
            }
        }

        public bool CheckType2(string formalLanguage)
        {
            if ((formalLanguage.Contains("TT") && formalLanguage.Contains("TH")) || (formalLanguage.Contains("VM") && formalLanguage.Contains("TH")))
            {
                return true;
            }
            return false;
        }


    }
}
