using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //no deja entrar amenos que sea miembro
            if (Session["Usuario"] == null && Session["Tipo"] == null) Response.Redirect("/View/Login/Login.aspx");
            else
            {
            if (Session["AnimacionInicio"] != null)
                {
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script>alert('A');</script>");
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "A", "<script type=text/javascript> alert('Hello World!') </script>");
                    Session["AnimacionInicio"] = null;
                }

                usuario _usuario = (usuario)Session["Usuario"];
                nombreUsuarioLbl.Text = _usuario.nombres +" "+ _usuario.apellidoPaterno; 

            }
        }

        protected void CerrarSesionBtn_Click(object sender, EventArgs e)
        {
            Session["Tipo"] = null;
            Session["Usuario"] = null;
            Response.Redirect("/View/Login/Login.aspx"); 
        }
    }
}