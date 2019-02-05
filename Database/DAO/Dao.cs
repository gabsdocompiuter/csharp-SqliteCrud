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
                ExecuteNonQuery(GetSqlInsert(mdl));
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
        /// Método para editar um registro
        /// </summary>
        /// <param name="mdl"></param>
        public void Editar(Mdl mdl)
        {
            try
            {
                AbrirConexão();
                ExecuteNonQuery(GetSqlUpdate(mdl));
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
        /// Método para excluir um registro
        /// </summary>
        /// <param name="mdl"></param>
        public void Excluir(Mdl mdl)
        {
            try
            {
                AbrirConexão();

                string sql = $@"DELETE FROM {mdl.GetType().Name}
                                   WHERE id = {mdl.Id};";

                ExecuteNonQuery(sql);
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

        #region Métodos Privados

        /// <summary>
        /// Monta o sql de insert
        /// </summary>
        /// <param name="mdl">Model</param>
        /// <returns>Retorna sql para adicionar no banco</returns>
        private string GetSqlInsert(Mdl mdl)
        {
            string sql = $@"INSERT INTO {mdl.GetType().Name} (";

            #region CAMPOS
            bool first = true;
            foreach (var campo in mdl.GetType().GetProperties())
            {
                //Verifica se o campo não é do Mdl (o id fica guardado lá)
                if (campo.DeclaringType.Name != "Mdl")
                {
                    sql += first ? "" : ", ";
                    first = false;
                    
                    sql += campo.Name;
                }
            }
            #endregion

            sql += $@") VALUES(";

            #region VALUES

            first = true;
            foreach (var campo in mdl.GetType().GetProperties())
            {
                //Verifica se o campo não é do Mdl (o id fica guardado lá)
                if (campo.DeclaringType.Name != "Mdl")
                {
                    sql += first ? "" : ", ";
                    first = false;

                    sql += "'" + campo.GetValue(mdl, null) + "'";
                }
            }

            #endregion

            sql += ");";

            return sql;
        }

        /// <summary>
        /// Monta o sql de update
        /// </summary>
        /// <param name="mdl">Model</param>
        /// <returns>Retorna sql para editar no banco</returns>
        private string GetSqlUpdate(Mdl mdl)
        {
            string sql = $"UPDATE {mdl.GetType().Name} SET ";
            
            bool first = true;
            foreach (var campo in mdl.GetType().GetProperties())
            {
                //Verifica se o campo não é do Mdl (o id fica guardado lá)
                if (campo.DeclaringType.Name != "Mdl")
                {
                    sql += first ? "" : "AND ";
                    first = false;

                    sql += campo.Name;
                    sql += " = '" + campo.GetValue(mdl, null) + "'";
                }
            }

            sql += $" WHERE id = {mdl.Id};";

            return sql;
        }

        #endregion
    }
}