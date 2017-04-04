using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Com.Comm100.Forum.UI
{
    public partial class ajaxcall : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

               
        [WebMethod]
        public static string DeleteMyTopic(int topicid)
        {
            try
            {
                ForumTopicDAL objTop = new ForumTopicDAL();
                int op = objTop.DeleteMyPost(SiteSession.CurrentUser.UserOrOperatorId, topicid);
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
    }
}