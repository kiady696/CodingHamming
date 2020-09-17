namespace Codage
{
    partial class Form1
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.aEncoder = new System.Windows.Forms.TextBox();
            this.coder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.errorMax = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.typeCodage = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // aEncoder
            // 
            this.aEncoder.Location = new System.Drawing.Point(220, 80);
            this.aEncoder.Name = "aEncoder";
            this.aEncoder.Size = new System.Drawing.Size(100, 20);
            this.aEncoder.TabIndex = 0;
            // 
            // coder
            // 
            this.coder.Location = new System.Drawing.Point(340, 244);
            this.coder.Name = "coder";
            this.coder.Size = new System.Drawing.Size(75, 23);
            this.coder.TabIndex = 1;
            this.coder.Text = "Envoyer";
            this.coder.UseVisualStyleBackColor = true;
            this.coder.Click += new System.EventHandler(this.coder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(84, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Message à envoyer";
            // 
            // errorMax
            // 
            this.errorMax.Location = new System.Drawing.Point(220, 134);
            this.errorMax.Name = "errorMax";
            this.errorMax.Size = new System.Drawing.Size(100, 20);
            this.errorMax.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(199, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Nombre d\'erreur max pour chaque k-bloc";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(479, 86);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Codage";
            // 
            // typeCodage
            // 
            this.typeCodage.FormattingEnabled = true;
            this.typeCodage.Location = new System.Drawing.Point(583, 78);
            this.typeCodage.Name = "typeCodage";
            this.typeCodage.Size = new System.Drawing.Size(121, 21);
            this.typeCodage.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.typeCodage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.errorMax);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.coder);
            this.Controls.Add(this.aEncoder);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox aEncoder;
        private System.Windows.Forms.Button coder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox errorMax;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox typeCodage;
    }
}

