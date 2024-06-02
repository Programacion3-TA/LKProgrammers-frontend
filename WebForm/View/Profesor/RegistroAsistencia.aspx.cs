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

        //Sesion idsalon,fechaEdicion,asistencias

        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack) {
                string idsalon = Request.QueryString["idsalon"];
                List<alumno> alumnos = daoServicio.listarAlumnosxsalon(int.Parse(idsalon)).ToList();


                //procedimiento cuando se realiza un registro
                if (!string.IsNullOrEmpty(idsalon) && Session["fechaEdicion"] == null)
                {
                    List<asistencia> asistencias = new List<asistencia>();


                    foreach (alumno alumno in alumnos)
                    {
                        asistencia asistencia = new asistencia();
                        asistencia.dniAlumno = alumno.dni;
                        asistencia.fechaHora = DateTime.Now;
                        asistencia.estado = estadoAsistencia.Presente;
                        asistencia.fechaHoraSpecified = true;
                        asistencia.estadoSpecified = true;

                        asistencias.Add(asistencia);
                    }
                    Session["asistencias"] = asistencias;

                }

                MostrarAlumnosSalon(alumnos);
            }

        }
        protected void MostrarAlumnosSalon(List<alumno> alumnos)
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
            RadioButtonList radioList = (RadioButtonList)sender; //accedo al radio que seleccione

            GridViewRow fila = (GridViewRow)(radioList.NamingContainer); //accedo a la fila

            HiddenField hfDniAlumno = (HiddenField)fila.FindControl("HiddenDniAlumno");//me permite obtener el valor oculto en la grid sada

            string dniAlu = hfDniAlumno.Value;
            string asistenciaEstado = radioList.SelectedValue;
            //buscaremos si se encuentra el alumno 

            List<asistencia> asistencias = (List<asistencia>)Session["asistencias"];
            asistencia asistenciaAlumno = asistencias.Find(x => dniAlu.Equals(x.dniAlumno));

            //si no se encuentra
            /*if (asistenciaAlumno == null)
            {
                //se crea nuevo
                asistenciaAlumno = new asistencia();
                asistenciaAlumno.dniAlumno = hfDniAlumno.Value;
            }
            else
            {
                // quitamos la asistencia
                asistencias.Remove(asistenciaAlumno);

            }*/
            asistencias.Remove(asistenciaAlumno);
            switch (asistenciaEstado)
            {
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

            asistenciaAlumno.estadoSpecified = true;
            asistencias.Add(asistenciaAlumno);
            Session["asistencias"] = asistencias;
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Session["asistencias"] = null;
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx");
        }

        protected void BtnGuardarAsistencia_Click(object sender, EventArgs e)
        {
            int asisHechas = 0;
            List<asistencia> asistencias = (List<asistencia>)Session["asistencias"];

            Session["asistencias"] = null; //reiniciamos

            if (Session["fechaEdicion"] == null)
            {
                foreach(asistencia asistencia_ in asistencias)
                {
                    //aveces no guarda todo : Comunications link failure
                    asisHechas += daoServicio.insertarAsistencia(asistencia_);
                }
                CallJavascript("showInsertModal('asistenRegistradasModal')");
            }
            else
            {
                foreach(asistencia asistencia_ in asistencias)
                {
                    asisHechas += daoServicio.modificarAsistencia(asistencia_);
                }
                CallJavascript("showUpdateModal('asistenRegistradasModal')");
            }


        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(),"", script, true);
        }


        //funcion de carga para cada fila de los ItemTample,  cuando se genere grid
        protected void GridAlumnos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //se realiza solo si se presiono el botono de edicion
            if (Session["fechaEdicion"] != null)
            {
                string fecha = (string)Session["fechaEdicion"];
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    RadioButtonList rdlAsistencias = (RadioButtonList)e.Row.FindControl("RadAsistencia"); //control de radiobutton de cada fila

                    string dni = DataBinder.Eval(e.Row.DataItem, "dni").ToString();  //accedo  a su valor id guardado
                    asistencia asistencia=daoServicio.listarAsistenciaFecha(dni, DateTime.Parse(fecha)).ToList().FirstOrDefault();

                    List<asistencia> asistencias = (List<asistencia>)Session["asistencias"];
                    asistencias.Add(asistencia);
                    Session["asistencias"] = asistencias;


                    string estado = asistencia.estado.ToString().Substring(0,1);


                    ListItem btnRadioSelect  = rdlAsistencias.Items.FindByValue(estado);
                    if(btnRadioSelect != null)
                    {
                        rdlAsistencias.ClearSelection();
                        btnRadioSelect.Selected = true;
                    }
                }
            }
        }
    }

}