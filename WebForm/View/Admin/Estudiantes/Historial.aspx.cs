using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.Utils;

namespace WebForm.View.Admin.Historial
{
    public partial class Historial : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient daoservicio;
        private BindingList<alumno> alumnos;

        protected void Page_Load(object sender, EventArgs e)
        {
            daoservicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                TxtCriterioBusqueda.Text = "";
                alumno[] resultado = daoservicio.listarAlumnosEliminados();
                if (resultado == null)
                {
                    //LblNoAlumnos.Visible = true;
                    LblNoAlumnos.Text = "No se encontraron alumnos";
                    return;
                }
                // Existen alumnos
                alumnos = new BindingList<alumno>(resultado.ToList());
                Session["AlumnosEliminados"] = alumnos;
                CargarTabla();
            }
        }

        protected void CargarTabla()
        {
            alumnos = Session["AlumnosEliminados"] as BindingList<alumno>;
            GridAlumnosEliminados.DataSource = alumnos;
            GridAlumnosEliminados.DataBind();
        }

        protected void GridAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridAlumnosEliminados.PageIndex = e.NewPageIndex;
            alumnos = Session["AlumnosEliminados"] as BindingList<alumno>;
            GridAlumnosEliminados.DataSource = alumnos;
            GridAlumnosEliminados.DataBind();
        }

        protected void LkBtnBuscar_Click(object sender, EventArgs e)
        {
            // Buscar

            if (string.IsNullOrEmpty(TxtCriterioBusqueda.Text))
            {
                // No se ha ingresado un criterio de búsqueda
                CallJavascript("showNotification('Bad', 'Es necesario ingresar un criterio de búsqueda')");
                return;
            }
            
            alumno[] resultado = daoservicio.listarAlumnosEliminadosFiltro(TxtCriterioBusqueda.Text);

            if (resultado == null)
            {
                // No se ha encontrado alumnos
                LblNoAlumnos.Text = "No se encontraron alumnos";
                CallJavascript("showNotification('Info', 'No se han encontrado alumnos con los criterios de búsqueda ingresados')");
                return;
            }

            // Se ha encontrado personal
            BindingList<alumno> alumnosRecuperados = new BindingList<alumno>(resultado.ToList());
            alumnos = alumnosRecuperados;
            Session["AlumnosEliminados"] = alumnos;
            CargarTabla();
        }

        protected void BtnRestaurar_Click(object sender, EventArgs e)
        {
            string dni = ((Button)sender).CommandArgument;
            int resultado = daoservicio.restaurarUsuario( dni );
            if(resultado == 0)
            {
                CallJavascript("showNotification('Bad', 'No se pudo restaurar el alumno')");
            }
            else
            {
                CallJavascript("showNotification('Ok', 'Se pudo restaurar el alumno')");
                Session["AlumnosEliminados"] = new BindingList<alumno>((daoservicio.listarAlumnosEliminados() ?? new alumno[] { }).ToList());
                if ((Session["AlumnosEliminados"] as BindingList<alumno>).Count() == 0)
                {
                    LblNoAlumnos.Text = "No se encontraron alumnos";
                }
                CargarTabla();
            }

        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}