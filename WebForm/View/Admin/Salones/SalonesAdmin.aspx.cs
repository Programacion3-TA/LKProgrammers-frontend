﻿using System;
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
        private LKServicioWebClient serviciodao;
        private BindingList<salon> salones;

        protected void Page_Load(object sender, EventArgs e)
        {
            serviciodao = new LKServicioWebClient();
            if (!IsPostBack)
            {
                cargarTabla();
                cargarProfesores();
                cargarAnisoEscolares();
            }
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
            if (f != null)
            {
                salones = new BindingList<salon>(f);
            }
            GridSalones.DataSource = salones;
            Session["Salones"] = salones;
            GridSalones.DataBind();
        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            TxtCode.Text = "";
            TxtCapMaxima.Text = "0";
            TxtCapMinima.Text = "0";
            DDTutor.SelectedIndex = 0;  // Ensure default tutor is selected
            CallJavascript("showModalFormSalon()");
        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string code = btn.CommandArgument;
            BindingList<salon> d = new BindingList<salon>(serviciodao.listarSalones());
            salon salonActual = d.ToList().Find(x => x.id == Int32.Parse(code));

            TxtCode.Text = salonActual.id.ToString();
            DDAnioEscolar.SelectedValue = salonActual.idAnioEscolar.ToString();
            SLGrado.SelectedValue = salonActual.gradoSalon.ToString();
            TxtCapMaxima.Text = salonActual.capacidadMaxima.ToString();
            TxtCapMinima.Text = salonActual.capacidadMinima.ToString();
            DDTutor.SelectedValue = salonActual.tutor.dni;

            CallJavascript("showModalFormSalon()");
        }

        protected void BtnVer_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string salonId = btn.CommandArgument;
            // Se ve un salón a detalle -> SalonDetalle
            BindingList<salon> salones = Session["Salones"] as BindingList<salon>;
            salon salonElegido = salones.ToList().Find(p => p.id == int.Parse(salonId));
            Session["SalonElegido"] = salonElegido;
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
            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                var f = serviciodao.listarSalones();
                if (f == null)
                {
                    op.id = 1;
                }
                else
                {
                    op.id = serviciodao.listarSalones().Max(s => s.id) + 1;
                }
            }
            else // actualizar
            {
                op.id = int.Parse(TxtCode.Text);
            }

            op.capacidadMaxima = int.Parse(TxtCapMaxima.Text);
            op.capacidadMinima = int.Parse(TxtCapMinima.Text);
            op.idAnioEscolar = int.Parse(DDAnioEscolar.SelectedValue);
            op.tutor = new profesor { dni = DDTutor.SelectedValue };
            op.gradoSalonSpecified = true;
            if (Enum.TryParse(SLGrado.SelectedValue, true, out grado grado))
            {
                op.gradoSalon = grado;
            }

            if (string.IsNullOrEmpty(TxtCode.Text)) // crear
            {
                serviciodao.insertar_salon(op);
            }
            else // actualizar
            {
                serviciodao.modificar_salon(op);
            }

            cargarTabla();
            cargarProfesores();
            cargarAnisoEscolares();
            Response.Redirect(Request.Url.AbsoluteUri);
        }

        protected void GridSalones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridSalones.PageIndex = e.NewPageIndex;
            salones = Session["Salones"] as BindingList<salon>;
            GridSalones.DataSource = salones;
            GridSalones.DataBind();
        }
    }
}