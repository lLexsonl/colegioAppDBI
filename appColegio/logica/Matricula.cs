using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appColegio.accesoDatos;

namespace appColegio.logica
{
    class Matricula
    {
        Datos accesoDato = new Datos();

        public int registrarMatricula(int codEst, int codGrado, string fechaM, int anioLectivo)
        {
            string consulta = "insert into Matricula(estcodigo, gradocodigo, fechamatricula, aniolectivo) values (" +
                codEst + "," + codGrado + ", '" + fechaM + "'," + anioLectivo + ")";
            return accesoDato.ejecutarDML(consulta);
        }

        public DataSet consultarTodaInfo()
        {
            string consulta = "select e.estcodigo, e.estnombre, e.estapellido, e.estfnacimiento, g.gradocodigo, g.gradonombre, g.gradojornada, g.colnit, m.fechamatricula, m.aniolectivo " +
                "from estudiante e inner join matricula m on e.estcodigo = m.estcodigo " +
                "inner join gradoescolar g on g.gradocodigo = m.gradocodigo";
            return accesoDato.ejecutarSelect(consulta);
        }

        public DataSet consultarXFecha(string fechaMatricula)
        {
            string consulta = "select c.colnombre, g.gradonombre, g.gradojornada, e.estcodigo, e.estnombre, e.estapellido "+
                "from colegio c inner join gradoescolar g on c.colnit = g.colnit " +
                "inner join matricula m on g.gradocodigo = m.gradocodigo " +
                "inner join estudiante e on e.estcodigo = m.estcodigo " +
                "where m.fechamatricula = '" + fechaMatricula + "'";
            return accesoDato.ejecutarSelect(consulta);
        }

        public DataSet consultarXanioYjornada()
        {
            string consulta = "select count(estcodigo) " +
                "from(select estcodigo, gradocodigo from matricula where aniolectivo = 2019) m inner join(select gradocodigo from gradoescolar where gradojornada = 'mañana') g on g.gradocodigo = m.gradocodigo";
            return accesoDato.ejecutarSelect(consulta);
        }
    }
}
