namespace Assignment1
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWordlistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.validateCrozzleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crozzleArea = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.scoreContainer = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(284, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openWordlistToolStripMenuItem,
            this.openCrozzleToolStripMenuItem,
            this.validateCrozzleToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openWordlistToolStripMenuItem
            // 
            this.openWordlistToolStripMenuItem.Name = "openWordlistToolStripMenuItem";
            this.openWordlistToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.openWordlistToolStripMenuItem.Text = "Open Wordlist";
            this.openWordlistToolStripMenuItem.Click += new System.EventHandler(this.openWordlistToolStripMenuItem_Click);
            // 
            // openCrozzleToolStripMenuItem
            // 
            this.openCrozzleToolStripMenuItem.Name = "openCrozzleToolStripMenuItem";
            this.openCrozzleToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.openCrozzleToolStripMenuItem.Text = "Open Crozzle";
            this.openCrozzleToolStripMenuItem.Click += new System.EventHandler(this.openCrozzleToolStripMenuItem_Click);
            // 
            // validateCrozzleToolStripMenuItem
            // 
            this.validateCrozzleToolStripMenuItem.Name = "validateCrozzleToolStripMenuItem";
            this.validateCrozzleToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.validateCrozzleToolStripMenuItem.Text = "Validate Crozzle";
            this.validateCrozzleToolStripMenuItem.Click += new System.EventHandler(this.validateCrozzleToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(186, 24);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // crozzleArea
            // 
            this.crozzleArea.Location = new System.Drawing.Point(0, 27);
            this.crozzleArea.Multiline = true;
            this.crozzleArea.Name = "crozzleArea";
            this.crozzleArea.ReadOnly = true;
            this.crozzleArea.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.crozzleArea.Size = new System.Drawing.Size(284, 235);
            this.crozzleArea.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 267);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Score:";
            // 
            // scoreContainer
            // 
            this.scoreContainer.AutoSize = true;
            this.scoreContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.scoreContainer.Location = new System.Drawing.Point(45, 268);
            this.scoreContainer.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.scoreContainer.Name = "scoreContainer";
            this.scoreContainer.Size = new System.Drawing.Size(45, 17);
            this.scoreContainer.TabIndex = 3;
            this.scoreContainer.Text = "            ";
            this.scoreContainer.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 288);
            this.Controls.Add(this.scoreContainer);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.crozzleArea);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWordlistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem validateCrozzleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.TextBox crozzleArea;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label scoreContainer;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

