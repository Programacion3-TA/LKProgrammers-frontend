using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.ServiceModel.Description;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.View.Admin.Profesores;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace WebForm.View.Admin.Salones
{
    public partial class SalonesAdmin : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        private BindingList<salon> salones;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            serviciodao = new ServicioWS.LKServicioWebClient();
            cargarTabla();
            cargarProfesores();
            cargarAnisoEscolares();
            
            
        }

        private void cargarAnisoEscolares()
        {
            BindingList<anioEscolar> anios = new BindingList<anioEscolar>(serviciodao.listarAnioEscolarVigente().ToList());
            DDAnioEscolar.DataSource = anios;
            DDAnioEscolar.DataValueField = "id";
            DDAnioEscolar.DataTextField = "nombre";
            DDAnioEscolar.DataBind();
        }

        private void cargarProfesores()
        {
            BindingList<profesor> profesores = new BindingList<profesor>( serviciodao.listarProfesores().ToList());
            DDTutor.DataSource = profesores;
            DDTutor.DataValueField = "codigoProfesor";
            DDTutor.DataTextField = "nombres";
            DDTutor.DataBind();
        }

        private void cargarTabla()
        {
            salones = new BindingList<salon>(serviciodao.listarSalones().ToList());
            GridSalones.DataSource = salones;
            GridSalones.DataBind();
        }


        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtCapMaxima.Text = "0";
            TxtCapMinima.Text = "0";
            CallJavascript("showModalFormSalon()");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            string code = btn.CommandArgument; // recibo el id

            salon salonActual = salones.ToList().Find(x => x.id == Int32.Parse(code));
            // Busco y recupero los datos del profe
            TxtCode.Text = salonActual.id.ToString();
            DDAnioEscolar.SelectedValue = salonActual.idAnioEscolar.ToString();
            SLGrado.SelectedValue = salonActual.gradoSalon.ToString();
            TxtCapMaxima.Text = salonActual.capacidadMaxima.ToString();
            TxtCapMinima.Text = salonActual.capacidadMinima.ToString();
            DDTutor.SelectedValue = salonActual.tutor.codigoProfesor.ToString();
            CallJavascript("showModalFormSalon()");
            
        }
        protected void BtnVer_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string salonId = btn.CommandArgument;
            Response.Redirect($"SalonDetalle.aspx?salonId={salonId}");
        }

        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            return;
        }
    }
}