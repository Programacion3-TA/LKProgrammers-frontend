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
            var c = serviciodao.listarCursos();
            if (c != null)
            {
                GridCursos.DataSource = new BindingList<curso>(c);
            }
            else
            {
                GridCursos.DataSource = new BindingList<curso>();
            }
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
            Button btn = (Button)sender;
            int code = int.Parse(btn.CommandArgument);
            curso profesor = serviciodao.listarCursos().ToList().FirstOrDefault(x => x.id == code);
            TxtCode.Text = profesor.id.ToString();
            TxtNombre.Text = profesor.nombre;
            TxtDescripción.Text = profesor.descripcion;
            CallJavascript("showModalFormCurso()");
        }

        protected void DelRow_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            serviciodao.eliminar_cursos(int.Parse(code));
            cargarTabla();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            var cur = serviciodao.listarCursos();
            BindingList<curso> lis_cur = new BindingList<curso>();
            curso op = new curso();
            if (cur != null)
            {
                lis_cur = new BindingList<curso>(cur);
            }
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                op.id = lis_cur.Count() + 1;
                op.nombre = TxtNombre.Text;
                op.descripcion = TxtDescripción.Text;
                serviciodao.insertar_cursos(op);
                cargarTabla();
            }
            else //actualizar
            {
                op = lis_cur.ToList().Find(x => x.id == int.Parse(TxtCode.Text));
                op.nombre = TxtNombre.Text;
                op.descripcion = TxtDescripción.Text;
                serviciodao.editar_cursos(op);
                cargarTabla();
            }
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void BtnCompetencias_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int id = int.Parse(btn.CommandArgument);
            Response.Redirect("CompetenciasCurso.aspx?curso=" + id);
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}