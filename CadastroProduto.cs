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
    public partial class CadastroProduto : Form
    {
        public CadastroProduto()
        {
            InitializeComponent();
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



        private void textBoxProduto2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDescricao_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPreco_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxEstoque_TextChanged(object sender, EventArgs e)
        {

        }
        // Evento para 
        private void buttonConProduto_Click(object sender, EventArgs e)
        {
            // Dados do formulário
            string nomeProduto = textBoxProduto2.Text;
            string descricao = textBoxDescricao.Text;
            decimal preco;
            int estoque;

            // Verificação de entrada
            if (string.IsNullOrWhiteSpace(nomeProduto) ||
                string.IsNullOrWhiteSpace(descricao) ||
                !decimal.TryParse(textBoxPreco.Text, out preco) ||
                !int.TryParse(textBoxEstoque.Text, out estoque))
            {
                MessageBox.Show("Por favor, preencha todos os campos corretamente.");
                return;
            }

            // String de conexão para autenticação pelo Windows
            string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Query para inserir o produto no banco
                string query = "INSERT INTO Produtos (NomeProduto, Descricao, Preco200g, Estoque) " +
                               "VALUES (@NomeProduto, @Descricao, @Preco, @Estoque)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Adicionando parâmetros
                    command.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                    command.Parameters.AddWithValue("@Descricao", descricao);
                    command.Parameters.AddWithValue("@Preco", preco);
                    command.Parameters.AddWithValue("@Estoque", estoque);

                    try
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                        MessageBox.Show("Produto registrado com sucesso! Status: Ativo");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao registrar o produto: " + ex.Message);
                    }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
