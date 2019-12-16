namespace BroadCast
{
    partial class Frm_Notification
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
            this.rtx_Text = new System.Windows.Forms.RichTextBox();
            this.materialDivider1 = new MaterialSkin.Controls.MaterialDivider();
            this.btn_Send = new MaterialSkin.Controls.MaterialRaisedButton();
            this.lbl_BasicInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // rtx_Text
            // 
            this.rtx_Text.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtx_Text.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.rtx_Text.Location = new System.Drawing.Point(12, 113);
            this.rtx_Text.Name = "rtx_Text";
            this.rtx_Text.Size = new System.Drawing.Size(426, 170);
            this.rtx_Text.TabIndex = 0;
            this.rtx_Text.Text = "";
            // 
            // materialDivider1
            // 
            this.materialDivider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materialDivider1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.materialDivider1.Depth = 0;
            this.materialDivider1.Location = new System.Drawing.Point(0, 290);
            this.materialDivider1.MouseState = MaterialSkin.MouseState.HOVER;
            this.materialDivider1.Name = "materialDivider1";
            this.materialDivider1.Size = new System.Drawing.Size(450, 2);
            this.materialDivider1.TabIndex = 1;
            this.materialDivider1.Text = "materialDivider1";
            // 
            // btn_Send
            // 
            this.btn_Send.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Send.AutoSize = true;
            this.btn_Send.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btn_Send.Depth = 0;
            this.btn_Send.Icon = null;
            this.btn_Send.Location = new System.Drawing.Point(187, 302);
            this.btn_Send.MouseState = MaterialSkin.MouseState.HOVER;
            this.btn_Send.Name = "btn_Send";
            this.btn_Send.Primary = true;
            this.btn_Send.Size = new System.Drawing.Size(56, 36);
            this.btn_Send.TabIndex = 2;
            this.btn_Send.Text = "Send";
            this.btn_Send.UseVisualStyleBackColor = true;
            this.btn_Send.Click += new System.EventHandler(this.btn_Send_Click);
            // 
            // lbl_BasicInfo
            // 
            this.lbl_BasicInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_BasicInfo.AutoEllipsis = true;
            this.lbl_BasicInfo.AutoSize = true;
            this.lbl_BasicInfo.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_BasicInfo.Location = new System.Drawing.Point(12, 85);
            this.lbl_BasicInfo.Name = "lbl_BasicInfo";
            this.lbl_BasicInfo.Size = new System.Drawing.Size(87, 14);
            this.lbl_BasicInfo.TabIndex = 3;
            this.lbl_BasicInfo.Text = "Basic Info";
            // 
            // Frm_Notification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 350);
            this.Controls.Add(this.lbl_BasicInfo);
            this.Controls.Add(this.btn_Send);
            this.Controls.Add(this.materialDivider1);
            this.Controls.Add(this.rtx_Text);
            this.Name = "Frm_Notification";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "通知";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtx_Text;
        private MaterialSkin.Controls.MaterialDivider materialDivider1;
        private MaterialSkin.Controls.MaterialRaisedButton btn_Send;
        private System.Windows.Forms.Label lbl_BasicInfo;
    }
}