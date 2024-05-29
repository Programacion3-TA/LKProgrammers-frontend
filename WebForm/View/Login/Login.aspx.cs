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
               if (Session["Tipo"] as string == "Administrador")
                {
                    Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
                }
                Response.Redirect("/View/" + Session["Tipo"] + "/" + Session["Tipo"] + ".aspx");
            }
        }
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            char t = (char)daoServicio.acceder_a_pagina(TxtUsuario.Text, TxtContrasenia.Text);
            //redireccionamiento de paginas dependiendo del usuario
            if (t == 'N') //NO ES NADIE ASI QUE EMITE UN ERROR: 78 en ASCII
            {
                //implementar estilos de errores con JS
                
                TxtUsuario.Text = "";
                TxtContrasenia.Text = "";
                Response.Redirect("/View/Login/Login.aspx");
            }
            if (t == 'P') //ES EL PROFESOR
            {
                Session["Usuario"] = daoServicio.listarProfesores().ToList().Find(x => (x.usuario1 == TxtUsuario.Text && x.contrasenia == TxtContrasenia.Text));
                Session["Tipo"] = "Profesor";
                Response.Redirect("/View/Profesor/CursoProfesor.aspx");
            }
            if (t == 'E')//ES EL ESTUDIANTE: 69 ASCII
            {
                Session["Usuario"] = daoServicio.listarAlumnos().ToList().Find(x => (x.usuario1 == TxtUsuario.Text && x.contrasenia == TxtContrasenia.Text));
                Session["Tipo"] = "Alumno";
                Response.Redirect("/View/Alumno/Alumno.aspx");
            }
            if (t == 'A') //ADMINISTRATIVO
            {
                Session["Usuario"] = daoServicio.listarAdministradores().ToList().Find(x => (x.usuario1 == TxtUsuario.Text && x.contrasenia == TxtContrasenia.Text));
                Session["Tipo"] = "Administrador";
                Response.Redirect("/View/Admin/AnioAcademico/AniosAcademicos.aspx");
            }
        }
    }
}