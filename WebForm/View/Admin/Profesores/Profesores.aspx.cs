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
            // Comprobamos que se trata de un admin
            if (Session["Usuario"] == null)
            {
                Response.Redirect("/View/Login/Login.aspx");
            }
            else
            {
                daoServicio = new LKServicioWebClient();
                string tipo = Session["Tipo"] as string; // Verifico el tipo
                if (tipo != "Administrador") Response.Redirect("/View/Login/Login.aspx");
                usuario usuario_actual = Session["Usuario"] as usuario;
                                
            }

            CargarTabla();
        }
        
        private void CargarTabla()
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
            GridProfesores.DataSource = listProfesor;
            GridProfesores.DataBind();
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            int code = Int32.Parse(btn.CommandArgument);
            profesor profesor = listProfesor.FirstOrDefault(x => x.codigoProfesor == code);
            TxtCode.Text = profesor.codigoProfesor.ToString();
            TxtNombre.Text = profesor.nombres;
            TxtApellidoPat.Text = profesor.apellidoPaterno;
            TxtApellidoMat.Text = profesor.apellidoMaterno;
            TxtDireccion.Text = profesor.direccion;
            TxtTelefono.Text = profesor.telefono;
            TxtGenero.Text = ((char)profesor.genero).ToString();
            TxtCorreo.Text = profesor.correoElectronico;
            TxtUsername.Text = profesor.usuario1;
            TxtPassword.Text = profesor.contrasenia;
            TxtFechaNacimiento.Text = profesor.fechaNac.ToShortDateString();
            TxtDNI.Text = profesor.dni;
            TxtEspecialidad.Text = profesor.especialidad;
            CallJavascritp("showModalForm()");
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            daoServicio.eliminarProfesor(int.Parse(code));
            CargarTabla();
        }

        protected void ButGuardar_Click(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            profesor op = new profesor();
            BindingList<profesor> ListaProfesor = new BindingList<profesor>(daoServicio.listarProfesores());
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                op.codigoProfesor = ListaProfesor.Count() + 1;
                op.nombres = TxtNombre.Text;
                op.apellidoPaterno = TxtApellidoPat.Text;
                op.apellidoMaterno = TxtApellidoMat.Text;
                op.especialidad = TxtEspecialidad.Text;
                op.direccion = TxtDireccion.Text;
                op.telefono = TxtTelefono.Text;
                op.genero = TxtGenero.Text[0];
                op.correoElectronico = TxtCorreo.Text;
                op.usuario1 = TxtUsername.Text;
                op.contrasenia = TxtPassword.Text;
                op.fechaNacSpecified = true;
                op.fechaNac = DateTime.Parse(TxtFechaNacimiento.Text);
                op.dni = TxtDNI.Text;
                daoServicio.insertarprofesor(op);
                CargarTabla();
            }
            else //actualizar
            {
                op = ListaProfesor.ToList().Find(x => x.codigoProfesor == int.Parse(TxtCode.Text));
                op.nombres = TxtNombre.Text;
                op.apellidoPaterno = TxtApellidoPat.Text;
                op.apellidoMaterno = TxtApellidoMat.Text;
                op.especialidad = TxtEspecialidad.Text;
                op.direccion = TxtDireccion.Text;
                op.telefono = TxtTelefono.Text;
                op.genero = TxtGenero.Text[0];
                op.correoElectronico = TxtCorreo.Text;
                op.usuario1 = TxtUsername.Text;
                op.contrasenia = TxtPassword.Text;
                op.fechaNacSpecified = true;
                op.fechaNac = DateTime.Parse(TxtFechaNacimiento.Text);
                op.dni = TxtDNI.Text;
                daoServicio.editarProfesor(op);
                CargarTabla();
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtNombre.Text = "";
            TxtApellidoMat.Text = "";
            TxtApellidoPat.Text = "";
            TxtEspecialidad.Text = "";
            CallJavascritp("showModalForm()");
        }
        private void CallJavascritp(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}