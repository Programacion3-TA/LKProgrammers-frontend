using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.Profesor
{
    public partial class ReporteAsistenciaAlumno : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
           if(!IsPostBack)
            {
                string dniAlumno = Request.QueryString["dniAlu"];
                string fechaIni = Request.QueryString["fechaIni"];
                string fechaFin = Request.QueryString["fechaFin"];

                LLenarInformacion(dniAlumno,fechaIni,fechaFin);

            }
        }
        protected void LLenarInformacion(string dniAlumno,string fechaIni,string fechaFin)
        {
            DateTime FechaIni = DateTime.Parse(fechaIni);
            DateTime FechaFin = DateTime.Parse(fechaFin);

            alumno alumno = daoServicio.listarAlumnosFiltro(dniAlumno).ToList().FirstOrDefault();
            List<asistencia> asistencias = daoServicio.listarAsitencias(dniAlumno).ToList();
            profesor profesor = (profesor)Session["Usuario"];

            NombeAlumnoTxtRep.Text = alumno.nombres + " " + alumno.apellidoPaterno + " " + alumno.apellidoMaterno;
            DniAlumnoTxtRep.Text = dniAlumno;
            GradoAlumnoTxtRep.Text = alumno.grado.ToString();
            TelefonoAlumnoTxtRep.Text = alumno.telefono;
            FechaIniReporteTxt.Text = fechaIni;
            FechaFinReporteTxt.Text = fechaFin;
            SalonAlumnoTxtRep.Text = ((int)Session["idsalon"]).ToString(); //debe ser el nombre del salon
            FechaActualTxtRep.Text = DateTime.Now.ToString().Split(' ')[0];
            NombreTutorTxtRep.Text = profesor.nombres + " " + profesor.apellidoPaterno + " " + profesor.apellidoMaterno;

            asistencias.RemoveAll(x => x.fechaHora < FechaIni || x.fechaHora > FechaFin);
            MostrarAsistencias(asistencias);
        }
        protected void MostrarAsistencias(List<asistencia> asistencias)
        {
            AsistenciaAlumnoGrid.DataSource = asistencias;
            AsistenciaAlumnoGrid.DataBind();
        }

        protected void RegresarAsistenciasBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx");
        }
    }
}