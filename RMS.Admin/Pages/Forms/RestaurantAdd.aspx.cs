using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Abstract;
using DataAccess.Concrete;
using DataModel.Model;
using RMS.Admin.Model;

namespace RMS.Admin.Pages.Forms
{
    public partial class RestaurantAdd : System.Web.UI.Page
    {
        

        static RestorauntModel restorauntModel = new RestorauntModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            BindDdl<DataModel.Model.Cuisine>(fvRestorauntEdit, "ddlCuisines", new CuisineManager());
            BindDdl<DataModel.Model.City>(fvRestorauntEdit, "ddlCity", new CityManager());
            BindDdl<DataModel.Model.Country>(fvRestorauntEdit, "ddlCountry", new CountryManager());
        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var restManager = new RestaurantManager();
            var cityManager = new CityManager();
            var countryManager = new CountryManager();

            var restaurant = new Restaurant();

            restaurant = new DataModel.Model.Restaurant();
            restaurant.Name = (fvRestorauntEdit.FindControl("txtName") as TextBox).Text;
            restaurant.PhoneNumber = (fvRestorauntEdit.FindControl("txtPhone") as TextBox).Text;
            if (restaurant.Adress == null)
                restaurant.Adress = new DataModel.Model.Address();
            if (GetDdlValue(fvRestorauntEdit, "ddlCity") != -1)
                restaurant.Adress.City = cityManager.GetById(GetDdlValue(fvRestorauntEdit, "ddlCity"));
            if (GetDdlValue(fvRestorauntEdit, "ddlCountry") != -1)
                restaurant.Adress.Country = countryManager.GetById(GetDdlValue(fvRestorauntEdit, "ddlCountry"));
            restaurant.Adress.Street = (fvRestorauntEdit.FindControl("txtStreet") as TextBox).Text;
            restaurant.Description = (fvRestorauntEdit.FindControl("txtDesc") as TextBox).Text;

            restaurant.Cuisines = restorauntModel.Cuisines;
            restManager.Add(restaurant);

            Response.Redirect("~/Pages/Contents/Restaurant.aspx");


        }
        // Bind data to dropdownlist
        private void BindDdl<T>(FormView fv, string name, IDataManager<T> manager)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl != null)
            {
                ddl.DataSource = manager.GetAll();
                ddl.DataBind();
            }
        }
        // Get value from dropdownlist
        public int GetDdlValue(FormView fv, string name)
        {
            var ddl = fv.FindControl(name) as DropDownList;
            if (ddl.SelectedValue != null)
                return Convert.ToInt32(ddl.SelectedValue);
            return -1;
        }


        protected void gvCuisines_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var cuisine = restorauntModel.Cuisines.FirstOrDefault(c => c.Id == Convert.ToInt32(e.CommandArgument));
                restorauntModel.Cuisines.Remove(cuisine);
                var gvCuisines = fvRestorauntEdit.FindControl("gvCuisines") as GridView;
                gvCuisines.DataSource = restorauntModel.Cuisines;
                gvCuisines.DataBind();
            }
        }

        protected void btnAddCusine_OnClick(object sender, EventArgs e)
        {

            var ddlCuisines = fvRestorauntEdit.FindControl("ddlCuisines") as DropDownList;
            var gvCuisines = fvRestorauntEdit.FindControl("gvCuisines") as GridView;

            var cuisineManager = new CuisineManager();

            int id = Convert.ToInt32(ddlCuisines.SelectedValue);

            var cuisine = cuisineManager.GetById(new DataModel.Model.Cuisine() { Id = id });
            restorauntModel.Cuisines.Add(cuisine);
            gvCuisines.DataSource = restorauntModel.Cuisines;
            gvCuisines.DataBind();
        }

        protected void btnCancel_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/Contents/Restaurant.aspx");

        }
    }
}