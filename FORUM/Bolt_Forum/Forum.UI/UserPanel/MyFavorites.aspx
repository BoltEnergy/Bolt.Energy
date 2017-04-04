<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MyFavorites.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserMyFavorites" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_FavoriteDeleteConfirm] %>');
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Topic_FavoriteTitle]%>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_Topic_FavoriteSubTitle] %>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <th width="10%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnStatus]%>
                    </p>
                </th>
                <th width="40%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Public_Topic] %>
                    </p>
                </th>                
                <th width="10%">
                    <%=Proxy[EnumText.enumForum_Topic_ColumnAuthor]%>
                </th>
                <th width="20%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnLastPost] %>
                    </p>
                </th>
                <th width="15%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnForum] %>
                    </p>
                </th>
                <th width="5%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnOperation] %>
                    </p>
                </th>
            </tr>
            <asp:Repeater ID="RepeaterMyFavorites" runat="server" OnItemCommand="RepeaterMyFavorites_ItemCommand">
                <ItemTemplate>
                    <tr class="<%#((this.RepeaterMyFavorites.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> "
                        onmousemove="changeCSS(this,'trOnMouseOverStyle');" onmouseout="changeCSS(this,'trOdd');">
                        <td class="row1" style="text-align:center">
                            <p>
                                <%# ImageStutas(Convert.ToInt32(Eval("TopicId")), Convert.ToBoolean(Eval("IfClosed")), Convert.ToBoolean(Eval("IfMarkedAsAnswer")), Convert.ToBoolean(Eval("IfParticipant")))%>
                            </p>
                        </td>
                        <td class="row1">
                            <p>
                                <a class="topic_link" href='../Topic.aspx?forumId=<%#Eval("CurrentForumId") %>&topicId=<%#Eval("TopicId")%>&siteId=<%=SiteId%>'
                                    target="_blank" title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("Subject").ToString())) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(ReplaceProhibitedWords(Convert.ToString(Eval("Subject")))), 60)%></a>
                            </p>
                        </td>
                        <td class="row1" style="text-align:center">
                            <p>
                                <%# Convert.ToBoolean(Eval("PostUserOrOperatorIfDeleted")) ? "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString())), 30) + "</span>" : "<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'><a class='user_link' href='../User_Profile.aspx?userId=" + Eval("PostUserOrOperatorId") + "&Siteid=" + SiteId + "' target='_blank'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString())), 30) + "</a></span>"%>
                                
                                
                            </p>                        
                        </td>
                        <td class="row1" style="text-align:center">
                            <p>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("LastPostTime")))%>
                            </p>
                            <p>
                                <%=Proxy[EnumText.enumForum_Topic_FieldBy]%>
                                <%# Convert.ToBoolean(Eval("LastPostUserOrOperatorIfDeleted")) ? "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString())) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostUserOrOperatorName").ToString())), 30) + "</span>" : "<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString())) + "'><a class='user_link' href='../User_Profile.aspx?userId=" + Eval("LastPostUserOrOperatorId") + "&Siteid=" + SiteId + "' target='_blank'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostUserOrOperatorName").ToString())), 30) + "</a></span>"%>
                                <%--    &nbsp;<a class="user_link" title="<%#Server.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString()))%>"
                                        href="../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("LastPostUserOrOperatorId")%>"
                                        target="_blank">
                                        <%#Server.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString()))%></a>--%>
                            </p>
                        </td>
                        <td class="row1">
                            <p>
                                <%# Server.HtmlEncode(ReplaceProhibitedWords(ForumPath(Convert.ToInt32(Eval("CurrentForumId")), Convert.ToInt32(Eval("TopicId")))))%>
                            </p>
                        </td>
                        <td class="row2" style="text-align: center">
                            <p>
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("CurrentForumId")) + "_" +Convert.ToString(Eval("TopicId"))%>' 
                                    ToolTip="<%#Proxy[EnumText.enumForum_Topic_HelpDelete] %>" OnClientClick="return deleteConfirm();" />
                            </p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="6" align="center" class="row5">
                    <p>
                        <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                            Mode="LinkButton" PageSize="10" OnChangePageSize="aspnetPager_ChangePageSize">
                        </cc1:ASPNetPager>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div class="pos_bottom_10">
        <table class="tb_legend" cellspacing="0">
            <tr>
                <td class="cat3">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row3">
                    <p>
                        <table cellpadding="1" cellspacing="1" width='100%'>
                            <tr>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/noread_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/noread_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/noread_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/participate_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/participate_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/read_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>'
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
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/read_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>'
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
                                <td width="25%" colspan="4">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <p>
                                                    <img src="../<%=ImagePath %>/status/read_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>'
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
                            </tr>
                        </table>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
