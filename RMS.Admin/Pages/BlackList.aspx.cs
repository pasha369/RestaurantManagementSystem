using System;
using System.Web.UI.WebControls;
using DataAccess.Concrete.User;

namespace RMS.Admin.Pages
{
    public partial class BlackList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvBlackList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Remove")
            {
                UserDataManager userDataManager = new UserDataManager();
                var Arg = e.CommandArgument;
                userDataManager.RemoveFromBlackList(Convert.ToInt32(e.CommandArgument));
            }
            Response.Redirect(Request.RawUrl);
        }
    }
}