namespace UiDesignDemo
{
    partial class Schedule
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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.webControl3 = new Awesomium.Windows.Forms.WebControl(this.components);
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1062, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "l";
            // 
            // webControl3
            // 
            this.webControl3.Location = new System.Drawing.Point(129, 102);
            this.webControl3.Margin = new System.Windows.Forms.Padding(2);
            this.webControl3.Size = new System.Drawing.Size(812, 415);
            this.webControl3.TabIndex = 34;
            
            // 
            // Schedule
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1062, 574);
            this.Controls.Add(this.webControl3);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Schedule";
            this.Text = "Schedule";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private Awesomium.Windows.Forms.WebControl webControl3;
    }
}