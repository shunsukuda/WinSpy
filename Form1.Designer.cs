namespace WinSpy
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
            this.components = new System.ComponentModel.Container();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.PosLabel = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.HandlerLabel = new System.Windows.Forms.Label();
            this.ClassNameLabel = new System.Windows.Forms.Label();
            this.WindowTextLabel = new System.Windows.Forms.Label();
            this.ParentsLabel = new System.Windows.Forms.Label();
            this.ChildrenLabel = new System.Windows.Forms.Label();
            this.WindowRectLabel = new System.Windows.Forms.Label();
            this.ClientRectLabel = new System.Windows.Forms.Label();
            this.ModuleFileNameLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(492, 12);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(296, 286);
            this.textBox1.TabIndex = 0;
            // 
            // PosLabel
            // 
            this.PosLabel.AutoSize = true;
            this.PosLabel.Location = new System.Drawing.Point(12, 9);
            this.PosLabel.Name = "PosLabel";
            this.PosLabel.Size = new System.Drawing.Size(26, 15);
            this.PosLabel.TabIndex = 1;
            this.PosLabel.Text = "Pos";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // HandlerLabel
            // 
            this.HandlerLabel.AutoSize = true;
            this.HandlerLabel.Location = new System.Drawing.Point(12, 36);
            this.HandlerLabel.Name = "HandlerLabel";
            this.HandlerLabel.Size = new System.Drawing.Size(49, 15);
            this.HandlerLabel.TabIndex = 2;
            this.HandlerLabel.Text = "Handler";
            // 
            // ClassNameLabel
            // 
            this.ClassNameLabel.AutoSize = true;
            this.ClassNameLabel.Location = new System.Drawing.Point(12, 67);
            this.ClassNameLabel.Name = "ClassNameLabel";
            this.ClassNameLabel.Size = new System.Drawing.Size(64, 15);
            this.ClassNameLabel.TabIndex = 3;
            this.ClassNameLabel.Text = "ClassName";
            // 
            // WindowTextLabel
            // 
            this.WindowTextLabel.AutoSize = true;
            this.WindowTextLabel.Location = new System.Drawing.Point(12, 91);
            this.WindowTextLabel.Name = "WindowTextLabel";
            this.WindowTextLabel.Size = new System.Drawing.Size(72, 15);
            this.WindowTextLabel.TabIndex = 4;
            this.WindowTextLabel.Text = "WindowText";
            // 
            // ParentsLabel
            // 
            this.ParentsLabel.AutoSize = true;
            this.ParentsLabel.Location = new System.Drawing.Point(12, 121);
            this.ParentsLabel.Name = "ParentsLabel";
            this.ParentsLabel.Size = new System.Drawing.Size(46, 15);
            this.ParentsLabel.TabIndex = 5;
            this.ParentsLabel.Text = "Parents";
            // 
            // ChildrenLabel
            // 
            this.ChildrenLabel.AutoSize = true;
            this.ChildrenLabel.Location = new System.Drawing.Point(12, 150);
            this.ChildrenLabel.Name = "ChildrenLabel";
            this.ChildrenLabel.Size = new System.Drawing.Size(51, 15);
            this.ChildrenLabel.TabIndex = 6;
            this.ChildrenLabel.Text = "Children";
            // 
            // WindowRectLabel
            // 
            this.WindowRectLabel.AutoSize = true;
            this.WindowRectLabel.Location = new System.Drawing.Point(12, 177);
            this.WindowRectLabel.Name = "WindowRectLabel";
            this.WindowRectLabel.Size = new System.Drawing.Size(74, 15);
            this.WindowRectLabel.TabIndex = 7;
            this.WindowRectLabel.Text = "WindowRect";
            // 
            // ClientRectLabel
            // 
            this.ClientRectLabel.AutoSize = true;
            this.ClientRectLabel.Location = new System.Drawing.Point(12, 207);
            this.ClientRectLabel.Name = "ClientRectLabel";
            this.ClientRectLabel.Size = new System.Drawing.Size(60, 15);
            this.ClientRectLabel.TabIndex = 8;
            this.ClientRectLabel.Text = "ClientRect";
            // 
            // ModuleFileNameLabel
            // 
            this.ModuleFileNameLabel.AutoSize = true;
            this.ModuleFileNameLabel.Location = new System.Drawing.Point(12, 237);
            this.ModuleFileNameLabel.Name = "ModuleFileNameLabel";
            this.ModuleFileNameLabel.Size = new System.Drawing.Size(97, 15);
            this.ModuleFileNameLabel.TabIndex = 9;
            this.ModuleFileNameLabel.Text = "ModuleFileName";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.ModuleFileNameLabel);
            this.Controls.Add(this.ClientRectLabel);
            this.Controls.Add(this.WindowRectLabel);
            this.Controls.Add(this.ChildrenLabel);
            this.Controls.Add(this.ParentsLabel);
            this.Controls.Add(this.WindowTextLabel);
            this.Controls.Add(this.ClassNameLabel);
            this.Controls.Add(this.HandlerLabel);
            this.Controls.Add(this.PosLabel);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TextBox textBox1;
        private Label PosLabel;
        private System.Windows.Forms.Timer timer1;
        private Label HandlerLabel;
        private Label ClassNameLabel;
        private Label WindowTextLabel;
        private Label ParentsLabel;
        private Label ChildrenLabel;
        private Label WindowRectLabel;
        private Label ClientRectLabel;
        private Label ModuleFileNameLabel;
    }
}