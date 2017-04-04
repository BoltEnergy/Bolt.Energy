<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="SearchResult.aspx.cs" Inherits="Com.Comm100.Forum.UI.SearchResult"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script language="javascript" type="text/javascript">

        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
    </script>

    <style type="text/css">
        #tbSuject
        {
            width: 350px;
        }
        #htmlEditor
        {
            width: 352px;
        }
    </style>
    <%--<div class="sitePath">
                <uc:NavigationBar ID="NavSearchResult" runat="server" />
            </div>--%>
    <div class="pos_bottom_10">
        <div class="divLeft">
            <p>
                <b>
                    <%=Proxy[EnumText.enumForum_Topic_TitleSearchResult]%></b>
                <asp:Label ID="lblResultCount" runat="server" EnableViewState="false"></asp:Label>
            </p>
        </div>
        <div class="buttons" style="float: right; padding-right: 15px;">
            <ul class="buttons_menu">
                <li class="next"><a class="move_link" id="btnReturnBack" runat="server" onserverclick="btnReturnBack_Click"
                    href="#" enableviewstate="false"><span class="move">
                        <%=Proxy[EnumText.enumForum_SearchResult_ButtonReturn]%></span><li class="li_end"></li></a></li>
            </ul>
        </div>
        <div class="errorMsg">
            <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label></div>
        <div class="clear">
        </div>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_NavigationBar_SearchResult]%>
                        <%--<asp:Label ID="lblForumName2" runat="server" Text="" EnableViewState="false"></asp:Label>--%></div>
                </div>
            </div>
        </div>
        <asp:Repeater ID="rtpData" runat="server" OnItemDataBound="rtpData_ItemDataBound">
            <HeaderTemplate>
                <table class="tb_forum" cellspacing='0'>
                    <tr>
                        <th width="200px">
                            <p>
                                <%=Proxy[EnumText.enumForum_SearchResult_FiledAuthor]%>
                            </p>
                        </th>
                        <th>
                            <p>
                                <%=Proxy[EnumText.enumForum_SearchResult_FiledMessage]%>
                            </p>
                        </th>
                    </tr>
                   
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td colspan="2" class="row3">
                        <p>
                            <asp:PlaceHolder ID="phaddCode" runat="server"></asp:PlaceHolder>
                        </p>
                    </td>
                </tr>
                <tr class="trOdd">
                    <td class='row1' width="200px" align="center">
                        <p>
                            <%# Convert.ToBoolean(Eval("IfPostUserOrOperatorDeleted")) ? Proxy[EnumText.enumForum_Public_DeletedUser] : "<a class='user_link' href='User_Profile.aspx?userId=" + Eval("PostUserOrOperatorId") + "&Siteid=" + SiteId + "' target='_blank' title='" + GetTooltipString(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'>" + System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString()), 15)) + "</a>"%>
                            <%--<a class="user_link" href='User_Profile.aspx?siteId=<%=SiteId%>&userId=<%#Eval("PostUserOrOperatorId")%>'
                                target="_blank">
                                <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString()))%>
                            </a>--%>
                        </p>
                    </td>
                    <td class='row2'>
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_SearchResult_FiledPostSubject]%></b> <a class="topic_link"
                                    href="Topic.aspx?siteId=<%=SiteId%>&forumId=<%#GetForumId(Convert.ToInt32(Eval("TopicId")))%>&topicId=<%#Eval("TopicId")%>&postId=<%#Eval("PostId")%>&goToPost=true&a=1#Post<%#Eval("PostId")%>"
                                    target="_blank" title='<%#GetTooltipString(Eval("Subject").ToString())%>'>
                                    <%#System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("Subject").ToString()),30))%></a> <b>
                                        <%=Proxy[EnumText.enumForum_SearchResult_FiledPosted]%></b>
                            <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%>
                        </p>
                        <p>
                            <a class="topic_link" style="font-weight:normal" href="Topic.aspx?siteId=<%=SiteId%>&forumId=<%#GetForumId(Convert.ToInt32(Eval("TopicId")))%>&topicId=<%#Eval("TopicId")%>&postId=<%#Eval("PostId")%>&goToPost=true&a=1#Post<%#Eval("PostId")%>"
                                target="_blank">
                                <%#GetPostContent(Eval("TextContent").ToString())%>
                            </a>
                        </p>
                    </td>
                </tr>
                 <tr>
                        <td colspan="2" class="spacer_line">
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <table class="tb_forum" cellspacing='0'>
            <asp:Repeater ID="RepeaterSearchRustlt" runat="server" EnableViewState="false">
                <HeaderTemplate>
                    <tr>
                        <th class="tdStatus" width="7.5%" nowrap="nowrap">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnStatus]%>
                            </p>
                        </th>
                        <th class="tdSubject" width="37.5%">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnSubject]%>
                            </p>
                        </th>
                        <th class="tdLastPost" width="20%">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnLastPost]%>
                            </p>
                        </th>
                        <th class="tdReplies" width="7.5%" nowrap="nowrap">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnReplies]%>
                            </p>
                        </th>
                        <th class="tdViews" width="7.5%">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnViews]%>
                            </p>
                        </th>
                           <%--Promotion--%>
                         <%--<th width="50">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnPromotion]%>
                            </p>
                        </th>--%>
                        <th class="tdForumRight" width="20%">
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_ColumnForum]%>
                            </p>
                        </th>
                    </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr class="<%#((this.RepeaterSearchRustlt.Items.Count+1)%2 == 0)?"trEven":"trOdd"%>"
                        onmouseover="changeCSS(this,'trOnMouseOverStyle');" onmouseout="changeCSS(this,'<%#((this.RepeaterSearchRustlt.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> ');">
                        <td class="row1" align="center">
                            <p>
                                <%#Com.Comm100.Forum.UI.Common.WebUtility.StatusImage(this.ImagePath,TopicIfRead(Convert.ToInt32(Eval("TopicId"))), Convert.ToBoolean(Eval("IfClosed")), Convert.ToBoolean(Eval("IfMarkedAsAnswer")), Convert.ToBoolean(Convert.ToInt32(Eval("NumberOfReplies")) > 0 ? true : false))%>
                            </p>
                        </td>
                        <td class="row1" width="40%">
                            <p>
                                <a class="topic_link" href='Topic.aspx?topicId=<%#Eval("TopicId")%>&siteId=<%=SiteId%>&forumId=<%#Eval("ForumId")%>'
                                    target="_blank" title='<%#GetTooltipString(Eval("Subject").ToString())%>'>
                                    <%#TopicIfRead(Convert.ToInt32(Eval("TopicId"))) ? System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("Subject").ToString()), 30)) : 
                                                                                    System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("Subject").ToString()), 30)) %></a>
                                <%#Convert.ToBoolean(Eval("IfSticky")) ? "<img src=\"Images/status/sticky.gif\" alt='" + Proxy[EnumText.enumForum_Topic_StatusSticky] + "' title='" + Proxy[EnumText.enumForum_Topic_StatusSticky] + "' />" : ""%>
                            </p>
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_FieldBy]%>
                                <%# Convert.ToBoolean(Eval("PostUserOrOperatorIfDeleted")) ? System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString()), 15)) : "<a class='user_link' href='User_Profile.aspx?userId=" + Eval("PostUserOrOperatorId") + "&Siteid=" + SiteId + "' target='_blank' title='" + GetTooltipString(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'>" + System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString()), 15)) + "</a>"%>
                                <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%>
                            </p>
                        </td>
                        <td class="row1" width="20%">
                            <p>
                                <%#Convert.ToInt32(Eval("NumberOfReplies")) == 0 ? "" : "<a class='topic_link' target='_blank' href='Topic.aspx?topicId=" + Eval("TopicId") + "&siteId=" + SiteId + "&forumId=" + Eval("ForumId") + "&postId=-1&goToPost=true&a=1#bottom'>" + Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastPostTime"))) + "</a>"%>
                            </p>
                            <p>
                                <%#Convert.ToInt32(Eval("NumberOfReplies")) == 0 ? "" : Proxy[EnumText.enumForum_Topic_FieldBy] %>
                                <%#Convert.ToInt32(Eval("NumberOfReplies")) == 0 ? "" : (Convert.ToBoolean(Eval("LastPostUserOrOperatorIfDeleted")) ? System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("LastPostUserOrOperatorName").ToString(), 15)) : 
                                    "<a class='user_link' href='User_Profile.aspx?userId=" + Eval("LastPostUserOrOperatorId") + "&siteid=" + SiteId +"'target='_blank' title='" + GetTooltipString(Eval("LastPostUserOrOperatorName").ToString()) +"'>" + System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("LastPostUserOrOperatorName").ToString(), 15)) + "</a>")%>
                            </p>
                        </td>
                        <td class="row1" width="7.5%" align="center">
                            <p>
                                <%#Eval("NumberOfReplies")%></p>
                        </td>
                        <td class="row1" width="7.5%" align="center">
                            <p>
                                <%#Eval("NumberOfHits")%></p>
                        </td>
                        <!--Adding promotion count-->
                        <%--<td class="row1" width="7.5%" align="center">
                            <p>
                                <%#Eval("PromotionCount")%>
                            </p>
                        </td>--%>
                        <td class="row2" width="20%" title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("ForumName").ToString()))%>'>
                            <p>
                                <%#System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(ReplaceProhibitedWords(Eval("ForumName").ToString()), 15))%>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" class="spacer_line">
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </table>
    </div>
    <div class="pos_bottom_5">
        <div class="toolbar">
            <div class="divPager" style="text-align: right">
                <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                    Mode="LinkButton" PageSize="10">
                </cc1:ASPNetPager>
            </div>
        </div>
    </div>
    <div>
        <div class="clear">
        </div>
    </div>
    <%----Hidden legend--%>
    <div class="pos_bottom_10" style="display:none;">
        <table class="tb_legend" cellspacing="0" width="100%">
            <tr>
                <td class="cat3">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row3">
                    <table cellpadding="1" cellspacing="1" width='100%'>
                        <tr>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/noread_close.gif"%>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/noread_mark.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/noread_normal.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/participate_close.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/participate_mark.gif"%>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/participate_normal.gif" %>'alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/read_close.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/read_mark.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/read_normal.gif" %>' alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>'
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="20" align="center">
                                            <p>
                                                <img src='<%=this.ImagePath + "/status/sticky.gif" %>'alt='<%=Proxy[EnumText.enumForum_Topic_StatusSticky]%>'
                                                    align="absmiddle" style="margin-left: 3px;" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusSticky]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
