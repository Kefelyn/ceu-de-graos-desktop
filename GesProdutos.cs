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
        private readonly string connectionString = "Server=JANUARY\\SQLDEVELOPER;Database=SistemasFazenda;Trusted_Connection=True;";
        private DataGridView dataGridViewProdutos;


        public GesProdutos()
        {
            InitializeComponent();
            InitializeDataGridView(); // Inicializa e configura o DataGridView
            LoadProdutos();

            // Associa os eventos KeyDown aos campos de texto de pesquisa
            textBoxCodigo.KeyDown += textBoxCodigo_KeyDown;
            textBoxProdutoNome.KeyDown += textBoxProdutoNome_KeyDown;
            textBoxPreco.KeyDown += textBoxPreco_KeyDown;
            textBoxEstoque.KeyDown += textBoxEstoque_KeyDown;
        }
        private void InitializeDataGridView()
        {
            dataGridViewProdutos = new DataGridView
            {
                Location = new System.Drawing.Point(50, 230),
                Size = new System.Drawing.Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            this.Controls.Add(dataGridViewProdutos); // Adiciona o DataGridView ao formulário
        }
        private void LoadProdutos()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = @"SELECT ProdutoID AS [Código], NomeProduto AS [Nome], Preco200g AS [Preço], Estoque 
                                     FROM Produtos";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridViewProdutos.DataSource = dataTable; // Define o DataTable como fonte de dados
                    }
                    else
                    {
                        MessageBox.Show("Nenhum produto cadastrado ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewProdutos.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produtos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Método para realizar a pesquisa de produtos
        private void PesquisarProduto(string coluna, string valor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = $@"
                        SELECT ProdutoID AS [Código], NomeProduto AS [Nome], Preco200g AS [Preço], Estoque 
                        FROM Produtos WHERE {coluna} LIKE @valor";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@valor", "%" + valor + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridViewProdutos.DataSource = dataTable;
                        }
                        else
                        {
                            MessageBox.Show("Nenhum produto encontrado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridViewProdutos.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBoxProdutoNome_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string ProdutoNome = textBoxProdutoNome.Text.Trim();
                if (!string.IsNullOrEmpty(ProdutoNome))
                {
                    PesquisarProduto("NomeProduto", ProdutoNome);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codigo = textBoxCodigo.Text.Trim();
                if (!string.IsNullOrEmpty(codigo))
                {
                    PesquisarProduto("ProdutoID", codigo);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void textBoxEstoque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string estoque = textBoxEstoque.Text.Trim();
                if (!string.IsNullOrEmpty(estoque))
                {
                    PesquisarProduto("Estoque", estoque);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxDelProduto_Click(object sender, EventArgs e)
        {
            // Verifica se há uma linha selecionada
            if (dataGridViewProdutos.SelectedRows.Count > 0)
            {
                // Obtém o código do produto selecionado (supondo que o código esteja na primeira coluna)
                int produtoId = Convert.ToInt32(dataGridViewProdutos.SelectedRows[0].Cells["Código"].Value);

                // Confirmação para excluir o produto
                DialogResult result = MessageBox.Show("Tem certeza que deseja excluir o produto selecionado?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Chama o método para excluir o produto do banco de dados
                    ExcluirProduto(produtoId);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um produto para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ExcluirProduto(int produtoId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Consulta SQL para excluir o produto
                    string query = "DELETE FROM Produtos WHERE ProdutoID = @ProdutoID";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@ProdutoID", produtoId);

                    // Executa o comando
                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Produto excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Após excluir, atualiza o DataGridView
                        LoadProdutos();
                    }
                    else
                    {
                        MessageBox.Show("Erro ao excluir o produto. Verifique se o produto existe.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao excluir o produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void pictureBoxEditProduto_Click(object sender, EventArgs e)
        {

        }

        private void buttonSair_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair do programa?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Fecha o formulário atual
                Application.Exit(); // Fecha o programa
            }
        }

        private void textBoxPreco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string preco = textBoxPreco.Text.Trim();
                if (!string.IsNullOrEmpty(preco))
                {
                    PesquisarProduto("Preco200g", preco);
                    e.SuppressKeyPress = true;
                }
            }
        }
    }
}
