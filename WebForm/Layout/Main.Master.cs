using System;
using System.Collections.Generic;
using System.EnterpriseServices.CompensatingResourceManager;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;
using System.Text.RegularExpressions;

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
            }

            RenderizarPath();

            usuario _usuario = (usuario)Session["Usuario"];
            FotoPerfilAsp.ImageUrl = ObtenerFotoPerfil((string)Session["Tipo"], (char)_usuario.genero);
            string _tipo_usuario = (( (char) _usuario.genero ) == 'M')? (string)Session["Tipo"] : Regex.Replace((string)Session["Tipo"], "o$", "") + "a";
            nombreUsuarioLbl.Text = $"{_usuario.nombres} {_usuario.apellidoPaterno} <span class=\"badge text-bg-primary\">{_tipo_usuario}</span>";
        }

        protected string ObtenerFotoPerfil(string rol, char genero)
        {
            string url = "/Public/img/";
            switch (rol)
            {
                case "Alumno":
                    url += (genero == 'M')? "alumno.jpg" : "alumna.jfif";
                    break;
                case "Profesor":
                    url += (genero == 'M') ? "profesor.jfif" : "profesora.png";
                    break;
                case "Administrador":
                    url += (genero == 'M') ? "admin.jpg" : "admin.jpg";
                    break;
            }

            return url;
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
                { "Alumno", "<i style=\"color: #262626;\" class=\"fa-solid fa-graduation-cap\"></i>" },
                {"Profesor", "<i style =\"color: #262626;\" class=\"fa-solid fa-chalkboard-user\"></i>" },
                {"Admin", "<i style =\"color: #262626;\" class=\"fa-solid fa-user-tie\"></i>" },
                {"CalendarioAlumno", "<i style =\"color: #262626;\" class=\"fa-solid fa-calendar-days\"></i>" },
                {"AsistenciaAlumno", "<i style =\"color: #262626;\" class=\"fa-solid fa-timeline\"></i>" },
                {"NotaAlumno", "<i style =\"color: #262626;\" class=\"fa-solid fa-star\"></i>" },
                {"CursoAlumno", "<i style =\"color: #262626;\" class=\"fa-solid fa-book\"></i>" },
                {"AsistenciaProfesor", "<i style =\"color: #262626;\" class=\"fa-solid fa-timeline\"></i>" },
                {"CalificarProfesor", "<i style =\"color: #262626;\" class=\"fa-solid fa-star\"></i>" },
                {"CompetenciaProfesor", "<i style =\"color: #262626;\" class=\"fa-solid fa-box\"></i>" },
                {"RegistroProfesor", "<i style =\"color: #262626;\" class=\"fa-solid fa-paper-plane\"></i>" },
            };
            string Ruta = Request
                .Url
                .ToString()
                .Split('?')
                [0]
                .Split(new string[] { "View/" }, StringSplitOptions.None)[1];
            string[] Atribs = Ruta.Split('/');
            Atribs[Atribs.Length - 1] = Atribs[Atribs.Length - 1].Split('.')[0];
            string html = MyReact.CreateComponent("li", "class=\"breadcrumb-item\" style=\"color: #262626;\"",
                MyReact.CreateComponent("a", "href=\"#\" style=\"color: #262626; text-decoration: none;\"", "<i style=\"color: #262626;\" class=\"fa-solid fa-house fa-5xs me-2\"></i> Colegio"));
            foreach(string rut in Atribs)
            {
                string icono = (Relaciones.ContainsKey(rut)) ? Relaciones[rut] : "";
                html += MyReact.CreateComponent("li", "class=\"breadcrumb-item\" style=\"color: #525252;\"",
                    MyReact.CreateComponent("a", "href=\"#\" style=\"color: #262626; text-decoration: none;\"", $"{icono} {rut}")); ;
            }
            PathUsuariosLit.Text = html;
            // <li class="breadcrumb-item"><a href="#">Home</a></li>
        }
    }
}