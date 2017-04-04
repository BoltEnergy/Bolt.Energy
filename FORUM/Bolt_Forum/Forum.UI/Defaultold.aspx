<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Defaultold.aspx.cs" Inherits="Com.Comm100.Forum.UI.Defaultold" ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divContent">
        <div class="divMsg">
            <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
        </div>
        <asp:Repeater ID="repeaterCategory" runat="server" OnItemDataBound="repeaterCategory_ItemDataBound"
            EnableViewState="false">
            <ItemTemplate>
                <div class="pos_bottom_10">
                    <div class="cat2">
                        <div class="top_cat2">
                            <div class="top_cat2_left">
                                <div class="top_cat2_right">
                                    <span class="TitleName">
                                        <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("Name").ToString()))%>
                                    </span>
                                    <asp:Label ID="lblCategoryId" Visible="false" runat="server" Text='<%#Eval("CategoryId")%>'
                                        EnableViewState="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    <table class="tb_forum" cellspacing='0' width="100%">
                        <tr>
                            <th>
                                <p>
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnForum]%>
                                    Name
                                </p>
                            </th>
                            <%--<th width="10%">
                                <p>
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnModerators]%>
                                </p>
                            </th>--%>
                            <th width="200">
                                <p>
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnLastPost]%>
                                </p>
                            </th>
                            <th width="50">
                                <p>
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnTopics]%>
                                </p>
                            </th>
                            <th width="50">
                                <p>
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnPost]%>
                                </p>
                            </th>
                        </tr>
                        <asp:Repeater ID="repeaterForum" runat="server" OnItemDataBound="repeaterForum_ItemDataBound"
                            EnableViewState="false">
                            <ItemTemplate>
                                <asp:Label ID="lblTr" runat="server" Text="" EnableViewState="false"></asp:Label>
                                <td class="row1">
                                    <p class="forum_name">
                                        <span title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("Name").ToString()).Replace("'","\'"))%>'>
                                            <asp:Label ID="lblForumName" runat="server" Text="" EnableViewState="false"></asp:Label>
                                        </span>
                                    </p>
                                    <p class="forum_desc">
                                        <span title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("Description").ToString()).Replace("'","\'"))%>'>
                                            <%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "Description").ToString())), 60)%>
                                        </span>
                                    </p>
                                    <p class="moderator_desc">
                                        <b>
                                            <%=Proxy[EnumText.enumForum_Topic_ColumnModerators]%> </b>
                                            :
                                            <asp:Repeater ID="repeaterModerators" runat="server" OnItemDataBound="repeaterModerators_ItemDataBound"
                                                EnableViewState="false">
                                                <ItemTemplate>
                                                    <span title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("DisplayName").ToString()).Replace("'","\'"))%>'>
                                                        <a class="user_link" href='User_Profile.aspx?userId=<%#Eval("Id")%>&siteid=<%=SiteId%>'
                                                            target="_blank">
                                                            <asp:Label ID="lblDisplayName" runat="server" Text="" EnableViewState="false"></asp:Label>
                                                        </a></span>
                                                    <asp:Label ID="lblSep" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                       
                                    </p>
                                    <asp:Label ID="lblForumId" Visible="false" runat="server" Text='<%#Eval("ForumId")%>'
                                        EnableViewState="false"></asp:Label>
                                </td>
                                <%--<td class="row1" align="center">
                                </td>--%>
                                <td class="row1">
                                    <p>
                                        
                                        <asp:Label ID="lblLastPostSubject" runat="server" Text="" EnableViewState="false"></asp:Label>
                                        <%# Convert.ToInt32(Eval("NumberOfPosts")) == 0 ? "" : Convert.ToBoolean(Eval("LastPostCreatedUserOrOperatorIfDeleted")) ? Proxy[EnumText.enumForum_Topic_FieldBy] + "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostCreatedUserOrOperatorName").ToString())) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostCreatedUserOrOperatorName").ToString())), 30) + "</span><br />" : Proxy[EnumText.enumForum_Topic_FieldBy] + "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostCreatedUserOrOperatorName").ToString())) + "'><a class='user_link' href='User_Profile.aspx?userId=" + Eval("LastPostCreatedUserOrOperatorId") + "&siteid=" + SiteId.ToString() + "' target=\"_blank\">" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostCreatedUserOrOperatorName").ToString())), 30) + "</a></span>"%></p>
                                    <p>
                                        <%# Convert.ToInt32(Eval("NumberOfPosts")) == 0 ? "" : Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastPostPostTime")))%></p>
                                </td>
                                <td class="row1" align="center">
                                    <p>
                                        <%#Eval("NumberOfTopics")%></p>
                                </td>
                                <td class="row2" align="center">
                                    <p>
                                        <%#Eval("NumberOfPosts")%></p>
                                </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </table>
                </div><br />
            </ItemTemplate>
            
        </asp:Repeater>
        <br />
        <div class="pos_bottom_10" style="display:none;">
            <table class="tb_
                
                
                " cellspacing="0" width="100%">
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
                            <img src="<%=ImagePath %>/status/lock.gif" alt='<%=Proxy[EnumText.enumForum_Topic_FieldLockedForum]%>'
                                title='<%=Proxy[EnumText.enumForum_Topic_FieldLockedForum]%>' />
                            <%=Proxy[EnumText.enumForum_Topic_FieldLockedForum]%>
                        </p>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
