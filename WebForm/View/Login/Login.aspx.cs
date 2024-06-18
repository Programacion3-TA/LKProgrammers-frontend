using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (Session["Error"] != null)
            {
                ErrorAlert.Text =
                    "<div class=\"alert alert-danger\" role=\"alert\">" +
                    "   <i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i>" +
                    $"  {Session["Error"] as string}" +
                    "</div>";
                Session["Error"] = null;
            }

            if (Session["Usuario"] == null || Session["Tipo"] == null)
                return;

            switch (Session["Tipo"] as string)
            {
                case "Administrador":
                    Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
                    break;
                case "Profesor":
                    Response.Redirect("/View/Profesor/ProfesorVista.aspx");
                    break;
                case "Alumno":
                    Response.Redirect("/View/Alumno/Alumno.aspx");
                    break;
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            //redireccionamiento de paginas dependiendo del usuario
            usuario user = (usuario)daoServicio.verificarUsuario(TxtUsuario.Text, TxtContrasenia.Text);
            //redireccionamiento de paginas dependiendo del usuario
            if (user == null) //NO ES NADIE ASI QUE EMITE UN ERROR
            {
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Session["Error"] = "El nombre de usuario y/o la contraseña fueron introducidos incorrectamente, o el usuario no existe";
                Response.Redirect("/View/Login/Login.aspx");
            }
            Session["Usuario"] = user;
            Session["Confetti"] = true;
            
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
                Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
            }
        }
    }
}