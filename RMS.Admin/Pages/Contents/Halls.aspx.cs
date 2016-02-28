using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;

namespace RMS.Admin.Pages.Contents
{
    public partial class Halls : System.Web.UI.Page
    {
        private static int currentId;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        
        {
            if(fvHall.CurrentMode == FormViewMode.Edit)
            {
                var t = fvHall.DataItem;
                var ddlRestaurant = fvHall.FindControl("ddlRestoraunt") as DropDownList;
                var txtNumber = fvHall.FindControl("txtNumber") as TextBox;

                var restaurantId = Convert.ToInt32(ddlRestaurant.SelectedValue);

                var dataManager = new RestaurantManager();
                var hallManager = new HallManager();

                var curRestaurant = dataManager.Get(restaurantId);

                var hall = hallManager.Get(currentId);
                hall.Number = txtNumber.Text;
                hall.Restaurant = curRestaurant;

                hallManager.Update(hall);
            }
            if(fvHall.CurrentMode == FormViewMode.Insert)
            {
                var ddlRestaurant = fvHall.FindControl("ddlRestoraunt") as DropDownList;
                var txtNumber = fvHall.FindControl("txtNumber") as TextBox;

                var restaurantId = Convert.ToInt32(ddlRestaurant.SelectedValue);

                var dataManager = new RestaurantManager();
                var hallManager = new HallManager();

                var curRestaurant = dataManager.Get(restaurantId);

                var hall = new DataModel.Model.Hall();
                hall.Number = txtNumber.Text;
                hall.Restaurant = curRestaurant;

                hallManager.Add(hall);
            }

            gvHall.DataBind();
            HideModal();
        }

        private DropDownList BindDdl<T>(FormView fv, string name, IDataManager<T> manager)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl != null)
            {
                ddl.DataSource = manager.Get();
                ddl.DataBind();
            }
            return ddl;
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            fvHall.ChangeMode(FormViewMode.Insert);
            ShowModal();
        }

        /// <summary>
        /// Run bootstrap modal window
        /// </summary>
        private void ShowModal()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#pop').modal('hide');");
            sb.Append("$('.modal-backdrop').remove();");
            sb.Append("$('#pop').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }
        /// <summary>
        /// Hide bootstrap modal window
        /// </summary>
        public void HideModal()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#pop').modal('hide');");
            sb.Append("$('.modal-backdrop').remove();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }

        protected void gvHall_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Modify")
            {
                currentId = Convert.ToInt32(e.CommandArgument);

                fvHall.ChangeMode(FormViewMode.Edit);
                ShowModal();
            }
        }

        protected void fvHall_OnPreRender(object sender, EventArgs e)
        {
            var fv = sender as FormView;

            BindDdl(sender as FormView, "ddlRestoraunt", new RestaurantManager());

            if (fv.CurrentMode == FormViewMode.Edit)
            {
                var hallManager = new HallManager();
                var hall = hallManager.Get(currentId);


                var txtNumber = fv.FindControl("txtNumber") as TextBox;
                var ddlR = fv.FindControl("ddlRestoraunt") as DropDownList;
                
                ddlR.Items.FindByValue(hall.Restaurant.Id.ToString()).Selected = true;
                txtNumber.Text = hall.Number;
            }
        }
    }
}