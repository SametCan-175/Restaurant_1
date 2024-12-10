namespace Restaurant_1
{
    partial class CustomInputReservation
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
            this.txtCustomerName = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btnCheckReservation = new System.Windows.Forms.Button();
            this.dateTimePicker = new System.Windows.Forms.DateTimePicker();
            this.lblMasaNumarasi = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCustomerName
            // 
            this.txtCustomerName.Location = new System.Drawing.Point(133, 150);
            this.txtCustomerName.Name = "txtCustomerName";
            this.txtCustomerName.Size = new System.Drawing.Size(226, 22);
            this.txtCustomerName.TabIndex = 0;
            this.txtCustomerName.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btnCheckReservation
            // 
            this.btnCheckReservation.Location = new System.Drawing.Point(296, 242);
            this.btnCheckReservation.Name = "btnCheckReservation";
            this.btnCheckReservation.Size = new System.Drawing.Size(75, 36);
            this.btnCheckReservation.TabIndex = 2;
            this.btnCheckReservation.Text = "button1";
            this.btnCheckReservation.UseVisualStyleBackColor = true;
            this.btnCheckReservation.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Location = new System.Drawing.Point(159, 191);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new System.Drawing.Size(200, 22);
            this.dateTimePicker.TabIndex = 3;
            // 
            // lblMasaNumarasi
            // 
            this.lblMasaNumarasi.AutoSize = true;
            this.lblMasaNumarasi.Location = new System.Drawing.Point(186, 113);
            this.lblMasaNumarasi.Name = "lblMasaNumarasi";
            this.lblMasaNumarasi.Size = new System.Drawing.Size(44, 16);
            this.lblMasaNumarasi.TabIndex = 4;
            this.lblMasaNumarasi.Text = "label1";
            // 
            // CustomInputReservation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(839, 488);
            this.Controls.Add(this.lblMasaNumarasi);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.btnCheckReservation);
            this.Controls.Add(this.txtCustomerName);
            this.Name = "CustomInputReservation";
            this.Text = "CustomInputReservation";
            this.Load += new System.EventHandler(this.CustomInputReservation_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCustomerName;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btnCheckReservation;
        private System.Windows.Forms.DateTimePicker dateTimePicker;
        private System.Windows.Forms.Label lblMasaNumarasi;
    }
}