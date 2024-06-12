using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.Layout
{
    public partial class MainAdministrador : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Tipo"] as string != "Administrador")
            {
                Response.Redirect("/View/Login/Login.aspx");
            }
            
        }
    }
}