using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;

namespace Database
{
    public class Db
    {
        private SQLiteConnection conn;
        private SQLiteCommand comm;

        /// <summary>
        /// Construtor
        /// </summary>
        public Db()
        {
            conn = new SQLiteConnection("Data Source=banco.db;");
        }

        protected void AbrirConexão()
        {
            conn.Open();

            comm = new SQLiteCommand(conn);
        }

        protected void FecharConexao()
        {

        }

        public void ExecuteNonQuery(string sql)
        {
            comm.CommandText = sql;
            comm.ExecuteNonQuery();
        }
    }
}
