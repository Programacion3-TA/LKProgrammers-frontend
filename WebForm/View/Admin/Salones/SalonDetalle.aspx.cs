using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebForm.View.Admin.Salones
{
    public partial class SalonDetalle : System.Web.UI.Page
    {
        private string salonId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Retrieve the salon ID from the query string
                salonId = Request.QueryString["salonId"];

                // Display the salon ID in the Literal control
                LitSalonId.Text = salonId;
                
            }
        }
    }
}