using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Database
{
    public class CriaTabelas
    {
        private SQLiteCommand comm;

        public CriaTabelas(SQLiteCommand cmm)
        {
            comm = cmm;

            string campos = MontaCampos(new DaoConfig());
        }

        private string MontaCampos(Object o)
        {
            //Quantidade da campos na tabela
            int qtdCampos = o.GetType().GetProperties().Length;

            string sqlFields = string.Empty;
            
            bool primeiro = true;
            foreach (var campo in o.GetType().GetProperties())
            {
                if (!primeiro)
                {
                    sqlFields += ", ";
                }
                else primeiro = false;

                sqlFields += campo.Name;

                switch (campo.PropertyType.Name)
                {
                    case "String":
                        sqlFields += " TEXT ";
                        break;

                    default:
                        break;
                }

                if (campo.Name == "id") sqlFields += "AUTOINCREMENT";
            }
            return sqlFields;
        }



    }
}
