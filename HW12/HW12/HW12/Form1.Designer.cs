namespace HW12
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
            this.input_textbox = new System.Windows.Forms.TextBox();
            this.download_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.result_textBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.current_status = new System.Windows.Forms.TextBox();
            this.demo_button = new System.Windows.Forms.Button();
            this.time_textBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // input_textbox
            // 
            this.input_textbox.Location = new System.Drawing.Point(70, 19);
            this.input_textbox.Name = "input_textbox";
            this.input_textbox.Size = new System.Drawing.Size(430, 22);
            this.input_textbox.TabIndex = 0;
            this.input_textbox.Text = "http://";
            // 
            // download_button
            // 
            this.download_button.Location = new System.Drawing.Point(506, 17);
            this.download_button.Name = "download_button";
            this.download_button.Size = new System.Drawing.Size(111, 30);
            this.download_button.TabIndex = 1;
            this.download_button.Text = "Download!";
            this.download_button.UseVisualStyleBackColor = true;
            this.download_button.Click += new System.EventHandler(this.download_button_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "URL:";
            // 
            // result_textBox
            // 
            this.result_textBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.result_textBox.Location = new System.Drawing.Point(11, 178);
            this.result_textBox.Multiline = true;
            this.result_textBox.Name = "result_textBox";
            this.result_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.result_textBox.Size = new System.Drawing.Size(1078, 500);
            this.result_textBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 155);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(52, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Result:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Status:";
            // 
            // current_status
            // 
            this.current_status.Location = new System.Drawing.Point(70, 49);
            this.current_status.Multiline = true;
            this.current_status.Name = "current_status";
            this.current_status.Size = new System.Drawing.Size(186, 98);
            this.current_status.TabIndex = 6;
            // 
            // demo_button
            // 
            this.demo_button.Location = new System.Drawing.Point(671, 17);
            this.demo_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.demo_button.Name = "demo_button";
            this.demo_button.Size = new System.Drawing.Size(419, 50);
            this.demo_button.TabIndex = 9;
            this.demo_button.Text = "Generate 8 Lists of 1 million ints each. Then time how long it takes to sort each" +
    "";
            this.demo_button.UseVisualStyleBackColor = true;
            this.demo_button.Click += new System.EventHandler(this.demo_button_Click);
            // 
            // time_textBox
            // 
            this.time_textBox.Location = new System.Drawing.Point(671, 72);
            this.time_textBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.time_textBox.Multiline = true;
            this.time_textBox.Name = "time_textBox";
            this.time_textBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.time_textBox.Size = new System.Drawing.Size(419, 75);
            this.time_textBox.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1098, 688);
            this.Controls.Add(this.time_textBox);
            this.Controls.Add(this.demo_button);
            this.Controls.Add(this.current_status);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.result_textBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.download_button);
            this.Controls.Add(this.input_textbox);
            this.Name = "Form1";
            this.Text = "HW12 Eric Chen 11381898";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox input_textbox;
        private System.Windows.Forms.Button download_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox result_textBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox current_status;
        private System.Windows.Forms.Button demo_button;
        private System.Windows.Forms.TextBox time_textBox;
    }
}

