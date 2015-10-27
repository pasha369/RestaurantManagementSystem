using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess.Concrete;
using DataModel.Model;

namespace RMS.Admin.Pages
{
    public partial class Messages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var lstReview = new MessageManager().GetAllReview();
                
                ddlAuthor.DataSource = lstReview.Select(r => r.Author).Distinct();
                ddlAuthor.DataBind();
                ddlAuthor.Items.Insert(0, "------");
                ddlAuthor.SelectedIndex = 0;

                grvReview.DataSource = lstReview;
                grvReview.DataBind();

            }
        }

        protected void btnSpamClick(object sender, EventArgs e)
        {
            var lstReview = GetIdxReviews();

            foreach (var review in lstReview)
            {
                var msgManager = new MessageManager();
                msgManager.CheckSpam(review);
            }
            Response.Redirect(Request.RawUrl);
        }

        protected void btnApplyClick(object sender, EventArgs e)
        {
            var lstReview = GetIdxReviews();

            foreach (var review in lstReview)
            {
                var msgManager = new MessageManager();
                msgManager.ApplyReview(review);
            }
            Response.Redirect(Request.RawUrl);
        }

        private List<DataModel.Model.Review> GetIdxReviews()
        {
            var lstReview = new List<DataModel.Model.Review>();

            foreach (GridViewRow row in grvReview.Rows)
            {
                var chbx = (CheckBox)row.FindControl("chbxSelect");
                if (chbx.Checked)
                {
                    var dataKey = grvReview.DataKeys[row.DataItemIndex];
                    if (dataKey != null)
                    {
                        var review = new DataModel.Model.Review();
                        review.Id = (int)dataKey.Values["Id"];
                        lstReview.Add(review);
                    }
                }
            }
            return lstReview;
        }

        protected void ddlAuthor_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            List<Review> lstMsg;
            var messageManager = new MessageManager();

            var curAuthor = ddlAuthor.SelectedValue.ToString(CultureInfo.InvariantCulture);
            if (curAuthor == "------")
            {
                lstMsg = messageManager.GetAllReview();
            }
            else
            {
                lstMsg = messageManager
                    .GetAllReview()
                    .Where(r => Convert.ToInt32(ddlAuthor.SelectedValue) == r.Author.Id)
                    .ToList();
            }

            grvReview.DataSource = lstMsg;
            grvReview.DataBind();
        }


    }
}