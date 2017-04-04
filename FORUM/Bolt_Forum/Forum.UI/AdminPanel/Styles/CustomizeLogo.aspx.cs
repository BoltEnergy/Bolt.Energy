using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;

namespace Com.Comm100.Forum.UI.AdminPanel.Styles
{
    public partial class CustomizeLogo : AdminBasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                SiteWithPermissionCheck tmpSite = SiteProcess.GetSiteById(
                    CurrentUserOrOperator.UserOrOperatorId,CurrentUserOrOperator.SiteId);
               
                if (tmpSite.IfCustomizeLogo && tmpSite.CustomizeLogo != null && tmpSite.CustomizeLogo.Length > 0)
                {
                    Response.ClearContent();
                    Response.ContentType = "image/gif";
                    Response.BinaryWrite(tmpSite.CustomizeLogo);
                }
            }
            catch (Exception exp)
            {
                LogHelper.WriteExceptionLog(exp);
            }
        }
    }
}
