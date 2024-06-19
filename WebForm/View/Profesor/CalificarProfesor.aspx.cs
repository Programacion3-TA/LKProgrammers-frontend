using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
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
            //if (Session["CalificarProfesorNoti"] != null)
            //{
            //    CallJavascript("showNotification('Ok', mensaje='Se actualizaron las notas con éxito', titulo = 'Asistencias registrada!')");
            //    Session["CalificarProfesorNoti"] = null;
            //}
            if (!IsPostBack)
            {
                cursoHorario[] cursosHorario = daoServicio.listarCursosPorProfesor(((profesor)Session["Usuario"]).dni);
                Session["cursosHorario"] = cursosHorario;
                CargarCursos((cursoHorario[])Session["cursosHorario"]);
            }
        }


        protected void CargarCursos(cursoHorario[] cursoHorarios)
        {
           

            if (cursoHorarios == null) return;

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

        protected void AsignarNotaBtn_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string idCompetencia = button.CommandArgument;
            string[] Resultados = CursoDictadoDrpL.SelectedValue.Split('|');
            string salonId = Resultados[0];
            Response.Redirect("/View/Profesor/RegistroNotas.aspx?idsalon="+salonId+"&idcompetencia="+idCompetencia);
        }

        protected void CursoDictadoDrpL_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] Resultados = CursoDictadoDrpL.SelectedValue.Split('|');
            string CursoId = Resultados[1];
            string salonId = Resultados[0];
            alumno[] alumnos = daoServicio.listarAlumnosxsalon(int.Parse(salonId));
            competencia[] competencias = daoServicio.listarCompetencias(int.Parse(CursoId)) ?? new competencia[] { };
            CargarCompetencias(competencias);
            CargarDropAlumnos(alumnos);
        }
        protected void CargarDropAlumnos(alumno[] alumnos)
        {
            if(alumnos != null)
            {
                List<alumno> alumnosList = TransformarNombres(alumnos);
                AlumnosDelCursoDrpl.DataSource = alumnosList; 
                AlumnosDelCursoDrpl.DataBind();
                Session["AlumnosCurso"] = alumnosList;
            }
        }
        protected List<alumno> TransformarNombres(alumno[] alus)
        {
            foreach (alumno alumno in alus)
            {
                alumno.nombres += " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
            }
            return alus.ToList();
        }

        protected void CargarCompetencias(competencia[] competencias)
        {
            List<competencia> competenciasList= competencias.ToList();
            CompetenciaCursosGrid.DataSource = competenciasList;
            CompetenciaCursosGrid.DataBind();

            int i = 1;
            string formula = "\\[ \\text{Nota final} =  ";
            foreach (competencia comp in competencias)
            {
                formula += $"{comp.peso} \\times "+"N_{"+ i +"} +";
                i++;
            }
            formula = formula.Substring(0,formula.Length - 2);
            formula += " \\]";
            FormulaPesos.Text = formula;
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

        protected void ReporteNotasAlumnoBtn_Click(object sender, EventArgs e)
        {
            string dniAlumno = AlumnosDelCursoDrpl.SelectedValue.ToString();
            alumno alumno = daoServicio.listarAlumnosFiltro(dniAlumno).FirstOrDefault();
            if (alumno != null)
            {
                alumno.nombres += " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
                string grado = TransformarGrado(alumno.grado.ToString());
                profesor prof = (profesor)(Session["Usuario"]);
                string nombre = prof.nombres;
                nombre += " " + prof.apellidoPaterno + " " + prof.apellidoMaterno;
                string idsalon = ((int)Session["idsalon"]).ToString();

                Byte[] FileBuffer = daoServicio.reportePDFNotas(dniAlumno,alumno.nombres,grado,alumno.telefono,nombre,idsalon);
                if (FileBuffer != null)
                {
                    Response.Clear();
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-length", FileBuffer.Length.ToString());
                    Response.BinaryWrite(FileBuffer);
                }

            }

        }
        protected string TransformarGrado(string grado)
        {
            switch (grado)
            {
                case "INI2":
                    return "2 años";
                case "INI3":
                    return "3 años";
                case "INI4":
                    return "4 años";
                case "INI5":
                    return "5 años";
                case "PRIM1":
                    return "Primero de Primaria";
                case "PRIM2":
                    return "Segundo de Primaria";
                case "PRIM3":
                    return "Tercero de Primaria";
                case "PRIM4":
                    return "Cuarto de Primaria";
                case "PRIM5":
                    return "Quinto de Primaria";
                case "PRIM6":
                    return "Sexto de Primaria";
                default:
                    return "Fallo en el grado";
            }
        }
    }

   
}