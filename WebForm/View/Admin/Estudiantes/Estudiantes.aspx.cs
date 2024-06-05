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
            GridAlumnos.DataSource = new BindingList<alumno>(daoservicio.listarAlumnos()); 
            GridAlumnos.DataBind();
        }
        protected void EditRow_Click(object sender, EventArgs e)
        {
            
            LinkButton btn = (LinkButton)sender;
            int codigoBusca = int.Parse(btn.CommandArgument); // Recibe el codigo del alumno

            alumno alumn = daoservicio.listarAlumnos().ToList().Find(alu => alu.codigoAlumno == codigoBusca);
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
            daoservicio.eliminarAlumno(codigo);
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
            
            alumno al = new alumno();
            bool actualizaOcrea = string.IsNullOrEmpty(TxtCode.Text);
            if (actualizaOcrea)
            {
                al.codigoAlumno = daoservicio.listarAlumnos().Count() + 1;
            }
            else
            {
                al = daoservicio.listarAlumnos().ToList().Find(alu => alu.codigoAlumno == int.Parse(TxtCode.Text));
            }
            al.dni = TxtDNI.Text;//1
            al.nombres = TxtNombre.Text;//2
            al.apellidoPaterno = TxtApellidoPat.Text;//3
            al.apellidoMaterno = TxtApellidoMat.Text;//4
            al.correoElectronico = TxtCorreo.Text;//5
            al.telefono = TxtTelefono.Text;//6
            al.direccion = TxtDireccion.Text;//7
            al.genero = DDGenero.SelectedValue.ToString()[0];//8
            al.gradoSpecified = true;
            al.fechaNacSpecified = true;
            al.fechaNac = DateTime.Parse(TxtFechaNac.Text);//9
            al.grado = (grado)Enum.Parse(typeof(grado), SLGrado.Text);//10
            al.usuario1 = TxtUsuario.Text;
            al.contrasenia = TxtContrasenia.Text;
            if (actualizaOcrea)
            {
                daoservicio.insertarAlumno(al);
            }
            else
            {
                daoservicio.editarAlumno(al);
            }
            CargarTabla() ;
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}