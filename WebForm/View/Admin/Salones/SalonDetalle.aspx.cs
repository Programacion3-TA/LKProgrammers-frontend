using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebForm.ServicioWS;

namespace WebForm.View.Admin.Salones
{
    public partial class SalonDetalle : System.Web.UI.Page
    {
        private string salonId;
        private LKServicioWebClient serviciodao;
        private BindingList<alumno> alumnosSalon;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                serviciodao = new LKServicioWebClient();
                salonId = Request.QueryString["salonId"];
                cargarTabla();
                // Retrieve the salon ID from the query string

                // Display the salon ID in the Literal control
                LitSalonId.Text = salonId;

            }
        }

        private void cargarTabla()
        {
            var list = serviciodao.listarAlumnosxsalon(Convert.ToInt32(salonId));
            if (list != null)
            {
                alumnosSalon = new BindingList<alumno>(list);
            }

            GridSalones.DataSource = alumnosSalon;
            GridSalones.DataBind();
        }
    }
}