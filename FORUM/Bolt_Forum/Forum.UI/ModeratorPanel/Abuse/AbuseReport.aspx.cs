#if OPENSOURCE
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  Copyright(C) Comm100 Network Corporation 2010                                                                               //
//  This software is licensed under Microsoft Reciprocal License, an open source license ceritified by Open Source Initiative   //
//  You must always retain this copyright notice in this file                                                                   //
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Com.Comm100.Forum.Bussiness;
using Com.Comm100.Forum.Process;
using Com.Comm100.Framework.Common;
using Com.Comm100.Framework.Enum.Forum;
using Com.Comm100.Forum.Language;

namespace Com.Comm100.Forum.UI.ModeratorPanel.Abuse
{
    public partial class AbuseReport : Com.Comm100.Forum.UI.ModeratorPanel.ModeratorBasePage
    {
        #region Page Property
        public string SortFiled
        {
            set { ViewState["SortFiled"] = value; }
            get { return ViewState["SortFiled"].ToString(); }
        }
        public string SortMethod
        {
            set { ViewState["SortMethod"] = value; }
            get { return ViewState["SortMethod"].ToString(); }
        }

        public Image SortFiledImage
        {
            set { ViewState["SortFiledImage"] = value.ID; }
            get
            {
                return this.tbHeader.FindControl(
                    ViewState["SortFiledImage"].ToString()) as Image;
            }
        }
        public void ChangeSort()
        {
            Image img = SortFiledImage;
            if (SortMethod.Equals("desc"))
            {
                SortMethod = "asc";
                img.ImageUrl = "~/images/sort_up.gif";
            }
            else
            {
                SortMethod = "desc";
                img.ImageUrl = "~/images/sort_down.gif";
            }
            img.Visible = true;
        }

        public void CurrentSort()
        {
            if (SortMethod.Equals("asc"))
            {
                SortFiledImage.ImageUrl = "~/images/sort_up.gif";
            }
            else
            {
                SortFiledImage.ImageUrl = "~/images/sort_down.gif";
            }
            //SortFiledImage.Visible = true;
        }
        #endregion

        #region Language
        string ErrorLoad;
        string AbuseStatusAll;
        string AbuseStatusPending;
        string AbuseStatusApproved;
        string AbuseStatusRefused;
        protected override void InitLanguage()
        {
            try
            {
                ErrorLoad = Proxy[EnumText.enumForum_Abuse_AbusedReportsLoadError];
                AbuseStatusAll = Proxy[EnumText.enumForum_Abuse_StatusAll];
                AbuseStatusPending=Proxy[EnumText.enumForum_Abuse_StatusPending];
                AbuseStatusApproved=Proxy[EnumText.enumForum_Abuse_StatusApproved];
                AbuseStatusRefused=Proxy[EnumText.enumForum_Abuse_StatusRefused];
                this.lblTitle.Text = Proxy[EnumText.enumForum_Abuse_AbusedReportsTitle];
                this.lblSubTitle.Text = Proxy[EnumText.enumForum_Abuse_AbusedReportsSubTitle];
                this.btnQuery1.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
                this.btnQuery2.Text = Proxy[EnumText.enumForum_Topic_ButtonQuery];
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = Proxy[EnumText.enumForum_Public_InitializatingLanguageError] + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                ((ModeratorMasterPage)Master).SetMenuSyle(EnumAdminMenuType.enumAbuse);
                if (!IsPostBack)
                {
                    /*Init Abuse Stutas Control*/
                    InitStutasControl();

                    /*default sort*/
                    this.SortFiled = "Date";
                    this.SortMethod = "desc";
                    this.SortFiledImage = imgReportDate;
                    SortFiledImage.ImageUrl = "~/images/sort_down.gif";
                    SortFiledImage.Visible = true;
                    RefreshData();
                }
                CurrentSort();
            }
            catch (Exception exp)
            {
                this.lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        #region Control Event
        protected void aspnetPager_Paging(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.PagingEventArgs pe)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void aspnetPager_ChangePageSize(object sender, Com.Comm100.Framework.WebControls.ASPNetPager.ChangePageSizeEventArgs ce)
        {
            try
            {
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnSort_click(object sender, CommandEventArgs e)
        {
            try
            {
                SetSortField(e.CommandName.ToString());
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad + exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }

        protected void btnQuery1_Click(object sender, EventArgs e)
        {
            try
            {
                this.aspnetPager.PageIndex = 0;
                RefreshData();
            }
            catch (Exception exp)
            {
                lblMessage.Text = ErrorLoad+ exp.Message;
                LogHelper.WriteExceptionLog(exp);
            }
        }
        #endregion

        #region Custom Methods
        protected void InitStutasControl()
        {
            this.ddlState.Items.Add(new ListItem(AbuseStatusAll, "-1"));
            this.ddlState.Items.Add(new ListItem(
                AbuseStatusRefused, Convert.ToInt32(EnumAbuseStatus.Refused).ToString()));
            this.ddlState.Items.Add(new ListItem(
                AbuseStatusApproved, Convert.ToInt32(EnumAbuseStatus.Approved).ToString()));
            this.ddlState.Items.Add(new ListItem(
                AbuseStatusPending, Convert.ToInt32(EnumAbuseStatus.Pending).ToString()));
        }

        public string GetStatus(EnumAbuseStatus status)
        {
            string statuString="";
            switch (status)
            {
                case EnumAbuseStatus.Pending:
                    statuString=AbuseStatusPending;
                    break;
                case EnumAbuseStatus.Approved:
                    statuString = AbuseStatusApproved;
                    break;
                case EnumAbuseStatus.Refused:
                    statuString = AbuseStatusRefused;
                    break;
            }
            return statuString;
        }

        protected void CheckQueryStutasSelected(out bool ifAllStatus, out EnumAbuseStatus status)
        {
            ifAllStatus = true;
            status = EnumAbuseStatus.Pending;
            if (this.ddlState.SelectedValue != "" && this.ddlState.SelectedValue != "-1")
            {
                ifAllStatus = false;
                status = (EnumAbuseStatus)Enum.Parse(typeof(EnumAbuseStatus), this.ddlState.SelectedValue);
            }
        }

        private void RefreshData()
        {
            /*To Show current Image */
            SortFiledImage.Visible = true;
            /*Check Query Filed*/
            int count = 0;
            bool ifAllStatus = true;
            string keywords = this.txtKeywords.Text;
            EnumAbuseStatus status = EnumAbuseStatus.Pending;
            CheckQueryStutasSelected(out ifAllStatus, out status);
            count = AbuseProcess.GetCountOfAbusesByModeratorWithQuery(this.UserOrOperatorId, this.SiteId, keywords, status, ifAllStatus);

            AbuseWithPermissionCheck[] abuses = AbuseProcess.GetAbusesByModeratorWithQueryAndPaging(
                CurrentUserOrOperator.UserOrOperatorId, SiteId, this.txtKeywords.Text, status, ifAllStatus,
                this.aspnetPager.PageIndex + 1, this.aspnetPager.PageSize,
                SortFiled, SortMethod);

            if (count == 0)
            {
                aspnetPager.Visible = false;
                rptData.DataSource = null;
                rptData.DataBind();
            }
            else
            {
                aspnetPager.CWCDataBind(this.rptData, abuses, count);
                aspnetPager.Visible = true;
            }
        }

        private void SetSortField(string sortFiled)
        {
            switch (sortFiled)
            {
                #region Sort Filed
                case "Report Date":
                    {
                        SortFiledImage = this.imgReportDate;
                        SortFiled = "Date";
                        ChangeSort();
                        break;
                    }
                case "Report User":
                    {
                        SortFiledImage = this.imgReportUser;
                        SortFiled = "AbuseUserOrOperatorName";
                        ChangeSort();
                        break;
                    }
                case "Post User":
                    {
                        SortFiledImage = this.imgPostUser;
                        SortFiled = "PostUserOrOperatorName";
                        ChangeSort();
                        break;
                    }
                #endregion
            }
        }

        public string GetQueryString(int postId)
        {
            PostWithPermissionCheck post = PostProcess.GetPostByPostId(SiteId, UserOrOperatorId, false, postId);
            TopicWithPermissionCheck topic = TopicProcess.GetTopicByTopicId(SiteId, UserOrOperatorId, post.TopicId);
            return "postId=" + postId + "&topicId=" + post.TopicId + "&forumId=" + topic.ForumId;
        }
        #endregion
    }
}
