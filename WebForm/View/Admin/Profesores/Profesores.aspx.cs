using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Profesores
{
    
    public partial class Profesores : System.Web.UI.Page // Vista administrativa de profesores
    {
        private LKServicioWebClient daoServicio;
        private BindingList<profesor> listProfesor;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                var profesores = daoServicio.listarProfesores();
                if (profesores != null)
                {
                    listProfesor = new BindingList<profesor>(profesores);                    
                }
                else
                {
                    listProfesor = new BindingList<profesor>();
                }
                Session["Profesores"] = listProfesor;
                CargarTabla();
            }            
            
        }
        
        private void CargarTabla()
        {
            listProfesor = Session["Profesores"] as BindingList<profesor>;
            GridProfesores.DataSource = listProfesor;
            GridProfesores.DataBind();
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int code = Int32.Parse(btn.CommandArgument);

            listProfesor = Session["Profesores"] as BindingList<profesor>;

            profesor profesor = listProfesor.FirstOrDefault(x => x.codigoProfesor == code);
            TxtCode.Text = profesor.codigoProfesor.ToString();
            TxtNombre.Text = profesor.nombres;
            TxtApellidoPat.Text = profesor.apellidoPaterno;
            TxtApellidoMat.Text = profesor.apellidoMaterno;
            TxtDireccion.Text = profesor.direccion;
            TxtTelefono.Text = profesor.telefono;

            DDGenero.SelectedValue = profesor.genero.ToString();

            TxtCorreo.Text = profesor.correoElectronico;
            TxtUsername.Text = profesor.usuario1;
            TxtPassword.Text = profesor.contrasenia;
            TxtFechaNacimiento.Text = profesor.fechaNac.ToString("yyyy-MM-dd");            

            TxtDNI.Text = profesor.dni;
            TxtDNI.Enabled = false;
            TxtEspecialidad.Text = profesor.especialidad;
            CallJavascript("showModalForm()");
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int code = int.Parse(btn.CommandArgument);

            listProfesor = Session["Profesores"] as BindingList<profesor>;
            
            // Preguntar si se quiere eliminar de verdad
            var elementoAEliminar = listProfesor.FirstOrDefault(p => p.codigoProfesor == code);
            if (elementoAEliminar == null) return;
            profesor prof = elementoAEliminar as profesor;
            Session["profesorAEliminar"] = prof;
            LblWarning.Text = "¡Cuidado!";
            LblMensaje.Text = "¿Está seguro de que quiere eliminar al administrador " + prof.nombres + " " + prof.apellidoPaterno +
                " con código " + prof.codigoProfesor + " y DNI " + prof.dni + "?";
            BtnAceptarEliminar.Visible = true;
            CallJavascript("showModalFormWarning()");

        }

        protected void ButGuardar_Click(object sender, EventArgs e)
        {
            profesor op = new profesor();
            bool campoVacio = false;

            if (!string.IsNullOrEmpty(TxtNombre.Text))
                op.nombres = TxtNombre.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtApellidoPat.Text))
                op.apellidoPaterno = TxtApellidoPat.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtApellidoMat.Text))
                op.apellidoMaterno = TxtApellidoMat.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtEspecialidad.Text))
                op.especialidad = TxtEspecialidad.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtDireccion.Text))
                op.direccion = TxtDireccion.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtTelefono.Text))
                op.telefono = TxtTelefono.Text;
            else campoVacio = true;

            op.genero = DDGenero.SelectedValue[0];
            if (!string.IsNullOrEmpty(TxtCorreo.Text))
                op.correoElectronico = TxtCorreo.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtUsername.Text))
                op.usuario1 = TxtUsername.Text;
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtPassword.Text))
                op.contrasenia = TxtPassword.Text;
            else campoVacio = true;

            if (!string.IsNullOrEmpty(TxtFechaNacimiento.Text))
            {
                try
                {
                    op.fechaNac = DateTime.Parse(TxtFechaNacimiento.Text);
                    op.fechaNacSpecified = true;
                }
                catch (System.Exception)
                {
                    System.Diagnostics.Debug.WriteLine("Error en el parse");
                }
            }
            else campoVacio = true;

            if (!string.IsNullOrEmpty(TxtDNI.Text))
                op.dni = TxtDNI.Text;
            else campoVacio = true;

            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                if (campoVacio)
                {
                    LblWarning.Text = "¡Atención!";
                    LblMensaje.Text = "Se deben ingresar todos los datos del profesor.";
                    BtnAceptarEliminar.Visible = false;

                    CallJavascript("showModalFormWarning()");
                }
                else
                {
                    int result = daoServicio.insertarprofesor(op);

                    if (result != 0)
                    {
                        // se logró insertar
                        listProfesor = new BindingList<profesor>(daoServicio.listarProfesores().ToList());
                        Session["Profesores"] = listProfesor;
                        CargarTabla();
                    }
                    else
                    {
                        // No se logró insertar. Debe deberse al UNIQUE constraint
                        LblWarning.Text = "¡Atención!";
                        LblMensaje.Text = "No se puede crear un profesor cuyo DNI o correo electrónico ya haya sido registrado o cuyo nombre de usuario de intranet ya exista.";
                        BtnAceptarEliminar.Visible = false;

                        CallJavascript("showModalFormWarning()");
                    }
                    
                }                
            }
            else //actualizar
            {
                daoServicio.editarProfesor(op);
                var profesores = daoServicio.listarProfesores();
                if (profesores != null)
                {
                    listProfesor = new BindingList<profesor>(profesores);
                }
                else
                {
                    listProfesor = new BindingList<profesor>();
                }
                Session["Profesores"] = listProfesor;
                CargarTabla();
            }            
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtDNI.Text = "";
            TxtNombre.Text = "";
            TxtApellidoMat.Text = "";
            TxtApellidoPat.Text = "";
            TxtDireccion.Text = "";
            TxtCorreo.Text = "";
            TxtUsername.Text = "";
            TxtPassword.Text = "";
            TxtTelefono.Text = "";
            TxtFechaNacimiento.Text = "";
            TxtEspecialidad.Text = "";
            TxtDNI.Enabled = true;
            CallJavascript("showModalForm()");
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {

            profesor profeAEliminar= Session["profesorAEliminar"] as profesor;
            int resultado = daoServicio.eliminarProfesor(profeAEliminar.codigoProfesor);
            
            var profesores = daoServicio.listarProfesores();

            if (profesores != null)
            {
                listProfesor = new BindingList<profesor>(profesores);
            }
            else
            {
                listProfesor = new BindingList<profesor>();
            }
            Session["Profesores"] = listProfesor;
            CargarTabla();
        }

        protected void LkBtnBuscar_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TxtCriterioBusqueda.Text))
            {
                var resultado = daoServicio.buscarProfesTodosCriterios(TxtCriterioBusqueda.Text);

                if (resultado != null)
                {
                    // Se ha encontrado profesores
                    BindingList<profesor> profesRecuperados = new BindingList<profesor>(resultado.ToList());
                    listProfesor = profesRecuperados;
                    Session["Profesores"] = listProfesor;
                    CargarTabla();
                }
                else
                {
                    // No se ha encontrado personal
                    LblWarning.Text = "¡Atención!";
                    LblMensaje.Text = "No se han encontrado profesores con los criterios de búsqueda ingresados.";
                    BtnAceptarEliminar.Visible = false;

                    CallJavascript("showModalFormWarning()");
                }
            }
            else
            {
                // No se ha ingresado un criterio de búsqueda
                LblWarning.Text = "¡Atención!";
                LblMensaje.Text = "Es necesario ingresar un criterio de búsqueda.";
                BtnAceptarEliminar.Visible = false;

                CallJavascript("showModalFormWarning()");
            }

            BtnRestaurar.Visible = true;
        }

        protected void BtnRestaurar_Click(object sender, EventArgs e)
        {
            var profesores = daoServicio.listarProfesores();
            if (profesores != null)
            {
                listProfesor = new BindingList<profesor>(profesores);
            }
            else
            {
                listProfesor = new BindingList<profesor>();
            }
            Session["Profesores"] = listProfesor;
            CargarTabla();
        }

        protected void BtnAgregarCurso_Click(object sender, EventArgs e)
        {
            // Asignamos el curso al profesor
            BindingList<curso> cursosLibres = Session["CursosDisponibles"] as BindingList<curso>;
            Button btn = (Button)sender;
            int code = Int32.Parse(btn.CommandArgument);
            curso curs = cursosLibres.FirstOrDefault(x => x.id == code);
            profesor profesorActual = Session["ProfesorActual"] as profesor;
            daoServicio.insertarCursoProfesor(curs.id, profesorActual.dni);

            // Acutalizo cursos dictados mostrados en el modal
            var resultado = daoServicio.listarSoloCursosPorProfesor(profesorActual.dni);

            if (resultado != null)
            {
                // Se encontraron cursos
                LblNoCursosDictados.Visible = false;
                BindingList<curso> cursosDictados = new BindingList<curso>(resultado.ToList());
                Session["CursosDictados"] = cursosDictados;

                GVCursosDictados.DataSource = cursosDictados;
                GVCursosDictados.DataBind();
                GVCursosDictados.Visible = true;
            }
            else
            {
                // No se encontraron cursos
                GVCursosDictados.Visible = false;
                LblNoCursosDictados.Visible = true;
            }

            GVCursosEncontrados.Visible = false;

            CallJavascript("showModalFormCursosDictados()");
        }

        protected void BtnEliminarCurso_Click(object sender, EventArgs e)
        {
            // Desasignamos el curso al profesor
            profesor profesorActual = Session["ProfesorActual"] as profesor;
            Button btn = (Button)sender;
            int code = Int32.Parse(btn.CommandArgument);

            BindingList<curso> cursosDictados = Session["CursosDictados"] as BindingList<curso>;
            curso cursoH = cursosDictados.FirstOrDefault(x => x.id == code);

            daoServicio.eliminarCursoProfesor(cursoH.id, profesorActual.dni);

            // Actualizo cursos dictados
            var resultado = daoServicio.listarSoloCursosPorProfesor(profesorActual.dni);

            if (resultado != null)
            {
                // Se encontraron cursos
                LblNoCursosDictados.Visible = false;
                cursosDictados = new BindingList<curso>(resultado.ToList());
                Session["CursosDictados"] = cursosDictados;

                GVCursosDictados.DataSource = cursosDictados;
                GVCursosDictados.DataBind();
                GVCursosDictados.Visible = true;
            }
            else
            {
                // No se encontraron cursos
                GVCursosDictados.Visible = false;
                LblNoCursosDictados.Visible = true;
            }

            GVCursosEncontrados.Visible = false;

            CallJavascript("showModalFormCursosDictados()");
        }

        protected void BtnCursos_Click(object sender, EventArgs e)
        {
            // Cargamos la información relacionada a los cursos dictados por el profesor
            Button btn = (Button)sender;
            int code = Int32.Parse(btn.CommandArgument);
            GVCursosEncontrados.Visible = false;
            listProfesor = Session["Profesores"] as BindingList<profesor>;

            profesor profesor = listProfesor.FirstOrDefault(x => x.codigoProfesor == code);
            Session["ProfesorActual"] = profesor;
            var resultado = daoServicio.listarSoloCursosPorProfesor(profesor.dni);

            if (resultado != null)
            {
                // Se encontraron cursos
                LblNoCursosDictados.Visible = false;
                BindingList<curso> cursosDictados = new BindingList<curso>(resultado.ToList());
                Session["CursosDictados"] = cursosDictados;

                GVCursosDictados.DataSource = cursosDictados;
                GVCursosDictados.DataBind();
                GVCursosDictados.Visible = true;
            }
            else
            {
                // No se encontraron cursos
                GVCursosDictados.Visible = false;
                LblNoCursosDictados.Visible = true;
            }
            CallJavascript("showModalFormCursosDictados()");
        }

        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            // Llenado el GV con cursos disponibles que sigan el criterio de búsqueda
            if (string.IsNullOrEmpty(TxtCriterioCursos.Text))
            {
                // Si no se ha pasado un criterio, mostrar todos los disponibles
                var resultado = daoServicio.listarCursosSinProfesor();

                if (resultado != null)
                {
                    // Se encontraron cursos libres
                    LblNoCursosDisp.Visible = false;
                    BindingList<curso> cursosLibres = new BindingList<curso>(resultado.ToList());
                    Session["CursosDisponibles"] = cursosLibres;

                    GVCursosEncontrados.DataSource = cursosLibres;
                    GVCursosEncontrados.Visible = true;
                    GVCursosEncontrados.DataBind();
                }
                else
                {
                    // No se encontraron cursos
                    LblNoCursosDisp.Visible = true;
                    GVCursosEncontrados.Visible = false;
                }
            }
            else
            {
                // Buscamos cursos por el criterio
                var resultado = daoServicio.buscarCursoNombreCodigo(TxtCriterioCursos.Text);

                if (resultado != null)
                {
                    // Se encontraron cursos libres
                    LblNoCursosDisp.Visible = false;
                    BindingList<curso> cursosLibres = new BindingList<curso>(resultado.ToList());
                    Session["CursosDisponibles"] = cursosLibres;

                    GVCursosEncontrados.DataSource = cursosLibres;
                    GVCursosEncontrados.Visible = true;
                    GVCursosEncontrados.DataBind();
                }
                else
                {
                    // No se encontraron cursos
                    GVCursosEncontrados.Visible = false;
                    LblNoCursosDisp.Visible = true;
                }
            }
            CallJavascript("showModalFormCursosDictados()");

        }

        protected void GridProfesores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProfesores.PageIndex = e.NewPageIndex;
            listProfesor = Session["Profesores"] as BindingList<profesor>;
            GridProfesores.DataSource = listProfesor;
            GridProfesores.DataBind();
        }
    }
}