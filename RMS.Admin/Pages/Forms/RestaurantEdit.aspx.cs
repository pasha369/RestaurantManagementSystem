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
    public partial class RestaurantEdit : System.Web.UI.Page
    {
        public static List<DataModel.Model.Cuisine> Cuisines = new List<DataModel.Model.Cuisine>();
        DropDownList _ddlCuisines;
        DropDownList _ddlCountry;
        DropDownList _ddlCity;

        static RestorauntModel restorauntModel = new RestorauntModel();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindData();
            }

        }


        protected void gvCuisines_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                var cuisine = restorauntModel.Cuisines.FirstOrDefault(c => c.Id == Convert.ToInt32(e.CommandArgument));
                restorauntModel.Cuisines.Remove(cuisine);
                var gvCuisines = fvRestorauntEdit.FindControl("gvCuisines") as GridView;
                gvCuisines.DataSource = restorauntModel.Cuisines;
                gvCuisines.DataBind();
            }
        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            var restManager = new RestaurantManager();
            var cityManager = new CityManager();
            var countryManager = new CountryManager();

            var restaurant = new Restaurant();

            var txtName = fvRestorauntEdit.FindControl("txtName") as TextBox;
            var txtPhone = fvRestorauntEdit.FindControl("txtPhone") as TextBox;

            restaurant = restManager.GetById(restorauntModel.Id);
            restaurant.Name = txtName.Text;
            restaurant.PhoneNumber = Convert.ToInt32(txtPhone.Text);
            if (restaurant.Adress == null)
                restaurant.Adress = new DataModel.Model.Address();
            if (GetDdlValue(fvRestorauntEdit, "ddlCity") != -1)
                restaurant.Adress.City = cityManager.GetById(GetDdlValue(fvRestorauntEdit, "ddlCity"));
            if (GetDdlValue(fvRestorauntEdit, "ddlCountry") != -1)
                restaurant.Adress.Country = countryManager.GetById(GetDdlValue(fvRestorauntEdit, "ddlCountry"));
            restaurant.Adress.Street = (fvRestorauntEdit.FindControl("txtStreet") as TextBox).Text;
            restaurant.Description = (fvRestorauntEdit.FindControl("txtDesc") as TextBox).Text;
            restaurant.Cuisines = restorauntModel.Cuisines;
            restManager.Update(restaurant);

            Response.Redirect("~/Pages/Contents/Restaurant.aspx");

        }
        void BindData()
        {
            var dataManager = new RestaurantManager();
            restorauntModel.Id = Convert.ToInt32(Request.QueryString["Id"]);
            var restaurant = dataManager.GetById(restorauntModel.Id);
            var gvCuisines = fvRestorauntEdit.FindControl("gvCuisines") as GridView;
            restorauntModel.Cuisines = restaurant.Cuisines;
            var ddlCity = fvRestorauntEdit.FindControl("ddlCity") as DropDownList;
            var ddlCountry = fvRestorauntEdit.FindControl("ddlCountry") as DropDownList;

            BindDdl<DataModel.Model.Cuisine>(fvRestorauntEdit, "ddlCuisines", new CuisineManager());
            BindDdl<DataModel.Model.City>(fvRestorauntEdit, "ddlCity", new CityManager());
            BindDdl<DataModel.Model.Country>(fvRestorauntEdit, "ddlCountry", new CountryManager());
            if (restaurant.Cuisines.Count > 0)
            {
                gvCuisines.DataSource = restaurant.Cuisines.ToList();
                gvCuisines.DataBind();
            }

            var address = restaurant.Adress;
            if (address != null)
            {
                ddlCountry.SelectedValue = restaurant.Adress.Country.Name;
                ddlCity.SelectedValue = restaurant.Adress.City.Name;
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