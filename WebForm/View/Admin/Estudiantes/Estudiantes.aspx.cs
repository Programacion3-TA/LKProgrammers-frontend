using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.View.Admin.Profesores;

namespace WebForm.View.Admin.Estudiantes
{
    public partial class Estudiantes : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient daoservicio;
        private BindingList<alumno> alumnos;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoservicio = new ServicioWS.LKServicioWebClient();
            
            var resultado = daoservicio.listarAlumnos();
            if (resultado != null)
            {
                // Existen alumnos
                alumnos = new BindingList<alumno>(resultado.ToList());
                CargarTabla();
               
            }
            else
            {
                LblNoAlumnos.Visible = true;
            }                
            
        }

        protected void CargarTabla()
        {
            GridAlumnos.DataSource = alumnos;
            LblNoAlumnos.Visible = false;
            GridAlumnos.DataBind();
        }

        protected void GridAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAlumnos.PageIndex = e.NewPageIndex;
            GridAlumnos.DataBind();
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument); // Recibe el codigo del alumno

            alumno alumn = daoservicio.listarAlumnos().ToList().Find(alu => alu.codigoAlumno == codigoBusca);
            TxtCode.Text = alumn.codigoAlumno.ToString();
            TxtDNI.Text = alumn.dni.ToString();
            TxtDNI.Enabled = false;
            TxtNombre.Text = alumn.nombres;
            TxtTelefono.Text = alumn.telefono;
            TxtApellidoPat.Text = alumn.apellidoPaterno;
            TxtApellidoMat.Text = alumn.apellidoMaterno;
            TxtCorreo.Text = alumn.correoElectronico;
            SLGrado.SelectedValue = alumn.grado.ToString();
            DDGenero.SelectedValue = alumn.genero.ToString();
            TxtDireccion.Text = alumn.direccion;
            TxtUsuario.Text = alumn.usuario1;
            TxtContrasenia.Text = alumn.contrasenia;
            TxtFechaNac.Text = alumn.fechaNac.ToString("yyyy-MM-dd");
            CallJavascript("showModalFormAgregarEstudiante()");

        }

        protected void DelRow_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            int codigo = int.Parse(btn.CommandArgument);

            // Preguntar si se quiere eliminar de verdad
            var elementoAEliminar = alumnos.FirstOrDefault(p => p.codigoAlumno == codigo);
            if (elementoAEliminar == null) return;
            alumno al = elementoAEliminar as alumno;
            Session["alumnoAEliminar"] = al;
            LblWarning.Text = "¡Cuidado!";
            LblMensaje.Text = "¿Está seguro de que quiere eliminar al alumno " + al.nombres + " " + al.apellidoPaterno +
                " con código " + al.codigoAlumno + " y DNI " + al.dni + "?";
            BtnAceptarEliminar.Visible = true;
            CallJavascript("showModalFormWarning()");
            
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            // Agregar alumno
            TxtCode.Text = "";
            TxtDNI.Text = "";
            TxtDNI.Enabled = true;
            TxtNombre.Text = "";
            TxtTelefono.Text = "";
            TxtApellidoPat.Text = "";
            TxtApellidoMat.Text = "";
            TxtCorreo.Text = "";
            DDGenero.SelectedValue = "";
            TxtDireccion.Text = "";
            TxtUsuario.Text = "";
            TxtContrasenia.Text = "";
            TxtFechaNac.Text = "";
            SLGrado.SelectedValue = "";
            CallJavascript("showModalFormAgregarEstudiante()");
        }

        protected void BntGuardar_Click(object sender, EventArgs e)
        {
            alumno al = new alumno();
            bool actualizaOcrea = string.IsNullOrEmpty(TxtCode.Text);
            
            bool campoVacio = false;

            if (!string.IsNullOrEmpty(TxtDNI.Text))
                al.dni = TxtDNI.Text; //1            
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtNombre.Text))
                al.nombres = TxtNombre.Text; //2
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtApellidoPat.Text))
                al.apellidoPaterno = TxtApellidoPat.Text; //3
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtApellidoMat.Text))
                al.apellidoMaterno = TxtApellidoMat.Text; //4
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtCorreo.Text))
                al.correoElectronico = TxtCorreo.Text; //5
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtTelefono.Text))
                al.telefono = TxtTelefono.Text; //6
            else campoVacio = true;
            if (!string.IsNullOrEmpty(TxtDireccion.Text))
                al.direccion = TxtDireccion.Text; //7
            else campoVacio = true; 
            if (!string.IsNullOrEmpty(DDGenero.SelectedValue))
                al.genero = DDGenero.SelectedValue.ToString()[0]; //8
            
            al.gradoSpecified = true;

            if (!string.IsNullOrEmpty(TxtFechaNac.Text))
            {
                al.fechaNacSpecified = true;
                al.fechaNac = DateTime.Parse(TxtFechaNac.Text); //9
            }
            else campoVacio = true;

            if (!string.IsNullOrEmpty(SLGrado.Text))
                al.grado = (grado)Enum.Parse(typeof(grado), SLGrado.Text); //10
            else campoVacio = true;
            
            if (!string.IsNullOrEmpty(TxtUsuario.Text))
                al.usuario1 = TxtUsuario.Text;
            else campoVacio = true;

            if (!string.IsNullOrEmpty(TxtContrasenia.Text))
                al.contrasenia = TxtContrasenia.Text;
            else campoVacio = true;

            if (actualizaOcrea)
            {
                if (campoVacio)
                {
                    LblWarning.Text = "¡Atención!";
                    LblMensaje.Text = "Se deben ingresar todos los datos del administrador.";
                    BtnAceptarEliminar.Visible = false;

                    CallJavascript("showModalFormWarning()");
                }
                else
                {
                    int result = daoservicio.insertarAlumno(al);

                    if (result != 0)
                    {
                        // se logró insertar
                        var resultado = daoservicio.listarAlumnos();
                        if (resultado != null)
                        {
                            alumnos = new BindingList<alumno>(resultado.ToList());
                            CargarTabla();
                        }
                        else
                        {
                            // no hay alumnos
                            LblNoAlumnos.Visible = true;
                        }
                        
                    }
                    else
                    {
                        // No se logró insertar. Debe deberse al UNIQUE constraint
                        LblWarning.Text = "¡Atención!";
                        LblMensaje.Text = "No se puede crear un alumno cuyo DNI o correo electrónico ya haya sido registrado o cuyo nombre de usuario de intranet ya exista.";
                        BtnAceptarEliminar.Visible = false;

                        CallJavascript("showModalFormWarning()");
                    }
                }
   
            }
            else
            {
                int result = daoservicio.editarAlumno(al);
                if (result != 0)
                {
                    var resultado = daoservicio.listarAlumnos();
                    if (resultado != null)
                    {
                        alumnos = new BindingList<alumno>(resultado.ToList());
                        CargarTabla();
                    }
                    else LblNoAlumnos.Visible = true;
                }
            }
            
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnAceptarEliminar_Click(object sender, EventArgs e)
        {
            alumno alumnoAEliminar = Session["alumnoAEliminar"] as alumno;
            int resultado1 = daoservicio.eliminarAlumno(alumnoAEliminar.codigoAlumno);

            var resultado2 = daoservicio.listarAlumnos();
            if (resultado2 != null)
            {
                alumnos = new BindingList<alumno>(resultado2.ToList());
                CargarTabla();
            }

        }

        protected void BtnRestaurar_Click(object sender, EventArgs e)
        {
            var resultado = daoservicio.listarAlumnos();
            if (resultado != null)
            {
                // Existen alumnos
                alumnos = new BindingList<alumno>(resultado.ToList());
                CargarTabla();

            }
        }

        protected void LkBtnBuscar_Click(object sender, EventArgs e)
        {
            // Buscar
        }
    }
}
