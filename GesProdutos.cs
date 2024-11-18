using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TelasDesktopPIM
{
    public partial class GesProdutos : Form
    {
        private readonly string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Trusted_Connection=True;";
        private DataGridView dataGridViewProdutos;


        public GesProdutos()
        {
            InitializeComponent();
            InitializeDataGridView(); // Inicializa e configura o DataGridView
            CarregarProdutos(); // Carrega os produtos ao inicializar o formulário

        }
        private void InitializeDataGridView()
        {
            dataGridViewProdutos = new DataGridView
            {
                Location = new System.Drawing.Point(36, 230), // Defina a posição no formulário
                Size = new System.Drawing.Size(700, 200), // Defina o tamanho do DataGridView
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, // Ajuste das colunas automaticamente
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            this.Controls.Add(dataGridViewProdutos); // Adiciona o DataGridView ao formulário
        }
        private void CarregarProdutos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Consulta para buscar produtos
                    string query = "SELECT ProdutoID AS Código, NomeProduto AS Nome, Preco200g AS Preço, Estoque FROM Produtos";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridViewProdutos.DataSource = dataTable; // Define o DataTable como fonte de dados para o DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto cadastrado ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewProdutos.DataSource = null; // Mantém o DataGridView visível, mesmo sem dados
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadProdutos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Consulta para buscar os produtos
                    string query = "SELECT ProdutoID AS Código, NomeProduto AS Nome, Preco200g AS Preço, Estoque FROM Produtos";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridViewProdutos.DataSource = dataTable; // Define o DataTable como fonte de dados para o DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto cadastrado ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewProdutos.DataSource = null; // Mantém o DataGridView visível, mesmo sem dados
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void buttonGestão_Click(object sender, EventArgs e)
        {
            Gestao gestao = new Gestao();
            gestao.ShowDialog();
        }

        private void buttonVendas_Click(object sender, EventArgs e)
        {
            Vendas vendas = new Vendas();
            vendas.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            CadastroProduto cadastroProduto = new CadastroProduto();
            cadastroProduto.ShowDialog();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxCodProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEstoque_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxDelProduto_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxEditProduto_Click(object sender, EventArgs e)
        {

        }
    }
}
