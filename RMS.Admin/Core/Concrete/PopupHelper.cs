using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace RMS.Admin.Core.Concrete
{
    public static class PopupHelper
    {
        public static void ShowPopup(string id, Control control, bool addScript = false)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('"+id+"').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "ModalScript", sb.ToString(), addScript);
        }
        public static void HidePopup(string id, Control control, bool addScript = false)
        {
            var sb = new System.Text.StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('"+id+"').modal('hide');");
            sb.Append("$('.modal-backdrop').remove();");
            sb.Append(@"</script>");
            ScriptManager.RegisterClientScriptBlock(control, control.GetType(), "ModalScript", sb.ToString(), addScript);
        }
    }
}