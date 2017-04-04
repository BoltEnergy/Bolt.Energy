<%@ Page Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserTopicsList.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserTopicsEdit" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="false"></asp:Label>
    </div>
    <div class="divTableHeader">
        <div class="tbl-h-l">
            <div class="tbl-h-r">
                <div class="tbl-h-c">
                    <div class="divTitleUserPanel" style="float: left;margin-top:3px;">
                        <span style="color: #888; font-size: 12px; margin-top: 5px; float: left;">
                            <%=Proxy[EnumText.enumForum_Topic_TitleMyTopics]%>&nbsp; </span>
                        <div style="margin-top: 3px; float: left;">
                            <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_Topic_HelpMyTopics]%>')"
                                onmouseout="CloseHelp()" />
                        </div>
                    </div>
                    <div style="clear: both;">
                        <div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table class="tableForums" cellpadding='0' cellspacing='0' style="_width:auto">
        <tr>
            <td class="tdStatus" width="15%" style="padding-left: 25px; text-align: left">
                <%=Proxy[EnumText.enumForum_Topic_ColumnStatus]%>
            </td>
            <td class="tdSubject" width="30%" style="padding-left: 0">
                <%=Proxy[EnumText.enumForum_Topic_ColumnSubject]%>
            </td>
            <td class="tdDate" width="20%" style="padding-left: 0;">
                <%=Proxy[EnumText.enumForum_Topic_ColumnDate]%>
            </td>
            <td class="tdLastPost" width="18%" style="text-align: left">
                <%=Proxy[EnumText.enumForum_Topic_ColumnLastPost]%>
            </td>
            <td class="tdTopics" width="9%">
                <%=Proxy[EnumText.enumForum_Topic_ColumnReplies]%>
            </td>
            <td class="tdPosts" width="8%">
                <%=Proxy[EnumText.enumForum_Topic_ColumnViews]%>
            </td>
        </tr>
        <asp:Repeater ID="RepeaterForum" runat="server" EnableViewState="false">
            <ItemTemplate>
                <tr class="<%#((this.RepeaterForum.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> " onmousemove="changeCSS(this,'trOnMouseOverStyle');"
                    onmouseout="changeCSS(this,'<%#((this.RepeaterForum.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> ');">
                    <td style="border-left: solid 1px #aaa; text-align: left; padding-left: 25px; padding-top: 5px;
                        padding-bottom: 5px;">
                        <%# StatusImage(Convert.ToBoolean(Eval("IfClosed")), Convert.ToBoolean(Eval("IfMarkedAsAnswer")))%>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px;">
                        <span class="linkTopic"><a href='../Topic.aspx?forumId=<%#Eval("ForumId") %>&topicId=<%#Eval("TopicId")%>&siteId=<%=SiteId %>'
                            target="_blank" title='<%#GetTooltipString(Eval("Subject").ToString()) %>'>
                            <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString( System.Web.HttpUtility.HtmlEncode( Eval("Subject").ToString()),20) %>
                        </a></span>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px;">
                        <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px;">
                        <%#Convert.ToInt32(Eval("NumberOfReplies"))==0 ? "" : "<a target='_blank' href='../Topic.aspx?topicId=" + Eval("TopicId") + "&siteId=" + SiteId + "&forumId=" + Eval("ForumId") + "&postId=-1&goToPost=true&a=1#bottom'>" + Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastPostTime"))) + "</a>"%>
                        <br />
                        <%#Convert.ToInt32(Eval("NumberOfReplies")) == 0 ? "" : Proxy[EnumText.enumForum_Topic_FieldBy]%>
                        <%# Convert.ToInt32(Eval("NumberOfReplies")) == 0 ? "" : Convert.ToBoolean(Eval("LastPostUserOrOperatorIfDeleted")) ? Proxy[EnumText.enumForum_Topic_FieldDeleteUser] : "<span class='linkUser'><a href='../User_Profile.aspx?userId=" + Eval("LastPostUserOrOperatorId") + "&siteid=" + SiteId + "' target='_blank' title='" + GetTooltipString(Eval("LastPostUserOrOperatorName").ToString()) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(Eval("LastPostUserOrOperatorName").ToString()), 8) + "</a></span>"%>
                    </td>
                    <td style="text-align: center; padding-top: 5px; padding-bottom: 5px;">
                        <%#Eval("NumberOfReplies")%>
                    </td>
                    <td style="border-right: solid 1px #aaa; text-align: center; padding-top: 5px; padding-bottom: 5px;">
                        <%#Eval("NumberOfHits")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
           <td colspan="6" class="cat-bottom" style="padding-bottom:5px;padding-left:10px">
                <div >
                    <cc1:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                        OnPaging="aspnetPager_Paging" EnableViewState="True" Mode="ImageButton" PageSize="10">
                    </cc1:ASPNetPager>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="6" class="tdButtom">
                <div class="tbl-f-l">
                    <div class="tbl-f-r">
                        <div class="tbl-f-c">
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    

    <div style="padding-bottom: 5px; padding-top: 10px">
        <b>
           <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
        </b>
    </div>
    <div class="divLegend">
        <table cellpadding="1" cellspacing="1" width='100%'>
            <tr>
                <td width="25%">
                    <img src="../Images/status/participate_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>' align="absmiddle"/>
                    <%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>' align="absmiddle"/>
                    <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/participate_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>' align="absmiddle"/>
                    <%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
