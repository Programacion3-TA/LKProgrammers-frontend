using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.AsistenciaAlumno
{
    public partial class AsistenciaAlumno : System.Web.UI.Page
    {
        // public static List<asistencia> lista; // Lista de asistencias por alumno
        private LKServicioWebClient serviciodao;

        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            RenderizarAsistencias();
        }

        protected List<asistencia> Inicializarasistencias()
        {

            return new List<asistencia>
            {
                new asistencia() { fechaHora = DateTime.Now, estado = estadoAsistencia.Presente, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-1), estado = estadoAsistencia.Presente, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-2), estado = estadoAsistencia.Justificada, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-3), estado = estadoAsistencia.Tardanza, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-4), estado = estadoAsistencia.Tardanza, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-5), estado = estadoAsistencia.Tardanza, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-6), estado = estadoAsistencia.Tardanza, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-7), estado = estadoAsistencia.Presente, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-8), estado = estadoAsistencia.Ausente, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-9), estado = estadoAsistencia.Presente, aceptaJustificacion=false },
                new asistencia() { fechaHora = DateTime.Now.AddDays(-10), estado = estadoAsistencia.Presente, aceptaJustificacion=false },
            };
        }

        protected void RenderizarAsistencias()
        {
            string dniAlumno = ((alumno)Session["Usuario"]).dni;
            List<asistencia> asistencias = (serviciodao.listarAsitencias(dniAlumno) ?? new asistencia[] { }).ToList();
            //asistencias = Inicializarasistencias(); // Datos estaticos

            GriAsistenciasAlumnos.DataSource = new BindingList<asistencia>(asistencias);
            GriAsistenciasAlumnos.DataBind();
        }

        protected void JustificarBtn_Click(object sender, EventArgs e)
        {
            string raw = ((Button)sender).CommandArgument;
            int index = raw.IndexOf("@");

            string fechaFormatoRaw = raw.Substring(0, index);
            string justificacionRaw = raw.Substring(index + 1);

            InputJustificacion.Text = justificacionRaw;
            InputCalendario.SelectedDate = DateTime.Parse(fechaFormatoRaw);
            CallJavascript("mostrarModal('JustificarAsistenciaModal')");
        }

        protected void EnviarJustifiBtn_Click(object sender, EventArgs e)
        {
            alumno alumno = (alumno)Session["Usuario"];
            DateTime _fecha = InputCalendario.SelectedDate;
            string _dniAlumno = alumno.dni;
            string _justificacion = InputJustificacion.Text ?? "";
            Cuack.Text = _fecha.ToString();
            asistencia asis = new asistencia();
            asis.fechaHora = _fecha;
            asis.fechaHoraSpecified = true;
            asis.dniAlumno = _dniAlumno;
            asis.justificacion = _justificacion;
            int estado = serviciodao.generarJustificacionAlumno(asis);

            if (estado != 0)
            {
                CallJavascript("showNotification('Ok', 'Se modificó correctamente la justificación')");
            }
            else
            {
                CallJavascript("showNotification('Bad', 'Hubo un error aquí')");
            }
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
        protected string GenerarFormatoEstado(estadoAsistencia est)
        {
            string item = "<i ";
            switch (est)
            {
                case estadoAsistencia.Presente:
                    item += "class=\"fa fa-check\"";
                    break;
                case estadoAsistencia.Tardanza:
                    item += "class=\"fa fa-times\"";
                    break;
                case estadoAsistencia.Ausente:
                    item += "class=\"fa fa-clock-o\"";
                    break;
                case estadoAsistencia.Justificada:
                    item += "class=\"fa fa-cloud\"";
                    break;
            }
            item += $" aria-hidden=\"true\"></i> {est}";
            return item;
        }

        protected bool VerificarEstadoNoJustificado(estadoAsistencia est, bool acep)
        {
            return (est == estadoAsistencia.Tardanza && !acep) || (est == estadoAsistencia.Ausente && !acep);
        }

        protected string TituloBotonJustificacion(estadoAsistencia est, bool acep)
        {
            if (est == estadoAsistencia.Presente) return "No necesita Justificación";
            if (est == estadoAsistencia.Justificada || acep) return "Justificado";
            return "Justificación";
        }
    }
}