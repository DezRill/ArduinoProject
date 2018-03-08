namespace UiDesignDemo
{
    partial class WebCamera
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
            this.btnStart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.lbCams = new System.Windows.Forms.ListBox();
            this.pbWebCamPreview = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbWebCamPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(838, 193);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(253, 36);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(838, 235);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(253, 39);
            this.button2.TabIndex = 2;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // lbCams
            // 
            this.lbCams.FormattingEnabled = true;
            this.lbCams.Location = new System.Drawing.Point(838, 27);
            this.lbCams.Name = "lbCams";
            this.lbCams.Size = new System.Drawing.Size(253, 160);
            this.lbCams.TabIndex = 3;
            // 
            // pbWebCamPreview
            // 
            this.pbWebCamPreview.Location = new System.Drawing.Point(12, 27);
            this.pbWebCamPreview.Name = "pbWebCamPreview";
            this.pbWebCamPreview.Size = new System.Drawing.Size(820, 498);
            this.pbWebCamPreview.TabIndex = 0;
            this.pbWebCamPreview.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(858, 304);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(206, 75);
            this.button1.TabIndex = 4;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(893, 453);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 20);
            this.textBox1.TabIndex = 5;
            this.textBox1.Text = "12345";
            // 
            // WebCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1103, 563);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lbCams);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.pbWebCamPreview);
            this.Name = "WebCamera";
            this.Text = "WebCamera";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebCamera_FormClosing);
            this.Load += new System.EventHandler(this.WebCamera_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbWebCamPreview)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbWebCamPreview;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox lbCams;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}