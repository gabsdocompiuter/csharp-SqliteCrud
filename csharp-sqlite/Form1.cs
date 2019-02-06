using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database.Model;
using Database.DAL;

namespace csharp_sqlite
{
    public partial class Form1 : Form
    {
        private CrudCliente crud;

        private Cliente cliente = null;

        public Form1()
        {
            InitializeComponent();

            crud = new CrudCliente();
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            cliente = crud.GetCliente(int.Parse(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()));

            tbId.Text = cliente.Id.ToString();
            tbNome.Text = cliente.Nome;
            tbEmail.Text = cliente.Email;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            Cliente cl = new Cliente()
            {
                Nome = tbNome.Text,
                Email = tbEmail.Text
            };
            
            crud.Insert(cl);
            PreparaTela();
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (cliente != null)
            {
                cliente.Nome = tbNome.Text;
                cliente.Email = tbEmail.Text;

                crud.Update(cliente);
                PreparaTela();
            }
        }

        private void BtnExcluir_Click(object sender, EventArgs e)
        {
            if ((cliente != null) && (MessageBox.Show("Excluir registro?", "Excluir?", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) == DialogResult.Yes))
            {
                crud.Delete(cliente);
                PreparaTela();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AtualizaDataGrid();
        }

        private void PreparaTela()
        {
            cliente = null;

            AtualizaDataGrid();
            LimpaTela();
        }

        private void AtualizaDataGrid()
        {
            dataGridView1.DataSource = crud.GetDataTable();
        }

        private void LimpaTela()
        {
            tbId.Text = string.Empty;
            tbNome.Text = string.Empty;
            tbEmail.Text = string.Empty;
        }
    }
}
