﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;
namespace WebForm.View.AsistenciaProfesor
{
    public partial class AsistenciaProfesor : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            //no tocar
            ValidationSettings.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;

            daoServicio = new LKServicioWebClient();
            


            if(!IsPostBack){
                //if (!((string)(Session["Tipo"])).Equals("Profesor")) ; evita que ingresen cuentasn que no son tipo Profesor
                int idsalon;
                profesor profesor = (profesor)Session["Usuario"];
                idsalon = daoServicio.esTutorAsignado(profesor.dni);
                if (idsalon == -1) Response.Redirect("/View/Profesor/ErroNoTutor.aspx");
                Session["idsalon"] = idsalon;
            }
               
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("/View/Profesor/RegistroAsistencia.aspx");
        }
    }
}