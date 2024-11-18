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
        string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Integrated Security=True;";



        public Vendas()
        {
            InitializeComponent();
        }

        private void buttonGestão_Click(object sender, EventArgs e)
        {
            Gestao gestao = new Gestao();
            gestao.ShowDialog();
        }




        private void textBoxcliente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT TOP 1 
                            u.Nome AS Cliente
                        FROM BDPedidos p
                        INNER JOIN BDusuario u ON p.UsuarioID = u.UsuarioID
                        ORDER BY p.DataPedido DESC";

                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxcliente.Text = reader["Cliente"].ToString();
                    }
                    else
                    {
                        textBoxcliente.Text = "Nenhum cliente encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar cliente: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void textBoxProduto_TextChanged(object sender, EventArgs e)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT TOP 1 
                            pr.NomeProduto AS Produto
                        FROM BDPedidos p
                        INNER JOIN BDItensPedido ip ON p.PedidoID = ip.PedidoID
                        INNER JOIN Produtos pr ON ip.ProdutoID = pr.ProdutoID
                        ORDER BY p.DataPedido DESC";

                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxProduto.Text = reader["Produto"].ToString();
                    }
                    else
                    {
                        textBoxProduto.Text = "Nenhum produto encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar produto: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    

    private void textBoxDataCompra_TextChanged(object sender, EventArgs e)
    {

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT TOP 1 
                            p.DataPedido AS DataCompra
                        FROM BDPedidos p
                        ORDER BY p.DataPedido DESC";

                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxDataCompra.Text = Convert.ToDateTime(reader["DataCompra"]).ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        textBoxDataCompra.Text = "Nenhuma data encontrada.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar data da compra: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

    

    private void textBoxStatus_TextChanged(object sender, EventArgs e)
    {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = @"
                        SELECT TOP 1 
                            s.StatusDescricao AS Status
                        FROM BDPedidos p
                        INNER JOIN StatusPedidos s ON p.StatusID = s.StatusID
                        ORDER BY p.DataPedido DESC";

                    SqlCommand command = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxStatus.Text = reader["Status"].ToString();
                    }
                    else
                    {
                        textBoxStatus.Text = "Nenhum status encontrado.";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar status: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
    }

    

    private void buttonSair_Click(object sender, EventArgs e)
        {

        }

        private void buttonVendas_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
