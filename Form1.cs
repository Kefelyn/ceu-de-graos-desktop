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

        // Login fixo do administrador
        private string emailAdmin = "admin@desktop.com";  // Email do admin
        private string senhaAdmin = "admin123";         // Senha do admin

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
            string email = textBoxUsuario.Text;
            string senha = textBoxsenha.Text;

            // Verifica se o login é para o admin fixo
            if (VerificarLoginAdmin(email, senha))
            {
                Gestao gestao = new Gestao();  // Acesso à área de gestão
                gestao.ShowDialog();
                this.Close(); // Fecha a tela de login se o login for bem-sucedido
            }
            else
            {
                MessageBox.Show("Usuário ou senha incorretos, Tente novamente!", "Erro de Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        // Método para verificar se é o admin fixo
        public bool VerificarLoginAdmin(string email, string senha)
        {
            return email == emailAdmin && senha == senhaAdmin;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

