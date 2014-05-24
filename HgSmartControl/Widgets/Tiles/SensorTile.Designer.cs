namespace HgSmartControl.Widgets.Tiles
{
    partial class SensorTile
    {
        /// <summary> 
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione componenti

        /// <summary> 
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare 
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelName = new System.Windows.Forms.Label();
            this.labelField = new System.Windows.Forms.Label();
            this.labelValue = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.labelName, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelField, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.labelValue, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 14F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(114, 130);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelName.ForeColor = System.Drawing.Color.White;
            this.labelName.Location = new System.Drawing.Point(3, 100);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(108, 16);
            this.labelName.TabIndex = 7;
            this.labelName.Text = "Sensor";
            this.labelName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelField
            // 
            this.labelField.AutoSize = true;
            this.labelField.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelField.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelField.ForeColor = System.Drawing.Color.Cyan;
            this.labelField.Location = new System.Drawing.Point(3, 116);
            this.labelField.Name = "labelField";
            this.labelField.Size = new System.Drawing.Size(108, 14);
            this.labelField.TabIndex = 8;
            this.labelField.Text = "Temperature";
            this.labelField.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelValue
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelValue.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValue.Location = new System.Drawing.Point(3, 0);
            this.labelValue.Name = "labelValue";
            this.labelValue.Size = new System.Drawing.Size(108, 100);
            this.labelValue.TabIndex = 9;
            this.labelValue.Text = "21°C";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SensorTile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "SensorTile";
            this.Size = new System.Drawing.Size(114, 130);
            this.Load += new System.EventHandler(this.SensorTile_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelField;
        private System.Windows.Forms.Label labelValue;
    }
}
