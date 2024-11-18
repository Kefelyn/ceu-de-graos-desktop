using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TelasDesktopPIM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            // Define o caractere de senha como asterisco
            textBoxsenha.PasswordChar = '*';

        }


        // String de conexão com o banco de dados
        private string connectionString = "Server=DESKTOP-AGU3OAL;Database=SistemasFazenda;Integrated Security=True;";





        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBoxUsuario_TextChanged(object sender, EventArgs e)
        {
            // Habilita o botão "Entrar" se ambos os campos tiverem texto
            entrar.Enabled = !string.IsNullOrWhiteSpace(textBoxUsuario.Text) && !string.IsNullOrWhiteSpace(textBoxsenha.Text);


        }

        private void textBoxsenha_TextChanged(object sender, EventArgs e)
        {
            // Habilita o botão "Entrar" se ambos os campos tiverem texto
            entrar.Enabled = !string.IsNullOrWhiteSpace(textBoxUsuario.Text) && !string.IsNullOrWhiteSpace(textBoxsenha.Text);


        }

        private void entrar_Click(object sender, EventArgs e)
        {
            string usuario = textBoxUsuario.Text;
            string senha = textBoxsenha.Text;

            // Verifica se o login é bem-sucedido
            if (VerificarLogin(usuario, senha))
            {
                Gestao gestao = new Gestao();
                gestao.ShowDialog();
                this.Close(); // Fecha a tela de login se o login for bem-sucedido
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos, Tente novamente!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // Método para verificar login no banco de dados
        public bool VerificarLogin(string email, string senha)
        {
            bool loginValido = false;

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT COUNT(1) FROM BDusuario WHERE Email = @Email AND Senha = @Senha AND Ativo = 1";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Define os parâmetros para prevenir SQL Injection
                        cmd.Parameters.AddWithValue("@Email", email);
                        cmd.Parameters.AddWithValue("@Senha", senha);

                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        loginValido = count == 1; // O login é válido se existir um registro ativo com o email e senha fornecidos
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao conectar ao banco de dados: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return loginValido;
        }


    }
}

