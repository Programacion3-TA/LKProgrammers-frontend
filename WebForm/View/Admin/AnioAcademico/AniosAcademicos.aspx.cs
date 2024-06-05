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
            var anioEscolar = serviciodao.listarAnioEscolarVigente();
            if (anioEscolar != null)
            {
                // Muestro el (o los) anio escolar vigente
                anioVigente = new BindingList<anioEscolar>(anioEscolar);
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
            anios = new BindingList<anioEscolar>();
            var anio = serviciodao.listarAniosEscolares();
            if(anio != null)
            {
                anios = new BindingList<anioEscolar>(anio);
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

            anioEscolar anio = anios.ToList().Find(x => x.id == int.Parse(code));
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
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            serviciodao.elimar_anio_escolar(int.Parse(code));
            cargarAnioVigente();
            cargarAniosEscolares();
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            anioEscolar op = new anioEscolar();
            var f = serviciodao.listarAnioEscolarVigente();
            BindingList<anioEscolar> Lis_anio = new BindingList<anioEscolar>();
            if (f != null)
            {
                Lis_anio = new BindingList<anioEscolar>(f);
            }
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                op.id = Lis_anio.Count() + 1;
                op.nombre = TxtNombre.Text;
                op.fechaInicioSpecified = true;
                op.fechaInicio = DateTime.Parse(TxtFechaInicio.Text);
                op.fechaFinSpecified = true;
                op.fechaFin = DateTime.Parse(TxtFechaFin.Text);
                serviciodao.insertar_new_age(op);
                cargarAnioVigente();
                cargarAniosEscolares();
            }
            else //actualizar
            { 
                op = Lis_anio.ToList().Find(x => x.id == int.Parse(TxtCode.Text));
                op.nombre = TxtNombre.Text;
                op.fechaInicioSpecified = true;
                op.fechaInicio = DateTime.Parse(TxtFechaInicio.Text);
                op.fechaFinSpecified = true;
                op.fechaFin = DateTime.Parse(TxtFechaFin.Text);
                serviciodao.editar_age(op);
                cargarAnioVigente();
                cargarAniosEscolares();
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        private void CallJavascript(string function)
        {
            string script = "window.onload = function() {" + function + "; };";
            ClientScript.RegisterStartupScript(GetType(), "", script, true);
        }
    }
}