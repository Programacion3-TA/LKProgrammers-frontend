using System;
using System.Collections.Generic;
using System.Linq;
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
                
                cursoHorario[] cursosHorarios = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni) ?? new cursoHorario[]{ };
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

            StringBuilder strBuild = new StringBuilder();
            string nombres = ((profesor)Session["Usuario"]).nombres + " "+((profesor)Session["Usuario"]).apellidoPaterno;
            if (cursoHorarios != null)
            {
                foreach (cursoHorario cursoHor in cursoHorarios)
                {
                    LinkButton linkButton = new LinkButton();   
                    linkButton.Click += new EventHandler(CursoProfesorBtn_Click);
                    linkButton.ID = $"CursoProfesorBtn-{cursoHor.curso.id}";
                    linkButton.CssClass = "d-flex flex-column cursoCaja";
                    linkButton.CommandArgument = $"{cursoHor.idsalon}|{cursoHor.curso.id}"; // se manda como argumento
                    linkButton.Controls.Add(new LiteralControl($"<div class=\"h-50\" style=\"background-color:" + PruebasColores[rand.Next(0, PruebasColores.Length)] + "\"></div>"));
                    linkButton.Controls.Add(new LiteralControl($"<div class=\"p-2 infoCaja\">"));
                    linkButton.Controls.Add(new LiteralControl($"<p>{cursoHor.curso.nombre}</p>"));
                    linkButton.Controls.Add(new LiteralControl("<div class=\"line\"></div>"));
                    linkButton.Controls.Add(new LiteralControl($"<p>Profesor: {nombres}</p>"));
                    linkButton.Controls.Add(new LiteralControl($"<p>Código curso: {cursoHor.curso.id}</p>"));
                    linkButton.Controls.Add(new LiteralControl($"<p>Salón: {cursoHor.idsalon}</p>")); // Cerrando el <p> correctamente
                    linkButton.Controls.Add(new LiteralControl("</div>"));
                    CursosProfesorPHl.Controls.Add(linkButton);
                }
            }
        }
        protected void CursoProfesorBtn_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string[]args = btn.CommandArgument.Split('|');
        }
    }
}