using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
using WebForm.View.Admin.Profesores;

namespace WebForm.View.Admin.AnioAcademico
{
    public partial class AniosAcademicos : System.Web.UI.Page
    {
        private ServicioWS.LKServicioWebClient serviciodao;
        private BindingList<anioEscolar> anioVigente;
        private BindingList<anioEscolar> anios;
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Page_Load ejecutado");
            serviciodao = new ServicioWS.LKServicioWebClient();
            LblAnioVigente.Text = "Actualmente, no existe ningún año escolar en curso.";
            cargarAnioVigente();
            cargarAniosEscolares();

        }

        protected void cargarAnioVigente()
        {
            anioVigente = new BindingList<anioEscolar>(serviciodao.listarAnioEscolarVigente().ToList());
            
            if (anioVigente != null)
            {
                // Muestro el (o los) anio escolar vigente
                GVAnioVigente.DataSource = anioVigente;
                GVAnioVigente.DataBind();

                LblAnioVigente.Text = "Bienvenido al " + anioVigente[0].nombre;
            }
            else
            {
                // No hay ningun anio escolar vigente
                LblAnioVigente.Text = "Actualmente, no existe ningún año escolar en curso.";
            }
        }

        protected void cargarAniosEscolares()
        {
            anios = new BindingList<anioEscolar>(serviciodao.listarAniosEscolares().ToList());

            foreach (anioEscolar anio_esc in anioVigente)
            {
                anios.Remove(anio_esc);
            }            
            
            GVAnios.DataSource = anios;
            GVAnios.DataBind();
        }

        protected void BtnAgregarAnio_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtNombre.Text = "";
            TxtFechaInicio.Text = "";
            TxtFechaFin.Text = "";
            TxtFechaCerrado.Text = "";
            
            TxtFechaCerrado.Enabled = false;
            CallJavascript("showModalFormAnio()");
        }

        protected void EditRowClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument; // recibo el codigo del profesor

            anioEscolar anio = anios.ToList().Find(x => x.id == Int32.Parse(code));
            // Busco y recupero los datos del profe
            TxtCode.Text = anio.id.ToString();
            TxtNombre.Text = anio.nombre;
            TxtFechaInicio.Text = anio.fechaInicio.ToString("yyyy-MM-dd");
            TxtFechaFin.Text = anio.fechaFin.ToString("yyyy-MM-dd");
            TxtFechaCerrado.Text = anio.fechaCerrado.ToString("yyyy-MM-dd");

            TxtFechaCerrado.Enabled = true;
            CallJavascript("showModalFormAnio()");
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