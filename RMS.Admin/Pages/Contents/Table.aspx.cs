using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;
using DataModel.Model;

namespace RMS.Admin.Pages.Contents
{
    public partial class Table : System.Web.UI.Page
    {
        private static int currentId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }
        private string GetValueFromTextbox(string id)
        {
            var tb = fvTable.FindControl(id) as TextBox;
            return (tb != null) ? tb.Text : string.Empty;
        }

        protected void ddlRestaurant_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var dataManager = new RestaurantManager();

            var ddlRestaurant = fvTable.FindControl("ddlRestaurant") as DropDownList;
            var ddlHall = fvTable.FindControl("ddlHall") as DropDownList;

            var restaurantId = Convert.ToInt32(ddlRestaurant.SelectedValue);
            var restaurant = dataManager.GetById(new DataModel.Model.Restaurant(){Id = restaurantId});

            ddlHall.DataSource = restaurant.Halls;
            ddlHall.DataBind();
            ShowModal();
        }

        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            fvTable.ChangeMode(FormViewMode.Insert);
            ShowModal();
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var dataManager = new RestaurantManager();
            var hallManager = new HallManager();
            var tableManager = new DinnerTableManager();
            var restManager = new RestaurantManager();
            if(fvTable.CurrentMode == FormViewMode.Insert)
            {
                var ddlHall = fvTable.FindControl("ddlHall") as DropDownList;
                var hallId = Convert.ToInt32(ddlHall.SelectedValue);

                var element = (fvTable.FindControl("ddlRestaurant") as DropDownList).SelectedValue;
                int Id = Convert.ToInt32(element);
                var dinnerTable = new DataModel.Model.DinnerTable();

                dinnerTable.Restaurant = dataManager.GetById(Id);
                dinnerTable.Number = Convert.ToInt32(GetValueFromTextbox("txtNumber"));
                dinnerTable.Hall = hallManager.GetById(new Hall() { Id = hallId });

                tableManager.Add(dinnerTable);
                HideModal();
            }
            if (fvTable.CurrentMode == FormViewMode.Edit)
            {
                var table = tableManager.GetById(currentId);
                table.Number = Convert.ToInt32(GetValueFromTextbox("txtNumber"));
                table.Restaurant = restManager.GetById(GetDdlValue(fvTable, "ddlRestaurant"));
                table.Hall = hallManager.GetById(GetDdlValue(fvTable, "ddlHall"));
                tableManager.Update(table);
            }
            gvTables.DataBind();
            HideModal();
        }

        public int GetDdlValue(FormView fv, string name)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl.SelectedValue != null)
                return Convert.ToInt32(ddl.SelectedValue);
            return -1;
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

        protected void gvTables_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Modify")
            {
                currentId = Convert.ToInt32(e.CommandArgument);
                var tableManager = new DinnerTableManager();
                fvTable.ChangeMode(FormViewMode.Edit);
                var lst = new List<DinnerTable>();
                lst.Add(tableManager.GetById(currentId));
                fvTable.DataSource = lst;
                fvTable.DataBind();
                ShowModal();
            }
        }

        protected void ddlHall_OnDataBinding(object sender, EventArgs e)
        {
            var ddl = sender as DropDownList;
            var dataManager = new RestaurantManager();

            var ddlRestaurant = fvTable.FindControl("ddlRestaurant") as DropDownList;

            var restaurantId = Convert.ToInt32(ddlRestaurant.SelectedValue);
            var restaurant = dataManager.GetById(restaurantId);

            ddl.DataSource = restaurant.Halls;
        }
    }
}