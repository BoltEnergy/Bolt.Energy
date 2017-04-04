<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumHeader.ascx.cs"
    Inherits="Com.Comm100.Forum.UI.UserControl.ForumHeader" %>
<%@ Import Namespace="Com.Comm100.Language" %>
<div class="divMsg">
    <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
</div>
<div id="Logo" runat="server">
    <div class="logo-row">
        <div id="divDefaultHeader" runat="server">
            <div id="Img" class="pos_left_10 pos_top_10 pos_bottom_10">
                <asp:Image ID="imgLogo" alt="" runat="server" />
            </div>
            <div>
                <div class="clear">
                </div>
            </div>
        </div>
        <%--<div id="divCustomizeHeader" runat="server">
    </div>--%>
    </div>
</div>
<%--<div id="Logo" >
                <div id="logo_row">
                    <div class="pos_left_10 pos_top_10 pos_bottom_10">
                        <img src="Images/comm100.gif" alt="" />
                    </div>
                </div>
            </div>
--%>
<div id="divheader">
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</div>
<%--<div id="Search">
    <asp:TextBox ID="txtSearch" ValidationGroup="QuickSearch" runat="server" CssClass="txtsearch"></asp:TextBox>
    <asp:LinkButton ID="imgbtnSearch" runat="server" OnClick="imgbtnSearch_Click">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/btnImages/icon_user_search.gif"
            Style="margin-bottom: -3px" /></asp:LinkButton>
    <br />
    <asp:HyperLink ID="lnkAdvancedSearch" runat="server"><b style="font-size:10px"><%=Proxy[EnumText.enumForum_HeaderFooter_AdvancedSearch] %></b></asp:HyperLink>
</div>--%>
<div class="clear">
</div>

<script type="text/javascript">
    var url = document.URL;
    var ifAdvanced = url.substr(url.lastIndexOf("ifAdvancedMode") + 15, 4); //ifAdvanceMode=true
    if (ifAdvanced.toLowerCase() == "true") {
        //get Header from Parent Window
        var WinParnet = window.opener;
        if (WinParnet != null) {
            var divheader = document.getElementById('divheader');
            var controlPrefix = WinParnet.document.getElementById("hdnControlPrefix").value;
            divheader.innerHTML = WinParnet.tinyMCE.get(controlPrefix + "_txtPageHeader_editor").getContent();
        }

    }
</script>

