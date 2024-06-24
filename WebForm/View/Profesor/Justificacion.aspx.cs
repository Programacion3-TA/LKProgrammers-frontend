using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Profesor
{
    public partial class Justificacion : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            List<asistencia> asistenciasSinJust = (List<asistencia>)Session["asistenciasSinJustificar"];
            CargarAsistenciasSinJustificar(asistenciasSinJust);
        }

        protected string ParsearFecha(DateTime fecha)
        {
            CultureInfo culture = new CultureInfo("es-ES");
            return fecha.ToString("dddd d 'de' MMMM 'del' yyyy", culture);
        }

        
        protected void CargarAsistenciasSinJustificar(List<asistencia> asistencias)
        {
            GridAsistencias.DataSource = asistencias;
            GridAsistencias.DataBind();
        }

        protected void AprobarJustiBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string arg = btn.CommandArgument;
            string[] args = arg.Split('|');
            DateTime fecha = DateTime.Parse(args[1]);
            int idsalon = (int)Session["idsalon"];
            asistencia asis = new asistencia();
            asis.dniAlumno = args[0];
            asis.fechaHora = fecha;
            asis.fechaHoraSpecified = true;
            

            daoServicio.aceptarJustificacion(asis);

            Session["asistenciasSinJustificar"] = (daoServicio.listarAsistenciasSinJustificar(idsalon) ?? new asistencia[] { }).ToList();
            List<asistencia> asistenciasSinJust = (List<asistencia>)Session["asistenciasSinJustificar"];
            CallJavascript("showNotification('Ok', 'Se aceptó la justificación!')");
            CargarAsistenciasSinJustificar(asistenciasSinJust);
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        protected void NegarJutiBtn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string arg = btn.CommandArgument;
            string[] args = arg.Split('|');
            DateTime fecha = DateTime.Parse(args[1]);
            int idsalon = (int)Session["idsalon"];
            asistencia asis = new asistencia();
            asis.dniAlumno = args[0];
            asis.fechaHora = fecha;
            asis.fechaHoraSpecified = true;
            daoServicio.negaJustificacion(asis);
            Session["asistenciasSinJustificar"] = (daoServicio.listarAsistenciasSinJustificar(idsalon) ?? new asistencia[] { }).ToList();
            List<asistencia> asistenciasSinJust = (List<asistencia>)Session["asistenciasSinJustificar"];
            CallJavascript("showNotification('Ok', 'Se rechazó la justificación!')");
            CargarAsistenciasSinJustificar(asistenciasSinJust);
        }

        protected void BtnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/AsistenciaProfesor.aspx");
        }
    }
}