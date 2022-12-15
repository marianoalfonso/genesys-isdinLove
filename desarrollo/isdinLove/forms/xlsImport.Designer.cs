namespace isdinLove.forms
{
    partial class xlsImport
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.chkPackaging = new System.Windows.Forms.CheckBox();
            this.chkRemito = new System.Windows.Forms.CheckBox();
            this.chkProducto = new System.Windows.Forms.CheckBox();
            this.chkEmail = new System.Windows.Forms.CheckBox();
            this.dgvProductos = new System.Windows.Forms.DataGridView();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 12);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(751, 584);
            this.dataGridView1.TabIndex = 0;
            // 
            // chkPackaging
            // 
            this.chkPackaging.AutoSize = true;
            this.chkPackaging.Location = new System.Drawing.Point(804, 23);
            this.chkPackaging.Name = "chkPackaging";
            this.chkPackaging.Size = new System.Drawing.Size(76, 17);
            this.chkPackaging.TabIndex = 1;
            this.chkPackaging.Text = "packaging";
            this.chkPackaging.UseVisualStyleBackColor = true;
            // 
            // chkRemito
            // 
            this.chkRemito.AutoSize = true;
            this.chkRemito.Location = new System.Drawing.Point(804, 47);
            this.chkRemito.Name = "chkRemito";
            this.chkRemito.Size = new System.Drawing.Size(54, 17);
            this.chkRemito.TabIndex = 2;
            this.chkRemito.Text = "remito";
            this.chkRemito.UseVisualStyleBackColor = true;
            // 
            // chkProducto
            // 
            this.chkProducto.AutoSize = true;
            this.chkProducto.Location = new System.Drawing.Point(804, 71);
            this.chkProducto.Name = "chkProducto";
            this.chkProducto.Size = new System.Drawing.Size(68, 17);
            this.chkProducto.TabIndex = 3;
            this.chkProducto.Text = "producto";
            this.chkProducto.UseVisualStyleBackColor = true;
            // 
            // chkEmail
            // 
            this.chkEmail.AutoSize = true;
            this.chkEmail.Location = new System.Drawing.Point(804, 95);
            this.chkEmail.Name = "chkEmail";
            this.chkEmail.Size = new System.Drawing.Size(50, 17);
            this.chkEmail.TabIndex = 4;
            this.chkEmail.Text = "email";
            this.chkEmail.UseVisualStyleBackColor = true;
            // 
            // dgvProductos
            // 
            this.dgvProductos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProductos.Location = new System.Drawing.Point(898, 71);
            this.dgvProductos.Name = "dgvProductos";
            this.dgvProductos.Size = new System.Drawing.Size(399, 181);
            this.dgvProductos.TabIndex = 5;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(804, 293);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(493, 303);
            this.dataGridView2.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(818, 274);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(195, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "devolucion del STORED PROCEDURE";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(922, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "consolidacion del PEDIDO";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // xlsImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1309, 608);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.dgvProductos);
            this.Controls.Add(this.chkEmail);
            this.Controls.Add(this.chkProducto);
            this.Controls.Add(this.chkRemito);
            this.Controls.Add(this.chkPackaging);
            this.Controls.Add(this.dataGridView1);
            this.Name = "xlsImport";
            this.Text = "xlsImport";
            this.Load += new System.EventHandler(this.xlsImport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProductos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckBox chkPackaging;
        private System.Windows.Forms.CheckBox chkRemito;
        private System.Windows.Forms.CheckBox chkProducto;
        private System.Windows.Forms.CheckBox chkEmail;
        private System.Windows.Forms.DataGridView dgvProductos;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}