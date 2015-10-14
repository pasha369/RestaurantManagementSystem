using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RMS.Admin.Core.Abstract;
using RMS.Admin.Core.Concrete;

namespace RMS.Admin
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_OnClick(object sender, EventArgs e)
        {
            ISecurityService securityService = new FormsSecurityService();
            securityService.Logout();

        }

    }
}