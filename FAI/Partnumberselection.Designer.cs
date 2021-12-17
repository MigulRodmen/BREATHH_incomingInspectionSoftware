namespace FAI
{
    partial class Partnumberselection
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Partnumberselection));
            this.searchButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.partNumberComboBox = new System.Windows.Forms.ComboBox();
            this.batchTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(272, 19);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(93, 22);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Part Number";
            // 
            // partNumberComboBox
            // 
            this.partNumberComboBox.FormattingEnabled = true;
            this.partNumberComboBox.Location = new System.Drawing.Point(12, 21);
            this.partNumberComboBox.Name = "partNumberComboBox";
            this.partNumberComboBox.Size = new System.Drawing.Size(121, 21);
            this.partNumberComboBox.TabIndex = 6;
            this.partNumberComboBox.TextUpdate += new System.EventHandler(this.partNumberComboBox_TextUpdate);
            this.partNumberComboBox.TextChanged += new System.EventHandler(this.partNumberComboBox_TextChanged);
            // 
            // batchTextBox
            // 
            this.batchTextBox.Location = new System.Drawing.Point(141, 21);
            this.batchTextBox.Name = "batchTextBox";
            this.batchTextBox.Size = new System.Drawing.Size(125, 20);
            this.batchTextBox.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(138, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Batch#";
            // 
            // Partnumberselection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 48);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.batchTextBox);
            this.Controls.Add(this.partNumberComboBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.searchButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Partnumberselection";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "First Article Inspection";
            this.Load += new System.EventHandler(this.Partnumberselection_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label3;
        public System.Windows.Forms.ComboBox partNumberComboBox;
        private System.Windows.Forms.TextBox batchTextBox;
        private System.Windows.Forms.Label label1;
    }
}

