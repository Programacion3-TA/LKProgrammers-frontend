using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.Profesor
{
    public partial class RegistroAsistencia : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;

        protected void Page_Load(object sender, EventArgs e)
        {
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack) {

                int idsalon = (int)Session["idsalon"];     
                
                List<asistencia> asistencias = new List<asistencia>();
                List<alumno> alumnos = daoServicio.listarAlumnosxsalon(idsalon).ToList();

                //lleno los alumnos con asistencia presente
                foreach(alumno alumno in alumnos)
                {
                    asistencia asistencia = new asistencia();
                    asistencia.dniAlumno = alumno.dni;
                    asistencia.fechaHora = DateTime.Now;
                    asistencia.estado = estadoAsistencia.Presente;

                    asistencias.Add(asistencia);
                }

                Session["asistencias"] = asistencias;
                //identificar si se hizo la asistencia
                mostrarAlumnosSalon(alumnos);
                

            }

        }
        protected void mostrarAlumnosSalon(List<alumno> alumnos)
        {
            
            //por ahora para mostrar el nombre completo
            foreach(alumno alu in alumnos)
            {
                alu.nombres = alu.nombres + " " + alu.apellidoPaterno + " " + alu.apellidoMaterno;
            }

            GridAlumnos.DataSource = alumnos;
            GridAlumnos.DataBind();

        }

        protected void RadAsistencia_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            RadioButtonList radioList = (RadioButtonList)sender; //radioQue disparo

            GridViewRow fila = (GridViewRow)(radioList.NamingContainer);

            HiddenField hfDniAlumno = (HiddenField)fila.FindControl("HiddenDniAlumno");

            string dniAlu = hfDniAlumno.Value;
            string asistenciaEstado = radioList.SelectedValue;
            //buscaremos si se encuentra el alumno 

            List<asistencia> asistencias = (List<asistencia>)Session["asistencias"];
            asistencia asistenciaAlumno = asistencias.Find(x => dniAlu.Equals(x.dniAlumno));

            //si no se encuentra
            if(asistenciaAlumno == null)
            {
                //se crea nuevo
                asistenciaAlumno = new asistencia();
                asistenciaAlumno.dniAlumno = hfDniAlumno.Value;
            }
            else
            {
                // quitamos la asistencia
                asistencias.Remove(asistenciaAlumno);

            }
            asistenciaAlumno.fechaHora = DateTime.Now;

            switch (asistenciaEstado) {
                case "P":
                    asistenciaAlumno.estado = estadoAsistencia.Presente;
                    break;
                case "T":
                    asistenciaAlumno.estado = estadoAsistencia.Tardanza;
                    break;
                case "A":
                    asistenciaAlumno.estado = estadoAsistencia.Ausente;
                    break;
            }

            asistencias.Add(asistenciaAlumno);
            Session["asistencias"] = asistencias;
            
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx"); 
        }

        protected void BtnGuardarAsistencia_Click(object sender, EventArgs e)
        {
            //registramos todo en la base de datos
            List<asistencia> asistencias = (List<asistencia>)Session["asistencias"];
            asistencia[] asistenciaArray = asistencias.ToArray();

            daoServicio.insertarTodasAsistencias(asistenciaArray);
            
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx");
        }

         
    }

}