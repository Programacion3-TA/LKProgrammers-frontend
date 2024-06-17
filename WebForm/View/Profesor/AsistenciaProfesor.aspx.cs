using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;


using System.IO;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            int idsalon = (int)Session["idsalon"] ;
            if (!IsPostBack) {
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                Session["errorFechas"] = false;
                profesor profesor = (profesor)Session["Usuario"];
                //cuando cargue, ya se verifico si es tutor 

                if (idsalon != -1)
                {
                    CargarFechas(idsalon);
                    List<alumno> alumnos = new List<alumno>();
                    var f = daoServicio.listarAlumnosxsalon(idsalon);
                    if(f != null)alumnos = f.ToList();
                    Session["alumnosAsistencia"] = alumnos;
                    Session["RealizoAsistenica"] = VerificarRegistroAsistenciaActual();
                    CargarAlumnosDropDown();
                }
                else
                {
                    Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                }

                /*
                if (Session["idsalon"] == null)
                {
                    idsalon = daoServicio.esTutorAsignado(profesor.dni);
                    if (idsalon == -1) Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                    Session["idsalon"] = idsalon;

                }
                if (Session["idsalon"] != null)
                {
                    idsalon = (int)Session["idsalon"];
                    CargarFechas(idsalon);
                    Session["RealizoAsistenica"]= VerificarRegistroAsistenciaActual();
                }*/

            }
            CargarFechas(idsalon);
            /*if ((bool)Session["errorFechas"])
            {
                CallJavascript("showModal('fechasReporteModal')");
            }*/

        }

        protected void CargarAlumnosDropDown()
        {
            List<alumno> alumnos = (List<alumno>)Session["alumnosAsistencia"];
            foreach(alumno alu in alumnos)
            {
                alu.nombres += " " + alu.apellidoPaterno + " " + alu.apellidoMaterno;
            }
            
            AlumnosDrpDown.DataSource = alumnos;
            AlumnosDrpDown.DataBind();
        }

        protected void BtnNiegaRegistros_Click(object sender, EventArgs e)
        {
            CallJavascript("showModal('bloqueoRegistroModal')");
        }

        protected void CargarFechas(int _idsalon)
        {
            List<DateTime> fechas = new List<DateTime>();
            var f = daoServicio.listarFechasAsistenciaSalon(_idsalon);
            List<string> fechasFormato = new List<string>();
            List<object> fechasconFormato = new List<object>();
            if (f != null)
            {
                fechas = f.ToList();
                fechasFormato = TransformarFechas(fechas);
                fechasconFormato = new List<object>();
                //llenamos la lista de objetos
                foreach (DateTime fecha in fechas)
                {
                    object key = new { Fecha = fecha.Date, FechaFormato = fechasFormato[fechas.IndexOf(fecha)] };
                    fechasconFormato.Add(key);
                }

                Session["fechas"] = fechas;
                //se impleemnto para que funcione el filtrado -> mejorar
                Session["fechasFormato"] = fechasFormato;
                Session["fechasconFormato"] = fechasconFormato;
            }
            GridAsistenciasFechas.DataSource = fechasconFormato; //verificar el Datafield
            GridAsistenciasFechas.DataBind();
        }
        protected List<string> TransformarFechas(List<DateTime> fechas)
        {
            List<string> formatoFechas = new List<string>();
            CultureInfo culture = new CultureInfo("es-ES");
            foreach(DateTime _fecha in fechas)
            {
                string formato = _fecha.ToString("dddd d 'de' MMMM 'del' yyyy", culture);
                formatoFechas.Add(formato);
            }
            return formatoFechas;
        }

        protected void BtnRegistrarAsistencia_Click(object sender, EventArgs e)
        {
            int idsalon = (int)Session["idsalon"];

            if (!((bool)Session["RealizoAsistenica"]))
            {
                Session["fechaEdicion"] = null;
                Response.Redirect("/View/Profesor/RegistroAsistencia.aspx?idsalon="+idsalon);
            }
            else
            {
                CallJavascript("showModal('bloqueoRegistroModal')");
            }
        }

        
        protected bool VerificarRegistroAsistenciaActual()
        {
            DateTime fechaHoy = DateTime.Now.Date;//comparamos fechas
            List<DateTime> fechasReg = (List<DateTime>)Session["fechas"];
            return fechaHoy.Equals(fechasReg[0].Date);
        }

        protected void BtnCerrarModal_Click(object sender, EventArgs e)
        {
           
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void editarAsistencia_Click(object sender, EventArgs e)
        {
            int idsalon = (int)Session["idsalon"];
            Button btn = (Button)sender;
            string fecha = btn.CommandArgument;
            Session["fechaEdicion"] = fecha; //editaremos los registros de esta fecha
            Session["asistencias"] = new List<asistencia>();
            Response.Redirect("/View/Profesor/RegistroAsistencia.aspx?idsalon=" + idsalon);
        }

        protected void FiltrarMesBtn_Click(object sender, EventArgs e)
        {
            //talvez deberia estar en el backend -> PASARLO

            string mes = MesesDropDown.Text;
            List<object> fechasconFormato;
            if(!mes.Equals(""))
            {
                List<DateTime> fechas = (List<DateTime>)Session["fechas"];
                List<string> fechasFormato = (List<string>)Session["fechasFormato"];

                fechasconFormato = new List<object>();

                //llenamos la lista de objetos
                foreach (DateTime fecha in fechas)
                {
                    if (fechasFormato[fechas.IndexOf(fecha)].Contains(mes))
                    {
                        object key = new { Fecha = fecha.Date, FechaFormato = fechasFormato[fechas.IndexOf(fecha)] };
                        fechasconFormato.Add(key);
                    }
                }
                Session["fechasconFormato"] = fechasconFormato;
            }
            else
            {
                fechasconFormato = (List<object>)Session["fechasconFormato"];
            }
            GridAsistenciasFechas.DataSource = fechasconFormato;
            GridAsistenciasFechas.DataBind();


        }

        protected void AsistenciasAlumnoBtn_Click(object sender, EventArgs e)
        {



            if (!string.IsNullOrEmpty(FechaFinalTxt.Text) && !string.IsNullOrEmpty(FechaIniTxt.Text) )
            {
                DateTime fechaInicialDate = DateTime.Parse(FechaIniTxt.Text).Date;
                DateTime fechaFinalDate = DateTime.Parse(FechaFinalTxt.Text).Date;

                if(VerificarFechas(fechaInicialDate,fechaFinalDate))
                Response.Redirect("/View/Profesor/ReporteAsistenciaAlumno.aspx?dniAlu="+AlumnosDrpDown.Text+"&fechaIni="+fechaInicialDate.ToString()+"&fechaFin="+fechaFinalDate.ToString());
            }
            
        }
        protected bool VerificarFechas(DateTime fechaIni,DateTime fechaFin)
        {
            return fechaIni < fechaFin;
        }

        protected void CerrarModalIncidenciaBtn_Click(object sender, EventArgs e)
        {

        }

      
        protected void SalirFechasBtn_Click(object sender, EventArgs e)
        {
        }

        protected void GridAsistenciasFechas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            List<object> fechaFormato = (List<object>)Session["fechasconFormato"];
            GridAsistenciasFechas.DataSource = fechaFormato;    
            GridAsistenciasFechas.PageIndex = e.NewPageIndex;
            GridAsistenciasFechas.DataBind();
        }
    }
}