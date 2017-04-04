<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserGroup.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.UserGroups.UserGroup" %>
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
            <asp:Button Id="btnNewUsergroup0" runat="server" CssClass="slbtn" 
                onclick="btnNewUsergroup1_Click"/>
        </div>
        <br />
        <div class="divTable">
            <table class="the-table">
                <tr>
                    <%--<th width="10%">
                        <asp:Label ID="lblId" runat="server"></asp:Label>
                    </th>--%>
                    <th width="25%">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </th>
                    <th width="35%">
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </th>
                    <th width="10%" class="cth">
                        <asp:Label ID="lblMemebers" runat="server"></asp:Label>
                    </th>
                    <th width="15%" class="cth">
                        <asp:Label ID="lblPermissions" runat="server"></asp:Label>
                    </th>
                    <th width="15%" class="cth">
                        <asp:Label ID="lblOperation" runat="server"></asp:Label>
                    </th>
                </tr>
                <asp:Repeater ID="rpUserGroup" runat="server">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpUserGroup.Items.Count+1)%2%>" onmousedown="highLightRow(this);">
                            <%--<td align="center">
                                <%#Convert.ToBoolean(Eval("IfAllForumUsersGroup")) ? "" : Convert.ToString(Eval("userGroupId")) %>
                            </td>--%>
                            <td>
                            <span title="<%#GetTooltipString(Eval("name").ToString()) %>">
                                <%# System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("name").ToString(),20))%>
                            </span>
                            </td>
                            <td>
                            <span title="<%#GetTooltipString(Eval("Description").ToString()) %>">
                                <%#Convert.ToBoolean(Eval("IfAllForumUsersGroup")) ? "" : System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("Description").ToString(),50))%>
                            </span>
                            </td>
                            <td class="ctd">
                                <%#Convert.ToBoolean(Eval("IfAllForumUsersGroup")) ? "" : "<a href=\"memberlist.aspx?siteId=" + SiteId + "&groupId=" + Convert.ToString(Eval("userGroupId")) + "\" title=\"Members\">" + GetCountOfMembers(Convert.ToInt32(Eval("userGroupId"))) + "</a>"%>
                            </td>
                            <td class="ctd">
                                <a href='PermissionSet.aspx?siteId=<%=SiteId %>&groupid=<%# Eval("userGroupid") %>'
                                    title="Permission Settings">
                                    <img src="../../Images/permission-manager.GIF" alt="<%=Proxy[EnumText.enumForum_UserGroups_HelpPermissions]%>" title="<%=Proxy[EnumText.enumForum_UserGroups_HelpPermissions]%>" /></a>
                            </td>
                            <td class="ctd">
                                <%#Convert.ToBoolean(Eval("IfAllForumUsersGroup")) ? "" : "<a href=\"UserGroupEdit.aspx?siteId=" + SiteId + "&groupid=" + Convert.ToString(Eval("usergroupid")) + "\"" + "title=\"" + Proxy[EnumText.enumForum_UserGroups_HelpEdit] + "\"><img src=\"../../images/database_edit.gif\" alt=\"" + Proxy[EnumText.enumForum_UserGroups_HelpEdit] + "\" /></a> <a href=\"UserGroup.aspx?action=delete&siteId=" + SiteId + "&groupId=" + Convert.ToString(Eval("usergroupid")) + "\" title=\"" + Proxy[EnumText.enumForum_UserGroups_HelpDelete] + "\"><img src=\"../../Images/database_delete.gif\" alt=\"Delete\" onclick=\"return confirm('" + Proxy[EnumText.enumForum_UserGroups_ConfirmDeleteUserGroup] + "'); \" /></a>"%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <%--<AlternatingItemTemplate>
                        <tr class="trStyle2" onmousedown="highLightRow(this);">
                            <td align="center">
                                <%# Eval("userGroupId") %>
                            </td>
                            <td>
                                <%# Eval("name") %>
                            </td>
                            <td>
                                <%# Eval("Description") %>
                            </td>
                            <td class="ctd">
                                <a href='memberlist.aspx?siteId=<%=SiteId %>&groupId=<%#Eval("userGroupId") %>' title="Members">
                                    <%#GetCountOfMembers(Convert.ToInt32(Eval("userGroupId"))) %></a>
                            </td>
                            <td class="ctd">
                                <a href='PermissionSet.aspx?siteId=<%=SiteId %>&groupid=<%# Eval("userGroupid") %>'
                                    title="Permission Settings">
                                    <img src="../../Images/permission-manager.GIF" alt="<%=Proxy[EnumText.enumForum_UserGroups_HelpPermissions]%>" title="<%=Proxy[EnumText.enumForum_UserGroups_HelpPermissions]%>" /></a>
                            </td>
                            <td class="ctd">
                                <a href='UserGroupEdit.aspx?siteId=<%=SiteId %>&groupid=<%# Eval("usergroupid") %>'
                                    title="Edit">
                                    <img src="../../images/database_edit.gif" alt="Edit" /></a>
                                <a href="UserGroup.aspx?action=delete&siteId=<%=SiteId %>&groupId=<%#Eval("userGroupId") %>"
                                    title="Delete">
                                    <img src="../../Images/database_delete.gif" alt="<%=Proxy[EnumText.enumForum_UserGroups_HelpDelete] %>" onclick="return confirm('<%=Proxy[EnumText.enumForum_UserGroups_ConfirmDeleteUserGroup] %>'); " /></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>--%>
                </asp:Repeater>
            </table>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button Id="btnNewUsergroup1" runat="server" CssClass="slbtn" 
                onclick="btnNewUsergroup1_Click"/>
        </div>
    </div>
</asp:Content>
