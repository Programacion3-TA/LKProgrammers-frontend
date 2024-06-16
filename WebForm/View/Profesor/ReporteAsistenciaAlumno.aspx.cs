using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using WebForm.ServicioWS;
using Microsoft.SqlServer.Server;
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
            GradoAlumnoTxtRep.Text = TransformarGrado(alumno.grado.ToString());
            TelefonoAlumnoTxtRep.Text = alumno.telefono;
            SalonAlumnoTxtRep.Text = ((int)Session["idsalon"]).ToString(); //debe ser el nombre del salon
            FechaActualTxtRep.Text = DateTime.Now.ToString().Split(' ')[0];
            FechaIniReporteTxt.Text = fechaIni.Split(' ')[0];
            FechaFinReporteTxt.Text = fechaFin.Split(' ')[0];
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

        protected void MostrarReporteBtn_Click(object sender, EventArgs e)
        {
            string dniAlumno = DniAlumnoTxtRep.Text;
            string nombreAlumno = NombeAlumnoTxtRep.Text;
            string grado = GradoAlumnoTxtRep.Text;
            string numeroTelefono = TelefonoAlumnoTxtRep.Text;
            string tutor = NombreTutorTxtRep.Text;
            string salon = SalonAlumnoTxtRep.Text;
            string fechaIni = FechaIniReporteTxt.Text;
            string fechaFin = FechaFinReporteTxt.Text;


            Byte[] FileBuffer = daoServicio.reportePDFAsistencias(dniAlumno,nombreAlumno,grado,numeroTelefono,tutor,salon,fechaIni,fechaFin);
            if (FileBuffer != null)
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-length", FileBuffer.Length.ToString());
                Response.BinaryWrite(FileBuffer);
            }

        }
        protected string TransformarGrado(string grado)
        {
            switch (grado)
            {
                case "INI2":
                    return "Segundo de Inicial";
                case "INI3":
                    return "Tercero de Inicial";
                case "INI4":
                    return "Cuarto de Inicial";
                case "INI5":
                    return "Quinto de Inicial";
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