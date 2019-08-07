using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appColegio.accesoDatos;

namespace appColegio.logica
{
    class Estudiante
    {
        //Objeto de la clase Datos
        Datos accesoDato = new Datos();
        public int registrarEstudiante(int codEst, string nombreEst, string apellidoEst, string fNacimiento)
        {
            string consulta = "insert into estudiante( estCodigo, estNombre, estApellido, estFNacimiento) values (" + codEst + ", '" + nombreEst + "','" + apellidoEst + "','" + fNacimiento + "')";
            return accesoDato.ejecutarDML(consulta);
        }
        public DataSet buscarEstudiante(int codigo)
        {
            string consulta = "select * from estudiante where estcodigo = " + codigo;
            return accesoDato.ejecutarSelect(consulta);
        }
        public int eliminarEstudiante(int codigo)
        {
            string consulta = "delete from Estudiante where estcodigo = " + codigo;
            return accesoDato.ejecutarDML(consulta);
        }
        public int actualizarEstudiante(int codEst, string nombreEst, string apellidoEst, string fNac)
        {
            string consulta = "update estudiante set estcodigo =" + codEst + ", estnombre = '" + nombreEst + "', estapellido = '" + apellidoEst + "', estfnacimiento = '" + fNac + "' where estcodigo = " + codEst; ;
            return accesoDato.ejecutarDML(consulta);
        }
    }
}
