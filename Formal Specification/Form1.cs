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
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Formal_Specification
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            newToolStripMenuItem.Click += NewToolStripMenuItem_Click;
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            saveToolStripMenuItem.Click += SaveToolStripMenuItem_Click;
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            generateToolStripMenuItem.Click += BtnGenerate_Click;
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            newStripButton1.Click  += NewToolStripMenuItem_Click;
            openStripButton2.Click += OpenToolStripMenuItem_Click;
            saveStripButton3.Click  += SaveToolStripMenuItem_Click;
            undoStripLabel1.Click += ToolStripLabel1_Click;
            redoStripLabel2.Click += ToolStripLabel2_Click;
            btnGenerate.Click += BtnGenerate_Click;
            btnRun.Click += BtnRun_Click;          
        }
      
        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save generate code", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveGenerateCode();                  
                }
            }
            richTextBox1.Text = "";
            fastColoredTextBox1.Text = "";
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.Text != "")
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to save generate code", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dialogResult == DialogResult.Yes)
                {
                    SaveGenerateCode();
                }               
            }
            openFileDialog1.ShowDialog();
            try
            {
                string file_name = openFileDialog1.FileName;
                string input = File.ReadAllText(file_name);
                richTextBox1.Text = input;
            }
            catch
            {
                MessageBox.Show("Can't open file");
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.Text != "")
            {
                SaveGenerateCode();
            }
            else
            {
                MessageBox.Show("There isn't content to save");
            }
                
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is a tool for generating programming language with: " +
                "\r\n - Input: a formal specification function (implicit) " +
                "\r\n - Output: a script program written by C# and Java programming language. " +
                "\r\n - Support operators: +, -, *, /, %, >, <, =, !=, >=, <=, !, &&, ||" +
                "\r\n\t\t - Made by TP  ", "Formal Specification Generating Tool");
        }

        private void ToolStripLabel1_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanUndo == true)
            {
                if (richTextBox1.RedoActionName != "Delete")
                    richTextBox1.Undo();
            }
        }

        private void ToolStripLabel2_Click(object sender, EventArgs e)
        {
            if (richTextBox1.CanRedo == true)
            {
                if (richTextBox1.RedoActionName != "Delete")
                    richTextBox1.Redo();
            }
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            if(richTextBox1.Text == "")
            {
                MessageBox.Show("Please input formal specification function (implicit)");
                return;
            }    
            richTextBox1.Text = richTextBox1.Text.Replace("\tpre", "pre").Replace("\tpost", "post");
            string[] stringSeparators = new string[] { "(" };
            string[] words = richTextBox1.Text.Split(stringSeparators, StringSplitOptions.None);
            string nameFunc = words[0];
            string[] words1 = { nameFunc, "pre", "post" };
            string pattern = string.Join("|", words1.Select(phr => Regex.Escape(phr)));
            var matches = Regex.Matches(richTextBox1.Text, pattern, RegexOptions.IgnoreCase);
            foreach (Match m in matches)
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.Blue;
            }
            string[] words2 = { "&&", "||", "VM", "TH" , "TT" };
            string pattern2 = string.Join("|", words2.Select(phr => Regex.Escape(phr)));
            var matches2 = Regex.Matches(richTextBox1.Text, pattern2, RegexOptions.IgnoreCase);
            foreach (Match m in matches2)
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionColor = Color.Brown;
            }
            if (rdBtnCSharp.Checked)
            {
                // Type 1 CSharp
                if(!CheckType2(richTextBox1.Text))
                {
                    FunctionType1 function = new FunctionType1();
                    function.splitFunction(richTextBox1.Text);
                    fastColoredTextBox1.Text = function.printFunctionCSharp();
                }

                // Type 2 CSharp
                else
                {
                    FunctionType2 functionType2 = new FunctionType2();
                    functionType2.splitFunction(richTextBox1.Text);
                    fastColoredTextBox1.Text = functionType2.printFunctionCSharp();
                }
                
            }

            if (rdBtnJava.Checked)
            {
                // Type 1 Java
                if (!CheckType2(richTextBox1.Text))
                {
                    FunctionType1 function = new FunctionType1();
                    function.splitFunction(richTextBox1.Text);
                    fastColoredTextBox1.Text = function.printFunctionJava();
                }

                // Type 2 Java
                else
                {
                    FunctionType2 functionType2 = new FunctionType2();
                    functionType2.splitFunction(richTextBox1.Text);
                    fastColoredTextBox1.Text = functionType2.printFunctionJava();
                }                    
            }
        }

        private void BtnRun_Click(object sender, EventArgs e)
        {
            if (fastColoredTextBox1.Text == "")
            {
                MessageBox.Show("Please generate formal specification function (implicit)");
                return;
            }
            if (rdBtnCSharp.Checked)
            {
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                ICodeCompiler icc = codeProvider.CreateCompiler();
                string Output = "Out.exe";
                Button ButtonObject = (Button)sender;


                System.CodeDom.Compiler.CompilerParameters parameters = new CompilerParameters();

                parameters.GenerateExecutable = true;
                parameters.OutputAssembly = Output;
                CompilerResults results = icc.CompileAssemblyFromSource(parameters, fastColoredTextBox1.Text);


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
                string path = "C:\\Users\\PC\\Program.java";
                if (!File.Exists(path))
                {
                    File.WriteAllText(path, fastColoredTextBox1.Text);
                }
                else
                {
                    File.Delete(path);
                    File.WriteAllText(path, fastColoredTextBox1.Text);
                }

                Process process = new Process();
                ProcessStartInfo processStartInfo = new ProcessStartInfo();
                processStartInfo.FileName = "cmd.exe";
                processStartInfo.Arguments = @"/k java " + path.Replace("\\\\", "\\");
                process.StartInfo = processStartInfo;
                process.Start();
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

        public void SaveGenerateCode()
        {
            SaveFileDialog savefile = new SaveFileDialog();
            savefile.Filter = "Program|.txt";
            if (savefile.ShowDialog() == DialogResult.OK)
            {
                StreamWriter sw = new StreamWriter(savefile.FileName);
                sw.WriteLine(fastColoredTextBox1.Text);
                sw.Close();
                DialogResult dialogResult = MessageBox.Show("Save succesfully", "Notify", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

    }
}
