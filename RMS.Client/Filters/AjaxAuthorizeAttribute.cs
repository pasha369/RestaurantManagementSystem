using System.Net.Http;
using System.Web;

namespace RMS.Client.Filters
{
    public class AjaxAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (IsAjaxRequest())
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }

        public static bool IsAjaxRequest()
        {
            return HttpContext.Current.Request.Headers["X-Requested-With"] != null && HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
    }
}