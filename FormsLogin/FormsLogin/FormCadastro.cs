﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsLogin
{
    public partial class FormCadastro : Form
    {
        public FormCadastro()
        {
            InitializeComponent();
        }

        private void FormCadastro_Load(object sender, EventArgs e)
        {

        }
        
        private void btBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (validationInserts())
            {
                SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Andrés\Documents\Data.mdf;Integrated Security=True;");
                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Table]" +
                    "([USERNAME],[PASSWORD],[NAME])" +
                    "VALUES('" + txtUsername.Text + "','" + txtPassword.Text + "','" + txtName.Text + "')", con);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New USER was added.");

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro:\n " + ex.Message);
                }
                finally
                {
                    con.Close();
                }

                txtUsername.Text = "";
                txtPassword.Text = "";
                txtName.Text = "";
            }
            else
            {
                MessageBox.Show("Inputs invalid!");
            }
            
        }

        private bool validationInserts()
        {
            Regex ER = new Regex(@"^(?=.*\d).{4,8}$", RegexOptions.None);
            if (!ER.IsMatch(txtPassword.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Password must be between 4 and 8 digits long and include at least one numeric digit.");
                return false;
            }
            throw new NotImplementedException();
        }
    }
}
