using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;

using System.Web.UI.WebControls;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum;

namespace Com.Comm100.Forum.UI.UserControl
{
    public partial class selectUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitDropDownList();
                bind();
            }
        }

        void bind()
        {
            int userType = GetUserType();

            if (userType == -1)
            {
                this.rpUser.DataSource = UserProcess.GetAllUserNotDelete(SiteId, CurrentUserOrOperator.UserOrOperatorId);
                this.rpUser.DataBind();
            }
            else if (userType == 99)
            {
                this.rpUser.DataSource = AdministratorProcess.GetAllAdministrators(
                    CurrentUserOrOperator.SiteId, "id", "asc", CurrentUserOrOperator.UserOrOperatorId);
                this.rpUser.DataBind();
            }
            else if(userType == 1)
            {
                this.rpUser.DataSource = AdministratorProcess.GetAllNotAdministratorsByKeyWordAndUserType(
                    txtKeyWord.Text, Com.Comm100.Framework.Enum.EnumUserType.Operator, "id", "asc", CurrentUserOrOperator.SiteId);
                this.rpUser.DataBind();
            }
            else if (userType == 2)
            {
                this.rpUser.DataSource = AdministratorProcess.GetAllNotAdministratorsByKeyWordAndUserType(
                    txtKeyWord.Text, Com.Comm100.Framework.Enum.EnumUserType.User, "id", "asc", CurrentUserOrOperator.SiteId);
                this.rpUser.DataBind();
            }
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (RepeaterItem r in rpUser.Items)
                {
                    System.Web.UI.HtmlControls.HtmlInputCheckBox chbUser = r.FindControl("chbUser") as HtmlInputCheckBox;

                    if (chbUser.Checked)
                    {
                        int id = int.Parse(chbUser.Value);
                        AdministratorProcess.AddAdministrator(id, this.SiteId, this.UserOrOperatorId);
                    }
                }
            }
            catch (Exception ex)
            {
                this.RegisterStartupScript("msg", string.Format("<script>closeWindow()</script>", ex.Message));
            }
        }

        protected void btnQuery_Click(object sender, EventArgs e)
        {
            this.bind();
        }
        void InitDropDownList()
        {
            ddlUserType.Items.Add("ALL");
#if HOSTED  
            ddlUserType.Items.Add("Admin");
#endif
            ddlUserType.Items.Add("User");
            ddlUserType.Items.Add("Registered User");
        }

        int GetUserType()
        {
            switch (ddlUserType.SelectedValue)
            {                
                case "User":
                    {
                        return Convert.ToInt32(EnumUserType.User);
                    }
                case "Registered User":
                    {
                        return Convert.ToInt32(EnumUserType.Operator);
                    }
                    case "Admin":
                    {
                        return 99;
                    }
            }

            return -1;
        }


    }
}
