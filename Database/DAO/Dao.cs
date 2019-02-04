using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Database.Model;

namespace Database.DAO
{
    public class Dao : Db
    {
        /// <summary>
        /// Método para salvar um registro
        /// </summary>
        /// <param name="mdl"></param>
        public void Salvar(Mdl mdl)
        {
            try
            {
                AbrirConexão();
                ExecuteNonQuery(Insert(mdl));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Houve um erro: \n" + ex);
            }
            finally
            {
                FecharConexao();
            }
        }

        /// <summary>
        /// Monta o sql de insert
        /// </summary>
        /// <param name="mdl">Model</param>
        /// <returns>Retorna sql para adicionar no banco</returns>
        private string Insert(Mdl mdl)
        {
            string sql = $@"INSERT INTO {mdl.GetType().Name} (";

            bool first = true;
            foreach (var campo in mdl.GetType().GetProperties())
            {
                if (!first)
                    sql += ", ";
                else
                    first = false;


                sql += campo.Name;
            }

            sql += $@") VALUES(";

            first = true;
            foreach (var campo in mdl.GetType().GetProperties())
            {
                if (!first)
                    sql += ", ";
                else first = false;

                sql += "'" + campo.GetValue(mdl, null) + "'";
            }

            sql += ");";

            return sql;
        }
    }
}
