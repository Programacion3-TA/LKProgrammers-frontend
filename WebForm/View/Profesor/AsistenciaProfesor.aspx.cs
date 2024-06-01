using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            
            if(!IsPostBack){
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                int idsalon;
                profesor profesor = (profesor)Session["Usuario"];
                idsalon = daoServicio.esTutorAsignado(profesor.dni);
                if (idsalon == -1) Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                Session["idsalon"] = idsalon;
                cargarFechas(idsalon);
            }
               
        }
        protected void cargarFechas(int _idsalon)
        {
            List<DateTime> fechas = daoServicio.listarFechasAsistenciaSalon(_idsalon).ToList();
            Session["fechas"] = fechas;
            List<string> fechasFormato = transformarFechas(fechas);

            GridAsistenciasFechas.DataSource = fechasFormato; //verificar el Datafield
            GridAsistenciasFechas.DataBind();

        }
        protected List<string> transformarFechas(List<DateTime> fechas)
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
    }
}