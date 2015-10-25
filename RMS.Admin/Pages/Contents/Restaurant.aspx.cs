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

namespace RMS.Admin.Pages.Contents
{
    public partial class Restaurant : System.Web.UI.Page
    {
        public static List<DataModel.Model.Cuisine> Cuisines = new List<DataModel.Model.Cuisine>();

        static RestorauntModel restorauntModel = new RestorauntModel();

        protected void Page_Load(object sender, EventArgs e)
        {
   


        }



        

        protected void gvCuisines_OnDataBinding(object sender, EventArgs e)
        {
            var gvCuisines = sender as GridView;
            gvCuisines.DataSource = restorauntModel.Cuisines;

        }

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
    }
}