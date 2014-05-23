namespace HgSmartControl.Widgets
{
    partial class GroupView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupView));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.labelGroup = new System.Windows.Forms.Label();
            this.pictureBoxScenarios = new System.Windows.Forms.PictureBox();
            this.centerPanelView = new HgSmartControl.Controls.CenterPanel();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScenarios)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.centerPanelView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxScenarios, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelGroup, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(300, 240);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // labelGroup
            // 
            this.labelGroup.AutoSize = true;
            this.labelGroup.BackColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.SetColumnSpan(this.labelGroup, 3);
            this.labelGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelGroup.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGroup.ForeColor = System.Drawing.Color.Yellow;
            this.labelGroup.Location = new System.Drawing.Point(50, 200);
            this.labelGroup.Margin = new System.Windows.Forms.Padding(0);
            this.labelGroup.Name = "labelGroup";
            this.labelGroup.Size = new System.Drawing.Size(200, 40);
            this.labelGroup.TabIndex = 9;
            this.labelGroup.Text = "Living Room";
            this.labelGroup.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.labelGroup.Click += new System.EventHandler(this.labelGroup_Click);
            // 
            // pictureBoxScenarios
            // 
            this.pictureBoxScenarios.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxScenarios.Image")));
            this.pictureBoxScenarios.Location = new System.Drawing.Point(256, 206);
            this.pictureBoxScenarios.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBoxScenarios.Name = "pictureBoxScenarios";
            this.pictureBoxScenarios.Size = new System.Drawing.Size(38, 28);
            this.pictureBoxScenarios.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxScenarios.TabIndex = 13;
            this.pictureBoxScenarios.TabStop = false;
            // 
            // centerPanel1
            // 
            this.centerPanelView.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.centerPanelView, 5);
            this.centerPanelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.centerPanelView.Location = new System.Drawing.Point(0, 0);
            this.centerPanelView.Margin = new System.Windows.Forms.Padding(0);
            this.centerPanelView.Name = "centerPanel1";
            this.centerPanelView.Padding = new System.Windows.Forms.Padding(20, 5, 20, 5);
            this.centerPanelView.Size = new System.Drawing.Size(300, 200);
            this.centerPanelView.TabIndex = 12;
            // 
            // GroupView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Name = "GroupView";
            this.Size = new System.Drawing.Size(300, 240);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxScenarios)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label labelGroup;
        private Controls.CenterPanel centerPanelView;
        private System.Windows.Forms.PictureBox pictureBoxScenarios;
    }
}
