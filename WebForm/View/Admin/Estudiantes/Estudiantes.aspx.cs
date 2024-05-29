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
        private BindingList<alumno> estudiantes;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Comprobamos que se trata de un admin
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/View/Login/Login.aspx");
            }
            else
            {
                daoservicio = new ServicioWS.LKServicioWebClient();
                string tipo = Session["Tipo"] as string; 
                if (tipo != "Administrador") Response.Redirect("/View/Login/Login.aspx");
                usuario usuario_actual = Session["Usuario"] as usuario;
            }
            CargarTabla();
        }
        protected void CargarTabla()
        {
            estudiantes = new BindingList<alumno>(daoservicio.listarAlumnos().ToList());

            GridAlumnos.DataSource = estudiantes;
            GridAlumnos.DataBind();
        }
        protected void EditRow_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument); // Recibe el codigo del alumno

            alumno alumn = estudiantes.ToList().Find(alu => alu.codigoAlumno == codigoBusca);
            TxtCode.Text = alumn.codigoAlumno.ToString();
            TxtDNI.Text = alumn.dni.ToString();
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

            // Implementarla edicion en bd
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = ( LinkButton )sender;
            int codigo = int.Parse(btn.CommandArgument);
            alumno alu = estudiantes.ToList().Find(al => al.codigoAlumno == codigo);
            estudiantes.ToList().Remove(alu);
            CargarTabla();

        }
        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            // Agregar alumno
            TxtCode.Text = "";
            TxtDNI.Text = "";
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
            
            // Implementar agregacion en bd

        }
        protected void BntGuardar_Click(object sender, EventArgs e)
        {
            /*
            AlumnoN alumno = new AlumnoN();
            bool actualizaOcrea = string.IsNullOrEmpty(TxtCode.Text);
            if (actualizaOcrea)
            {
                alumno.Codigo = alumnos.Count + 1;
            }
            else
            {
                alumno = alumnos.Find(alu => alu.Codigo == int.Parse(TxtCode.Text));
            }
            alumno.Dni = TxtDNI.Text;
            alumno.Nombre = TxtNombre.Text;
            alumno.ApellidoPat = TxtApellidoPat.Text;
            alumno.ApellidoMat = TxtApellidoMat.Text;
            alumno.Correo = TxtCorreo.Text;
            alumno.Telefono = TxtTelefono.Text;
            alumno.Grado = SLGrado.SelectedValue;

            if (actualizaOcrea)
            {
                alumnos.Add(alumno);
            }
            CargarTabla() ;*/
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}