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
    public partial class Vendas : Form
    {

        // String de conexão para autenticação pelo Windows
        string connectionString = "Server=JANUARY\\SQLDEVELOPER;Database=SistemasFazenda;Trusted_Connection=True;";


        private DataGridView dataGridViewPedidos;

        public Vendas()
        {
            InitializeComponent();
            InitializeDataGridView();
            LoadPedidos();

            textBoxCpfCnpj.KeyDown += textBoxCpfCnpj_KeyDown;
            textBoxProduto.KeyDown += textBoxProduto_KeyDown;
            textBoxDataCompra.KeyDown += textBoxDataCompra_KeyDown;
            textBoxValor.KeyDown += textBoxValor_KeyDown;
        }

        private void InitializeDataGridView()
        {
            dataGridViewPedidos = new DataGridView
            {
                Location = new System.Drawing.Point(50, 230),
                Size = new System.Drawing.Size(700, 200),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };

            this.Controls.Add(dataGridViewPedidos); // Adiciona o DataGridView ao formulário
        }

        private void dataGridViewPedidos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridViewPedidos.Columns["StatusColumn"].Index)
            {
                try
                {
                    int pedidoId = Convert.ToInt32(dataGridViewPedidos.Rows[e.RowIndex].Cells["ID do Pedido"].Value);
                    int statusId = Convert.ToInt32(dataGridViewPedidos.Rows[e.RowIndex].Cells["StatusColumn"].Value);

                    // Atualizar o Status no banco de dados
                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        string updateQuery = "UPDATE BDPedidos SET StatusID = @StatusID WHERE PedidoID = @PedidoID";
                        SqlCommand cmd = new SqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@StatusID", statusId);
                        cmd.Parameters.AddWithValue("@PedidoID", pedidoId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Status do pedido atualizado com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao atualizar status: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void LoadPedidos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Consulta SQL para carregar os pedidos
                    string query = @"
                SELECT 
                    BDPedidos.PedidoID AS [ID do Pedido],
                    BDusuario.Nome AS [Cliente],
                    Produtos.NomeProduto AS [Produto],
                    BDItensPedido.Quantidade AS [Quantidade],
                    BDItensPedido.Preco AS [Preço Unitário],
                    BDPedidos.DataPedido AS [Data da Compra],
                    StatusPedidos.StatusID AS [StatusID], -- Alterado para carregar o StatusID
                    StatusPedidos.StatusDescricao AS [Status]
                FROM BDPedidos
                INNER JOIN BDusuario ON BDPedidos.UsuarioID = BDusuario.UsuarioID
                INNER JOIN BDItensPedido ON BDPedidos.PedidoID = BDItensPedido.PedidoID
                INNER JOIN Produtos ON BDItensPedido.ProdutoID = Produtos.ProdutoID
                INNER JOIN StatusPedidos ON BDPedidos.StatusID = StatusPedidos.StatusID
                ORDER BY BDPedidos.DataPedido DESC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // Vincula os dados no DataGridView
                        dataGridViewPedidos.DataSource = dataTable;

                        // Adiciona o ComboBox para editar o Status
                        AddStatusComboBoxColumn();
                    }
                    else
                    {
                        MessageBox.Show("Nenhum pedido encontrado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dataGridViewPedidos.DataSource = null;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar pedidos: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void AddStatusComboBoxColumn()
        {
            // Criar o ComboBoxColumn
            DataGridViewComboBoxColumn statusColumn = new DataGridViewComboBoxColumn();
            statusColumn.HeaderText = "Status";
            statusColumn.Name = "StatusColumn";
            statusColumn.DataPropertyName = "StatusID"; // Usamos o StatusID para vincular com a tabela
            statusColumn.DisplayMember = "StatusDescricao"; // O texto que será exibido
            statusColumn.ValueMember = "StatusID"; // O valor que será enviado para o banco
            statusColumn.Width = 120;

            // Preencher o ComboBox com os Status disponíveis
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT StatusID, StatusDescricao FROM StatusPedidos";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable statusDataTable = new DataTable();
                    adapter.Fill(statusDataTable);

                    statusColumn.DataSource = statusDataTable;
                    dataGridViewPedidos.Columns.Add(statusColumn);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar status: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void PesquisarPedido(string coluna, string valor)
        {

            dataGridViewPedidos.DataSource = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Consulta SQL dinâmica para pesquisa
                    string query = $@"
                SELECT 
                BDPedidos.PedidoID AS [ID do Pedido],
                BDusuario.Nome AS [Cliente],
                Produtos.NomeProduto AS [Produto],
                BDItensPedido.Quantidade AS [Quantidade],
                BDItensPedido.Preco AS [Preço Unitário],
                BDPedidos.DataPedido AS [Data da Compra],
                StatusPedidos.StatusID AS [StatusID],
                StatusPedidos.StatusDescricao AS [Status],
                BDPedidos.ValorTotal AS [Valor Total]
            FROM BDPedidos
            INNER JOIN BDusuario ON BDPedidos.UsuarioID = BDusuario.UsuarioID
            INNER JOIN BDItensPedido ON BDPedidos.PedidoID = BDItensPedido.PedidoID
            INNER JOIN Produtos ON BDItensPedido.ProdutoID = Produtos.ProdutoID
            INNER JOIN StatusPedidos ON BDPedidos.StatusID = StatusPedidos.StatusID
            WHERE {coluna} LIKE @Valor
            ORDER BY BDPedidos.DataPedido DESC";

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Valor", valor);

                    try
                    {
                        connection.Open();
                        SqlDataReader reader = command.ExecuteReader();
                        // Processar os resultados da pesquisa aqui
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Erro ao realizar a pesquisa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex) // Adicionei o catch para o bloco externo
                {
                    MessageBox.Show($"Erro ao tentar abrir a conexão: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
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
                    PesquisarPedido("BDusuario.CPF_CNPJ", cpfCnpj);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
            }

        }

        private void textBoxProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // A pesquisa é feita ao pressionar Enter
            {
                PesquisarPedido("Produtos.NomeProduto", textBoxProduto.Text);
                e.SuppressKeyPress = true; // Impede o "bip" do Enter
            }
        }

        private void textBoxDataCompra_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // A pesquisa ocorre ao pressionar Enter
            {
                string dataCompra = textBoxDataCompra.Text.Trim();

                if (DateTime.TryParseExact(dataCompra, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data))
                {
                    PesquisarPedido("BDPedidos.DataPedido", data.ToString("yyyy-MM-dd"));
                    e.SuppressKeyPress = true;
                }
                else
                {
                    MessageBox.Show("Por favor, insira uma data válida no formato DD/MM/AAAA.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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

        private void buttonVendas_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBoxValor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Pesquisa apenas ao pressionar Enter
            {
                string valor = textBoxValor.Text.Trim();
                if (decimal.TryParse(valor, out decimal valorDecimal) && valorDecimal > 0)
                {
                    PesquisarPedido("BDPedidos.ValorTotal", valorDecimal.ToString("F2"));
                    e.SuppressKeyPress = true; // Impede o "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira um valor válido maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void pictureBoxDelVendas_Click(object sender, EventArgs e)
        {
            if (dataGridViewPedidos.SelectedRows.Count > 0) // Verifica se alguma linha está selecionada
            {
                // Pega o ID do pedido da linha selecionada
                int pedidoId = Convert.ToInt32(dataGridViewPedidos.SelectedRows[0].Cells["ID do Pedido"].Value);

                // Confirmação de exclusão
                DialogResult result = MessageBox.Show($"Tem certeza que deseja excluir o pedido com ID {pedidoId}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        // Conexão com o banco de dados para excluir o pedido
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();

                            // Comando SQL para excluir o pedido
                            string deleteQuery = "DELETE FROM BDPedidos WHERE PedidoID = @PedidoID";
                            SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                            cmd.Parameters.AddWithValue("@PedidoID", pedidoId);

                            // Executa a exclusão
                            int rowsAffected = cmd.ExecuteNonQuery();

                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Pedido excluído com sucesso.", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                LoadPedidos(); // Recarrega os pedidos no DataGridView
                            }
                            else
                            {
                                MessageBox.Show("Falha ao excluir o pedido.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao excluir pedido: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecione um pedido para excluir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
