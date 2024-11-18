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
    public partial class GesProducoes : Form
    {
        private readonly string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Trusted_Connection=True;";
        private DataGridView dataGridViewProducoes;


        public GesProducoes()
        {
            InitializeComponent();
            InitializeDataGridView(); // Inicializa e configura o DataGridView
            CarregarProducoes();      // Carrega as produções ao inicializar o formulário

        }
        private void InitializeDataGridView()
        {
            dataGridViewProducoes = new DataGridView
            {
                Location = new System.Drawing.Point(36, 230),
                Size = new System.Drawing.Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            // Define manualmente as colunas para exibição inicial
            dataGridViewProducoes.Columns.Add("Código", "Código");
            dataGridViewProducoes.Columns.Add("NomeProduto", "Nome do Produto");
            dataGridViewProducoes.Columns.Add("DataPlantio", "Data de Plantio");
            dataGridViewProducoes.Columns.Add("Status", "Status");

            this.Controls.Add(dataGridViewProducoes); // Adiciona o DataGridView ao formulário
        }
        private void CarregarProducoes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProducaoID AS Código, NomeProduto, DataPlantio, Status FROM DBProducoes";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridViewProducoes.DataSource = dataTable; // Define o DataTable como fonte de dados para o DataGridView
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma produção cadastrada ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewProducoes.DataSource = null; // Mantém o DataGridView visível, mesmo sem dados
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar produções: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        // Redireciona para a tela de gestao ao ser cliacado
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



        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CadastroProducao cadastroProducao = new CadastroProducao();
            cadastroProducao.ShowDialog();
        }

        private void textBoxCodigo_TextChanged(object sender, EventArgs e)
        {
            // Verifica se o Código foi preenchido para carregar os dados
            if (int.TryParse(textBoxCodigo.Text, out int producaoID))
            {
                CarregarDadosProducao(producaoID);
            }

        }
        // Metodo para carreegar os dados da produção
        private void CarregarDadosProducao(int producaoID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Tratamento de exeções
                try
                {
                    // Abre uma conexão com o banco de dados
                    connection.Open();
                    // Seleciona os dados registrados no banco de dados para exibição
                    string query = "SELECT * FROM DBProducoes WHERE ProducaoID = @ProducaoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProducaoID", producaoID);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            textBoxNomeProduto.Text = reader["NomeProduto"].ToString();
                            textBoxDataPlantio.Text = reader["DataPlantio"].ToString();
                            textBoxDataColheita.Text = reader["DataColheita"].ToString(); // Caso queira incluir a data de colheita
                        }
                        else
                        {
                            MessageBox.Show("Produção não encontrada.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados da produção: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void textBoxNomeProduto_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDataPlantio_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBoxDelProducao_Click(object sender, EventArgs e)
        {
            // Verifica se o código foi preenchido
            if (string.IsNullOrWhiteSpace(textBoxCodigo.Text))
            {
                MessageBox.Show("Por favor, selecione uma produção para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int codigoProducao;
            if (!int.TryParse(textBoxCodigo.Text, out codigoProducao))
            {
                MessageBox.Show("Código inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Confirmar a exclusão
            DialogResult dialogResult = MessageBox.Show("Tem certeza de que deseja excluir esta produção?", "Confirmar Exclusão", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.Yes)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();

                        // Verifica se a produção existe antes de tentar excluir
                        string verificaExistenciaQuery = "SELECT COUNT(*) FROM DBProducoes WHERE ProducaoID = @ProducaoID";
                        using (SqlCommand commandVerificacao = new SqlCommand(verificaExistenciaQuery, connection))
                        {
                            commandVerificacao.Parameters.AddWithValue("@ProducaoID", codigoProducao);
                            int count = (int)commandVerificacao.ExecuteScalar();

                            if (count == 0)
                            {
                                MessageBox.Show("A produção não foi encontrada no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Exclui a produção do banco de dados
                        string deleteQuery = "DELETE FROM DBProducoes WHERE ProducaoID = @ProducaoID";

                        using (SqlCommand commandDelete = new SqlCommand(deleteQuery, connection))
                        {
                            commandDelete.Parameters.AddWithValue("@ProducaoID", codigoProducao);
                            int rowsAffected = commandDelete.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Produção excluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CarregarProducoes(); // Atualiza a lista de produções após a exclusão
                            }
                            else
                            {
                                MessageBox.Show("Erro ao excluir a produção.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir a produção: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void pictureBoxEditProducao_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBoxCodigo.Text))
            {
                MessageBox.Show("Selecione uma produção para editar.");
                return;
            }

            int producaoID = int.Parse(textBoxCodigo.Text);
            string nomeProduto = textBoxNomeProduto.Text;
            DateTime dataPlantio = DateTime.Parse(textBoxDataPlantio.Text);
           
            string dataColheita = textBoxDataColheita.Text;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE DBProducoes SET NomeProduto = @NomeProduto, DataPlantio = @DataPlantio, DataColheita = @DataColheita " +
                                   "WHERE ProducaoID = @ProducaoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@NomeProduto", nomeProduto);
                        command.Parameters.AddWithValue("@DataPlantio", dataPlantio);
                       
                        command.Parameters.AddWithValue("@DataColheita", string.IsNullOrWhiteSpace(dataColheita) ? (object)DBNull.Value : dataColheita);
                        command.Parameters.AddWithValue("@ProducaoID", producaoID);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Produção atualizada com sucesso!");
                            CarregarProducoes(); // Recarrega as produções
                        }
                        else
                        {
                            MessageBox.Show("Erro ao atualizar a produção.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao editar produção: " + ex.Message);
                }
            }
        }
    

        // Este é o evento correto para o clique duplo
        private void DataGridViewProducoes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Verifica se o clique foi em uma linha válida
            {
                DataGridViewRow row = dataGridViewProducoes.Rows[e.RowIndex];
                textBoxCodigo.Text = row.Cells["Código"].Value.ToString();
                textBoxNomeProduto.Text = row.Cells["NomeProduto"].Value.ToString();
                textBoxDataPlantio.Text = row.Cells["DataPlantio"].Value.ToString();
             

                // Para a DataColheita, considere verificar se esse valor está disponível.
                textBoxDataColheita.Text = row.Cells["DataColheita"].Value.ToString();
            }
        }


        private void buttonSair_Click(object sender, EventArgs e)
        {
            Gestao gestao = new Gestao();
            gestao.ShowDialog();
         }
    }
}



