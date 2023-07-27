namespace Namava_Direct_Downloader
{
    partial class AdvancedDialog
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
            this.lblSaveFormat = new System.Windows.Forms.Label();
            this.lblFormatInfo = new System.Windows.Forms.Label();
            this.btnFormatReset = new System.Windows.Forms.Button();
            this.lblLine1 = new System.Windows.Forms.Label();
            this.cboxSaveFormat = new System.Windows.Forms.ComboBox();
            this.lblPreviewT = new System.Windows.Forms.Label();
            this.lblPreview = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSaveFormat
            // 
            this.lblSaveFormat.AutoSize = true;
            this.lblSaveFormat.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSaveFormat.Location = new System.Drawing.Point(9, 13);
            this.lblSaveFormat.Name = "lblSaveFormat";
            this.lblSaveFormat.Size = new System.Drawing.Size(73, 13);
            this.lblSaveFormat.TabIndex = 0;
            this.lblSaveFormat.Text = "Save Format: ";
            // 
            // lblFormatInfo
            // 
            this.lblFormatInfo.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblFormatInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblFormatInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblFormatInfo.Location = new System.Drawing.Point(12, 36);
            this.lblFormatInfo.Name = "lblFormatInfo";
            this.lblFormatInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblFormatInfo.Size = new System.Drawing.Size(410, 51);
            this.lblFormatInfo.TabIndex = 2;
            this.lblFormatInfo.Text = "$N replaces the video name, $Q replaces the video quality, $S the season number, " +
    "and $E the episode number. You can write this in any format.";
            this.lblFormatInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnFormatReset
            // 
            this.btnFormatReset.Location = new System.Drawing.Point(362, 8);
            this.btnFormatReset.Name = "btnFormatReset";
            this.btnFormatReset.Size = new System.Drawing.Size(60, 23);
            this.btnFormatReset.TabIndex = 3;
            this.btnFormatReset.Text = "reset";
            this.btnFormatReset.UseVisualStyleBackColor = true;
            this.btnFormatReset.Click += new System.EventHandler(this.btnFormatReset_Click);
            // 
            // lblLine1
            // 
            this.lblLine1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLine1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLine1.Location = new System.Drawing.Point(12, 93);
            this.lblLine1.Name = "lblLine1";
            this.lblLine1.Size = new System.Drawing.Size(411, 2);
            this.lblLine1.TabIndex = 5;
            // 
            // cboxSaveFormat
            // 
            this.cboxSaveFormat.FormattingEnabled = true;
            this.cboxSaveFormat.Items.AddRange(new object[] {
            "$N-S$SE$E-($Qp)",
            "$N-S$SE$E",
            "S$SE$E-$Qp",
            "$N-($Qp)"});
            this.cboxSaveFormat.Location = new System.Drawing.Point(108, 9);
            this.cboxSaveFormat.Name = "cboxSaveFormat";
            this.cboxSaveFormat.Size = new System.Drawing.Size(248, 21);
            this.cboxSaveFormat.TabIndex = 8;
            this.cboxSaveFormat.Text = "$N-S$SE$E-($Qp)";
            this.cboxSaveFormat.SelectedIndexChanged += new System.EventHandler(this.cboxSaveFormat_TextUpdate);
            this.cboxSaveFormat.TextUpdate += new System.EventHandler(this.cboxSaveFormat_TextUpdate);
            // 
            // lblPreviewT
            // 
            this.lblPreviewT.AutoSize = true;
            this.lblPreviewT.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreviewT.Location = new System.Drawing.Point(12, 104);
            this.lblPreviewT.Name = "lblPreviewT";
            this.lblPreviewT.Size = new System.Drawing.Size(48, 13);
            this.lblPreviewT.TabIndex = 9;
            this.lblPreviewT.Text = "Preview:";
            // 
            // lblPreview
            // 
            this.lblPreview.AutoSize = true;
            this.lblPreview.Location = new System.Drawing.Point(69, 105);
            this.lblPreview.Name = "lblPreview";
            this.lblPreview.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPreview.Size = new System.Drawing.Size(0, 13);
            this.lblPreview.TabIndex = 10;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(362, 99);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 23);
            this.btnOk.TabIndex = 11;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // AdvancedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 128);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblPreview);
            this.Controls.Add(this.lblPreviewT);
            this.Controls.Add(this.cboxSaveFormat);
            this.Controls.Add(this.lblLine1);
            this.Controls.Add(this.btnFormatReset);
            this.Controls.Add(this.lblFormatInfo);
            this.Controls.Add(this.lblSaveFormat);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AdvancedDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Advanced Settings";
            this.Load += new System.EventHandler(this.AdvancedDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSaveFormat;
        private System.Windows.Forms.Button btnFormatReset;
        private System.Windows.Forms.Label lblFormatInfo;
        private System.Windows.Forms.Label lblLine1;
        private System.Windows.Forms.ComboBox cboxSaveFormat;
        private System.Windows.Forms.Label lblPreviewT;
        private System.Windows.Forms.Label lblPreview;
        private System.Windows.Forms.Button btnOk;
    }
}