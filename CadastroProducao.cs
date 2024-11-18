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
    public partial class CadastroProducao : Form
    {

        // String de conexão para autenticação pelo Windows
        string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Integrated Security=True;";
        public CadastroProducao()
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





        // Eventos gerados automaticamente
        private void textBoxProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxPlantio_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxColheita_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxQuantidade_TextChanged(object sender, EventArgs e)
        {

        }

        // Evento para inserir a produção no banco de dadps após clicar no botão
        private void buttonConfirmar_Click(object sender, EventArgs e)
        {
            string nomeProduto = textBoxProduto1.Text;
            DateTime dataPlantio;
            DateTime dataColheita;
            int quantidadePlantada;

            // Verificação de entrada
            if (string.IsNullOrWhiteSpace(nomeProduto) ||
                !DateTime.TryParse(textBoxPlantio.Text, out dataPlantio) ||
                !DateTime.TryParse(textBoxColheita.Text, out dataColheita) ||
                !int.TryParse(textBoxQuantidade.Text, out quantidadePlantada))
            {
                MessageBox.Show("Por favor, insira todos os valores corretamente.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); // Abre a conexão com o banco
                    MessageBox.Show("Conexão aberta com sucesso.");

                    // Verifica se o produto inserido existe no banco de dados e obtém o ProdutoID
                    string queryProduto = "SELECT ProdutoID FROM Produtos WHERE NomeProduto = @NomeProduto";
                    using (SqlCommand commandProduto = new SqlCommand(queryProduto, connection))
                    {
                        commandProduto.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                        var produtoID = commandProduto.ExecuteScalar();

                        if (produtoID == null) // Produto não encontrado
                        {
                            MessageBox.Show("Produto não encontrado no banco de dados.");
                            return;
                        }

                        // Insere a nova produção no banco de dados com o status inicial "Plantado"
                        string status = "Plantado";
                        string queryProducao = "INSERT INTO DBProducoes (ProdutoID, NomeProduto, DataPlantio, DataColheita, Status, QuantidadePlantada) " +
                                               "VALUES (@ProdutoID, @NomeProduto, @DataPlantio, @DataColheita, @Status, @QuantidadePlantada)";

                        using (SqlCommand commandProducao = new SqlCommand(queryProducao, connection))
                        {
                            commandProducao.Parameters.Add("@ProdutoID", SqlDbType.Int).Value = produtoID;
                            commandProducao.Parameters.Add("@NomeProduto", SqlDbType.NVarChar).Value = nomeProduto;
                            commandProducao.Parameters.Add("@DataPlantio", SqlDbType.DateTime).Value = dataPlantio;
                            commandProducao.Parameters.Add("@DataColheita", SqlDbType.DateTime).Value = dataColheita;
                            commandProducao.Parameters.Add("@Status", SqlDbType.NVarChar).Value = status;
                            commandProducao.Parameters.Add("@QuantidadePlantada", SqlDbType.Int).Value = quantidadePlantada;

                            int rowsAffected = commandProducao.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Produção registrada com sucesso! Status atual: Plantado");
                            }
                            else
                            {
                                MessageBox.Show("Erro ao registrar a produção: Nenhuma linha foi afetada.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao registrar a produção: " + ex.Message);
                }
            }


        }





        private void buttonCancelar_Click(object sender, EventArgs e)
        {
            // Volta para a tela de gestão
            Gestao gestao = new Gestao();
            gestao.ShowDialog();

        }

        private void textBoxProduto1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
