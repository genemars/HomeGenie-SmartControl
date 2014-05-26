namespace HgSmartControl.Widgets
{
    partial class GroupList
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
            this.components = new System.ComponentModel.Container();
            this.listPanelItems = new HgSmartControl.Controls.ListPanel(this.components);
            this.SuspendLayout();
            // 
            // listPanelItems
            // 
            this.listPanelItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listPanelItems.Location = new System.Drawing.Point(0, 0);
            this.listPanelItems.Name = "listPanelItems";
            this.listPanelItems.Size = new System.Drawing.Size(320, 240);
            this.listPanelItems.TabIndex = 0;
            // 
            // GroupList
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.listPanelItems);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "GroupList";
            this.Size = new System.Drawing.Size(320, 240);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ListPanel listPanelItems;




    }
}
