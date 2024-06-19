using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Alumno
{
    public partial class NotaAlumno : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            RenderizarCursos();
        }
        protected void RenderizarCursos()
        {
            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            List<curso> cursos = (serviciodao.listarCursosPorAlumno(dni) ?? new curso[] { }).ToList();
            GridCursosNotas.DataSource = cursos;
            GridCursosNotas.DataBind();
        }

        protected void BtnVerNotas__Click(object sender, EventArgs e)
        {
            alumno alum = (alumno)Session["Usuario"];
            string dni = alum.dni;
            int cursoId = int.Parse(((Button)sender).CommandArgument);
            List<nota> notas = (serviciodao.listarNotasAlumnoCurso(dni, cursoId) ?? new nota[] { }).ToList();
            GridViewNotasAlumno.DataSource = notas;
            GridViewNotasAlumno.DataBind();

            double sumaParcial = 0, sumaFaltante = 0, pesos = 0;
            foreach(nota n in notas)
            {
                pesos += n.competencia.peso;
                sumaParcial += n.calificacion * n.competencia.peso;
                sumaFaltante += (20 - n.calificacion) * n.competencia.peso;
            }
            CallJavaScript("graficarPie([" + (sumaParcial/pesos)  + ", " + (sumaFaltante/pesos) + "]); mostrarModal('NotasCursoModal')");
        }

        protected void BtnGenerarPdf__Click(object sender, EventArgs e)
        {
            // Diego
        }

        protected void CallJavaScript(string func)
        {
            string script = "window.onload = function() {" + func + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}