namespace HgSmartControl.Widgets.Items
{
    partial class GroupItem
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
            this.labelGroupName = new System.Windows.Forms.Label();
            this.panelIndicators = new System.Windows.Forms.Panel();
            this.valueIndicator7 = new HgSmartControl.Widgets.ValueIndicator();
            this.valueIndicator4 = new HgSmartControl.Widgets.ValueIndicator();
            this.valueIndicator3 = new HgSmartControl.Widgets.ValueIndicator();
            this.valueIndicator2 = new HgSmartControl.Widgets.ValueIndicator();
            this.valueIndicator1 = new HgSmartControl.Widgets.ValueIndicator();
            this.panelIndicators.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelGroupName
            // 
            this.labelGroupName.AutoSize = true;
            this.labelGroupName.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelGroupName.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGroupName.ForeColor = System.Drawing.Color.Yellow;
            this.labelGroupName.Location = new System.Drawing.Point(0, 0);
            this.labelGroupName.Name = "labelGroupName";
            this.labelGroupName.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.labelGroupName.Size = new System.Drawing.Size(89, 22);
            this.labelGroupName.TabIndex = 0;
            this.labelGroupName.Text = "Living Room";
            // 
            // panelIndicators
            // 
            this.panelIndicators.Controls.Add(this.valueIndicator7);
            this.panelIndicators.Controls.Add(this.valueIndicator4);
            this.panelIndicators.Controls.Add(this.valueIndicator3);
            this.panelIndicators.Controls.Add(this.valueIndicator2);
            this.panelIndicators.Controls.Add(this.valueIndicator1);
            this.panelIndicators.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelIndicators.Location = new System.Drawing.Point(0, 23);
            this.panelIndicators.Margin = new System.Windows.Forms.Padding(0);
            this.panelIndicators.Name = "panelIndicators";
            this.panelIndicators.Size = new System.Drawing.Size(320, 20);
            this.panelIndicators.TabIndex = 1;
            // 
            // valueIndicator7
            // 
            this.valueIndicator7.AutoSize = true;
            this.valueIndicator7.BackColor = System.Drawing.Color.Transparent;
            this.valueIndicator7.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueIndicator7.Icon = global::HgSmartControl.Properties.Resources.plug;
            this.valueIndicator7.Location = new System.Drawing.Point(203, 0);
            this.valueIndicator7.Name = "valueIndicator7";
            this.valueIndicator7.Size = new System.Drawing.Size(37, 20);
            this.valueIndicator7.TabIndex = 12;
            this.valueIndicator7.ValueText = "2";
            // 
            // valueIndicator4
            // 
            this.valueIndicator4.AutoSize = true;
            this.valueIndicator4.BackColor = System.Drawing.Color.Transparent;
            this.valueIndicator4.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueIndicator4.Icon = global::HgSmartControl.Properties.Resources.temperature;
            this.valueIndicator4.Location = new System.Drawing.Point(151, 0);
            this.valueIndicator4.Name = "valueIndicator4";
            this.valueIndicator4.Size = new System.Drawing.Size(52, 20);
            this.valueIndicator4.TabIndex = 11;
            this.valueIndicator4.ValueText = "22.5";
            // 
            // valueIndicator3
            // 
            this.valueIndicator3.AutoSize = true;
            this.valueIndicator3.BackColor = System.Drawing.Color.Transparent;
            this.valueIndicator3.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueIndicator3.Icon = global::HgSmartControl.Properties.Resources.humidity;
            this.valueIndicator3.Location = new System.Drawing.Point(100, 0);
            this.valueIndicator3.Name = "valueIndicator3";
            this.valueIndicator3.Size = new System.Drawing.Size(51, 20);
            this.valueIndicator3.TabIndex = 10;
            this.valueIndicator3.ValueText = "32%";
            // 
            // valueIndicator2
            // 
            this.valueIndicator2.AutoSize = true;
            this.valueIndicator2.BackColor = System.Drawing.Color.Transparent;
            this.valueIndicator2.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueIndicator2.Icon = global::HgSmartControl.Properties.Resources.energy;
            this.valueIndicator2.Location = new System.Drawing.Point(37, 0);
            this.valueIndicator2.Name = "valueIndicator2";
            this.valueIndicator2.Size = new System.Drawing.Size(63, 20);
            this.valueIndicator2.TabIndex = 9;
            this.valueIndicator2.ValueText = "35.5W";
            // 
            // valueIndicator1
            // 
            this.valueIndicator1.AutoSize = true;
            this.valueIndicator1.BackColor = System.Drawing.Color.Transparent;
            this.valueIndicator1.Dock = System.Windows.Forms.DockStyle.Left;
            this.valueIndicator1.Icon = global::HgSmartControl.Properties.Resources.bulb;
            this.valueIndicator1.Location = new System.Drawing.Point(0, 0);
            this.valueIndicator1.Name = "valueIndicator1";
            this.valueIndicator1.Size = new System.Drawing.Size(37, 20);
            this.valueIndicator1.TabIndex = 8;
            this.valueIndicator1.ValueText = "3";
            // 
            // GroupItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panelIndicators);
            this.Controls.Add(this.labelGroupName);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.MaximumSize = new System.Drawing.Size(320, 48);
            this.Name = "GroupItem";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Size = new System.Drawing.Size(320, 48);
            this.Load += new System.EventHandler(this.GroupItem_Load);
            this.Click += new System.EventHandler(this.GroupItem_Click);
            this.panelIndicators.ResumeLayout(false);
            this.panelIndicators.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelGroupName;
        private System.Windows.Forms.Panel panelIndicators;
        private ValueIndicator valueIndicator7;
        private ValueIndicator valueIndicator4;
        private ValueIndicator valueIndicator3;
        private ValueIndicator valueIndicator2;
        private ValueIndicator valueIndicator1;



    }
}
