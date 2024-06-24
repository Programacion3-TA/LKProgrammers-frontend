using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Channels;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Cursos
{
    public partial class CompetenciasCurso : System.Web.UI.Page
    {
        private int cursoId;        
        private LKServicioWebClient servicioDAO = new LKServicioWebClient();
        private BindingList<competencia> competenciasCurso = new BindingList<competencia>();
        protected void Page_Load(object sender, EventArgs e)
        {
            cursoId = Int32.Parse(Request.QueryString["curso"]);
            string nombreCurso = servicioDAO.listarCursos().ToList().Find(c => c.id == cursoId).nombre;
            LtCurso.Text = nombreCurso;
            if (!IsPostBack)
            {
                var list = servicioDAO.listarCompetencias(cursoId);
                if (list != null)
                {
                    competenciasCurso = new BindingList<competencia>(list);
                    Session["CompetenciasCurso"] = competenciasCurso;
                    cargarTabla();
                }
            }
        }

        private void cargarTabla()
        {            
            GridCompetenciasCurso.DataSource = competenciasCurso;
            GridCompetenciasCurso.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            CallJavascript("showModalFormCompetencia()");
        }
        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            TxtID.Text = ((LinkButton)sender).CommandArgument;
            competencia comp = new competencia();
            competenciasCurso = Session["CompetenciasCurso"] as BindingList<competencia>;

            comp = competenciasCurso.ToList().Find(c => c.id == Int32.Parse(TxtID.Text));
            TxtDescripcion.Text = comp.descripcion;
            TxtPeso.Text = comp.peso.ToString();
            CallJavascript("showModalFormCompetencia()");

            var list = servicioDAO.listarCompetencias(cursoId);
            if (list != null)
            {
                competenciasCurso = new BindingList<competencia>(list);
                Session["CompetenciasCurso"] = competenciasCurso;
                cargarTabla();
            }
        }

        protected void BtnQuitar_Click(object sender, EventArgs e)
        {
            string competenciaId = ((LinkButton)sender).CommandArgument;
            servicioDAO.eliminarCompetencia(Int32.Parse(competenciaId));
            Response.Redirect(Request.Url.AbsoluteUri);

        }

        protected void BtnGuardarCompetencia_Click(object sender, EventArgs e)
        {
            int anioEscolarId = servicioDAO.listarAnioEscolarVigente()[0].id;
            competencia competencia = new competencia();
            competencia.descripcion = TxtDescripcion.Text;
            competencia.peso = float.Parse(TxtPeso.Text);
            string competenciaId = TxtID.Text;
            if (competenciaId=="")
            {
                servicioDAO.insertarCompetencia(competencia, cursoId, anioEscolarId);
            }
            else
            {
                competencia.id = Int32.Parse(competenciaId);
                servicioDAO.modificarCompetencia(competencia, cursoId, anioEscolarId);
            }
            
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void GridCompetenciasCurso_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridCompetenciasCurso.PageIndex = e.NewPageIndex;
            competenciasCurso = Session["CompetenciasCurso"] as BindingList<competencia>;
            GridCompetenciasCurso.DataSource = competenciasCurso;
            GridCompetenciasCurso.DataBind();
        }
    }
}