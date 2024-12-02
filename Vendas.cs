using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

            textBoxCodigo.KeyDown += textBoxCodigo_KeyDown;
            textBoxProduto.KeyDown += textBoxProduto_KeyDown;
            textBoxDataCompra.KeyDown += textBoxDataCompra_KeyDown;
            textBoxValor.KeyDown += textBoxValor_KeyDown;
            textBoxQuantidade.KeyDown += textBoxQuantidade_KeyDown;
            textBoxCodigo.KeyDown += textBoxCodigo_KeyDown;
            textBoxCodigoCliente.KeyDown += textBoxCodigoCliente_KeyDown;
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

        private void LoadPedidos()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    // Consulta SQL para carregar os pedidos com as colunas necessárias
                    string query = @"
                    SELECT 
                        BDPedidos.PedidoID AS [ID do Pedido],  
                        BDusuario.UsuarioID AS [Cliente],  -- Alterado para UsuarioID
                        Produtos.NomeProduto AS [Produto],     
                        BDItensPedido.Quantidade AS [Quantidade], 
                        BDPedidos.ValorTotal AS [Valor Total], 
                        BDPedidos.DataPedido AS [Data da Compra]
                    FROM BDPedidos
                    INNER JOIN BDusuario ON BDPedidos.UsuarioID = BDusuario.UsuarioID
                    INNER JOIN BDItensPedido ON BDPedidos.PedidoID = BDItensPedido.PedidoID
                    INNER JOIN Produtos ON BDItensPedido.ProdutoID = Produtos.ProdutoID
                    ORDER BY BDPedidos.DataPedido DESC";


                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        // Vincula os dados no DataGridView
                        dataGridViewPedidos.DataSource = dataTable;

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
                        BDusuario.UsuarioID AS [Cliente],  -- Alterado para UsuarioID
                        Produtos.NomeProduto AS [Produto],   
                        BDItensPedido.Quantidade AS [Quantidade], 
                        BDPedidos.ValorTotal AS [Valor Total], 
                        BDPedidos.DataPedido AS [Data da Compra]
                    FROM BDPedidos
                    INNER JOIN BDusuario ON BDPedidos.UsuarioID = BDusuario.UsuarioID
                    INNER JOIN BDItensPedido ON BDPedidos.PedidoID = BDItensPedido.PedidoID
                    INNER JOIN Produtos ON BDItensPedido.ProdutoID = Produtos.ProdutoID
                    WHERE {coluna} LIKE @Valor
                    ORDER BY BDPedidos.DataPedido DESC";


                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@Valor", "%" + valor + "%");

                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridViewPedidos.DataSource = dataTable;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao realizar a pesquisa: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonGestão_Click(object sender, EventArgs e)
        {
            Gestao gestao = new Gestao();
            gestao.ShowDialog();
        }




        private void textBoxCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Quando a tecla Enter for pressionada
            {
                string pedidoId = textBoxCodigo.Text.Trim(); // Obtém o texto digitado no campo textBoxCodigo

                if (!string.IsNullOrEmpty(pedidoId)) // Verifica se o campo não está vazio
                {
                    // Chama o método de pesquisa passando a coluna "PedidoID" e o valor inserido
                    PesquisarPedido("BDPedidos.PedidoID", pedidoId);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira um ID de pedido para realizar a pesquisa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBoxProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // A pesquisa ocorre ao pressionar Enter
            {
                string produto = textBoxProduto.Text.Trim();

                // Verifica se o campo de pesquisa não está vazio
                if (!string.IsNullOrEmpty(produto))
                {
                    PesquisarPedido("Produtos.NomeProduto", produto);
                    e.SuppressKeyPress = true; // Impede o "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira o nome do produto para realizar a pesquisa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void textBoxDataCompra_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // A pesquisa ocorre ao pressionar Enter
            {
                string dataCompra = textBoxDataCompra.Text.Trim();

                if (DateTime.TryParseExact(dataCompra, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out DateTime data))
                {
                    PesquisarPedido("BDPedidos.DataPedido", data.ToString("yyyy-MM-dd")); // Verifique o formato da data aqui
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
            if (e.KeyCode == Keys.Enter) // Pesquisa ao pressionar Enter
            {
                string valor = textBoxValor.Text.Trim(); // Obtém o valor digitado no campo

                // Verifica se o valor é um número válido e maior que zero
                if (decimal.TryParse(valor, out decimal valorDecimal) && valorDecimal > 0)
                {
                    // Formata o valor como uma string com 2 casas decimais
                    string valorFormatado = valorDecimal.ToString("F2");

                    // Chama a função PesquisarPedido usando LIKE no SQL para permitir pesquisa parcial
                    PesquisarPedido("BDPedidos.ValorTotal", "%" + valorFormatado + "%");
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

        private void labelGestao_Click(object sender, EventArgs e)
        {

        }

        private void textBoxQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // Pesquisa apenas ao pressionar Enter
            {
                string quantidade = textBoxQuantidade.Text.Trim();
                if (int.TryParse(quantidade, out int quantidadeInt) && quantidadeInt > 0)
                {
                    PesquisarPedido("BDItensPedido.Quantidade", quantidadeInt.ToString());
                    e.SuppressKeyPress = true; // Impede o "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira uma quantidade válida maior que zero.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBoxCodigoCliente_KeyDown(object sender, KeyEventArgs e)
        {
            string UsuarioId = textBoxCodigoCliente.Text.Trim();

            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(UsuarioId)) // Verifica se o campo não está vazio
                {
                    PesquisarPedido("BDusuario.UsuarioID", UsuarioId);
                    e.SuppressKeyPress = true; // Impede o som do "bip" ao pressionar Enter
                }
                else
                {
                    MessageBox.Show("Por favor, insira um ID de pedido para realizar a pesquisa.", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
    }
}
