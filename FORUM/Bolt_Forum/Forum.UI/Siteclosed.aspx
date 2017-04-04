<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Siteclosed.aspx.cs" Inherits="Com.Comm100.Forum.UI.Siteclosed" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td class="row4" colspan="2" width="50%" style="text-align: left; padding-left: 100px;">
                    <p>
                        <b>
                            <asp:Label ID="lblSiteName" runat="server" Text="" EnableViewState="false"></asp:Label>
                            <%=Proxy[EnumText.enumForum_Settings_FieldClosedInfo]%></b></p>
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Settings_FieldReason]%></b></p>
                    <p>
                        <asp:Label ID="lblCloseReason" runat="server" Text="" EnableViewState="false"></asp:Label></p>
                </td>
               <%-- <td class="row2" width="50%">
                </td>--%>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <b><a href="AdminPanel/Login.aspx?siteid=<%=SiteId%>">
                            <%=Proxy[EnumText.enumForum_Settings_LinkAdminLogin]%></a></b></p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
