<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LinkBar.ascx.cs" Inherits="Com.Comm100.Forum.UI.UserControl.LinkBar" %>
<div class="bolt_logo"><img src="~/App_Themes/StyleTemplate_Default/images/logo.png" runat="server" /></div>
<div class="top-main-menu" style="display:none;">
    <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg"></asp:Label>

<img id="imgHome" src="" align="absmiddle" runat="server" />
<asp:HyperLink ID="lnkHome" runat="server"></asp:HyperLink>
<img id="imgSearch" src="" align="absmiddle" runat="server" visible="false" />
<asp:HyperLink ID="lnkSearch" runat="server" visible="false"></asp:HyperLink>
<img id="imgRegister" src="" align="absmiddle" runat="server" Visible="false" />
<asp:HyperLink ID="lnkRegister" runat="server" Visible="false"></asp:HyperLink>
<img id="imgAdminControlPanel" src="" align="absmiddle" runat="server" visible="false" />
<asp:HyperLink ID="lnkAdminControlPanel" runat="server" visible="False"  ></asp:HyperLink>
<img id="imgModeratorControlPanel" src="" align="absmiddle" runat="server" visible="false" />
<asp:HyperLink ID="lnkModeratorControlPanel" runat="server" visible="False"></asp:HyperLink>
<img id="imgUserControlPanel" src="" align="absmiddle" runat="server" />
<asp:HyperLink ID="lnkUserControlPanel" runat="server"></asp:HyperLink>
<img id="imgMessages" src="" Visible="false" align="absmiddle" runat="server" />
<asp:HyperLink ID="lnkMessages" runat="server" Visible="false" ></asp:HyperLink>
<img id="imgLogout" src="" align="absmiddle" runat="server" />
<asp:HyperLink ID="lnkLogout" runat="server"></asp:HyperLink>
<img id="imgLogin" src="" align="absmiddle" runat="server" />
<asp:HyperLink ID="lnkLogin" runat="server"></asp:HyperLink>
</div>


