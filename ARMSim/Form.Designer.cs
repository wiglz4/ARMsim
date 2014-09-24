namespace ARMSim
{
    partial class ARMSimForm
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

        #region Windows ARMSimForm Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.LoadFileButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.StopButton = new System.Windows.Forms.Button();
            this.StepButton = new System.Windows.Forms.Button();
            this.RunButton = new System.Windows.Forms.Button();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.myConsole = new System.Windows.Forms.RichTextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.Disassembly = new System.Windows.Forms.TabPage();
            this.DisBox = new System.Windows.Forms.TextBox();
            this.Trace = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Memory = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.RegisterGridView = new System.Windows.Forms.DataGridView();
            this.Registers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Values = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Stack = new System.Windows.Forms.TabPage();
            this.StackGridView = new System.Windows.Forms.DataGridView();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Value2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Flags = new System.Windows.Forms.TabPage();
            this.FlagGridView = new System.Windows.Forms.DataGridView();
            this.Flag = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Values2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.MemGridView = new System.Windows.Forms.DataGridView();
            this.Address = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Word4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.GoButton = new System.Windows.Forms.Button();
            this.MemAddr = new System.Windows.Forms.TextBox();
            this.TraceBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.Disassembly.SuspendLayout();
            this.Trace.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.Memory.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RegisterGridView)).BeginInit();
            this.Stack.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.StackGridView)).BeginInit();
            this.Flags.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FlagGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MemGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.checkBox1);
            this.splitContainer1.Panel1.Controls.Add(this.LoadFileButton);
            this.splitContainer1.Panel1.Controls.Add(this.ResetButton);
            this.splitContainer1.Panel1.Controls.Add(this.StopButton);
            this.splitContainer1.Panel1.Controls.Add(this.StepButton);
            this.splitContainer1.Panel1.Controls.Add(this.RunButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(1009, 561);
            this.splitContainer1.SplitterDistance = 158;
            this.splitContainer1.TabIndex = 0;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Checked = true;
            this.checkBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox1.Location = new System.Drawing.Point(15, 52);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(75, 17);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Trace Log";
            this.checkBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // LoadFileButton
            // 
            this.LoadFileButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LoadFileButton.Location = new System.Drawing.Point(14, 12);
            this.LoadFileButton.Name = "LoadFileButton";
            this.LoadFileButton.Size = new System.Drawing.Size(133, 34);
            this.LoadFileButton.TabIndex = 4;
            this.LoadFileButton.Text = "LoadFile";
            this.LoadFileButton.UseVisualStyleBackColor = true;
            this.LoadFileButton.Click += new System.EventHandler(this.LoadFileButton_Click);
            // 
            // ResetButton
            // 
            this.ResetButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ResetButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ResetButton.Location = new System.Drawing.Point(14, 520);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(133, 23);
            this.ResetButton.TabIndex = 3;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // StopButton
            // 
            this.StopButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StopButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StopButton.Location = new System.Drawing.Point(14, 490);
            this.StopButton.Name = "StopButton";
            this.StopButton.Size = new System.Drawing.Size(133, 23);
            this.StopButton.TabIndex = 2;
            this.StopButton.Text = "Stop | Break";
            this.StopButton.UseVisualStyleBackColor = true;
            this.StopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // StepButton
            // 
            this.StepButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.StepButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.StepButton.Location = new System.Drawing.Point(14, 460);
            this.StepButton.Name = "StepButton";
            this.StepButton.Size = new System.Drawing.Size(133, 23);
            this.StepButton.TabIndex = 1;
            this.StepButton.Text = "Step";
            this.StepButton.UseVisualStyleBackColor = true;
            this.StepButton.Click += new System.EventHandler(this.StepButton_Click);
            // 
            // RunButton
            // 
            this.RunButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RunButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.RunButton.Location = new System.Drawing.Point(14, 420);
            this.RunButton.Name = "RunButton";
            this.RunButton.Size = new System.Drawing.Size(133, 34);
            this.RunButton.TabIndex = 0;
            this.RunButton.Text = "Run";
            this.RunButton.UseVisualStyleBackColor = true;
            this.RunButton.Click += new System.EventHandler(this.RunButton_Click);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl1);
            this.splitContainer2.Size = new System.Drawing.Size(847, 561);
            this.splitContainer2.SplitterDistance = 268;
            this.splitContainer2.TabIndex = 0;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.BackColor = System.Drawing.SystemColors.Control;
            this.splitContainer3.Panel1.Controls.Add(this.myConsole);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer3.Size = new System.Drawing.Size(847, 268);
            this.splitContainer3.SplitterDistance = 413;
            this.splitContainer3.TabIndex = 0;
            // 
            // myConsole
            // 
            this.myConsole.BackColor = System.Drawing.SystemColors.MenuText;
            this.myConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.myConsole.Dock = System.Windows.Forms.DockStyle.Fill;
            this.myConsole.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.myConsole.ForeColor = System.Drawing.Color.Lime;
            this.myConsole.Location = new System.Drawing.Point(0, 0);
            this.myConsole.Name = "myConsole";
            this.myConsole.Size = new System.Drawing.Size(413, 268);
            this.myConsole.TabIndex = 0;
            this.myConsole.Text = "";
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.Disassembly);
            this.tabControl2.Controls.Add(this.Trace);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(430, 268);
            this.tabControl2.TabIndex = 0;
            // 
            // Disassembly
            // 
            this.Disassembly.Controls.Add(this.DisBox);
            this.Disassembly.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Disassembly.Location = new System.Drawing.Point(4, 22);
            this.Disassembly.Name = "Disassembly";
            this.Disassembly.Padding = new System.Windows.Forms.Padding(3);
            this.Disassembly.Size = new System.Drawing.Size(422, 242);
            this.Disassembly.TabIndex = 0;
            this.Disassembly.Text = "Disassembly";
            this.Disassembly.UseVisualStyleBackColor = true;
            // 
            // DisBox
            // 
            this.DisBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DisBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DisBox.Location = new System.Drawing.Point(3, 3);
            this.DisBox.Multiline = true;
            this.DisBox.Name = "DisBox";
            this.DisBox.Size = new System.Drawing.Size(416, 236);
            this.DisBox.TabIndex = 0;
            // 
            // Trace
            // 
            this.Trace.Controls.Add(this.TraceBox);
            this.Trace.Location = new System.Drawing.Point(4, 22);
            this.Trace.Name = "Trace";
            this.Trace.Padding = new System.Windows.Forms.Padding(3);
            this.Trace.Size = new System.Drawing.Size(422, 242);
            this.Trace.TabIndex = 1;
            this.Trace.Text = "Trace";
            this.Trace.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.Memory);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.Stack);
            this.tabControl1.Controls.Add(this.Flags);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(847, 289);
            this.tabControl1.TabIndex = 0;
            // 
            // Memory
            // 
            this.Memory.Controls.Add(this.splitContainer4);
            this.Memory.Location = new System.Drawing.Point(4, 22);
            this.Memory.Name = "Memory";
            this.Memory.Padding = new System.Windows.Forms.Padding(3);
            this.Memory.Size = new System.Drawing.Size(839, 263);
            this.Memory.TabIndex = 0;
            this.Memory.Text = "Memory";
            this.Memory.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.RegisterGridView);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(839, 263);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Registers";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // RegisterGridView
            // 
            this.RegisterGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.RegisterGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Registers,
            this.Values});
            this.RegisterGridView.Location = new System.Drawing.Point(4, 5);
            this.RegisterGridView.Name = "RegisterGridView";
            this.RegisterGridView.Size = new System.Drawing.Size(268, 254);
            this.RegisterGridView.TabIndex = 1;
            // 
            // Registers
            // 
            this.Registers.HeaderText = "Register";
            this.Registers.Name = "Registers";
            // 
            // Values
            // 
            this.Values.HeaderText = "Value";
            this.Values.Name = "Values";
            // 
            // Stack
            // 
            this.Stack.Controls.Add(this.StackGridView);
            this.Stack.Location = new System.Drawing.Point(4, 22);
            this.Stack.Name = "Stack";
            this.Stack.Padding = new System.Windows.Forms.Padding(3);
            this.Stack.Size = new System.Drawing.Size(839, 263);
            this.Stack.TabIndex = 2;
            this.Stack.Text = "Stack";
            this.Stack.UseVisualStyleBackColor = true;
            // 
            // StackGridView
            // 
            this.StackGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.StackGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Position,
            this.Value2});
            this.StackGridView.Location = new System.Drawing.Point(4, 5);
            this.StackGridView.Name = "StackGridView";
            this.StackGridView.Size = new System.Drawing.Size(268, 254);
            this.StackGridView.TabIndex = 0;
            // 
            // Position
            // 
            this.Position.HeaderText = "Slot";
            this.Position.Name = "Position";
            // 
            // Value2
            // 
            this.Value2.HeaderText = "Value";
            this.Value2.Name = "Value2";
            // 
            // Flags
            // 
            this.Flags.Controls.Add(this.FlagGridView);
            this.Flags.Location = new System.Drawing.Point(4, 22);
            this.Flags.Name = "Flags";
            this.Flags.Padding = new System.Windows.Forms.Padding(3);
            this.Flags.Size = new System.Drawing.Size(839, 263);
            this.Flags.TabIndex = 3;
            this.Flags.Text = "Flags";
            this.Flags.UseVisualStyleBackColor = true;
            // 
            // FlagGridView
            // 
            this.FlagGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.FlagGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Flag,
            this.Values2});
            this.FlagGridView.Location = new System.Drawing.Point(4, 5);
            this.FlagGridView.Name = "FlagGridView";
            this.FlagGridView.Size = new System.Drawing.Size(268, 254);
            this.FlagGridView.TabIndex = 0;
            // 
            // Flag
            // 
            this.Flag.HeaderText = "Flags";
            this.Flag.Name = "Flag";
            // 
            // Values2
            // 
            this.Values2.HeaderText = "Value";
            this.Values2.Name = "Values2";
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(3, 3);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label1);
            this.splitContainer4.Panel1.Controls.Add(this.GoButton);
            this.splitContainer4.Panel1.Controls.Add(this.MemAddr);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.MemGridView);
            this.splitContainer4.Size = new System.Drawing.Size(833, 257);
            this.splitContainer4.SplitterDistance = 28;
            this.splitContainer4.TabIndex = 4;
            // 
            // MemGridView
            // 
            this.MemGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.MemGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Address,
            this.Word1,
            this.Word2,
            this.Word3,
            this.Word4});
            this.MemGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MemGridView.Location = new System.Drawing.Point(0, 0);
            this.MemGridView.Name = "MemGridView";
            this.MemGridView.Size = new System.Drawing.Size(833, 225);
            this.MemGridView.TabIndex = 3;
            // 
            // Address
            // 
            this.Address.HeaderText = "Address";
            this.Address.Name = "Address";
            // 
            // Word1
            // 
            this.Word1.HeaderText = "";
            this.Word1.Name = "Word1";
            // 
            // Word2
            // 
            this.Word2.HeaderText = "";
            this.Word2.Name = "Word2";
            // 
            // Word3
            // 
            this.Word3.HeaderText = "";
            this.Word3.Name = "Word3";
            // 
            // Word4
            // 
            this.Word4.HeaderText = "";
            this.Word4.Name = "Word4";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(4, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "Address:";
            // 
            // GoButton
            // 
            this.GoButton.Location = new System.Drawing.Point(748, 3);
            this.GoButton.Name = "GoButton";
            this.GoButton.Size = new System.Drawing.Size(75, 22);
            this.GoButton.TabIndex = 5;
            this.GoButton.Text = "Go";
            this.GoButton.UseVisualStyleBackColor = true;
            this.GoButton.Click += new System.EventHandler(this.GoButton_Click);
            // 
            // MemAddr
            // 
            this.MemAddr.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.MemAddr.ForeColor = System.Drawing.SystemColors.InactiveCaption;
            this.MemAddr.Location = new System.Drawing.Point(58, 4);
            this.MemAddr.Name = "MemAddr";
            this.MemAddr.Size = new System.Drawing.Size(686, 20);
            this.MemAddr.TabIndex = 4;
            this.MemAddr.Text = "0";
            // 
            // TraceBox
            // 
            this.TraceBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceBox.Location = new System.Drawing.Point(3, 3);
            this.TraceBox.Multiline = true;
            this.TraceBox.Name = "TraceBox";
            this.TraceBox.Size = new System.Drawing.Size(416, 236);
            this.TraceBox.TabIndex = 0;
            // 
            // ARMSimForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 561);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(900, 450);
            this.Name = "ARMSimForm";
            this.Text = "ARMSimForm";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.tabControl2.ResumeLayout(false);
            this.Disassembly.ResumeLayout(false);
            this.Disassembly.PerformLayout();
            this.Trace.ResumeLayout(false);
            this.Trace.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.Memory.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.RegisterGridView)).EndInit();
            this.Stack.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.StackGridView)).EndInit();
            this.Flags.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FlagGridView)).EndInit();
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MemGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.Button StepButton;
        private System.Windows.Forms.Button RunButton;
        private System.Windows.Forms.Button StopButton;
        private System.Windows.Forms.Button ResetButton;
        private System.Windows.Forms.Button LoadFileButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Memory;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage Stack;
        private System.Windows.Forms.RichTextBox myConsole;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage Disassembly;
        private System.Windows.Forms.TabPage Trace;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.TabPage Flags;
        private System.Windows.Forms.DataGridView RegisterGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Registers;
        private System.Windows.Forms.DataGridViewTextBoxColumn Values;
        private System.Windows.Forms.DataGridView FlagGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Flag;
        private System.Windows.Forms.DataGridViewTextBoxColumn Values2;
        private System.Windows.Forms.TextBox DisBox;
        private System.Windows.Forms.DataGridView StackGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn Value2;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button GoButton;
        private System.Windows.Forms.TextBox MemAddr;
        private System.Windows.Forms.DataGridView MemGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Address;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Word4;
        private System.Windows.Forms.TextBox TraceBox;
    }
}