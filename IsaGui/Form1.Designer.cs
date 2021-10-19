
namespace IsaGui
{
    partial class Form1
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
            this.assemblyBox = new System.Windows.Forms.ListBox();
            this.BinaryTextBox = new System.Windows.Forms.RichTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openBinaryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openAssemblyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StepButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.FlagLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.sBox = new System.Windows.Forms.TextBox();
            this.oBox = new System.Windows.Forms.TextBox();
            this.eqBox = new System.Windows.Forms.TextBox();
            this.zBox = new System.Windows.Forms.TextBox();
            this.cBox = new System.Windows.Forms.TextBox();
            this.ALU1Box = new System.Windows.Forms.TextBox();
            this.ALU2Box = new System.Windows.Forms.TextBox();
            this.ALU3Box = new System.Windows.Forms.TextBox();
            this.PCBox = new System.Windows.Forms.TextBox();
            this.SP1Box = new System.Windows.Forms.TextBox();
            this.SP2Box = new System.Windows.Forms.TextBox();
            this.rABox = new System.Windows.Forms.TextBox();
            this.rBBox = new System.Windows.Forms.TextBox();
            this.rCBox = new System.Windows.Forms.TextBox();
            this.rEBox = new System.Windows.Forms.TextBox();
            this.rFBox = new System.Windows.Forms.TextBox();
            this.rGBox = new System.Windows.Forms.TextBox();
            this.rHBox = new System.Windows.Forms.TextBox();
            this.rIBox = new System.Windows.Forms.TextBox();
            this.rJBox = new System.Windows.Forms.TextBox();
            this.rKBox = new System.Windows.Forms.TextBox();
            this.PC2Box = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label24 = new System.Windows.Forms.Label();
            this.StopButton = new System.Windows.Forms.Button();
            this.rDBox = new System.Windows.Forms.TextBox();
            this.rDLabel = new System.Windows.Forms.Label();
            this.statsButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // assemblyBox
            // 
            this.assemblyBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.assemblyBox.FormattingEnabled = true;
            this.assemblyBox.HorizontalScrollbar = true;
            this.assemblyBox.ItemHeight = 20;
            this.assemblyBox.Location = new System.Drawing.Point(32, 283);
            this.assemblyBox.Name = "assemblyBox";
            this.assemblyBox.Size = new System.Drawing.Size(852, 304);
            this.assemblyBox.TabIndex = 0;
            // 
            // BinaryTextBox
            // 
            this.BinaryTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BinaryTextBox.Location = new System.Drawing.Point(32, 108);
            this.BinaryTextBox.Name = "BinaryTextBox";
            this.BinaryTextBox.ReadOnly = true;
            this.BinaryTextBox.Size = new System.Drawing.Size(852, 130);
            this.BinaryTextBox.TabIndex = 1;
            this.BinaryTextBox.Text = "";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(27, 80);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 25);
            this.label1.TabIndex = 2;
            this.label1.Text = "Binary";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 255);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "ISA Assembly";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1337, 29);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openBinaryToolStripMenuItem,
            this.openAssemblyToolStripMenuItem});
            this.fileToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(46, 25);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openBinaryToolStripMenuItem
            // 
            this.openBinaryToolStripMenuItem.Name = "openBinaryToolStripMenuItem";
            this.openBinaryToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.openBinaryToolStripMenuItem.Text = "Open Binary";
            this.openBinaryToolStripMenuItem.Click += new System.EventHandler(this.openBinaryToolStripMenuItem_Click);
            // 
            // openAssemblyToolStripMenuItem
            // 
            this.openAssemblyToolStripMenuItem.Name = "openAssemblyToolStripMenuItem";
            this.openAssemblyToolStripMenuItem.Size = new System.Drawing.Size(189, 26);
            this.openAssemblyToolStripMenuItem.Text = "Open Assembly";
            this.openAssemblyToolStripMenuItem.Click += new System.EventHandler(this.openAssemblyToolStripMenuItem_Click);
            // 
            // StepButton
            // 
            this.StepButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StepButton.Location = new System.Drawing.Point(35, 39);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(75, 38);
            this.StepButton.TabIndex = 5;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.RunButton.Location = new System.Drawing.Point(126, 39);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(75, 38);
            this.RunButton.TabIndex = 6;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // FlagLabel
            // 
            this.FlagLabel.AutoSize = true;
            this.FlagLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FlagLabel.Location = new System.Drawing.Point(918, 68);
            this.FlagLabel.Name = "FlagLabel";
            this.FlagLabel.Size = new System.Drawing.Size(48, 20);
            this.FlagLabel.TabIndex = 9;
            this.FlagLabel.Text = "Flags";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(972, 68);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 20);
            this.label4.TabIndex = 10;
            this.label4.Text = "S";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(1005, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 20);
            this.label5.TabIndex = 11;
            this.label5.Text = "O";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1036, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 20);
            this.label6.TabIndex = 12;
            this.label6.Text = "EQ";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(1079, 68);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(19, 20);
            this.label7.TabIndex = 13;
            this.label7.Text = "Z";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1115, 68);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "C";
            // 
            // sBox
            // 
            this.sBox.Location = new System.Drawing.Point(974, 98);
            this.sBox.Name = "sBox";
            this.sBox.Size = new System.Drawing.Size(20, 20);
            this.sBox.TabIndex = 15;
            // 
            // oBox
            // 
            this.oBox.Location = new System.Drawing.Point(1008, 98);
            this.oBox.Name = "oBox";
            this.oBox.Size = new System.Drawing.Size(20, 20);
            this.oBox.TabIndex = 16;
            // 
            // eqBox
            // 
            this.eqBox.Location = new System.Drawing.Point(1043, 98);
            this.eqBox.Name = "eqBox";
            this.eqBox.Size = new System.Drawing.Size(20, 20);
            this.eqBox.TabIndex = 17;
            // 
            // zBox
            // 
            this.zBox.Location = new System.Drawing.Point(1078, 98);
            this.zBox.Name = "zBox";
            this.zBox.Size = new System.Drawing.Size(20, 20);
            this.zBox.TabIndex = 18;
            // 
            // cBox
            // 
            this.cBox.Location = new System.Drawing.Point(1114, 98);
            this.cBox.Name = "cBox";
            this.cBox.Size = new System.Drawing.Size(20, 20);
            this.cBox.TabIndex = 19;
            // 
            // ALU1Box
            // 
            this.ALU1Box.Location = new System.Drawing.Point(922, 181);
            this.ALU1Box.Name = "ALU1Box";
            this.ALU1Box.Size = new System.Drawing.Size(125, 20);
            this.ALU1Box.TabIndex = 20;
            // 
            // ALU2Box
            // 
            this.ALU2Box.Location = new System.Drawing.Point(1058, 180);
            this.ALU2Box.Name = "ALU2Box";
            this.ALU2Box.Size = new System.Drawing.Size(125, 20);
            this.ALU2Box.TabIndex = 21;
            // 
            // ALU3Box
            // 
            this.ALU3Box.Location = new System.Drawing.Point(1191, 181);
            this.ALU3Box.Name = "ALU3Box";
            this.ALU3Box.Size = new System.Drawing.Size(125, 20);
            this.ALU3Box.TabIndex = 22;
            // 
            // PCBox
            // 
            this.PCBox.Location = new System.Drawing.Point(922, 238);
            this.PCBox.Name = "PCBox";
            this.PCBox.Size = new System.Drawing.Size(125, 20);
            this.PCBox.TabIndex = 23;
            // 
            // SP1Box
            // 
            this.SP1Box.Location = new System.Drawing.Point(922, 291);
            this.SP1Box.Name = "SP1Box";
            this.SP1Box.Size = new System.Drawing.Size(125, 20);
            this.SP1Box.TabIndex = 24;
            // 
            // SP2Box
            // 
            this.SP2Box.Location = new System.Drawing.Point(1052, 291);
            this.SP2Box.Name = "SP2Box";
            this.SP2Box.Size = new System.Drawing.Size(125, 20);
            this.SP2Box.TabIndex = 25;
            // 
            // rABox
            // 
            this.rABox.Location = new System.Drawing.Point(923, 346);
            this.rABox.Name = "rABox";
            this.rABox.Size = new System.Drawing.Size(125, 20);
            this.rABox.TabIndex = 26;
            // 
            // rBBox
            // 
            this.rBBox.Location = new System.Drawing.Point(1059, 346);
            this.rBBox.Name = "rBBox";
            this.rBBox.Size = new System.Drawing.Size(125, 20);
            this.rBBox.TabIndex = 27;
            // 
            // rCBox
            // 
            this.rCBox.Location = new System.Drawing.Point(1192, 346);
            this.rCBox.Name = "rCBox";
            this.rCBox.Size = new System.Drawing.Size(125, 20);
            this.rCBox.TabIndex = 28;
            // 
            // rEBox
            // 
            this.rEBox.Location = new System.Drawing.Point(1059, 396);
            this.rEBox.Name = "rEBox";
            this.rEBox.Size = new System.Drawing.Size(125, 20);
            this.rEBox.TabIndex = 29;
            // 
            // rFBox
            // 
            this.rFBox.Location = new System.Drawing.Point(1192, 396);
            this.rFBox.Name = "rFBox";
            this.rFBox.Size = new System.Drawing.Size(125, 20);
            this.rFBox.TabIndex = 30;
            // 
            // rGBox
            // 
            this.rGBox.Location = new System.Drawing.Point(923, 443);
            this.rGBox.Name = "rGBox";
            this.rGBox.Size = new System.Drawing.Size(125, 20);
            this.rGBox.TabIndex = 31;
            // 
            // rHBox
            // 
            this.rHBox.Location = new System.Drawing.Point(1059, 443);
            this.rHBox.Name = "rHBox";
            this.rHBox.Size = new System.Drawing.Size(125, 20);
            this.rHBox.TabIndex = 32;
            // 
            // rIBox
            // 
            this.rIBox.Location = new System.Drawing.Point(1192, 443);
            this.rIBox.Name = "rIBox";
            this.rIBox.Size = new System.Drawing.Size(125, 20);
            this.rIBox.TabIndex = 33;
            // 
            // rJBox
            // 
            this.rJBox.Location = new System.Drawing.Point(923, 490);
            this.rJBox.Name = "rJBox";
            this.rJBox.Size = new System.Drawing.Size(125, 20);
            this.rJBox.TabIndex = 34;
            // 
            // rKBox
            // 
            this.rKBox.Location = new System.Drawing.Point(1059, 490);
            this.rKBox.Name = "rKBox";
            this.rKBox.Size = new System.Drawing.Size(125, 20);
            this.rKBox.TabIndex = 35;
            // 
            // PC2Box
            // 
            this.PC2Box.Location = new System.Drawing.Point(1058, 238);
            this.PC2Box.Name = "PC2Box";
            this.PC2Box.Size = new System.Drawing.Size(125, 20);
            this.PC2Box.TabIndex = 36;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(922, 154);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 20);
            this.label9.TabIndex = 41;
            this.label9.Text = "ALU Dest";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(1055, 154);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 20);
            this.label10.TabIndex = 42;
            this.label10.Text = "ALU OP1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(1191, 154);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 20);
            this.label11.TabIndex = 43;
            this.label11.Text = "ALU OP2";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(922, 215);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(39, 20);
            this.label12.TabIndex = 44;
            this.label12.Text = "PC1";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(922, 269);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(43, 20);
            this.label13.TabIndex = 45;
            this.label13.Text = "SP 1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1052, 269);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(43, 20);
            this.label14.TabIndex = 46;
            this.label14.Text = "SP 2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(923, 323);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 20);
            this.label3.TabIndex = 47;
            this.label3.Text = "rA";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1056, 323);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(25, 20);
            this.label15.TabIndex = 48;
            this.label15.Text = "rB";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(1192, 323);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(25, 20);
            this.label16.TabIndex = 49;
            this.label16.Text = "rC";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(1059, 372);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(25, 20);
            this.label17.TabIndex = 50;
            this.label17.Text = "rE";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(1192, 374);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(24, 20);
            this.label18.TabIndex = 51;
            this.label18.Text = "rF";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(923, 421);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(27, 20);
            this.label19.TabIndex = 52;
            this.label19.Text = "rG";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.Location = new System.Drawing.Point(1059, 418);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(26, 20);
            this.label20.TabIndex = 53;
            this.label20.Text = "rH";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(1192, 419);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(19, 20);
            this.label21.TabIndex = 54;
            this.label21.Text = "rI";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(923, 467);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(22, 20);
            this.label22.TabIndex = 55;
            this.label22.Text = "rJ";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(1059, 468);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(24, 20);
            this.label23.TabIndex = 56;
            this.label23.Text = "rK";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(1058, 215);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(39, 20);
            this.label24.TabIndex = 57;
            this.label24.Text = "PC2";
            // 
            // StopButton
            // 
            this.StopButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.StopButton.Location = new System.Drawing.Point(217, 39);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(75, 38);
            this.StopButton.TabIndex = 58;
            this.StopButton.Text = "Stop";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // rDBox
            // 
            this.rDBox.Location = new System.Drawing.Point(923, 396);
            this.rDBox.Name = "rDBox";
            this.rDBox.Size = new System.Drawing.Size(125, 20);
            this.rDBox.TabIndex = 60;
            // 
            // rDLabel
            // 
            this.rDLabel.AutoSize = true;
            this.rDLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rDLabel.Location = new System.Drawing.Point(923, 372);
            this.rDLabel.Name = "rDLabel";
            this.rDLabel.Size = new System.Drawing.Size(26, 20);
            this.rDLabel.TabIndex = 61;
            this.rDLabel.Text = "rD";
            // 
            // statsButton
            // 
            this.statsButton.Enabled = false;
            this.statsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.statsButton.Location = new System.Drawing.Point(308, 39);
            this.statsButton.Name = "statsButton";
            this.statsButton.Size = new System.Drawing.Size(75, 38);
            this.statsButton.TabIndex = 62;
            this.statsButton.Text = "Stats";
            this.statsButton.UseVisualStyleBackColor = true;
            this.statsButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1337, 608);
            this.Controls.Add(this.statsButton);
            this.Controls.Add(this.rDLabel);
            this.Controls.Add(this.rDBox);
            this.Controls.Add(this.StopButton);
            this.Controls.Add(this.label24);
            this.Controls.Add(this.label23);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.PC2Box);
            this.Controls.Add(this.rKBox);
            this.Controls.Add(this.rJBox);
            this.Controls.Add(this.rIBox);
            this.Controls.Add(this.rHBox);
            this.Controls.Add(this.rGBox);
            this.Controls.Add(this.rFBox);
            this.Controls.Add(this.rEBox);
            this.Controls.Add(this.rCBox);
            this.Controls.Add(this.rBBox);
            this.Controls.Add(this.rABox);
            this.Controls.Add(this.SP2Box);
            this.Controls.Add(this.SP1Box);
            this.Controls.Add(this.PCBox);
            this.Controls.Add(this.ALU3Box);
            this.Controls.Add(this.ALU2Box);
            this.Controls.Add(this.ALU1Box);
            this.Controls.Add(this.cBox);
            this.Controls.Add(this.zBox);
            this.Controls.Add(this.eqBox);
            this.Controls.Add(this.oBox);
            this.Controls.Add(this.sBox);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.FlagLabel);
            this.Controls.Add(this.RunButton);
            this.Controls.Add(this.StepButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BinaryTextBox);
            this.Controls.Add(this.assemblyBox);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "ISA Debugger";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox BinaryTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openBinaryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openAssemblyToolStripMenuItem;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Label FlagLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox sBox;
        private System.Windows.Forms.TextBox oBox;
        private System.Windows.Forms.TextBox eqBox;
        private System.Windows.Forms.TextBox zBox;
        private System.Windows.Forms.TextBox cBox;
        private System.Windows.Forms.TextBox ALU1Box;
        private System.Windows.Forms.TextBox ALU2Box;
        private System.Windows.Forms.TextBox ALU3Box;
        private System.Windows.Forms.TextBox PCBox;
        private System.Windows.Forms.TextBox SP1Box;
        private System.Windows.Forms.TextBox SP2Box;
        private System.Windows.Forms.TextBox rABox;
        private System.Windows.Forms.TextBox rBBox;
        private System.Windows.Forms.TextBox rCBox;
        private System.Windows.Forms.TextBox rEBox;
        private System.Windows.Forms.TextBox rFBox;
        private System.Windows.Forms.TextBox rGBox;
        private System.Windows.Forms.TextBox rHBox;
        private System.Windows.Forms.TextBox rIBox;
        private System.Windows.Forms.TextBox rJBox;
        private System.Windows.Forms.TextBox rKBox;
        private System.Windows.Forms.TextBox PC2Box;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.TextBox rDBox;
        private System.Windows.Forms.Label rDLabel;
        public System.Windows.Forms.ListBox assemblyBox;
        private System.Windows.Forms.Button statsButton;
    }
}

