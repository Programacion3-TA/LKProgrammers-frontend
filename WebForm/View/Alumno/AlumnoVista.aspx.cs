using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebForm.ServicioWS;
using System.Net;

namespace WebForm.View
{
    public partial class AlumnoVista : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();

            RenderizarCursos();
            
            if ((bool)Session["Confetti"])
            {
                Confetti.Text = "<script>(new JSConfetti()).addConfetti()</script>";
                Session["Confetti"] = false;

                string script = "window.onload = function() { showNotification('Info', 'Bienvenido!!! Ten un bonito día :D'); };";
                ClientScript.RegisterStartupScript(GetType(), "", script, true);
            }
        }

        protected void RenderizarCursos()
        {
            string[] PruebasColores = {
                "#6691FF",
                "#C6FF4E",
                "#FFDD66",
                "#FF6666"
            };
            Random rand = new Random();

            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            List<curso> cursos = (serviciodao.listarCursosPorAlumno(dni) ?? new curso[] { }).ToList();

            string html = "";
            foreach (curso cur in cursos)
            {

                // Falta modificar esto
                String nombreProfesor = "Heider";
                html += "" +
                    $"<a href=\"/View/Alumno/CursoAlumno.aspx\" class=\"d-flex flex-column cursoCaja\" style=\"text-decoration:none;\">" +
                    $"   <div class=\"h-50\" style=\"background-color:" + PruebasColores[rand.Next(0, PruebasColores.Length)] + "\"></div>" +
                    $"   <div class=\"p-2 infoCaja\">" +
                    $"       <p>{cur.nombre}</p>" +
                    $"       <div class=\"line\"></div>" +
                    $"       <p>Profesor: {nombreProfesor}</p>" +
                    $"       <p>Código curso: {cur.id}</p>" +
                    $"   </div>" +
                    $"</a>";
            }

            CursosContainer.Text = html;
        }
    }
}