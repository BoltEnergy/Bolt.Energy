<%@ Page Title="Forums" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ForumList.aspx.cs" Inherits="Forum.UI.AdminPanel.Forums.ForumList" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnNewForum1" runat="server" CssClass="lbtn" OnClick="btnNewForum_Click" />
            <asp:Button ID="btnCancel" runat="server" CssClass="lbtn" onclick="btnCancel_Click"/>
        </div>
        <br />
        <div class="divTable">
            <asp:Repeater ID="rptCategory" runat="server" OnItemDataBound="rptCategory_ItemDataBound">
                
                <ItemTemplate>
                    <%-- <tr>
                        <td colspan="7" class="divCpFirstTitle">
                            <%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%>
                        </td>
                    </tr>--%>
                    <div class="divCpFirstTitle">
                        <%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%></div>
                    <div class="divCpFirstContent" style="background-color: #f7f3f7">
                        <table class="the-table" cellpadding='0' cellspacing='0'>
                            <asp:Repeater ID="rptForum" runat="server" OnItemCommand="rptForum_ItemCommand" OnItemDataBound="rptForum_ItemDataBound">
                                <HeaderTemplate>
                                    <tr>
                                        <%--<th width="5%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnId]%></b>
                                        </th>--%>
                                        <th width="10%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnName]%></b>
                                        </th>
                                        <th class="cth" width="10%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnStatus]%></b>
                                        </th>
                                        <th class="cth" width="10%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnModerator]%></b>
                                        </th>
                                        <%--<th class="cth" width="15%">
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnAnnouncements] %></b>
                                        </th>--%>
                                        <%--<th class="cth" width="10%">
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnTopics] %></b>
                                        </th>--%>
                                        <th class="cth" width="10%" style="<%=IfDisplay%>">
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnPermissions] %></b>
                                        </th>
                                        <th class="cth" width="10%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnOrder]%></b>
                                        </th>
                                        <th class="cth" width="15%"  >
                                            <b><%=Proxy[EnumText.enumForum_Forums_ColumnOperation]%></b>
                                        </th>
                                    </tr>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <tr class="trStyle<%#Container.ItemIndex % 2%>" onmousedown="highLightRow(this);">
                                        <%--<td>
                                            <%#Eval("ForumId")%>
                                        </td>--%>
                                        <td>
                                            <span class="linkTopic"  title='<%#GetTooltipString(Eval("Name").ToString()) %>'>
                                                <%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()), 10)%>
                                            </span>
                                        </td>
                                        <%--<td title='<%#GetTooltipString(Eval("Description").ToString().Replace("'"," "))%>'>
                                            <%#System.Web.HttpUtility.HtmlEncode(Eval("Description").ToString())%>
                                        </td>--%>
                                        <td class="ctd">
                                            <%#GetForumStatusName((Com.Comm100.Framework.Enum.Forum.EnumForumStatus)Eval("Status"))%>
                                        </td>
                                        <td class="ctd">
                                            <asp:Repeater ID="rptModerator" runat="server" OnItemDataBound="rptModerator_ItemDataBound">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDisplayName" runat="server"></asp:Label>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                        </td>
                                        <%--<td class="ctd">
                                            <asp:ImageButton ID="imgbtnAnnouncement" CommandName="Announcement" CommandArgument='<%#Eval("ForumId") %>' ImageUrl="~/Images/users.gif" ToolTip="Announcement" runat="server" />
                                        </td>--%>
                                        <%--<td class="ctd">
                                            <asp:ImageButton ID="imgbtnTopics" CommandName="Topics" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/view.gif" ToolTip="Topics" runat="server" />
                                        </td>--%>
                                        <td class="ctd" style="<%=IfDisplay%>"> 
                                            <asp:ImageButton ID="imgbtnPermission" CommandName="Permission" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/permission-manager.gif" ToolTip="Permission" runat="server" />
                                        </td>
                                        
                                        <td class="ctd">
                                            <asp:ImageButton ID="imgbtnSortUp" CommandName="Up" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/sort_up.gif" ToolTip='<%#Proxy[EnumText.enumForum_Forums_HelpUp]%>' runat="server" />
                                            <asp:ImageButton ID="imgbtnSortDown" CommandName="Down" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/sort_down.gif" ToolTip='<%#Proxy[EnumText.enumForum_Forums_HelpDown]%>' runat="server" />
                                            <%--<a id="aSortUp" runat="server">
                                                <img id="imgSortUp" alt='<%#Proxy[EnumText.enumForum_Forums_HelpUp]%>' src="../../Images/sort_up.gif" runat="server" /></a>
                                            <a id="aSortDown" runat="server">
                                                <img id="imgSortDown" alt='<%#Proxy[EnumText.enumForum_Forums_HelpDown]%>' src="../../Images/sort_down.gif" runat="server" /></a>--%>
                                        </td>
                                        <td class="ctd">
                                            <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/database_edit.gif" ToolTip="<%#Proxy[EnumText.enumForum_Forums_HelpEdit]%>" runat="server" />
                                            <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("ForumId")%>' ImageUrl="~/Images/database_delete.gif" ToolTip="<%#Proxy[EnumText.enumForum_Forums_HelpDelete]%>" runat="server" OnClientClick="return ComfirmDel();"/>
                                            <%--<a href="ForumEdit.aspx?forumId=<%#Eval("ForumId")%>&siteid=<%= SiteId %>">
                                                <img alt='<%#Proxy[EnumText.enumForum_Forums_HelpEdit]%>' src="../../Images/database_edit.gif" /></a> &nbsp;&nbsp;<a href="ForumList.aspx?action=delete&forumId=<%#Eval("ForumId")%>&siteid=<%= SiteId %>"><img
                                                    alt='<%#Proxy[EnumText.enumForum_Forums_HelpDelete]%>' src="../../Images/database_delete.gif" onclick="return ComfirmDel()" /></a>--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </table>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnNewForum2" runat="server" CssClass="lbtn" OnClick="btnNewForum_Click" />
            &nbsp;
            <asp:Button ID="btnCancel1" runat="server" CssClass="lbtn" onclick="btnCancel_Click"/>
        </div>

        <script type="text/javascript">
            function ComfirmDel() {
                //var count = objCount;
                // debugger;
                if (confirm('<%=Proxy[EnumText.enumForum_Forums_PageConfirmDelete]%>')) {

                    return confirm('<%=Proxy[EnumText.enumForum_Forums_PageListErrorConfirmDelete]%>');
                }
                else
                    return false;
            }
        </script>

    </div>
</asp:Content>
