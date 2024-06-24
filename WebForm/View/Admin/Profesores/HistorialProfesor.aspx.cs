using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static System.Runtime.CompilerServices.RuntimeHelpers;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Profesores
{
    public partial class Historial : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        private BindingList<profesor> listProfesor;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            if (!IsPostBack)
            {
                var profesores = daoServicio.listarProfesoresEliminados();
                if (profesores != null)
                {
                    listProfesor = new BindingList<profesor>(profesores);
                }
                else
                {
                    listProfesor = new BindingList<profesor>();
                }
                Session["ProfesoresEliminados"] = listProfesor;
                CargarTabla();
            }
        }

        private void CargarTabla()
        {
            listProfesor = Session["ProfesoresEliminados"] as BindingList<profesor>;
            GridProfesoresEliminados.DataSource = listProfesor;
            GridProfesoresEliminados.DataBind();
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
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

            profesor[] resultado = daoServicio.buscarProfesEliminadosTodosCriterios(TxtCriterioBusqueda.Text);

            if (resultado == null)
            {
                // No se ha encontrado alumnos
                CallJavascript("showNotification('Info', 'No se han encontrado profesores con los criterios de búsqueda ingresados')");
                return;
            }

            // Se ha encontrado personal
            BindingList<profesor> profesoresRecuperados = new BindingList<profesor>(resultado.ToList());
            listProfesor = profesoresRecuperados;
            Session["ProfesoresEliminados"] = listProfesor;
            CargarTabla();
        }

        protected void BtnRestaurar_Click(object sender, EventArgs e)
        {
            string dni = ((Button)sender).CommandArgument;
            int resultado = daoServicio.restaurarUsuario(dni);
            if (resultado == 0)
            {
                CallJavascript("showNotification('Bad', 'No se pudo restaurar el profesor')");
            }
            else
            {
                CallJavascript("showNotification('Ok', 'Se pudo restaurar el profesor')");
                Session["ProfesoresEliminados"] = new BindingList<profesor>((new profesor[] { }).ToList());
                if ((Session["ProfesoresEliminados"] as BindingList<profesor>).Count() == 0)
                {
                    LblNoProfesor.Text = "No se encontraron alumnos";
                }
                CargarTabla();
            }

        }

        protected void BtnMostrarTodo_Click(object sender, EventArgs e)
        {
            var profesores = daoServicio.listarProfesoresEliminados();
            if (profesores != null)
            {
                listProfesor = new BindingList<profesor>(profesores);
            }
            else
            {
                listProfesor = new BindingList<profesor>();
            }
            Session["ProfesoresEliminados"] = listProfesor;
            CargarTabla();
        }


        protected void GridProfesores_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridProfesoresEliminados.PageIndex = e.NewPageIndex;
            listProfesor = Session["ProfesoresEliminados"] as BindingList<profesor>;
            GridProfesoresEliminados.DataSource = listProfesor;
            GridProfesoresEliminados.DataBind();
        }
    }
}