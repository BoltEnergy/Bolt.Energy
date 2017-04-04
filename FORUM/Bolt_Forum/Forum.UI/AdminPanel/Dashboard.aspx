<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Dashboard" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <span class="TitleLabel"><%=Proxy[EnumText.enumForum_Dashboard_Title]%></span></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <asp:Panel ID="panelContent" runat="server">
        <div class="divContent">
            <div class="divTable">
                    <asp:PlaceHolder ID="phNews" runat="server"></asp:PlaceHolder>
            </div>
         </div>
     </asp:Panel>
     
     <asp:Panel ID="panelIframe" runat="server">
        <iframe id="iframeContent" frameborder="0" scrolling="no" width="99%" src="http://www.comm100.com/opensourceforumdashboard.aspx" onload="reinitIframe();"></iframe>
        <script type="text/javascript">
            function reinitIframe() {
                var iframe = document.getElementById("iframeContent");
                try {
                    var bHeight = iframe.contentWindow.document.body.scrollHeight;
                    var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
                    var height = Math.max(bHeight, dHeight);
                    iframe.height = height;
                }
                catch (ex)
                { }
            }
        </script>
     </asp:Panel>
</asp:Content>
