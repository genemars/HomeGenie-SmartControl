namespace HgSmartControl
{
    partial class SmartControl
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

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupList1 = new HgSmartControl.Widgets.GroupList();
            this.SuspendLayout();
            // 
            // groupList1
            // 
            this.groupList1.BackColor = System.Drawing.Color.Black;
            this.groupList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupList1.ForeColor = System.Drawing.Color.White;
            this.groupList1.Location = new System.Drawing.Point(0, 0);
            this.groupList1.Name = "groupList1";
            this.groupList1.Size = new System.Drawing.Size(280, 246);
            this.groupList1.TabIndex = 0;
            this.groupList1.GroupSelected += new System.EventHandler<int>(this.groupList1_GroupSelected);
            // 
            // SmartControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(280, 246);
            this.ControlBox = false;
            this.Controls.Add(this.groupList1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SmartControl";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "HG Smart Control";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private Widgets.GroupList groupList1;






    }
}

