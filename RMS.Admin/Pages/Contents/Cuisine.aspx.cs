using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;

namespace RMS.Admin.Pages.Contents
{
    public partial class Cuisine : System.Web.UI.Page
    {
        private static int currentId;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvCuisines_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditCuisine")
            {
                var cuisineManager = new CuisineManager();
                fvCuisineEdit.ChangeMode(FormViewMode.Edit);
                currentId = Convert.ToInt32(e.CommandArgument);
                var temp = new List<DataModel.Model.Cuisine>();
                temp.Add(cuisineManager.GetById(currentId));
                fvCuisineEdit.DataSource = temp;
                fvCuisineEdit.DataBind();

                gvCuisines.DataBind();
                ShowModal();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var txtName = fvCuisineEdit.FindControl("txtName") as TextBox;
            var cuisineManager = new CuisineManager();



            if (fvCuisineEdit.CurrentMode == FormViewMode.Edit)
            {
                var cuisine = cuisineManager.GetById(currentId);
                cuisine.Name = txtName.Text;
                cuisineManager.Update(cuisine);
            }
            else if (fvCuisineEdit.CurrentMode == FormViewMode.Insert)
            {
                var cuisine = new DataModel.Model.Cuisine();
                cuisine.Name = txtName.Text;
                cuisineManager.Add(cuisine);   
            }
            gvCuisines.DataBind();
            HideModal();
            //Response.Redirect(Request.RawUrl);
        }
        public void ShowModal()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#pop').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }
        public void HideModal()
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#pop').modal('hide');");
            sb.Append("$('.modal-backdrop').remove();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ModalScript", sb.ToString(), false);
        }
        protected void btnPopupAdd_OnClick(object sender, EventArgs e)
        {
            fvCuisineEdit.ChangeMode(FormViewMode.Insert);
            
            ShowModal();
        }
    }
}