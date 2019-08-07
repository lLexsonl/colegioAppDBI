using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appColegio.accesoDatos;

namespace appColegio.logica
{
    class GradoEscolar
    {
        Datos accesoDato = new Datos();
        Colegio col = new Colegio();

        public DataSet buscarGrado(int codigo)
        {
            string consulta = "select * from gradoescolar where gradocodigo = " + codigo;
            return accesoDato.ejecutarSelect(consulta);
        }

        public int registrarGrado(int codigo, string nombre, string jornada, int colnit)
        {
            string consulta = "insert into gradoescolar(gradocodigo, gradonombre, gradojornada, colnit) values (" +
                codigo + ", '" + nombre + "', '" + jornada + "', " + colnit + ")";
           return accesoDato.ejecutarDML(consulta);
        }
    }
}
