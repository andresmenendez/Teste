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

namespace FormsLogin
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btClean_Click(object sender, EventArgs e)
        {
            txtPassword.Text = "";
            txtUser.Text = "";
        }

        private void lnkCadastro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCadastro formCadastro = new FormCadastro();
            formCadastro.Show();
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andrés\Documents\Data.mdf;Integrated Security=True;");
            SqlCommand cmd = new SqlCommand("Select * From [Table] where USERNAME = '" +
                txtUser.Text + "' and PASSWORD='" + txtPassword.Text + "'", con);

            try //Tenta executar o que estiver abaixo
            {
                con.Open(); // abre a conexão com o banco   
                cmd.ExecuteNonQuery(); // executa cmd
                                       /*Pronto após o cmd.ExecuteNonQuery(); selecionamos tudo o que tinha dentro do banco, agora os passos seguintes irão exibir as informações para que o usuário possa vê-las    */
                SqlDataAdapter da = new SqlDataAdapter(); /* da, adapta o banco de dados ao nosso projeto */
                DataSet ds = new DataSet();
                da.SelectCommand = cmd; // adapta cmd ao projeto
                da.Fill(ds); // preenche todas as informações dentro do DataSet  
                try
                {
                    DataRow dr = ds.Tables[0].Rows[0];
                    string nameLogin = dr["Name"].ToString();
                    MessageBox.Show("WELCOME " + nameLogin);
                }
                catch (Exception)
                {
                    MessageBox.Show("Sorry, Incorrect USERNAME or PASSWORD. Please, try it again.");
                }
                   
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro " + ex.Message); /*Se ocorer algum erro será informado em um msgbox*/
                
            }

            finally
            {
                con.Close(); /* Se tudo ocorrer bem fecha a conexão com o banco da dados, sempre é bom fechar a conexão após executar até o final o que nos interessa, isso pode evitar problemas futuros */
            }
        }
    }
}
