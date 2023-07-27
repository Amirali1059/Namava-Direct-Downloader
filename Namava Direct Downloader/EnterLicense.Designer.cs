
namespace Namava_Direct_Downloader
{
    partial class EnterLicense
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
            this.lblEnterLicense = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tboxLicense = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblEnterLicense
            // 
            this.lblEnterLicense.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnterLicense.Location = new System.Drawing.Point(13, 9);
            this.lblEnterLicense.Name = "lblEnterLicense";
            this.lblEnterLicense.Size = new System.Drawing.Size(209, 37);
            this.lblEnterLicense.TabIndex = 2;
            this.lblEnterLicense.Text = "This app is not activated yet, please enter the activation license:";
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(66, 80);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 5;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(147, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tboxLicense
            // 
            this.tboxLicense.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tboxLicense.Location = new System.Drawing.Point(12, 49);
            this.tboxLicense.Name = "tboxLicense";
            this.tboxLicense.Size = new System.Drawing.Size(210, 25);
            this.tboxLicense.TabIndex = 1;
            this.tboxLicense.Text = "XXXX-XXXX-XXXX-XXXX-XXXX";
            // 
            // EnterLicense
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 111);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.lblEnterLicense);
            this.Controls.Add(this.tboxLicense);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EnterLicense";
            this.Text = "Enter License";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblEnterLicense;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox tboxLicense;
    }
}