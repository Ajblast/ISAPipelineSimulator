
namespace PipelineGUI
{
    partial class ControlGUI
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StepButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.LookupButton = new System.Windows.Forms.Button();
            this.Lookup1Input = new System.Windows.Forms.TextBox();
            this.Lookup1Output = new System.Windows.Forms.TextBox();
            this.Lookup2Input = new System.Windows.Forms.TextBox();
            this.Lookup2Output = new System.Windows.Forms.TextBox();
            this.Lookup3Input = new System.Windows.Forms.TextBox();
            this.Lookup3Output = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(385, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importAssemblyToolStripMenuItem,
            this.importBinaryToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // importAssemblyToolStripMenuItem
            // 
            this.importAssemblyToolStripMenuItem.Name = "importAssemblyToolStripMenuItem";
            this.importAssemblyToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.importAssemblyToolStripMenuItem.Text = "Import Assembly";
            this.importAssemblyToolStripMenuItem.Click += new System.EventHandler(this.importAssemblyToolStripMenuItem_Click);
            // 
            // importBinaryToolStripMenuItem
            // 
            this.importBinaryToolStripMenuItem.Name = "importBinaryToolStripMenuItem";
            this.importBinaryToolStripMenuItem.Size = new System.Drawing.Size(198, 26);
            this.importBinaryToolStripMenuItem.Text = "Import Binary";
            this.importBinaryToolStripMenuItem.Click += new System.EventHandler(this.importBinaryToolStripMenuItem_Click);
            // 
            // StepButton
            // 
            this.StepButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StepButton.Location = new System.Drawing.Point(13, 39);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(86, 38);
            this.StepButton.TabIndex = 1;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RunButton.Location = new System.Drawing.Point(103, 39);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(86, 38);
            this.RunButton.TabIndex = 2;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // LookupButton
            // 
            this.LookupButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LookupButton.Location = new System.Drawing.Point(287, 39);
            this.LookupButton.Name = "LookupButton";
            this.LookupButton.Size = new System.Drawing.Size(86, 38);
            this.LookupButton.TabIndex = 4;
            this.LookupButton.Text = "Lookup";
            this.LookupButton.UseVisualStyleBackColor = true;
            // 
            // Lookup1Input
            // 
            this.Lookup1Input.Location = new System.Drawing.Point(10, 119);
            this.Lookup1Input.Name = "Lookup1Input";
            this.Lookup1Input.Size = new System.Drawing.Size(167, 23);
            this.Lookup1Input.TabIndex = 5;
            // 
            // Lookup1Output
            // 
            this.Lookup1Output.Location = new System.Drawing.Point(212, 119);
            this.Lookup1Output.Name = "Lookup1Output";
            this.Lookup1Output.Size = new System.Drawing.Size(161, 23);
            this.Lookup1Output.TabIndex = 6;
            // 
            // Lookup2Input
            // 
            this.Lookup2Input.Location = new System.Drawing.Point(10, 174);
            this.Lookup2Input.Name = "Lookup2Input";
            this.Lookup2Input.Size = new System.Drawing.Size(167, 23);
            this.Lookup2Input.TabIndex = 7;
            // 
            // Lookup2Output
            // 
            this.Lookup2Output.Location = new System.Drawing.Point(212, 174);
            this.Lookup2Output.Name = "Lookup2Output";
            this.Lookup2Output.Size = new System.Drawing.Size(161, 23);
            this.Lookup2Output.TabIndex = 8;
            // 
            // Lookup3Input
            // 
            this.Lookup3Input.Location = new System.Drawing.Point(10, 226);
            this.Lookup3Input.Name = "Lookup3Input";
            this.Lookup3Input.Size = new System.Drawing.Size(167, 23);
            this.Lookup3Input.TabIndex = 9;
            // 
            // Lookup3Output
            // 
            this.Lookup3Output.Location = new System.Drawing.Point(212, 226);
            this.Lookup3Output.Name = "Lookup3Output";
            this.Lookup3Output.Size = new System.Drawing.Size(161, 23);
            this.Lookup3Output.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(10, 95);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 21);
            this.label1.TabIndex = 11;
            this.label1.Text = "Lookup1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(10, 150);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 21);
            this.label2.TabIndex = 12;
            this.label2.Text = "Lookup2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(10, 200);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 21);
            this.label3.TabIndex = 13;
            this.label3.Text = "Lookup3";
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.StopButton.Location = new System.Drawing.Point(195, 39);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(86, 38);
            this.StopButton.TabIndex = 14;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // ControlGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 274);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Lookup3Output);
            this.Controls.Add(this.Lookup3Input);
            this.Controls.Add(this.Lookup2Output);
            this.Controls.Add(this.Lookup2Input);
            this.Controls.Add(this.Lookup1Output);
            this.Controls.Add(this.Lookup1Input);
            this.Controls.Add(this.LookupButton);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "ControlGUI";
            this.Text = "ControlGUI";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importAssemblyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importBinaryToolStripMenuItem;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button LookupButton;
        private System.Windows.Forms.TextBox Lookup1Input;
        private System.Windows.Forms.TextBox Lookup1Output;
        private System.Windows.Forms.TextBox Lookup2Input;
        private System.Windows.Forms.TextBox Lookup2Output;
        private System.Windows.Forms.TextBox Lookup3Input;
        private System.Windows.Forms.TextBox Lookup3Output;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button StopButton;
    }
}