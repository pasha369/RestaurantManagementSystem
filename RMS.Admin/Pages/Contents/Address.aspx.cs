using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Admin.Core.Concrete;

namespace RMS.Admin.Pages.Contents
{
    public partial class Address : System.Web.UI.Page
    {
        private static int currentId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void gvCounties_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                var countryManager = new CountryManager();
                fvAddressEdit.ChangeMode(FormViewMode.Edit);
                currentId = Convert.ToInt32(e.CommandArgument);
                var temp = new List<DataModel.Model.Country>();
                // Bind data
                temp.Add(countryManager.Get(currentId));
                fvAddressEdit.DataSource = temp;
                fvAddressEdit.DataBind();
                PopupHelper.ShowPopup("#pop", this);
            }
        }

        protected void btnPopupAdd_OnClick(object sender, EventArgs e)
        {
            fvAddressEdit.ChangeMode(FormViewMode.Insert);
            PopupHelper.ShowPopup("#pop", this);
        }


        protected void btnSaveCountry_OnClick(object sender, EventArgs e)
        {
            var txtName = fvAddressEdit.FindControl("txtName") as TextBox;

            var countryManager = new CountryManager();

            if (fvAddressEdit.CurrentMode == FormViewMode.Edit)
            {
                var country = countryManager.Get(currentId);
                country.Name = txtName.Text;
                countryManager.Update(country);
            }
            if (fvAddressEdit.CurrentMode == FormViewMode.Insert)
            {
                var country = new Country();
                country.Name = txtName.Text;
                countryManager.Add(country);
            }

            gvCounties.DataBind();
            PopupHelper.HidePopup("#pop", this);

        }

        protected void gvCities_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Modify")
            {
                var cityManager = new CityManager();
                fvCity.ChangeMode(FormViewMode.Edit);

                currentId = Convert.ToInt32(e.CommandArgument);
                var temp = new List<DataModel.Model.City>();
                var ddlCountry = fvCity.FindControl("ddlCountry") as DropDownList;
                
                // Bind data
                var city = cityManager.Get(currentId);

                temp.Add(city);
                fvCity.DataSource = temp;
                fvCity.DataBind();

                PopupHelper.ShowPopup("#popCity", this);

                

            }
        }

        protected void lbtnAddCity_OnClick(object sender, EventArgs e)
        {
            fvCity.ChangeMode(FormViewMode.Insert);
            PopupHelper.ShowPopup("#popCity", this);
        }

        protected void btnSaveCity_OnClick(object sender, EventArgs e)
        {
            var txtName = fvCity.FindControl("txtName") as TextBox;
            var countryId = Convert.ToInt32(GetDdlValue(fvCity, "ddlCountry"));

            var cityManager = new CityManager();
            var countryManager = new CountryManager();

            if (fvCity.CurrentMode == FormViewMode.Insert)
            {
                var city = new City();
                city.Name = txtName.Text;
                city.Country = countryManager.Get(countryId);
                cityManager.Add(city);
            }
            if (fvCity.CurrentMode == FormViewMode.Edit)
            {
                var city = cityManager.Get(currentId);
                city.Name = txtName.Text;
                city.Country = countryManager.Get(countryId);
                cityManager.Update(city);
            }
            gvCities.DataBind();
            PopupHelper.HidePopup("#popCity", this);
        }

        public int GetDdlValue(FormView fv, string name)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl.SelectedValue != null)
                return Convert.ToInt32(ddl.SelectedValue);
            return -1;
        }

        protected void ddlCountry_OnPreRender(object sender, EventArgs e)
        {
            var cityManager = new CityManager();
            var city = cityManager.Get(currentId);
            if(city.Country != null)
            {
                (sender as DropDownList).Items.FindByValue(city.Country.Id.ToString()).Selected = true;
            }
        }
    }
}