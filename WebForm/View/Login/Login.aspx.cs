using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
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
            usuario user = (usuario)daoServicio.verificarUsuario(TxtUsuario.Text, TxtContrasenia.Text);
            //redireccionamiento de paginas dependiendo del usuario
            if (user == null) //NO ES NADIE ASI QUE EMITE UN ERROR
            {
                //implementar estilos de errores con JS
                
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Response.Redirect("/View/Login/Login.aspx");
            }
            Session["Usuario"] = user;
            if (user is profesor) //ES EL PROFESOR
            {
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/ProfesorVista.aspx");
            }
            else if (user is alumno)//ES EL ESTUDIANTE
            {
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            else if (user is personalAdministrativo) //ADMINISTRATIVO
            {
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/Profesores/Profesores.aspx");
            }
        }
    }
}