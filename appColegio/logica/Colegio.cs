using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appColegio.accesoDatos;

namespace appColegio.logica
{
    class Colegio
    {
        Datos accesoDato = new Datos();

        public int registrarColegio(int nitCol, string nombreCol, string tipoCol)
        {
            //Formato: insert into colegio(colnit, colnombre, coltipo) values(100, 'Normal Superior', 'público');
            string consulta = "insert into colegio (colnit, colnombre, coltipo) values (" + nitCol + ",'" + nombreCol + "','" + tipoCol + "')";
            return accesoDato.ejecutarDML(consulta);
        }

        public DataSet buscarColegio(int nitcol)
        {
            string consulta = "select colnit, colnombre, coltipo from colegio where colnit = " + nitcol;
            return accesoDato.ejecutarSelect(consulta);
        }

        public int actualizarColegio(int nitCol, string nombreCol, string tipoCol)
        {
            //Formato: update colegio set colnombre = 'John F Kennedy' where colnit = 200;
            string consulta = "update colegio set colnit = "+ nitCol + ", colnombre = '" + nombreCol +
                "', coltipo = '" + tipoCol + "' where colnit = " + nitCol;
            return accesoDato.ejecutarDML(consulta);
        }

        public int eliminarColegio(int nitCol)
        {
            string consulta = "delete from colegio where colnit = " + nitCol;
            return accesoDato.ejecutarDML(consulta);
        }
    }
}
