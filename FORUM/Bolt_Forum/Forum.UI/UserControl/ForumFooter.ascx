<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ForumFooter.ascx.cs"
    Inherits="Com.Comm100.Forum.UI.UserControl.ForumFooter" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>

<div class="divMsg">
    <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg"></asp:Label></div>
<!--change by techtier for hiding the Dropdown menu -->
<div id="forumJump" style="display: none;">
    <%=Proxy[EnumText.enumForum_HeaderFooter_ForumJump]%>
    <asp:DropDownList ID="ddlForumJump" runat="server">
    </asp:DropDownList>
    <asp:HiddenField ID="hdnForumJump" runat="server" Value="" />
</div>
<%--<div id="divCustomizeFooter" runat="server">
</div>--%>

<footer id="footerNew">
        <div class="container">
            <p>© <script>document.write(new Date().getFullYear())</script> <a href="#">Bolt Energy Market, Inc.</a></p>
            <p class="links"><a id="achPrivacy" runat="server" class="privacy" href="#/privacy">Privacy Policy</a>
                 <a id="achTerms" runat="server" class="privacy" href="#/terms">Terms of Service</a>
            </p>
        </div>
</footer>


<div id="divfooter" style="display:none;" >
    <asp:Literal ID="Literal1" runat="server" Visible="false"></asp:Literal>
</div>
<div id="copyright" runat="server" style="display:none;">
<center>
    <asp:Literal ID="literalCopyright" runat="server" visible="false" ></asp:Literal>
</center>    
</div>

<script type="text/javascript">
    var url = document.URL;
    var ifAdvanced = url.substr(url.lastIndexOf("ifAdvancedMode") + 15, 4); //ifAdvanceMode=true
    if (ifAdvanced.toLowerCase() == "true") {
        //get footer from Parent Window
        var WinParnet = window.opener;
        if (WinParnet != null) {
            //set footer
            var divfooter = document.getElementById('divfooter');
            var controlPrefix = WinParnet.document.getElementById("hdnControlPrefix").value;
            divfooter.innerHTML = WinParnet.tinyMCE.get(controlPrefix + "_txtPageFooter").getContent();
        }

    }

    function jumpForum() {
        var oldSelectedForumId = '<%=Request.QueryString["forumId"] %>';
        var ddlForumJump = document.getElementById("<%= ddlForumJump.ClientID%>");
        if (ddlForumJump.options[ddlForumJump.selectedIndex].value == "-1" || ddlForumJump.options[ddlForumJump.selectedIndex].value == "-2" || ddlForumJump.options[ddlForumJump.selectedIndex].value.charAt(0) == 'c' || ddlForumJump.options[ddlForumJump.selectedIndex].value.toLowerCase() == 'forumid=' + oldSelectedForumId) {
            return;
        }
        var hdnForumJump = document.getElementById("<%= hdnForumJump.ClientID%>");
        hdnForumJump.value = "1";
        document.forms[0].submit();
    }
    

</script>

