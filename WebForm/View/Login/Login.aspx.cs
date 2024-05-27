using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Models;
using WebForm.ServicioWS;

namespace WebForm.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (Session["Usuario"] != null && Session["Tipo"] != null)
            {
                Response.Redirect("/View/" + Session["Tipo"] + "/" + Session["Tipo"] + ".aspx");
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
          //  Usuario usuario = usuarios.Find(pro => (pro.Username == TxtUsuario.Text || pro.Correo == TxtUsuario.Text) && pro.Password == TxtContrasenia.Text);
            List<usuario> usuarios = daoServicio.listarUsuarios().ToList();
            usuario user = usuarios.Find(x => (x.usuario1 == TxtUsuario.Text || x.correoElectronico == TxtUsuario.Text) && x.contrasenia == TxtContrasenia.Text);

            //redireccionamiento de paginas dependiendo del usuario
            if(user == null)
            {
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                string script = "alert('Usuario o contraseña incorrectos');";
                ClientScript.RegisterStartupScript(this.GetType(), "AlertScript", script, true);
            }

            Session["Usuario"] = user;
            //Session["AnimacionInicio"] = true; webada de fidel
            if(user is profesor)
            {
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/CursoProfesor.aspx");
            }
            //rayita estuvo aqui, 
            if (user is alumno)
            {
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            if(user is personalAdministrativo)
            {
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/Profesores/Profesores.aspx");
            }
        }
    }
}