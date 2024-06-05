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
            salones = new BindingList<salon>(serviciodao.listarSalones());
            cargarTabla();
            cargarProfesores();
            cargarAnisoEscolares();
        }
        private void cargarAnisoEscolares()
        {

            var list = serviciodao.listarAnioEscolarVigente().ToList();
            BindingList<anioEscolar> anios = list != null ? new BindingList<anioEscolar>(list) : new BindingList<anioEscolar>();

            DDAnioEscolar.DataSource = anios;
            DDAnioEscolar.DataValueField = "id";
            DDAnioEscolar.DataTextField = "nombre";
            DDAnioEscolar.DataBind();
        }
        private void cargarProfesores()
        {

            var list = serviciodao.listarProfesores().ToList();
            BindingList<profesor> profesores = list != null ? new BindingList<profesor>(list) : new BindingList<profesor>();

            DDTutor.DataSource = profesores;
            DDTutor.DataValueField = "dni";
            DDTutor.DataTextField = "nombres";
            DDTutor.DataBind();
        }
        private void cargarTabla()
        {

            var f = serviciodao.listarSalones();
            salones = new BindingList<salon>();
            if(f != null)
            {
                salones = new BindingList<salon>(f);
            }
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
            salones = new BindingList<salon>(serviciodao.listarSalones());
            salon salonActual = salones.ToList().Find(x => x.id == Int32.Parse(code));
            // Busco y recupero los datos del profe
            TxtCode.Text = salonActual.id.ToString();
            DDAnioEscolar.DataSource = serviciodao.listarAnioEscolarVigente();
            DDAnioEscolar.DataTextField = "nombre";
            DDAnioEscolar.DataValueField = "id";
            DDAnioEscolar.DataBind();
            DDAnioEscolar.SelectedValue = salonActual.idAnioEscolar.ToString();
            SLGrado.SelectedValue = salonActual.gradoSalon.ToString();
            TxtCapMaxima.Text = salonActual.capacidadMaxima.ToString();
            TxtCapMinima.Text = salonActual.capacidadMinima.ToString();
            DDTutor.DataSource = serviciodao.listarProfesores();
            DDTutor.DataTextField = "nombres";
            DDTutor.DataValueField = "dni";
            DDTutor.DataBind();
            DDTutor.SelectedValue = salonActual.tutor.dni;
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
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            serviciodao.eliminar_salon(int.Parse(code));
            cargarTabla();
            cargarProfesores();
            cargarAnisoEscolares();
        }
        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            salon op = new salon();
            salones = new BindingList<salon>(serviciodao.listarSalones());
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                op.id = salones.Count() + 1;
                op.capacidadMaxima = int.Parse(TxtCapMaxima.Text);
                op.capacidadMinima = int.Parse(TxtCapMinima.Text);
                op.idAnioEscolar = int.Parse(DDAnioEscolar.Text);
                op.tutor = new profesor();
                op.tutor.dni = DDTutor.SelectedValue;
                op.gradoSalonSpecified = true;
                if(Enum.TryParse(SLGrado.Text, true, out grado grado))
                {
                    op.gradoSalon = grado;
                }
                serviciodao.insertar_salon(op);
            }
            else //actualizar
            {
                op.id = int.Parse(TxtCode.Text);
                op.capacidadMaxima = int.Parse(TxtCapMaxima.Text);
                op.capacidadMinima = int.Parse(TxtCapMinima.Text);
                op.idAnioEscolar = int.Parse(DDAnioEscolar.Text);
                op.tutor = new profesor();
                op.tutor.dni = DDTutor.SelectedValue;
                op.gradoSalonSpecified = true;
                if (Enum.TryParse(SLGrado.Text, true, out grado grado))
                {
                    op.gradoSalon = grado;
                }
                serviciodao.modificar_salon(op);
                
            }
            cargarTabla();
            cargarProfesores();
            cargarAnisoEscolares();
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}