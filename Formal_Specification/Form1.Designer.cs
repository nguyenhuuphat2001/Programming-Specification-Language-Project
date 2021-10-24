
namespace Formal_Specification
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnBuild = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.richTextBox2 = new System.Windows.Forms.RichTextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.rdBtnType1 = new System.Windows.Forms.RadioButton();
            this.rdBtnType2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // btnBuild
            // 
            this.btnBuild.Location = new System.Drawing.Point(425, 42);
            this.btnBuild.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnBuild.Name = "btnBuild";
            this.btnBuild.Size = new System.Drawing.Size(129, 46);
            this.btnBuild.TabIndex = 2;
            this.btnBuild.Text = "Build";
            this.btnBuild.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox1.Location = new System.Drawing.Point(21, 154);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(575, 572);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // richTextBox2
            // 
            this.richTextBox2.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.richTextBox2.Location = new System.Drawing.Point(590, 0);
            this.richTextBox2.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.richTextBox2.Name = "richTextBox2";
            this.richTextBox2.Size = new System.Drawing.Size(938, 894);
            this.richTextBox2.TabIndex = 4;
            this.richTextBox2.Text = "";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(425, 96);
            this.btnRun.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(129, 46);
            this.btnRun.TabIndex = 5;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            // 
            // rdBtnType1
            // 
            this.rdBtnType1.AutoSize = true;
            this.rdBtnType1.Checked = true;
            this.rdBtnType1.Location = new System.Drawing.Point(99, 42);
            this.rdBtnType1.Name = "rdBtnType1";
            this.rdBtnType1.Size = new System.Drawing.Size(98, 34);
            this.rdBtnType1.TabIndex = 6;
            this.rdBtnType1.TabStop = true;
            this.rdBtnType1.Text = "Type 1";
            this.rdBtnType1.UseVisualStyleBackColor = true;
            // 
            // rdBtnType2
            // 
            this.rdBtnType2.AutoSize = true;
            this.rdBtnType2.Location = new System.Drawing.Point(99, 96);
            this.rdBtnType2.Name = "rdBtnType2";
            this.rdBtnType2.Size = new System.Drawing.Size(98, 34);
            this.rdBtnType2.TabIndex = 6;
            this.rdBtnType2.Text = "Type 2";
            this.rdBtnType2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1551, 900);
            this.Controls.Add(this.rdBtnType2);
            this.Controls.Add(this.rdBtnType1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.richTextBox2);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.btnBuild);
            this.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
            this.Name = "Form1";
            this.Text = "Convert";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBuild;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.RichTextBox richTextBox2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.RadioButton rdBtnType1;
        private System.Windows.Forms.RadioButton rdBtnType2;
    }
}

