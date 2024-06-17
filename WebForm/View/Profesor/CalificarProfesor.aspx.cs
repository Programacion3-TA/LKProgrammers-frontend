using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Profesor
{
    public partial class CalificarProfesor : System.Web.UI.Page
    {
        public class CursoMostrar
        {
            public String cursoDescrip { get; set; }
            public String cursoIdent { get; set; }
        }

        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                cursoHorario[] cursosHorario = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni);
                Session["cursosHorario"] = cursosHorario;
                CargarCursos((cursoHorario[])Session["cursosHorario"]);
            }
        }


        protected void CargarCursos(cursoHorario[] cursoHorarios)
        {
           

            if (cursoHorarios != null)
            {
                List<cursoHorario> cursoHorarioList = cursoHorarios.ToList();

                List<CursoMostrar> mostrarCurso = new List<CursoMostrar>();
                foreach (cursoHorario cur in cursoHorarioList)
                {

                    CursoMostrar elem = new CursoMostrar();   
                    elem.cursoDescrip = cur.curso.nombre + " - salon:" + cur.idsalon; //lo usaremos para mostrar los nombres
                    elem.cursoIdent = cur.idsalon + "|" + cur.curso.id; //identificar al salon y los cursos
                    mostrarCurso.Add(elem);
                }
                CursoDictadoDrpL.DataSource = mostrarCurso;
                CursoDictadoDrpL.DataBind();
            }
        }

        protected void AsignarNotaBtn_Click(object sender, EventArgs e)
        {
            string[] Resultados = CursoDictadoDrpL.SelectedValue.Split('|');
            string salonId = Resultados[0];

            alumno[] alumnos = daoServicio.listarAlumnosxsalon(int.Parse(salonId));
            if(alumnos != null)
            {
                foreach (alumno alumno in alumnos)
                {
                    alumno.nombres += " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
                }
                CargarAlumnos(alumnos);
                CallJavascript("showModal('listarAlumnosModal')");
            }


        }
        protected void CargarAlumnos(alumno[] alumnos)
        {
            List<alumno> alumnosList = alumnos.ToList();
            GridAlumnos.DataSource= alumnosList;
            GridAlumnos.DataBind();
        } 

        protected void CursoDictadoDrpL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Resultados = CursoDictadoDrpL.SelectedValue.Split('|');
            string CursoId = Resultados[1];

            competencia[] competencias = daoServicio.listarCompetencias(int.Parse(CursoId));
            if(competencias != null)
            {
                CargarCompetencias(competencias);
            }
        }
        protected void CargarCompetencias(competencia[] competencias)
        {
            List<competencia> competenciasList= competencias.ToList();
            CompetenciaCursosGrid.DataSource = competenciasList;
            CompetenciaCursosGrid.DataBind();
        }

        protected void CompetenciaCursosGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            CompetenciaCursosGrid.PageIndex = e.NewPageIndex;
            CompetenciaCursosGrid.DataBind();
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}