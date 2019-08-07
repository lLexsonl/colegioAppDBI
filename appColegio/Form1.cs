using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using appColegio.logica;

namespace appColegio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        //Crear Objeto Colegio
        Colegio col = new Colegio();
        //Crear Ojeto estudiante
        Estudiante est = new Estudiante();
        //Crear Objeto Grado Escolar
        GradoEscolar grado = new GradoEscolar();
        //Crear objeto Matricula
        Matricula matricula = new Matricula();

        #region Registrar Colegio
        private void btnRegistrarCol_Click(object sender, EventArgs e)
        {
            int nitColegio;
            string nombreColegio, tipoColegio = "";
            bool soloLetra;
            try
            {
                nitColegio = int.Parse(txtNitColReg.Text);
                nombreColegio = txtNombreColReg.Text;
                soloLetra = Regex.IsMatch(nombreColegio, @"^[a-zA-Z\s]{1,30}$");
                if(!soloLetra)
                {
                    MessageBox.Show("-El campo Nombre solo debe contener LETRAS.\n-El campo Nombre debe contener al menos una letra."+
                        "\nEl campo Nombre NO debe contener mas de 30 letras.", "Registro Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //MessageBox.Show("Nombre solo tiene letras");
                if (rdbPrivadoReg.Checked)
                {
                    tipoColegio = "privado";
                    //MessageBox.Show("tipo es privado");
                }
                else if(rdbPublicoReg.Checked)
                {
                    tipoColegio = "público";
                    //MessageBox.Show("tipo es público");
                }
                else
                {
                    MessageBox.Show("No ha seleccionado el Tipo de Colegio", "Registro Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DataSet dsResultado = new DataSet();
                dsResultado = col.buscarColegio(nitColegio);
                if(dsResultado.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("El Colegio con el nit: " + nitColegio + " ya se encuentra registrado", "Registrar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtNitColReg.Text = "";
                    txtNombreColReg.Text = "";
                    rdbPrivadoReg.Checked = false;
                    rdbPublicoReg.Checked = false;
                    return;
                }
                int filasAfectadas = col.registrarColegio(nitColegio, nombreColegio, tipoColegio);
                //MessageBox.Show("Filas afectadas = " + filasAfectadas);
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Colegio Registrado Satisfactoriamente", "Registro Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNitColReg.Text = "";
                    txtNombreColReg.Text = "";
                    rdbPrivadoReg.Checked = false;
                    rdbPublicoReg.Checked = false;
                    return;
                }
                MessageBox.Show("Colegio NO Registrado", "Registro Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch
            {
                MessageBox.Show("Tener en cuenta\nEl campo Nit NO debe contener letras\nEl campo nit no debe ser mayor a un entero de 32 bits"+
                    "\nEl Colegio ya se encuentra registrado", "Registro de Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Actualizar Colegio
        private void btnBuscarColAct_Click(object sender, EventArgs e)
        {
            int nitcol;
            try
            {
                nitcol = int.Parse(txtNitBusAct.Text);
                if (nitcol < 0)
                {
                    MessageBox.Show("El campo nit solo debe contener números positivos", "Buscar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                DataSet dsResultado = new DataSet();
                dsResultado = col.buscarColegio(nitcol);
                if(dsResultado.Tables[0].Rows.Count > 0)
                {
                    lbNitColActualizar.Text = dsResultado.Tables[0].Rows[0][0].ToString();
                    txtNombreColAct.Text = dsResultado.Tables[0].Rows[0][1].ToString();
                    if(rdbPrivadoAct.Text.Equals(dsResultado.Tables[0].Rows[0][2].ToString()))
                    {
                        rdbPrivadoAct.Checked = true;
                    }
                    else if(dsResultado.Tables[0].Rows[0][2].ToString().Equals("público"))
                    {
                        rdbPublicoAct.Checked = true;
                    }
                }
                else
                {
                    MessageBox.Show("No se encuentra registrado el colegio con nit: " + nitcol, "Buscar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                MessageBox.Show("El campo Nit solo debe contener números", "Buscar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizarCol_Click(object sender, EventArgs e)
        {
            int nitcol;
            string nombrecol, tipocol;
            bool sololetra;
            try
            {
                nitcol = int.Parse(lbNitColActualizar.Text);
                nombrecol = txtNombreColAct.Text;
                sololetra = Regex.IsMatch(nombrecol, @"^[a-zA-Z\s]{1,30}$");
                if (!sololetra)
                {
                    MessageBox.Show("Tener en cuenta\n-El campo Nombre solo debe contener LETRAS.\n-El campo Nombre debe contener al menos una letra\n-El campo Nombre debe contener como maximo 30 caracteres", "Registro Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (rdbPrivadoAct.Checked)
                {
                    tipocol = "privado";
                }
                else if (rdbPublicoAct.Checked)
                {
                    tipocol = "público";
                }
                else
                {
                    MessageBox.Show("Por favor seleccione el tipo de colegio", "Actualizar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int filasAfectadas = col.actualizarColegio(nitcol, nombrecol, tipocol);
                if(filasAfectadas > 0)
                {
                    MessageBox.Show("Colegio actualizado exitosamente", "Actualizar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNitBusAct.Text = "";
                    lbNitColActualizar.Text = "";
                    txtNombreColAct.Text = "";
                    rdbPrivadoAct.Checked = false;
                    rdbPublicoAct.Checked = false;
                }
                else
                {
                    MessageBox.Show("NO se pudo actualizar el Colegio", "Actualizar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("El campo nit solo debe contener números", "Actualizar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Eliminar Colegio

        private void btnEliminarCol_Click(object sender, EventArgs e)
        {
            int nitcol;
            try
            {
                //Mensaje de confirmacion
                if (MessageBox.Show("¿Esta seguro de que desea eliminar el colegio?", "Eliminar Colegio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                nitcol = int.Parse(txtNitBuscarEli.Text);
                //Buscar el colegio antes de eliminar
                DataSet dsBusqueda = col.buscarColegio(nitcol);

                if (dsBusqueda.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("No existe el Colegio con nit: " + nitcol, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Eliminar colegio
                int filasAfectadas = col.eliminarColegio(nitcol);
                //MessageBox.Show("Filas afectadas: " + filasAfectadas + "\nNit: " + nitcol);
                if (filasAfectadas > 0)
                {
                    MessageBox.Show("Se Elimino el Colegio con nit: " + nitcol, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se pudo Eliminar el Colegio con nit: " + nitcol, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
            }
            catch(FormatException eformat)
            {
                MessageBox.Show("Tener en cuenta\n-El campo Nit No debe contener Letras", "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("¿Desea ver mas informacion sobre el error?", "Eliminar Colegio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + eformat.StackTrace, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (OverflowException eover)
            {
                MessageBox.Show("Tener en cuenta\n-El campo Nit no debe sobrepasar un entero de 32 bits", "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (MessageBox.Show("¿Desea ver mas informacion sobre el error?", "Eliminar Colegio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + eover.StackTrace, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception er)
            {
                MessageBox.Show("Ha habido un error al tratar de eliminar el Estudiante", "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (MessageBox.Show("¿Desea ver mas informacion sobre el error?", "Eliminar Colegio", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + er.StackTrace, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                txtCodigoBusEli.Text = "";
            }
        }
        #endregion

        #region Registrar Estudiante
        private void btnRegistarEst_Click(object sender, EventArgs e)
        {
            int codigo, anio, mes, dia;
            string nombre, apellido, fechaNacimiento;
            //bool validarCod;
            try
            {
                codigo = int.Parse(txtCodEstReg.Text);
                //validarCod = Regex.IsMatch(codigo.ToString(), "[0-9]{,15}");
                nombre = txtNombreEstReg.Text;
                apellido = txtApellidoReg.Text;
                anio = int.Parse(dtpFechaNacReg.Value.Date.Year.ToString());
                mes = int.Parse(dtpFechaNacReg.Value.Date.Month.ToString());
                dia = int.Parse(dtpFechaNacReg.Value.Date.Day.ToString());
                fechaNacimiento = dia + "/" + mes + "/" + anio;
                int filasAfectadas = est.registrarEstudiante(codigo, nombre, apellido, fechaNacimiento);
                if(filasAfectadas > 0)
                {
                    MessageBox.Show("Se registro un estudiante", "Registrar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodEstReg.Text = "";
                    txtNombreEstReg.Text = "";
                    txtApellidoReg.Text = "";
                }
                else
                {
                    MessageBox.Show("No se registró ningun esetudiante", "Registrar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("Tener en cuenta\nEl campo codigo NO debe contener letras\nEl campo codigo NO debe exceder el valor de un entero de 32 bits",
                    "Registrar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Actualizar Estudiante
        private void btnBuscarEstAct_Click(object sender, EventArgs e)
        {
            int codigo, anio, mes, dia;
            try
            {
                codigo = int.Parse(txtCodigoEstBusAct.Text);
                if (codigo < 0)
                {
                    MessageBox.Show("el codigo No puede ser menor de cero","actualizar estudiante",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                DataSet dsResultado = new DataSet();
                dsResultado = est.buscarEstudiante(codigo);

                if (dsResultado.Tables[0].Rows.Count > 0)
                {
                    lbCodigoEstudianteAct.Text = dsResultado.Tables[0].Rows[0][0].ToString();
                    txtNombreEstAct.Text = dsResultado.Tables[0].Rows[0][1].ToString();
                    txtApellidoEstAct.Text = dsResultado.Tables[0].Rows[0][2].ToString();

                    string fecha = dsResultado.Tables[0].Rows[0][3].ToString();
                    char[] delimitador = {'/', ' '};
                    string[] trozos = fecha.Split(delimitador);
                    //MessageBox.Show("dia '" +trozos[0] + "' mes '" + trozos[1] + "' anio '" + trozos[2] +"'", "Fecha sin /");

                    dia = int.Parse(trozos[0]);
                    mes = int.Parse(trozos[1]);
                    anio = int.Parse(trozos[2]);

                    //MessageBox.Show("dia '" + dia + "' mes '" + mes + "' anio '" + anio + "'", "Fecha");
                    dtpFechaNacAct.Value = new DateTime(anio, mes, dia);
                }
                else
                {
                    MessageBox.Show("No se actualizo ningun estudiante", "Actualizar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("El campo codigo NO debe contener letras", "Actualizar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnActualizarEst_Click(object sender, EventArgs e)
        {
            int codEst;
            string nombreEst, apellidoEst, fNacimiento;
            string dia, mes, anio;
            try
            {
                codEst = int.Parse(lbCodigoEstudianteAct.Text);
                nombreEst = txtNombreEstAct.Text;
                apellidoEst = txtApellidoEstAct.Text;

                dia = dtpFechaNacAct.Value.Date.Day.ToString();
                mes = dtpFechaNacAct.Value.Date.Month.ToString();
                anio = dtpFechaNacAct.Value.Date.Year.ToString();

                fNacimiento = dia + "/"+mes + "/"+ anio;

                int filas = est.actualizarEstudiante(codEst, nombreEst, apellidoEst, fNacimiento);
                if(filas > 0)
                {
                    MessageBox.Show("Se actualizo al estudiante con codigo: " + codEst, "Actualizar estudiante", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("No se logro actulizar el estudiante", "Actualizar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
                MessageBox.Show("El campo codigo no debe contener letras","Actulizar Estudiante",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        #endregion
        #region Eliminar Estudiante

        private void btnEliminarEst_Click(object sender, EventArgs e)
        {
            int codigo;
            try
            {
                if (MessageBox.Show("¿Esta seguro de que desea Eliminar el Estudiante?", "Eliminar Estudiante", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                if (txtCodigoBusEli.Text.Equals("") || txtCodigoBusEli.Text.Length <= 0)
                {
                    MessageBox.Show("Tener en cuenta\nEl campo codigo no debe estar vacio", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                codigo = int.Parse(txtCodigoBusEli.Text);
                if(codigo < 0)
                {
                    MessageBox.Show("Tener en cuenta\nEl codigo No puede ser menor de cero", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                int filasAfectadas = est.eliminarEstudiante(codigo);
                if (filasAfectadas > 0)
                    MessageBox.Show("Se elimino el estudiante satisfactoriamente", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("No existe Estudiante con codigo: " + codigo, "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(FormatException eformat)
            {
                MessageBox.Show("Tener en cuenta\nEl campo Codigo no debe contener letras", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("¿Desea saber mas información sobre error?", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + eformat.StackTrace, "Eliminar Estudiante", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            }
            catch(OverflowException eover)
            {
                MessageBox.Show("Tener en cuenta\nEl codigo no debe ser mayor a un entero de 32 bits", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("¿Desea saber mas información sobre el error?", "Eliminar Estudiante", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + eover.StackTrace, "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception er)
            {
                MessageBox.Show("Tener en cuenta\nHubo un error al tratar de eliminar al estudiante", "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("¿Desea saber mas información sobre el error?", "Eliminar Estudiante", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error: \n" + er.StackTrace, "Eliminar Estudiante", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Registrar Grado
        private void btnRegistrarGrado_Click(object sender, EventArgs e)
        {
            int codigo, filasAfectadas, colnit;
            string nombreGrado, jornadaGrado;
            try
            {
                codigo = int.Parse(txtCodigoGrado.Text);
                //Confirmar que no se duplique el grado
                DataSet dsBusqueda = new DataSet();
                dsBusqueda = grado.buscarGrado(codigo);
                if(dsBusqueda.Tables[0].Rows.Count > 0)
                {
                    MessageBox.Show("El Grado con codigo: " + codigo + " ya se encuntra registrado", "Regisrar Codigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                colnit = int.Parse(txtNitColGrado.Text);
                //Confirmar que el colegio si exista
                DataSet dsBusquedaCol = new DataSet();
                dsBusquedaCol = col.buscarColegio(colnit);
                if (dsBusquedaCol.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("El colegio con codigo: " + colnit + " NO se encuntra registrado", "Regisrar Codigo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                nombreGrado = txtNombreGrado.Text;
                jornadaGrado = cbxJornada.SelectedItem.ToString();
                filasAfectadas = grado.registrarGrado(codigo, nombreGrado, jornadaGrado, colnit);
                if(filasAfectadas > 0)
                {
                    MessageBox.Show("El grado con codigo: " + codigo + " se registro satisfactoriamente", "Registro Grado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoGrado.Text = "";
                    txtNombreGrado.Text = "";
                    txtNitColGrado.Text = "";
                    cbxJornada.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("El grado con codigo: " + codigo + "NO se logro registrar", "Registro Grado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            catch
            {
                MessageBox.Show("Tener en Cuenta\nEl campo codigo NO debe contener letras\nEl campo Nit Colegio NO debe contener letras", "Registrar Grado Escolar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        #region Matricula

        private void btnMatricula_Click(object sender, EventArgs e)
        {
            int codEst, codGrado, anioLectivo;
            string fechaMatricula;
            try
            {
                codEst = int.Parse(txtCodigoEstMatri.Text);
                codGrado = int.Parse(txtCodGradoMatri.Text);

                //busqueda del grado y del estudiante

                DataSet dsBEst = new DataSet();
                DataSet dsBGrado = new DataSet();

                dsBEst = est.buscarEstudiante(codEst);
                if(dsBEst.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("NO existe estudiante con Codigo: " + codEst, "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                dsBGrado = grado.buscarGrado(codGrado);
                if (dsBGrado.Tables[0].Rows.Count <= 0)
                {
                    MessageBox.Show("NO existe Grado Escolar con Codigo: " + codGrado, "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                fechaMatricula = dtpFechaMatricula.Value.ToString("dd/MM/yyyy");
                anioLectivo = int.Parse(dtpAnioLectvo.Value.ToString("yyyy"));
                //MessageBox.Show("Fecha Matricula: " + fechaMatricula + " \nAño Lectivo: " + anioLectivo, "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                int filasAfectadas = matricula.registrarMatricula(codEst, codGrado, fechaMatricula, anioLectivo);
                if(filasAfectadas > 0)
                {
                    MessageBox.Show("Se realizo la matricula exitosamente", "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigoEstMatri.Text = "";
                    txtCodGradoMatri.Text = "";
                }
                else
                {
                    MessageBox.Show("Hubo un error al intentar guardar la Matricula", "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch(FormatException eformat)
            {
                MessageBox.Show("Tener en cuenta\nEl campo Codigo tanto del Estudiante como del Grado NO deben contener letras", "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);

                //MessageBox.Show(err.StackTrace.ToString());
            }
            catch(OverflowException eover)
            {
                MessageBox.Show("Tener en cuenta:\nEl campo El campo Codigo tanto del Estudiante como del Grado NO deben ser mayores a un entero de 32 bits", "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception er)
            {
                MessageBox.Show("Ha ocurrido un Error", "Matricula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (MessageBox.Show("¿Desea ver mas informacion sobre el error?", "Matricula", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MessageBox.Show("Error:\n" + er.StackTrace, "Eliminar Colegio", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        #endregion

        #region Consultas
        private void btnConsultarTodaInfo_Click(object sender, EventArgs e)
        {
            try
            {

                DataSet dsResultado = new DataSet();
                dsResultado = matricula.consultarTodaInfo();
                dgvConsulta5.DataSource = dsResultado;
                dgvConsulta5.DataMember = "ResultadoDatos";
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un erro al tratar de hacer la consulta", "Consulta 5", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsultarXFecha_Click(object sender, EventArgs e)
        {
            string fechaMatricula;
            try
            {
                fechaMatricula = dtpConsultaFecha.Value.ToString("dd/MM/yyyy");
                //MessageBox.Show(fechaMatricula);
                DataSet dsResultado = new DataSet();
                dsResultado = matricula.consultarXFecha(fechaMatricula);
                dgvConsulta6.DataSource = dsResultado;
                dgvConsulta6.DataMember = "ResultadoDatos";
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un erro al tratar de hacer la consulta", "Consulta 6", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConsulta7_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet dsResultado = new DataSet();
                dsResultado = matricula.consultarXanioYjornada();
                if(dsResultado.Tables[0].Rows.Count > 0)
                {
                    lbConsulta7Resp.Text =  dsResultado.Tables[0].Rows[0][0].ToString();
                }
                else
                    MessageBox.Show("Ha ocurrido un erro al tratar de hacer la consulta", "Consulta 7", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show("Ha ocurrido un erro al tratar de hacer la consulta", "Consulta 7", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

    }
}
