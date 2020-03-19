namespace VersaoDesktop.Forms
{
    partial class FormMenuScans
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
            this.btnUnionMangas = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnVulcanNovel
            // 
            this.btnVulcanNovel.Enabled = false;
            this.btnVulcanNovel.Location = new System.Drawing.Point(15, 14);
            this.btnVulcanNovel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnVulcanNovel.Name = "btnVulcanNovel";
            this.btnVulcanNovel.Size = new System.Drawing.Size(149, 46);
            this.btnVulcanNovel.TabIndex = 0;
            this.btnVulcanNovel.Text = "Vulcan Novel";
            this.btnVulcanNovel.UseVisualStyleBackColor = true;
            this.btnVulcanNovel.Click += new System.EventHandler(this.btnVulcanNovel_Click);
            // 
            // btnUnionMangas
            // 
            this.btnUnionMangas.Location = new System.Drawing.Point(169, 14);
            this.btnUnionMangas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnUnionMangas.Name = "btnUnionMangas";
            this.btnUnionMangas.Size = new System.Drawing.Size(149, 46);
            this.btnUnionMangas.TabIndex = 1;
            this.btnUnionMangas.Text = "Union Mangas";
            this.btnUnionMangas.UseVisualStyleBackColor = true;
            this.btnUnionMangas.Click += new System.EventHandler(this.btnUnionMangas_Click);
            // 
            // FormMenuScans
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(672, 462);
            this.Controls.Add(this.btnUnionMangas);
            this.Controls.Add(this.btnVulcanNovel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "FormMenuScans";
            this.Text = "Scan";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnVulcanNovel;
        private System.Windows.Forms.Button btnUnionMangas;
    }
}

