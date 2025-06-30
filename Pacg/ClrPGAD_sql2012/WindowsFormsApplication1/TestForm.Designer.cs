namespace TestApplication
{
    partial class TestForm
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
            this.btnSubReg = new System.Windows.Forms.Button();
            this.lblSubResponse = new System.Windows.Forms.Label();
            this.btnCambiaStato = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSubReg
            // 
            this.btnSubReg.Location = new System.Drawing.Point(12, 97);
            this.btnSubReg.Name = "btnSubReg";
            this.btnSubReg.Size = new System.Drawing.Size(75, 23);
            this.btnSubReg.TabIndex = 0;
            this.btnSubReg.Text = "SubRegistrazione";
            this.btnSubReg.UseVisualStyleBackColor = true;
            this.btnSubReg.Click += new System.EventHandler(this.btnSubReg_Click);
            // 
            // lblSubResponse
            // 
            this.lblSubResponse.AutoSize = true;
            this.lblSubResponse.Location = new System.Drawing.Point(13, 123);
            this.lblSubResponse.Name = "lblSubResponse";
            this.lblSubResponse.Size = new System.Drawing.Size(0, 13);
            this.lblSubResponse.TabIndex = 1;
            // 
            // btnCambiaStato
            // 
            this.btnCambiaStato.Location = new System.Drawing.Point(13, 137);
            this.btnCambiaStato.Name = "btnCambiaStato";
            this.btnCambiaStato.Size = new System.Drawing.Size(75, 23);
            this.btnCambiaStato.TabIndex = 2;
            this.btnCambiaStato.Text = "CambioStato";
            this.btnCambiaStato.UseVisualStyleBackColor = true;
            this.btnCambiaStato.Click += new System.EventHandler(this.btnCambiaStato_Click);
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnCambiaStato);
            this.Controls.Add(this.lblSubResponse);
            this.Controls.Add(this.btnSubReg);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSubReg;
        private System.Windows.Forms.Label lblSubResponse;
        private System.Windows.Forms.Button btnCambiaStato;
    }
}

