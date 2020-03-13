namespace VersaoDesktop.Forms
{
    partial class ScanMenuForm
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnVulcanNovel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVulcanNovel
            // 
            this.btnVulcanNovel.Location = new System.Drawing.Point(222, 194);
            this.btnVulcanNovel.Name = "btnVulcanNovel";
            this.btnVulcanNovel.Size = new System.Drawing.Size(149, 45);
            this.btnVulcanNovel.TabIndex = 0;
            this.btnVulcanNovel.Text = "Vulcan Novel";
            this.btnVulcanNovel.UseVisualStyleBackColor = true;
            this.btnVulcanNovel.Click += new System.EventHandler(this.btnVulcanNovel_Click);
            // 
            // ScanMenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 461);
            this.Controls.Add(this.btnVulcanNovel);
            this.Name = "ScanMenuForm";
            this.Text = "Scan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVulcanNovel;
    }
}

