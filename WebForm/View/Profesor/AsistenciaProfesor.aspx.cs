using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;


using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System.IO;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (!IsPostBack) {
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                int idsalon = (int)Session["idsalon"] ;
                profesor profesor = (profesor)Session["Usuario"];
                //cuando cargue, ya se verifico si es tutor 

                if (idsalon != -1)
                {
                    CargarFechas(idsalon);
                    List<alumno> alumnos = daoServicio.listarAlumnosxsalon(idsalon).ToList();
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
            List<DateTime> fechas = daoServicio.listarFechasAsistenciaSalon(_idsalon).ToList();
            List<string> fechasFormato = TransformarFechas(fechas);
            List<object> fechasconFormato = new List<object>();

            //llenamos la lista de objetos
            foreach(DateTime fecha in fechas)
            {
                object key = new { Fecha = fecha.Date, FechaFormato = fechasFormato[fechas.IndexOf(fecha)] };
                fechasconFormato.Add(key);
            }

            Session["fechas"] = fechas;
            //se impleemnto para que funcione el filtrado -> mejorar
            Session["fechasFormato"] = fechasFormato;
            Session["fechasconFormato"] = fechasconFormato;
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
            string dniAlumno = AlumnosDrpDown.Text;
            List<asistencia> asistencias = daoServicio.listarAsitencias(dniAlumno).ToList();
            alumno alumno = daoServicio.listarAlumnosFiltro(dniAlumno).ToList().FirstOrDefault();
            AsistenciaAlumnoLbl.Text += " "+alumno.nombres + " " + alumno.apellidoPaterno;
            AsistenciaAlumnoGrid.DataSource = asistencias;
            AsistenciaAlumnoGrid.DataBind();
            CallJavascript("showModal('AsistenciaAlumnoModalCenter')");
        }

        protected void CerrarModalIncidenciaBtn_Click(object sender, EventArgs e)
        {

        }
    }
}