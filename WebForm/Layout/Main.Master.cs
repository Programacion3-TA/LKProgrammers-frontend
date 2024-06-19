using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //no deja entrar a menos que sea miembro
            if (Session["Usuario"] == null || Session["Tipo"] == null)
                Response.Redirect("/View/Login/Login.aspx");
            
            if (Session["AnimacionInicio"] != null)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script>alert('A');</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script type=text/javascript> alert('Hello World!') </script>");
                Session["AnimacionInicio"] = null;
            }

            if (Session["MyNotification"] != null)
            {
                MyNotification notf = (MyNotification)Session["MyNotification"];
                Session["MyNotification"] = null;
                string script = "<script> showNotification(\"" + notf.Tipo + "\", \"" + notf.Mensaje+ "\", \""+notf.Titulo+"\") </script>";
                ScriptsNotification.Text = script;
                //Dictionary<string, string> StadoClase = new Dictionary<NotificationStates, string>
                //{
                //    {"Ok", "alert alert-success" },
                //    {"Bad", "alert alert-danger" },
                //    {"Info", "alert alert-primary" }
                //};
                //ErrorAlert.Text =
                //    $"<div class=\"{StadoClase[notf.Estado]} my__notification position-fixed \" style=\"z-index:999;\" role=\"alert\">" +
                //    "   <i class=\"fa fa-exclamation-triangle\" aria-hidden=\"true\"></i>" +
                //    $"  {notf.Mensaje}" +
                //    "</div>";
            }

            RenderizarPath();

            usuario _usuario = (usuario)Session["Usuario"];
            string _tipo_usuario = (string)Session["Tipo"];
            nombreUsuarioLbl.Text = $"{_usuario.nombres} {_usuario.apellidoPaterno} <span class=\"badge text-bg-primary\">{_tipo_usuario}</span>";
        }

        protected void CerrarSesionBtn_Click(object sender, EventArgs e)
        {
            Session["Tipo"] = null;
            Session["Usuario"] = null;
            Response.Redirect("/View/Login/Login.aspx"); 
        }

        protected void RenderizarPath()
        {
            Dictionary<string, string> Relaciones = new Dictionary<string, string>
            {
                {"Alumno", "<i class=\"fa-solid fa-graduation-cap\"></i>" },
                {"Profesor", "<i class=\"fa-solid fa-chalkboard-user\"></i>" },
                {"Admin", "<i class=\"fa-solid fa-user-tie\"></i>" },
                {"CalendarioAlumno", "<i class=\"fa-solid fa-calendar-days\"></i>" },
                {"AsistenciaAlumno", "<i class=\"fa-solid fa-timeline\"></i>" },
                {"NotaAlumno", "<i class=\"fa-solid fa-star\"></i>" },
                {"CursoAlumno", "<i class=\"fa-solid fa-book\"></i>" },
                {"AsistenciaProfesor", "<i class=\"fa-solid fa-timeline\"></i>" },
                {"CalificarProfesor", "<i class=\"fa-solid fa-star\"></i>" },
                {"CompetenciaProfesor", "<i class=\"fa-solid fa-box\"></i>" },
                {"RegistroProfesor", "<i class=\"fa-solid fa-paper-plane\"></i>" },
            };
            string Ruta = Request.Url.ToString().Split(new string[] { "View/" }, StringSplitOptions.None)[1];
            string[] Atribs = Ruta.Split('/');
            Atribs[Atribs.Length - 1] = Atribs[Atribs.Length - 1].Split('.')[0];
            string html = MyReact.CreateComponent("li", "class=\"breadcrumb-item\" style=\"color: #000;\"",
                MyReact.CreateComponent("a", "href=\"#\"", "<i class=\"fa-solid fa-house fa-5xs me-2\"></i> Colegio"));
            foreach(string rut in Atribs)
            {
                string icono = (Relaciones.ContainsKey(rut)) ? Relaciones[rut] : "";
                html += MyReact.CreateComponent("li", "class=\"breadcrumb-item\" style=\"color: #000;\"",
                    MyReact.CreateComponent("a", "href=\"#\" style=\"color: #000;", $"{icono}{rut}")); ;
            }
            PathUsuarios.Text = html;
            // <li class="breadcrumb-item"><a href="#">Home</a></li>
        }
    }
}