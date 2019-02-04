using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database;

namespace csharp_sqlite
{
    public partial class Form1 : Form
    {
        private Db db;

        public Form1()
        {
            InitializeComponent();

            db = new Db();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            db.ExecuteNonQuery(richTextBox1.Text);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
