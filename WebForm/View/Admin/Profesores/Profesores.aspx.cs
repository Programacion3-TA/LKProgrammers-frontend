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
        private ServicioWS.LKServicioWebClient daoservicio;
        private BindingList<profesor> profesores;
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
                string tipo = Session["Tipo"] as string; // Verifico el tipo
                if (tipo != "Administrador") Response.Redirect("/View/Login/Login.aspx");
                usuario usuario_actual = Session["Usuario"] as usuario;
                                
            }

            CargarTabla();
        }
        
        private void CargarTabla()
        {
            profesores = new BindingList<profesor> (daoservicio.listarProfesores().ToList());
            
            GridProfesores.DataSource = profesores;
            GridProfesores.DataBind();
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {
            
            Button btn = (Button)sender;
            string code = btn.CommandArgument; // recibo el codigo del profesor

            profesor profe = profesores.ToList().Find(x => x.codigoProfesor == Int32.Parse(code));
            // Busco y recupero los datos del profe
            TxtCode.Text = profe.codigoProfesor.ToString();
            TxtNombre.Text = profe.nombres;
            TxtApellidoPat.Text = profe.apellidoPaterno;
            TxtApellidoMat.Text = profe.apellidoMaterno;
            TxtEspecialidad.Text = profe.especialidad;
            CallJavascritp("showModalForm()");
        }
        protected void DelRow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            profesor profe = profesores.ToList().Find(x => x.codigoProfesor == Int32.Parse(code));
            
            // Eliminar profesor. Ahora mismo no funciona
            profesores.Remove(profe);            
            daoServicio = new LKServicioWebClient();
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            daoServicio.eliminarProfesor(int.Parse(code));
            CargarTabla();
        }

        protected void ButGuardar_Click(object sender, EventArgs e)
        {            
            profesor profe = new profesor();
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear si es que no tiene codigo (es nuevo)
            {
                // Insertamos un nuevo profesor

                profe.nombres = TxtNombre.Text;
                profe.apellidoPaterno = TxtApellidoPat.Text;
                profe.apellidoMaterno = TxtApellidoMat.Text;
                profe.especialidad = TxtEspecialidad.Text;
                // Agregar profesor
                rofesores.Add(profe);
              /*
                profesor.codigoProfesor = ListaProfesor.Count() + 1;
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
                profesor.fechaNac= DateTime.Parse(TxtFechaNacimiento.Text);
                profesor.fechaNacSpecified = true;
                profesor.dni = TxtDNI.Text;
                daoServicio.insertarprofesor(profesor);*/
                CargarTabla();
            }
            /*else //actualizar
            {
                profesor = ListaProfesor.Find(x => x.Codigo == TxtCode.Text);
                profesor.Nombre = TxtNombre.Text;
                profesor.ApellidoMat= TxtApellidoMat.Text;
                profesor.ApellidoPat = TxtApellidoPat.Text;
                profesor.Especialidad = TxtEspecialidad.Text;
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
                profesor.fechaNac = DateTime.Parse(TxtFechaNacimiento.Text);
                profesor.fechaNacSpecified = true;
                profesor.dni = TxtDNI.Text;
                daoServicio.editarProfesor(profesor);
                CargarTabla();
            }
            Response.Redirect(Request.Url.AbsoluteUri);*/
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