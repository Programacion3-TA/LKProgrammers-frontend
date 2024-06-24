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
        private LKServicioWebClient serviciodao;
        private int cod_salon;
        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            RenderizarCursos();
            if (!IsPostBack && Session["salonAlumno"] == null)
            {
                alumno alu = (alumno)Session["Usuario"];
                salon salonAlumno = serviciodao.obtenerSalonxAlumno(alu.dni);
                Session["salonAlumno"] = salonAlumno;
            }

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
            cod_salon = serviciodao.obtenerSalonxAlumno(alum.dni).id;
            string dni = alum.dni;
            List<curso> cursos = (serviciodao.listarCursosPorAlumno(dni) ?? new curso[] { }).ToList();
            foreach (curso cur in cursos)
            {
                profesor profe = serviciodao.buscarProfesorDeCurso(cur.id);
                if (profe == null) continue;
                string nombreProfesor = profe.nombres+" "+profe.apellidoPaterno + " "+profe.apellidoMaterno;
                Panel cursoPanel = new Panel
                {
                    CssClass = "card"
                    //BackColor = System.Drawing.ColorTranslator.FromHtml(PruebasColores[rand.Next(0, PruebasColores.Length)])
                };
                cursoPanel.Controls.Add(new LiteralControl($"<div class=\"p-2 card-body\" style=\"background-color: {PruebasColores[rand.Next(0, PruebasColores.Length)]}\">"));
                cursoPanel.Controls.Add(new LiteralControl($"</div>"));
                cursoPanel.Controls.Add(new LiteralControl($"<div class=\"p-2 card-body\">"));
                cursoPanel.Controls.Add(new LiteralControl($"<h5 class=\"card-title\">{cur.nombre}</h5>"));
                cursoPanel.Controls.Add(new LiteralControl("<div class=\"line mb-2\"></div>"));
                cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Profesor: {nombreProfesor}</p>"));
                cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Código curso: {cur.id}</p>"));
                cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Salón: {cod_salon}</p>"));
                LinkButton cursoLink = new LinkButton
                {
                    ID = $"CursoAlumnoBtn-{cur.id}",
                    CommandArgument = $"{cur.nombre}|{cur.id}",
                    Text = "Ver detalles",
                    CssClass = "btn btn-primary"
                };
                cursoLink.Click += new EventHandler(Curso_AlumnoBTN_click);
                cursoPanel.Controls.Add(cursoLink);
                cursoPanel.Controls.Add(new LiteralControl("</div>"));
                CursosContainer.Controls.Add(cursoPanel);
            }
        }
        protected void Curso_AlumnoBTN_click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string[] args = btn.CommandArgument.Split('|');
            string name_curso = args[0];
            string idCurso = args[1];
            Session["CURSO"] = int.Parse(idCurso);
            Session["Curname"] = name_curso;
            // Redirigir a la página cursosVista con los parámetros necesarios
            Response.Redirect("/View/Alumno/CursoAlumno.aspx");
        }
    }
}