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
using WebForm.Utils;
using System.ComponentModel;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private List<DateTime> listaFechasEntero;
        private List<DateTime> listaFechasFiltrado;
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            listaFechasEntero = new List<DateTime>();
            listaFechasFiltrado = new List<DateTime>();
            // MesesDropDown.DataSource;

            int idsalon = (int)Session["idsalon"] ;
            if (!IsPostBack)
            {
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                Session["errorFechas"] = false;
                // profesor profesor = (profesor)Session["Usuario"];
                //cuando cargue, ya se verifico si es tutor 

                if (idsalon == -1) Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                List<alumno> alumnos = (daoServicio.listarAlumnosxsalon(idsalon) ?? new alumno[] { }).ToList();
                ModificarNombres(alumnos);
                Session["alumnosAsistencia"] = alumnos;
                Session["fechasAsistencia"] = (daoServicio.listarFechasAsistenciaSalon(idsalon) ?? new DateTime[] { }).ToList();
                Session["RealizoAsistenica"] = VerificarRegistroAsistenciaActual();
                Session["asistenciasSinJustificar"] = (daoServicio.listarAsistenciasSinJustificar(idsalon) ?? new asistencia[]{}).ToList();
            }
            CargarCantidad();
            CargarAlumnosDropDown((List<alumno>)Session["alumnosAsistencia"]);
            FiltrarMesBtn_Click(sender, e);
        }
        protected void CargarCantidad()
        {
            List<asistencia> asisSinJustificar = (List<asistencia>)Session["asistenciasSinJustificar"];
            if (asisSinJustificar.Count == 0) {
                CantidadJusitficacionesLbl.Text = "";
                return;
            }
            CantidadJusitficacionesLbl.Text = asisSinJustificar.Count.ToString();
        }
        protected void CargarAlumnosDropDown(List<alumno> alumnos)
        {
            AlumnosDrpDown.DataSource = alumnos;
            AlumnosDrpDown.DataBind();
        }
        protected void ModificarNombres(List<alumno> alumnos)
        {
            foreach (alumno alu in alumnos)
            {
                alu.nombres += $" {alu.apellidoPaterno} {alu.apellidoMaterno}";
            }
        }

        protected void BtnNiegaRegistros_Click(object sender, EventArgs e)
        {
            CallJavascript("showModal('bloqueoRegistroModal')");
        }

        protected void CargarFechas(List<DateTime> fechas)
        {
            GridAsistenciasFechas.DataSource = fechas; //verificar el Datafield
            GridAsistenciasFechas.DataBind();
        }

        protected string ParsearFecha(DateTime fecha)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            return fecha.ToString("dddd d 'de' MMMM 'del' yyyy", culture);
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
            Session["MyNotification"] = null;

            if (!((bool)Session["RealizoAsistenica"]))
            {
                Session["fechaEdicion"] = null;
                Response.Redirect($"/View/Profesor/RegistroAsistencia.aspx?idsalon={idsalon}");
            }
            else
            {
                // CallJavascript("showModal('bloqueoRegistroModal')");
                Session["MyNotification"] = new MyNotification { Tipo="Info", Mensaje="Ya se realizaron los registros de los alumnos el día de hoy" };
                Response.Redirect($"/View/Profesor/AsistenciaProfesor.aspx");
            }
        }

        
        protected bool VerificarRegistroAsistenciaActual()
        {
            DateTime fechaHoy = DateTime.Now.Date;//comparamos fechas
            // List<DateTime> fechasReg = (List<DateTime>)Session["fechas"];
            List<DateTime> fechas = (List<DateTime>)Session["fechasAsistencia"];
            if (fechas.Count == 0) return false;

            DateTime fechasReg = fechas[0];
            return fechaHoy.Equals(fechasReg);
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
            Response.Redirect($"/View/Profesor/RegistroAsistencia.aspx?idsalon={idsalon}");
        }

        protected void FiltrarMesBtn_Click(object sender, EventArgs e)
        {
            //talvez deberia estar en el backend -> PASARLO
            string filtro = MesesDropDown.Text;
            List<DateTime> fechas = (List<DateTime>)Session["fechasAsistencia"];
            if (filtro == "")
            {
                CargarFechas(fechas);
                return;
            }
            int mes = int.Parse(MesesDropDown.Text);

            listaFechasFiltrado = fechas.Where(d => d.Month == mes).ToList();
            CargarFechas(listaFechasFiltrado);
        }

        protected void AsistenciasAlumnoBtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(FechaFinalTxt.Text) || string.IsNullOrEmpty(FechaIniTxt.Text)) {
                CallJavascript("showNotification('Bad','Debe de elegir el rango de fechas')");
                return;
            } 
            
            DateTime fechaInicialDate = DateTime.Parse(FechaIniTxt.Text).Date;
            DateTime fechaFinalDate = DateTime.Parse(FechaFinalTxt.Text).Date;

            if(VerificarFechas(fechaInicialDate,fechaFinalDate))
                Response.Redirect("/View/Profesor/ReporteAsistenciaAlumno.aspx?dniAlu="+AlumnosDrpDown.Text+"&fechaIni="+fechaInicialDate.ToString()+"&fechaFin="+fechaFinalDate.ToString());
            else
                CallJavascript("showNotification('Bad','Las fecha inicial debe ser anterior a la fecha posterior)");

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
            GridAsistenciasFechas.PageIndex = e.NewPageIndex;
            GridAsistenciasFechas.DataBind();
        }

        protected void JustificacionesBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/Justificacion.aspx");
        }
        protected void CargarAsistenciasSinJustificar(List<asistencia> asistencias)
        {
            
        }

        /*
protected void eliminarAsistencia_Click(object sender, EventArgs e)
{
Button btn = (Button)sender;
string fechaElim = btn.CommandArgument;
DateTime fecha= DateTime.Parse(fechaElim);

List<alumno> alumnos = (List<alumno>)Session["alumnosAsistencia"];
int eliminado = 0;
foreach(alumno alu in alumnos)
{
asistencia asis = new asistencia();
asis.dniAlumno = alu.dni;
asis.fechaHora = fecha;
asis.fechaHoraSpecified = true;
eliminado = 
}

}*/
    }
}