using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using WebForm.ServicioWS;
using System.ComponentModel;
using System.Net;

namespace WebForm.View
{
    public partial class Alumno : System.Web.UI.Page
    {
        private BindingList<curso> cursos;
        private ServicioWS.LKServicioWebClient serviciodao;
        protected void Page_Load(object sender, EventArgs e)
        {
            string[] PruebasColores = {
                "#6691FF",
                "#C6FF4E",
                "#FFDD66",
                "#FF6666"
            };
            Random rand = new Random();
            StringBuilder sb = new StringBuilder();

            serviciodao = new ServicioWS.LKServicioWebClient();
            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            curso[] curList = serviciodao.listarCursosPorAlumno(dni);

            if (curList == null) return;

            foreach (curso cur in curList)
            {
                // Falta modificar esto
                String nombreProfesor = "Heider";
                sb.Append("" +
                    $"<a href=\"/View/Alumno/CursoAlumno.aspx\" class=\"d-flex flex-column cursoCaja\" style=\"text-decoration:none;\">" +
                    $"   <div class=\"h-50\" style=\"background-color:" + PruebasColores[rand.Next(0, PruebasColores.Length)] + "\"></div>" +
                    $"   <div class=\"p-2 infoCaja\">" +
                    $"       <p>{cur.nombre}</p>" +
                    $"       <div class=\"line\"></div>" +
                    $"       <p>Profesor: {nombreProfesor}</p>" +
                    $"       <p>Código curso: {cur.id}</p>" +
                    $"   </div>" +
                    $"</a>"
                    );

                //+
                //$"<div class=\"card\" style=\"width: 18rem;\">" +
                //$"   <div class=\"h-50\" style=\"background-color:" + PruebasColores[rand.Next(0, PruebasColores.Length)] + "\"></div>" +
                //$"  <div class=\"card-body\">" +
                //$"      <h5 class=\"card-title\">{cur.nombre} - {cur.id}</h5>" +
                //$"      <p class=\"card-text\">Profesor: {nombreProfesor}</p>" +
                //$"      <a href = \"#\" class=\"btn btn-primary\">Go somewhere</a>" +
                //$"  </div>" +
                //$"</div>"
            }

            CursosContainer.Text = sb.ToString();
            if ((bool)Session["Confetti"])
            {
                Confetti.Text = "<script>(new JSConfetti()).addConfetti()</script>";
                Session["Confetti"] = false;
            }
        }
    }
}