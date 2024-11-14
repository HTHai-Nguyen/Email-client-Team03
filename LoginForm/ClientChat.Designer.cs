namespace LoginForm
{
    partial class ClientChat
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
            this.chatText = new System.Windows.Forms.TextBox();
            this.chatView = new System.Windows.Forms.TextBox();
            this.send_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chatText
            // 
            this.chatText.Location = new System.Drawing.Point(45, 44);
            this.chatText.Multiline = true;
            this.chatText.Name = "chatText";
            this.chatText.Size = new System.Drawing.Size(217, 121);
            this.chatText.TabIndex = 0;
            this.chatText.TextChanged += new System.EventHandler(this.chatText_TextChanged);
            // 
            // chatView
            // 
            this.chatView.Location = new System.Drawing.Point(313, 44);
            this.chatView.Multiline = true;
            this.chatView.Name = "chatView";
            this.chatView.ReadOnly = true;
            this.chatView.Size = new System.Drawing.Size(475, 394);
            this.chatView.TabIndex = 1;
            this.chatView.TextChanged += new System.EventHandler(this.chatView_TextChanged);
            // 
            // send_btn
            // 
            this.send_btn.Location = new System.Drawing.Point(151, 226);
            this.send_btn.Name = "send_btn";
            this.send_btn.Size = new System.Drawing.Size(111, 42);
            this.send_btn.TabIndex = 3;
            this.send_btn.Text = "Send";
            this.send_btn.UseVisualStyleBackColor = true;
            this.send_btn.Click += new System.EventHandler(this.connect_btn_Click);
            // 
            // ClientChat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.send_btn);
            this.Controls.Add(this.chatView);
            this.Controls.Add(this.chatText);
            this.Name = "ClientChat";
            this.Text = "ClientChat";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatText;
        private System.Windows.Forms.TextBox chatView;
        private System.Windows.Forms.Button send_btn;
    }
}