using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Oracle.DataAccess.Client;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace appColegio.accesoDatos
{
    class Datos
    {
        string cadenaConexion = @"DATA SOURCE=CLON:1521/XE;USER ID=DB1; PASSWORD = oracle";
        // metodo para ejecutar DML(insert, update, delete)
        public int ejecutarDML(string consulta)
        {
            int filasAfectadas;
            //paso1: creo una conexion
            OracleConnection miConexion = new OracleConnection(cadenaConexion);
            //paso2: creo un comando (comando es un objeto que permite recibir la consulta y recibe un objeto oracle conecction)
            OracleCommand comando = new OracleCommand(consulta, miConexion);
            //paso3: abrir conexion
            miConexion.Open();
            //paso4: ejecuto el comando. Este devuelve el numero de filas que se afectaron.(ya sea con el insert, update, delete que viene en la consulta)
            filasAfectadas = comando.ExecuteNonQuery();
            return filasAfectadas;
        }

        //metodo para ejecutar sentencias select
        public DataSet ejecutarSelect(string consulta)
        {
            //paso1: creo un data set vacio
            DataSet miDataSet = new DataSet();
            //paso2: creo un adaptador
            OracleDataAdapter miAdaptador = new OracleDataAdapter(consulta, cadenaConexion);
            //paso3: lleno el data set a traves del adaptador( el adaptador ejecuta la sentencia select)
            miAdaptador.Fill(miDataSet, "ResultadoDatos");
            return miDataSet;
        }
    }
}