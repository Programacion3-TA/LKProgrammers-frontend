using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.ProfesorVista
{
    public partial class ProfesorVista : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (!IsPostBack)
            {
                cursoHorario[] cursosHorarios = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni) ?? new cursoHorario[]{};
                MostrarCursos(cursosHorarios);
            }
        }

        private void MostrarCursos(cursoHorario[] cursoHorarios)
        {
            string[] PruebasColores = {
        "#6691FF",
        "#C6FF4E",
        "#FFDD66",
        "#FF6666"
    };
            Random rand = new Random();
            string nombres = ((profesor)Session["Usuario"]).nombres + " " + ((profesor)Session["Usuario"]).apellidoPaterno;

            int pos = 0;
            if (cursoHorarios != null)
            {
                foreach (cursoHorario cursoHor in cursoHorarios)
                {
                    Panel cursoPanel = new Panel
                    {
                        CssClass = "card cajita",
                        //BackColor = System.Drawing.ColorTranslator.FromHtml(PruebasColores[rand.Next(0, PruebasColores.Length)])
                    };
                    cursoPanel.Controls.Add(new LiteralControl($"<div class=\"p-2 card-body\" style=\"background-color: {PruebasColores[rand.Next(0, PruebasColores.Length)]}\"></div>"));
                    cursoPanel.Controls.Add(new LiteralControl($"<div class=\"p-2 card-body\">"));
                    cursoPanel.Controls.Add(new LiteralControl($"<p>{cursoHor.curso.nombre}</p>"));
                    cursoPanel.Controls.Add(new LiteralControl("<div class=\"line mb-2\"></div>"));
                    cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Profesor: {nombres}</p>"));
                    cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Código curso: {cursoHor.curso.id}</p>"));
                    cursoPanel.Controls.Add(new LiteralControl($"<p class=\"card-text\">Salón: {cursoHor.idsalon}</p>"));
                    cursoPanel.Controls.Add(new LiteralControl($"<a class=\"btn btn-primary\" href=\"/View/Profesor/CursoProfesor.aspx?cursoId={cursoHor.curso.id}&cursoNombre={cursoHor.curso.nombre}\">Ver detalles</a>"));
                    //LinkButton cursoLink = new LinkButton
                    //{
                    //    ID = $"CursoProfesorBtn-{cursoHor.curso.id}-{pos++}",
                    //    CommandArgument = $"{cursoHor.curso.id}|{cursoHor.curso.nombre}",
                    //    Text = "Ver detalles",
                    //    CssClass = "btn btn-primary"
                    //};
                    //cursoLink.Click += new EventHandler(CursoProfesorBtn_Click);
                    //cursoPanel.Controls.Add(cursoLink);
                    cursoPanel.Controls.Add(new LiteralControl("</div>"));
                    CursosContainer.Controls.Add(cursoPanel);
                }
            }
        }


        protected void CursoProfesorBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string[] args = btn.CommandArgument.Split('|');
            string idCurso = args[0];
            string name_curso = args[1];
            Session["CURSO"] = int.Parse(idCurso);
            Session["Curname"] = name_curso;
            // Redirigir a la página cursosVista con los parámetros necesarios
            Response.Redirect("/View/Profesor/CursoProfesor.aspx");           
        }
    }
}
