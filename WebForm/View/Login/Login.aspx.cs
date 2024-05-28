using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Login
{
    public partial class Login : System.Web.UI.Page
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();

            if (Session["Usuario"] != null && Session["Tipo"] != null)
            {
                Response.Redirect("/View/" + Session["Tipo"] + "/" + Session["Tipo"] + ".aspx");
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            char t = (char)daoServicio.acceder_a_pagina(TxtUsuario.Text, TxtContrasenia.Text);
            //redireccionamiento de paginas dependiendo del usuario
            if (t == 'N') //NO ES NADIE ASI QUE EMITE UN ERROR
            {
                //implementar estilos de errores con JS
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Response.Redirect("/View/Login/Login.aspx");
            }
            if (t == 'P') //ES EL PROFESOR
            {
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/CursoProfesor.aspx");
            }
            if (t == 'E')//ES EL ESTUDIANTE
            {
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            if (t == 'A') //ADMINISTRATIVO
            {
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/Profesores/Profesores.aspx");
            }
        }
    }
}