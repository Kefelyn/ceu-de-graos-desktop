namespace TelasDesktopPIM
{
    partial class Vendas
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonSair = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.buttonVendas = new System.Windows.Forms.Button();
            this.buttonGestão = new System.Windows.Forms.Button();
            this.textBoxDataCompra = new System.Windows.Forms.TextBox();
            this.textBoxProduto = new System.Windows.Forms.TextBox();
            this.textBoxCodigo = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.labelGestao = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxValor = new System.Windows.Forms.TextBox();
            this.pictureBoxDelVendas = new System.Windows.Forms.PictureBox();
            this.textBoxQuantidade = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxCodigoCliente = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDelVendas)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LightGreen;
            this.panel1.Controls.Add(this.buttonSair);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.buttonVendas);
            this.panel1.Controls.Add(this.buttonGestão);
            this.panel1.Location = new System.Drawing.Point(1, 1);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1064, 73);
            this.panel1.TabIndex = 4;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // buttonSair
            // 
            this.buttonSair.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSair.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonSair.Location = new System.Drawing.Point(1012, 27);
            this.buttonSair.Margin = new System.Windows.Forms.Padding(4);
            this.buttonSair.Name = "buttonSair";
            this.buttonSair.Size = new System.Drawing.Size(52, 32);
            this.buttonSair.TabIndex = 5;
            this.buttonSair.Text = "Sair";
            this.buttonSair.UseVisualStyleBackColor = false;
            this.buttonSair.Click += new System.EventHandler(this.buttonSair_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TelasDesktopPIM.Properties.Resources.LogoHorizontal_250;
            this.pictureBox1.Location = new System.Drawing.Point(4, -6);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(353, 79);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // buttonVendas
            // 
            this.buttonVendas.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonVendas.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVendas.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonVendas.Location = new System.Drawing.Point(592, 14);
            this.buttonVendas.Margin = new System.Windows.Forms.Padding(4);
            this.buttonVendas.Name = "buttonVendas";
            this.buttonVendas.Size = new System.Drawing.Size(159, 46);
            this.buttonVendas.TabIndex = 3;
            this.buttonVendas.Text = "Vendas";
            this.buttonVendas.UseVisualStyleBackColor = false;
            this.buttonVendas.Click += new System.EventHandler(this.buttonVendas_Click);
            // 
            // buttonGestão
            // 
            this.buttonGestão.BackColor = System.Drawing.Color.ForestGreen;
            this.buttonGestão.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonGestão.ForeColor = System.Drawing.SystemColors.Control;
            this.buttonGestão.Location = new System.Drawing.Point(375, 14);
            this.buttonGestão.Margin = new System.Windows.Forms.Padding(4);
            this.buttonGestão.Name = "buttonGestão";
            this.buttonGestão.Size = new System.Drawing.Size(159, 46);
            this.buttonGestão.TabIndex = 1;
            this.buttonGestão.Text = "Gestão";
            this.buttonGestão.UseVisualStyleBackColor = false;
            this.buttonGestão.Click += new System.EventHandler(this.buttonGestão_Click);
            // 
            // textBoxDataCompra
            // 
            this.textBoxDataCompra.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDataCompra.Location = new System.Drawing.Point(771, 226);
            this.textBoxDataCompra.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxDataCompra.Name = "textBoxDataCompra";
            this.textBoxDataCompra.Size = new System.Drawing.Size(152, 24);
            this.textBoxDataCompra.TabIndex = 50;
            this.textBoxDataCompra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxDataCompra_KeyDown);
            // 
            // textBoxProduto
            // 
            this.textBoxProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxProduto.Location = new System.Drawing.Point(376, 226);
            this.textBoxProduto.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxProduto.Name = "textBoxProduto";
            this.textBoxProduto.Size = new System.Drawing.Size(134, 24);
            this.textBoxProduto.TabIndex = 49;
            this.textBoxProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxProduto_KeyDown);
            // 
            // textBoxCodigo
            // 
            this.textBoxCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigo.Location = new System.Drawing.Point(113, 226);
            this.textBoxCodigo.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCodigo.Name = "textBoxCodigo";
            this.textBoxCodigo.Size = new System.Drawing.Size(111, 24);
            this.textBoxCodigo.TabIndex = 48;
            this.textBoxCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCodigo_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Green;
            this.label3.Location = new System.Drawing.Point(767, 191);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(143, 20);
            this.label3.TabIndex = 46;
            this.label3.Text = "Data da compra";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Green;
            this.label2.Location = new System.Drawing.Point(372, 191);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 20);
            this.label2.TabIndex = 45;
            this.label2.Text = "Produto";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Green;
            this.label1.Location = new System.Drawing.Point(109, 191);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 44;
            this.label1.Text = "Código";
            // 
            // labelGestao
            // 
            this.labelGestao.AutoSize = true;
            this.labelGestao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelGestao.ForeColor = System.Drawing.Color.DarkGreen;
            this.labelGestao.Location = new System.Drawing.Point(146, 118);
            this.labelGestao.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelGestao.Name = "labelGestao";
            this.labelGestao.Size = new System.Drawing.Size(304, 36);
            this.labelGestao.TabIndex = 43;
            this.labelGestao.Text = "LISTA DE PEDIDOS";
            this.labelGestao.Click += new System.EventHandler(this.labelGestao_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Green;
            this.label5.Location = new System.Drawing.Point(647, 191);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 20);
            this.label5.TabIndex = 53;
            this.label5.Text = "Valor";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBoxValor
            // 
            this.textBoxValor.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxValor.Location = new System.Drawing.Point(651, 226);
            this.textBoxValor.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxValor.Name = "textBoxValor";
            this.textBoxValor.Size = new System.Drawing.Size(100, 24);
            this.textBoxValor.TabIndex = 54;
            this.textBoxValor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxValor_KeyDown);
            // 
            // pictureBoxDelVendas
            // 
            this.pictureBoxDelVendas.Image = global::TelasDesktopPIM.Properties.Resources.ic_lixeira_70;
            this.pictureBoxDelVendas.Location = new System.Drawing.Point(930, 217);
            this.pictureBoxDelVendas.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBoxDelVendas.Name = "pictureBoxDelVendas";
            this.pictureBoxDelVendas.Size = new System.Drawing.Size(28, 33);
            this.pictureBoxDelVendas.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxDelVendas.TabIndex = 55;
            this.pictureBoxDelVendas.TabStop = false;
            this.pictureBoxDelVendas.Click += new System.EventHandler(this.pictureBoxDelVendas_Click);
            // 
            // textBoxQuantidade
            // 
            this.textBoxQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxQuantidade.Location = new System.Drawing.Point(529, 226);
            this.textBoxQuantidade.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxQuantidade.Name = "textBoxQuantidade";
            this.textBoxQuantidade.Size = new System.Drawing.Size(100, 24);
            this.textBoxQuantidade.TabIndex = 57;
            this.textBoxQuantidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxQuantidade_KeyDown);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Green;
            this.label4.Location = new System.Drawing.Point(525, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 20);
            this.label4.TabIndex = 56;
            this.label4.Text = "Quantidade";
            // 
            // textBoxCodigoCliente
            // 
            this.textBoxCodigoCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxCodigoCliente.Location = new System.Drawing.Point(246, 226);
            this.textBoxCodigoCliente.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxCodigoCliente.Name = "textBoxCodigoCliente";
            this.textBoxCodigoCliente.Size = new System.Drawing.Size(111, 24);
            this.textBoxCodigoCliente.TabIndex = 59;
            this.textBoxCodigoCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxCodigoCliente_KeyDown);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Green;
            this.label6.Location = new System.Drawing.Point(242, 191);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(93, 20);
            this.label6.TabIndex = 58;
            this.label6.Text = "ID Cliente";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // Vendas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1067, 554);
            this.Controls.Add(this.textBoxCodigoCliente);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxQuantidade);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBoxDelVendas);
            this.Controls.Add(this.textBoxValor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxDataCompra);
            this.Controls.Add(this.textBoxProduto);
            this.Controls.Add(this.textBoxCodigo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelGestao);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Vendas";
            this.Text = "Vendas";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDelVendas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonSair;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button buttonVendas;
        private System.Windows.Forms.Button buttonGestão;
        private System.Windows.Forms.TextBox textBoxDataCompra;
        private System.Windows.Forms.TextBox textBoxProduto;
        private System.Windows.Forms.TextBox textBoxCodigo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelGestao;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxValor;
        private System.Windows.Forms.PictureBox pictureBoxDelVendas;
        private System.Windows.Forms.TextBox textBoxQuantidade;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxCodigoCliente;
        private System.Windows.Forms.Label label6;
    }
}