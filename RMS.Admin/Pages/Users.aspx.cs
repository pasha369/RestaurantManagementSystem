using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;
using DataAccess.Concrete.User;
using DataModel.Model;
using RMS.Admin.Core.Concrete;

namespace RMS.Admin.Pages
{
    public partial class Users : System.Web.UI.Page
    {
        private static int currentId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvClients_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                var clientManager = new ClientManager();
                currentId = Convert.ToInt32(e.CommandArgument);
                var client = clientManager.Get(currentId);
                fvRestaurateur.ChangeMode(FormViewMode.Edit);
                var src = new List<ClientInfo>();
                src.Add(client);
                fvRestaurateur.DataSource = src;
                fvRestaurateur.DataBind();
                PopupHelper.ShowPopup("#pop",this);
            }
            if(e.CommandName == "ToBlackLst")
            {
                var userManager = new UserDataManager();

                var user = userManager.GetById(Convert.ToInt32(e.CommandArgument));
                userManager.SetToBlackList(user);
            }
        }

        protected void GridView1_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "ToBlackLst")
            {

                var userManager = new UserDataManager();

                var user = userManager.GetById(Convert.ToInt32(e.CommandArgument));
                userManager.SetToBlackList(user);
                Response.Redirect(Request.RawUrl);
            }
        }

        // Add async trigger to button black list
        protected void btnToBlackLst_Load(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            udpClients.Triggers.Add(new AsyncPostBackTrigger { ControlID = btn.UniqueID, EventName = "Click" });
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var ddlRestaurant = GetDdlValue(fvRestaurateur, "ddlRestaurant");
            var ddlUsers = GetDdlValue(fvRestaurateur, "ddlUsers");
            
            var rstManager = new RestaurantManager();
            var clientManager = new ClientManager();
            var userManager = new UserManager();

            if (fvRestaurateur.CurrentMode == FormViewMode.Edit)
            {
                var client = clientManager.Get(currentId);
                client.Restaurant = rstManager.Get(ddlRestaurant);
                
                var user = userManager.Get(ddlUsers);
                user.Position = Role.Restaurateur;
                userManager.Update(user);
                client.UserInfo = user;
                clientManager.Update(client);
                

            }
            else if (fvRestaurateur.CurrentMode == FormViewMode.Insert)
            {
                var client = new ClientInfo();
                client.Restaurant = rstManager.Get(ddlRestaurant);

                var user = userManager.Get(ddlUsers);

                user.Position = Role.Restaurateur;
                userManager.Update(user);
                client.UserInfo = user;

                clientManager.Add(client);


            }
            PopupHelper.HidePopup("#pop", this);
            gvClients.DataBind();
        }

        protected void btnAddClient_OnClick(object sender, EventArgs e)
        {
            fvRestaurateur.ChangeMode(FormViewMode.Insert);
            PopupHelper.ShowPopup("#pop", this);
        }

        public int GetDdlValue(FormView fv, string name)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl.SelectedValue != null)
                return Convert.ToInt32(ddl.SelectedValue);
            return -1;
        }

        protected void ddlRestaurant_OnDataBinding(object sender, EventArgs e)
        {
            var rstManager = new RestaurantManager();

            var ddl = sender as DropDownList;
            ddl.DataSource = rstManager.Get();
            
        }

        protected void ddlUsers_OnDataBinding(object sender, EventArgs e)
        {
            var userManager = new UserManager();

            var ddl = sender as DropDownList;
            ddl.DataSource = userManager.Get();
        }
    }
}