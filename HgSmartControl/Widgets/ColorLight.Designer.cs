namespace HgSmartControl.Widgets
{
    partial class ColorLight
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColorLight));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.pictureBoxHue = new System.Windows.Forms.PictureBox();
            this.pictureBoxClose = new System.Windows.Forms.PictureBox();
            this.labelStatus = new System.Windows.Forms.Label();
            this.labelTitle = new System.Windows.Forms.Label();
            this.panelColor = new System.Windows.Forms.Panel();
            this.svColorGradient = new HgSmartControl.Controls.SvGradient();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxIcon, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxHue, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.svColorGradient, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pictureBoxClose, 2, 4);
            this.tableLayoutPanel1.Controls.Add(this.labelStatus, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.labelTitle, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelColor, 3, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(320, 240);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // pictureBoxIcon
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBoxIcon, 2);
            this.pictureBoxIcon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxIcon.Location = new System.Drawing.Point(10, 10);
            this.pictureBoxIcon.Margin = new System.Windows.Forms.Padding(10);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.tableLayoutPanel1.SetRowSpan(this.pictureBoxIcon, 2);
            this.pictureBoxIcon.Size = new System.Drawing.Size(100, 80);
            this.pictureBoxIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxIcon.TabIndex = 1;
            this.pictureBoxIcon.TabStop = false;
            // 
            // pictureBoxHue
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.pictureBoxHue, 6);
            this.pictureBoxHue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxHue.Image = global::HgSmartControl.Properties.Resources.huebar;
            this.pictureBoxHue.Location = new System.Drawing.Point(10, 151);
            this.pictureBoxHue.Margin = new System.Windows.Forms.Padding(10, 1, 10, 0);
            this.pictureBoxHue.Name = "pictureBoxHue";
            this.pictureBoxHue.Size = new System.Drawing.Size(300, 49);
            this.pictureBoxHue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxHue.TabIndex = 0;
            this.pictureBoxHue.TabStop = false;
            this.pictureBoxHue.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxHue_MouseAction);
            this.pictureBoxHue.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxHue_MouseAction);
            // 
            // pictureBoxClose
            // 
            this.pictureBoxClose.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxClose.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxClose.Image")));
            this.pictureBoxClose.Location = new System.Drawing.Point(123, 203);
            this.pictureBoxClose.Name = "pictureBoxClose";
            this.pictureBoxClose.Size = new System.Drawing.Size(54, 34);
            this.pictureBoxClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBoxClose.TabIndex = 11;
            this.pictureBoxClose.TabStop = false;
            this.pictureBoxClose.Click += new System.EventHandler(this.pictureBoxClose_Click);
            // 
            // labelStatus
            // 
            this.labelStatus.AutoSize = true;
            this.labelStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelStatus.Font = new System.Drawing.Font("Lucida Sans Unicode", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.labelStatus.Location = new System.Drawing.Point(120, 50);
            this.labelStatus.Margin = new System.Windows.Forms.Padding(0);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(60, 50);
            this.labelStatus.TabIndex = 13;
            this.labelStatus.Text = "75%";
            this.labelStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // labelTitle
            // 
            this.labelTitle.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.labelTitle, 3);
            this.labelTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelTitle.Font = new System.Drawing.Font("Lucida Sans Unicode", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTitle.Location = new System.Drawing.Point(120, 0);
            this.labelTitle.Margin = new System.Windows.Forms.Padding(0);
            this.labelTitle.Name = "labelTitle";
            this.labelTitle.Size = new System.Drawing.Size(180, 50);
            this.labelTitle.TabIndex = 12;
            this.labelTitle.Text = "Color Light";
            this.labelTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelColor
            // 
            this.panelColor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelColor.Location = new System.Drawing.Point(183, 53);
            this.panelColor.Name = "panelColor";
            this.panelColor.Size = new System.Drawing.Size(54, 44);
            this.panelColor.TabIndex = 14;
            // 
            // svColorGradient
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.svColorGradient, 6);
            this.svColorGradient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.svColorGradient.Location = new System.Drawing.Point(10, 100);
            this.svColorGradient.Margin = new System.Windows.Forms.Padding(10, 0, 10, 1);
            this.svColorGradient.Name = "svColorGradient";
            this.svColorGradient.Size = new System.Drawing.Size(300, 49);
            this.svColorGradient.SvColor = System.Drawing.Color.Red;
            this.svColorGradient.TabIndex = 2;
            this.svColorGradient.Text = "linearGradient1";
            // 
            // ColorLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ColorLight";
            this.Size = new System.Drawing.Size(320, 240);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxHue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxClose)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox pictureBoxHue;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private Controls.SvGradient svColorGradient;
        private System.Windows.Forms.PictureBox pictureBoxClose;
        private System.Windows.Forms.Label labelStatus;
        private System.Windows.Forms.Label labelTitle;
        private System.Windows.Forms.Panel panelColor;
    }
}
