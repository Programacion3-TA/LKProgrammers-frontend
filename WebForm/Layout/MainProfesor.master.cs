using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.Layout
{
    public partial class MainProfesor : System.Web.UI.MasterPage
    {
        private LKServicioWebClient daoServicio;
        protected void Page_Load(object sender, EventArgs e)
        {
            daoServicio = new LKServicioWebClient();
            usuario profesor = (usuario)Session["Usuario"];
            int idSalon = -1;
            //verificamos si es tutor o no  lo es
            if (!IsPostBack)
            {
                idSalon = daoServicio.esTutorAsignado(profesor.dni);
                Session["idsalon"] = idSalon;
            }
        }
    }
}