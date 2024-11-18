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
        private readonly string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemaFazenda;Trusted_Connection=True;";


        public ListClientes()
        {
            InitializeComponent();
            LoadClientes();

        }

        private void LoadClientes()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    // Busca os dados do primeiro cliente ativo na tabela BDusuario
                    string query = "SELECT Nome, CPF_CNPJ, DataCadastro FROM BDusuario WHERE Ativo = 1";

                    SqlCommand command = new SqlCommand(query, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        textBoxNomeCliente.Text = reader["Nome"] != DBNull.Value ? reader["Nome"].ToString() : "Sem Nome";
                        textBoxCpfCnpj.Text = reader["CPF_CNPJ"] != DBNull.Value ? reader["CPF_CNPJ"].ToString() : "Sem CPF/CNPJ";
                        textBoxDataRegistro.Text = reader["DataCadastro"] != DBNull.Value
                            ? Convert.ToDateTime(reader["DataCadastro"]).ToString("dd/MM/yyyy")
                            : "Sem Data";
                    }
                    else
                    {
                        MessageBox.Show("Nenhum cliente cadastrado ainda.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    reader.Close();
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





        private void textBoxCpfCnpj_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxNomeCliente_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBoxDataRegistro_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBoxDelClient_Click(object sender, EventArgs e)
        {

        }
    }
}
