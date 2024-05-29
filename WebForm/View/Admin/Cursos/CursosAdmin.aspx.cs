using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Cursos
{
    public partial class CursosAdmin : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        private BindingList<curso> cursos;
        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new ServicioWS.LKServicioWebClient();
            cargarTabla();
        }

        private void cargarTabla()
        {
            cursos = new BindingList<curso>(serviciodao.listarCursos().ToList());
            GridCursos.DataSource = cursos;
            GridCursos.DataBind();
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtDescripción.Text = "";
            TxtNombre.Text = "";
            CallJavascript("showModalFormCurso()");
        }

        protected void EditRow_Click(object sender, EventArgs e)
        {

        }

        protected void DelRow_Click(object sender, EventArgs e)
        {

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}