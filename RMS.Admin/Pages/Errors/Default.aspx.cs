using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RMS.Admin.Pages.Errors
{
    public partial class Default : System.Web.UI.Page
    {
        public class ErrorDetail
        {
            public string Code { get; set; }
            public string What { get; set; }
            public string Why { get; set; }
            public string Suggestion { get; set; }
        }

      
        protected void Page_Load(object sender, EventArgs e)
        {
            Exception error = Server.GetLastError();
            ErrorDetail errorDetail = GetErrorDetail(GetExceptionCause(error));
            DisplayErrorDetail(errorDetail);
            Server.ClearError();
        }

        private void DisplayErrorDetail(ErrorDetail errorDetail)
        {
            lblCode.Text = errorDetail.Code;
            lblErrorWhy.Text = errorDetail.Why;
            lblErrorWhat.Text = errorDetail.What;

        }

        private ErrorDetail GetErrorDetail(Exception exception)
        {
            var errorDetail = new ErrorDetail();
            var httpException = exception as HttpException;
            if (httpException != null)
                errorDetail.Code = httpException.GetHttpCode().ToString();
            errorDetail.Why = "--";
            errorDetail.What = exception.Message;
            errorDetail.Suggestion = exception.HelpLink;
            return errorDetail;
        }

        private Exception GetExceptionCause(Exception error)
        {
            while (error.InnerException != null)
            {
                error = error.InnerException;
            }
            return error;
        }
    }
}