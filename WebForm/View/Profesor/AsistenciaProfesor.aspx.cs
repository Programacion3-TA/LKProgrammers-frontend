using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        private bool realizoRegistroAsistencia;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (!IsPostBack) {
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                int idsalon ;
                profesor profesor = (profesor)Session["Usuario"];

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
                    realizoRegistroAsistencia = VerificarRegistroAsistenciaActual();
                }

                if (realizoRegistroAsistencia) {
                    //si se realizo se cambia el valor del onclikc
                    BtnRegistrarAsistencia.Click += new EventHandler(this.BtnNiegaRegistros_Click);
                }
            }
               
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

            foreach(DateTime fecha in fechas)
            {
                object key = new { Fecha = fecha, FechaFormato = fechasFormato[fechas.IndexOf(fecha)] };
                fechasconFormato.Add(key);
            }
            Session["fechas"] = fechas;
            GridAsistenciasFechas.DataSource = fechasconFormato; //verificar el Datafield
            GridAsistenciasFechas.DataBind();


            //un objeto
            //var fechasFormatoEnlazadas = fechasFormato.Select(f => new { Fecha = f }).ToList();


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

            Response.Redirect("/View/Profesor/RegistroAsistencia.aspx?idsalon="+idsalon);
        }

        protected void BtnBuscarDias_Click(object sender, EventArgs e)
        {
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
    }
}