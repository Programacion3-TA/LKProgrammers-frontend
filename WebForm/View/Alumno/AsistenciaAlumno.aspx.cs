using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.AsistenciaAlumno
{
    public partial class AsistenciaAlumno : System.Web.UI.Page
    {
        // public static List<Asistencia> lista; // Lista de asistencias por alumno
        private BindingList<asistencia> asistencias;
        private ServicioWS.LKServicioWebClient serviciodao;

        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new ServicioWS.LKServicioWebClient();
            asistencia[] asist = serviciodao.listarAsitencias(((alumno)Session["Usuario"]).dni);
            if (asist == null) return;
            asistencias = new BindingList<asistencia>(asist.ToList());
            
            GriAsistenciasAlumnos.DataSource = asistencias;
            GriAsistenciasAlumnos.DataBind();

            if (!IsPostBack) // Verificar si es la primera carga de la página
            {
                InicializarAsistencias(); // Inicializar la lista de asistencias solo en la primera carga
            }
            CargarAsistencias();
        }

        protected void InicializarAsistencias()
        {
            /*
            lista = new List<Asistencia>();
            lista.Add(new Asistencia() { Fecha = DateTime.Now, Estado = "Presente" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-1), Estado = "Presente" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-2), Estado = "Justificado" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-3), Estado = "Tardanza" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-4), Estado = "Tardanza" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-5), Estado = "Tardanza" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-6), Estado = "Presente" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-7), Estado = "Presente" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-8), Estado = "Falta" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-9), Estado = "Presente" });
            lista.Add(new Asistencia() { Fecha = DateTime.Now.AddDays(-10), Estado = "Presente" });*/
        }

        protected void CargarAsistencias()
        {
            /*
            GriAsistenciasAlumnos.DataSource = lista;
            GriAsistenciasAlumnos.DataBind();*/
        }

        protected void JustificarBtn_Click(object sender, EventArgs e)
        {
            Button bt = (Button)sender;
            string fecha = bt.CommandArgument;
            Session["fechaJustifica"] = fecha;
            CallJavascript("showModal('JustificarAsistenciaModal')");
        }

        protected void EnviarJustifiBtn_Click(object sender, EventArgs e)
        {
            alumno alumno = (alumno)Session["Usuario"];
            DateTime fecha = DateTime.Parse(((string)Session["fechaJustificada"]));
            string dniAlumno = alumno.dni;

        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}