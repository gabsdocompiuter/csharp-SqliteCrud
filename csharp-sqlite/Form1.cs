using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database.Model;
using Database.DAO;

namespace csharp_sqlite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cliente cl = new Cliente()
            {
                Nome = tbNome.Text,
                Email = tbEmail.Text
            };

            Dao d = new Dao();
            d.Salvar(cl);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
