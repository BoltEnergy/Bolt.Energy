using Com.Comm100.Forum.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Comm100.Forum.UI
{
    public partial class prelogin : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
          
            if (Request.QueryString["action"] != null && (Request.QueryString["userid"] != null || Request.QueryString["email"] != null) && Request.QueryString["action"] == "directlogin")
            {
                if(true)// (Request.UrlReferrer != null && Request.UrlReferrer.ToString().ToLower().Contains(WebUtility.GetAppSetting("BoltURL")))
                {
                    string useremail = Convert.ToString(Request.QueryString["userid"]);
                    if (string.IsNullOrEmpty(useremail))
                        useremail = Convert.ToString(Request.QueryString["email"]);

                    string fname = Convert.ToString(Request.QueryString["fname"]);
                    string lname = Convert.ToString(Request.QueryString["lname"]);
                    string key = Convert.ToString(Request.QueryString["key"]);

                    UserBE currUser = new MongoCon().Validateuser(useremail, key, isEncrypted: true);

                    if (currUser != null)
                    {
                        string userIp = "";
                        try
                        {
                            userIp = Request.ServerVariables[Constants.Remote_address];
                        }
                        catch (Exception ex)
                        {

                        }

                        LoginUserBE user = new LoginUserBE
                        {
                            Email = useremail,
                            FirstName = currUser.firstName,
                            LastName = currUser.lastName,
                            IpAddress = userIp,
                            MongoUID = currUser._id.ToString(),
                            AccountType=currUser.accountType
                        };

                        SiteSession.DirectLoginUser = user;

                       // DirectUserlogin();


                    }

                  

                    //SiteSession.DirectLoginUser = user;
                    Response.Redirect("directlogin.aspx");
                }
                else
                    Response.Redirect(WebUtility.GetAppSetting("BaseURL"));
            }
            else
                Response.Redirect(WebUtility.GetAppSetting("BaseURL"));
        }
    }
}