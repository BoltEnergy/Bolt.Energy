<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NavigationBar.ascx.cs"
    Inherits="Com.Comm100.Forum.UI.UserControl.NavigationBar" %>
<div class="divMsg">
    <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
</div>
<%--<asp:Literal ID="Literal1" runat="server"></asp:Literal>--%>

<asp:SiteMapPath ID="SiteMapPath1" runat="server" PathSeparator=" » " CssClass="NavigationBar">
</asp:SiteMapPath>
