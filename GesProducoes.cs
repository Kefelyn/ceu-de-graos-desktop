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
        private readonly string connectionString = "Server=JANUARY\\SQLDEVELOPER;Database=SistemasFazenda;Trusted_Connection=True;";
        private DataGridView dataGridViewProducoes;


        public GesProducoes()
        {
            InitializeComponent();
            InitializeDataGridView(); // Inicializa e configura o DataGridView
            CarregarProducoes();      // Carrega as produções ao inicializar o formulário

            // Associando os eventos KeyDown aos respectivos métodos
            textBoxCodigo.KeyDown += textBoxCodigo_KeyDown;
            textBoxNomeProduto.KeyDown += textBoxNomeProduto_KeyDown;
            textBoxDataPlantio.KeyDown += textBoxDataPlantio_KeyDown;
            textBoxDataColheita.KeyDown += textBoxDataColheita_KeyDown;
            textBoxQuantidade.KeyDown += textBoxQuantidade_KeyDown;

        }
        private void InitializeDataGridView()
        {
            dataGridViewProducoes = new DataGridView
            {
                Location = new System.Drawing.Point(50, 230),
                Size = new System.Drawing.Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            this.Controls.Add(dataGridViewProducoes); // Adiciona o DataGridView ao formulário
        }
        private void CarregarProducoes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT ProducaoID AS ProducaoID, NomeProduto, DataPlantio, DataColheita, QuantidadePlantada FROM ProducoesP";

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

        private void PesquisarProducao(string campo, string valor)
        {
            // Exemplo de depuração
            MessageBox.Show($"Pesquisando por: {campo} = {valor}");

            string query = $@"
        SELECT ProducaoID, NomeProduto, DataPlantio, DataColheita, QuantidadePlantada 
        FROM ProducoesP
        WHERE {campo} LIKE @Valor";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona o parâmetro de valor para a pesquisa
                        command.Parameters.AddWithValue("@Valor", "%" + valor + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridViewProducoes.DataSource = dataTable; // Exibe os dados no DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Nenhum resultado encontrado.", "Pesquisa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridViewProducoes.DataSource = null;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string codigo = textBoxCodigo.Text.Trim();


                if (!string.IsNullOrEmpty(codigo))
                {
                    // Depuração: Confirme que a pesquisa está sendo chamada corretamente
                    PesquisarProducao("ProducaoID", codigo);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira um código válido.");
                }
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
                    string query = "SELECT * FROM ProducoesP WHERE ProducaoID = @ProducaoID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ProducaoID", producaoID);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            textBoxCodigo.Text = reader["ProducaoID"].ToString();
                            textBoxNomeProduto.Text = reader["NomeProduto"].ToString();
                            textBoxDataPlantio.Text = reader["DataPlantio"].ToString();
                            textBoxDataColheita.Text = reader["DataColheita"].ToString();
                            textBoxQuantidade.Text = reader["QuantidadePlantada"].ToString();

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



        private void textBoxNomeProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string nomeProduto = textBoxNomeProduto.Text.Trim();
                if (!string.IsNullOrEmpty(nomeProduto))
                {
                    PesquisarProducao("NomeProduto", nomeProduto);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }

        private void textBoxDataPlantio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string dataPlantio = textBoxDataPlantio.Text.Trim();

                if (!string.IsNullOrEmpty(dataPlantio))
                {
                    // Valida se a data está no formato dd/MM/yyyy
                    if (DateTime.TryParseExact(dataPlantio, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dataValida))
                    {
                        // Converte para o formato yyyy-MM-dd para enviar ao banco
                        PesquisarProducao("DataPlantio", dataValida.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        MessageBox.Show("Por favor, insira uma data válida no formato DD/MM/AAAA.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }

        private void pictureBoxDelProducao_Click(object sender, EventArgs e)
        {
            // Verifica se alguma linha foi selecionada
            if (dataGridViewProducoes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Por favor, selecione uma produção para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Obtém o código da produção da linha selecionada
            int codigoProducao = Convert.ToInt32(dataGridViewProducoes.SelectedRows[0].Cells["ProducaoID"].Value);

            // Verifica se o código é válido
            if (codigoProducao <= 0)
            {
                MessageBox.Show("Código de produção inválido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                        string verificaExistenciaQuery = "SELECT COUNT(*) FROM ProducoesP WHERE ProducaoID = @ProducaoID";
                        using (SqlCommand commandVerificacao = new SqlCommand(verificaExistenciaQuery, connection))
                        {
                            commandVerificacao.Parameters.Add("@ProducaoID", SqlDbType.Int).Value = codigoProducao;
                            int count = (int)commandVerificacao.ExecuteScalar();

                            if (count == 0)
                            {
                                MessageBox.Show("A produção não foi encontrada no banco de dados.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }

                        // Exclui a produção do banco de dados
                        string deleteQuery = "DELETE FROM ProducoesP WHERE ProducaoID = @ProducaoID";

                        using (SqlCommand commandDelete = new SqlCommand(deleteQuery, connection))
                        {
                            commandDelete.Parameters.Add("@ProducaoID", SqlDbType.Int).Value = codigoProducao;
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
                    string query = "UPDATE ProducoesP SET NomeProduto = @NomeProduto, DataPlantio = @DataPlantio, DataColheita = @DataColheita " +
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
            DialogResult result = MessageBox.Show("Tem certeza que deseja sair do programa?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close(); // Fecha o formulário atual
                Application.Exit(); // Fecha o programa
            }
        }

        private void textBoxDataColheita_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string dataColheita = textBoxDataColheita.Text.Trim();

                if (!string.IsNullOrEmpty(dataColheita))
                {
                    // Valida se a data está no formato dd/MM/yyyy
                    if (DateTime.TryParseExact(dataColheita, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime dataValida))
                    {
                        // Converte para o formato yyyy-MM-dd para enviar ao banco
                        PesquisarProducao("DataColheita", dataValida.ToString("yyyy-MM-dd"));
                    }
                    else
                    {
                        MessageBox.Show("Por favor, insira uma data válida no formato DD/MM/AAAA.", "Entrada Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }

        private void textBoxQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string quantidade = textBoxQuantidade.Text.Trim();
                if (!string.IsNullOrEmpty(quantidade) && int.TryParse(quantidade, out _))
                {
                    PesquisarProducao("QuantidadePlantada", quantidade);
                    e.SuppressKeyPress = true;
                }
                else
                {
                    MessageBox.Show("Quantidade inválida.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        
    
        }

        private void labelGestao_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}



