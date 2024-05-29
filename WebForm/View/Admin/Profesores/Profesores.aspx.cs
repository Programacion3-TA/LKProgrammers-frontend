using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using System.Globalization;

namespace WebForm.View.Admin.Profesores
{
    
    public partial class Profesores : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        private BindingList<profesor> listProfesor;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            CargarTabla();
        }

        private void CargarTabla()
        {
            listProfesor = new BindingList<profesor>(daoServicio.listarProfesores());
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
            /*
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            Profesor profesor = ListaProfesor.Find(x => x.Codigo == code);
            ListaProfesor.Remove(profesor);
            CargarTabla();*/
        }
        protected void ButGuardar_Click(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            profesor profesor = new profesor();
            BindingList<profesor> ListaProfesor = new BindingList<profesor>(daoServicio.listarProfesores());
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                profesor.codigoProfesor = ListaProfesor.Count()+1;
                profesor.nombres = TxtNombre.Text;
                profesor.apellidoPaterno = TxtApellidoPat.Text;
                profesor.apellidoMaterno = TxtApellidoMat.Text;
                profesor.especialidad = TxtEspecialidad.Text;
                profesor.direccion = TxtDireccion.Text;
                profesor.telefono =TxtTelefono.Text;
                profesor.genero =TxtGenero.Text[0];
                profesor.correoElectronico = TxtCorreo.Text;
                profesor.usuario1 = TxtUsername.Text;
                profesor.contrasenia = TxtPassword.Text;
                profesor.fechaNac=DateTime.ParseExact(TxtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                profesor.dni = TxtDNI.Text;
                //daoServicio.insertarprofesor(profesor);
                CargarTabla();
            }
            else //actualizar
            {
                profesor = ListaProfesor.ToList().Find(x => x.codigoProfesor == int.Parse(TxtCode.Text));
                profesor.nombres = TxtNombre.Text;
                profesor.apellidoPaterno = TxtApellidoPat.Text;
                profesor.apellidoMaterno = TxtApellidoMat.Text;
                profesor.especialidad = TxtEspecialidad.Text;
                profesor.direccion = TxtDireccion.Text;
                profesor.telefono = TxtTelefono.Text;
                profesor.genero = TxtGenero.Text[0];
                profesor.correoElectronico = TxtCorreo.Text;
                profesor.usuario1 = TxtUsername.Text;
                profesor.contrasenia = TxtPassword.Text;
                profesor.fechaNac = DateTime.ParseExact(TxtFechaNacimiento.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                profesor.dni = TxtDNI.Text;
             //   daoServicio.editarprofesor(profesor);
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