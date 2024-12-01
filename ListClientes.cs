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
    public partial class ListClientes : Form
    {
        private readonly string connectionString = "Server=JANUARY\\SQLDEVELOPER;Database=SistemasFazenda;Trusted_Connection=True;";
        private DataGridView dataGridViewClientes;


        public ListClientes()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadClientes();

            // Associa os eventos KeyDown aos campos de texto
            textBoxCpfCnpj.KeyDown += textBoxCpfCnpj_KeyDown;
            textBoxNomeCliente.KeyDown += textBoxNomeCliente_KeyDown;
            textBoxDataRegistro.KeyDown += textBoxDataRegistro_KeyDown;
            textBoxEmail.KeyDown += textBoxEmail_KeyDown;   
            textBoxEndereco.KeyDown += textBoxEndereco_KeyDown;
            textBoxCodigo.KeyDown += textBoxCodigo_KeyDown;
        }

        private void InitializeDataGridView()
        {
            dataGridViewClientes = new DataGridView
            {
                Location = new System.Drawing.Point(50, 230),
                Size = new System.Drawing.Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            this.Controls.Add(dataGridViewClientes); // Adiciona o DataGridView ao formulário
        }

        private void LoadClientes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Busca os dados dos clientes ativo na tabela BDusuario
                    string query = @"SELECT UsuarioID AS [Código], Nome AS [Nome do Cliente], CPF_CNPJ AS [CPF/CNPJ], 
                                      DataCadastro AS [Data de Cadastro], Email AS [E-mail], Endereco AS [Endereço]
                                      FROM BDusuario WHERE Ativo = 1";


                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridViewClientes.DataSource = dataTable; // Define o DataTable como fonte de dados
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente cadastrado ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewClientes.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar clientes: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void buttonVendas_Click(object sender, EventArgs e)
        {
            Vendas vendas = new Vendas();
            vendas.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void buttonGestão_Click(object sender, EventArgs e)
        {
            Gestao gestao = new Gestao();
            gestao.ShowDialog();
        }

        private void textBoxCpfCnpj_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string cpfCnpj = textBoxCpfCnpj.Text.Trim();

                if (!string.IsNullOrEmpty(cpfCnpj))
                {
                    PesquisarCliente("CPF_CNPJ", cpfCnpj);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }

        private void textBoxNomeCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string nomeCliente = textBoxNomeCliente.Text.Trim();

                if (!string.IsNullOrEmpty(nomeCliente))
                {
                    PesquisarCliente("Nome", nomeCliente);
                    e.SuppressKeyPress = true;
                }
            }
        }

        private void textBoxDataRegistro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string dataCadastro = textBoxDataRegistro.Text.Trim();

                if (DateTime.TryParse(dataCadastro, out DateTime data))
                {
                    // Formate a data para yyyy-MM-dd
                    string dataFormatada = data.ToString("yyyy-MM-dd");

                    PesquisarCliente("DataCadastro", dataFormatada);  // Chama a função de pesquisa com a data formatada
                    e.SuppressKeyPress = true;  // Impede o som do "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira uma data válida no formato yyyy-MM-dd.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void PesquisarCliente(string coluna, string valor)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Query base para pesquisa
                    string query = $@"
                SELECT UsuarioID AS [Código], Nome AS [Nome do Cliente], CPF_CNPJ AS [CPF/CNPJ], 
                       DataCadastro AS [Data de Cadastro], Email AS [E-mail], Endereco AS [Endereço]
                FROM BDusuario WHERE {coluna} LIKE @valor AND Ativo = 1";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Adiciona o valor ao parâmetro de pesquisa
                        command.Parameters.AddWithValue("@valor", coluna == "DataCadastro" ? valor : "%" + valor + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Verifica se há resultados
                        if (dataTable.Rows.Count > 0)
                        {
                            dataGridViewClientes.DataSource = dataTable; // Exibe os dados no DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Nenhum cliente encontrado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridViewClientes.DataSource = null;
                        }
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Erro ao acessar o banco de dados: " + sqlEx.Message, "Erro de SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao pesquisar cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxDelClient_Click(object sender, EventArgs e)
        {
            
            if (dataGridViewClientes.SelectedRows.Count > 0)
            {
                // Obtém o valor do CPF/CNPJ da célula selecionada
                var cpfCnpjCellValue = dataGridViewClientes.SelectedRows[0].Cells["CPF/CNPJ"].Value;
                string cpfCnpj = cpfCnpjCellValue?.ToString();

                // Verifica se o CPF/CNPJ está vazio
                bool isCpfCnpjEmpty = string.IsNullOrEmpty(cpfCnpj);

                // Mensagem de confirmação personalizada
                string mensagemConfirmacao = isCpfCnpjEmpty
                    ? "Este cliente não possui CPF/CNPJ registrado. Deseja excluir mesmo assim?"
                    : "Tem certeza que deseja excluir este cliente?";

                DialogResult result = MessageBox.Show(mensagemConfirmacao, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(connectionString))
                        {
                            connection.Open();

                            // Define a query baseada na existência ou não de CPF/CNPJ
                            string query = isCpfCnpjEmpty
                                ? "DELETE FROM BDusuario WHERE CPF_CNPJ IS NULL OR CPF_CNPJ = ''"
                                : "DELETE FROM BDusuario WHERE CPF_CNPJ = @CpfCnpj";

                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                if (!isCpfCnpjEmpty)
                                {
                                    command.Parameters.AddWithValue("@CpfCnpj", cpfCnpj);
                                }

                                int rowsAffected = command.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    MessageBox.Show("Cliente excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    LoadClientes(); // Atualiza a lista de clientes
                                }
                                else
                                {
                                    MessageBox.Show("Erro ao excluir o cliente. Tente novamente.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um cliente para excluir.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBoxEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string email = textBoxEmail.Text.Trim();

                if (!string.IsNullOrEmpty(email))
                {
                    PesquisarCliente("Email", email);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxNomeCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Verifica se a tecla pressionada é Enter
            {
                string codigo = textBoxCodigo.Text.Trim(); // Obtém o valor do TextBox para o ID do usuário

                if (!string.IsNullOrEmpty(codigo) && int.TryParse(codigo, out int usuarioID)) // Valida que o código não está vazio e é numérico
                {
                    PesquisarCliente("UsuarioID", usuarioID.ToString()); // Converte o ID para string antes de passar para o método
                    e.SuppressKeyPress = true; // Impede o som do "bip"
                }
                else
                {
                    MessageBox.Show("Por favor, insira um código válido.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBoxEndereco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string endereco = textBoxEndereco.Text.Trim();

                if (!string.IsNullOrEmpty(endereco))
                {
                    PesquisarCliente("Endereco", endereco);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }
        }
    }
}
