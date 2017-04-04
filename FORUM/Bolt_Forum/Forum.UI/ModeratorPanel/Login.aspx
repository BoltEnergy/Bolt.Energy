<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Login.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.Login" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JS/Common/Common.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divContent">
        <div class="alertMsg">
            <asp:Label ID="lblMessage" CssClass="ErrorLabel" runat="server" EnableViewState="False">
            </asp:Label>
        </div>
        <div class="divTable">
            <div class="cat2">
                <div class="top_cat2">
                    <div class="top_cat2_left">
                        <div class="top_cat2_right">
                            <%=Proxy[EnumText.enumForum_login_TitleModeratorLogin]%>
                        </div>
                    </div>
                </div>
            </div>
            <table class="tb_forum" cellpadding='0' cellspacing='0'>
                <tr>
                    <td class="row1" width="40%" align="right">
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_login_FieldEmail]%></b></p>
                    </td>
                    <td class="row2" width="60%">
                        <p>
                            <asp:TextBox ID="txtUserName" runat="server" CssClass="txt"></asp:TextBox>
                            <span class="require">*</span>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorEmail" runat="server" ErrorMessage="Email is required."
                                ControlToValidate="txtUserName" Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="regularExpressionValidatorEmail" runat="server"
                                ErrorMessage="Email is error." Display="Dynamic" ControlToValidate="txtUserName"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="Login"></asp:RegularExpressionValidator>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="row1" align="right">
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_login_FieldPassword]%></b></p>
                    </td>
                    <td class="row2">
                        <p>
                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="txt"></asp:TextBox>
                            <span class="require">*</span>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorPassword" runat="server" ErrorMessage="Password is required."
                                ControlToValidate="txtPassword" Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="row1" align="right">
                        <p>
                            <b>
                                <%=Proxy[EnumText.enumForum_login_FieldVerificationCode]%></b></p>
                    </td>
                    <td class="row2" valign="middle">
                        <p>
                            <asp:TextBox ID="txtVerification" runat="server" CssClass="txt" Width="60px"></asp:TextBox>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ImageVerificationCode.aspx?siteid=<%= SiteId %>"
                                onclick="this.setAttribute('src', '../Images/ImageVerificationCode.aspx?siteid=<%= SiteId %>&random=' + Math.random())"
                                Style="cursor: hand; padding: 0px 2px 0px 0px; vertical-align: middle" /><span class="require">*</span>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorCode" runat="server" ErrorMessage="Verification Code is required."
                                ControlToValidate="txtVerification" Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                            <asp:CustomValidator ID="customValidatorCode" runat="server" ErrorMessage="Verification Code does't match."
                                ControlToValidate="txtVerification" Display="Dynamic" OnServerValidate="customValidatorCode_ServerValidate"
                                ValidationGroup="Login"></asp:CustomValidator>
                        </p>
                    </td>
                </tr>
                <tr>
                    <td class="row1">
                    </td>
                    <td class="row2">
                        <%=Proxy[EnumText.enumForum_login_FieldForgetPassword]%>&nbsp;<u><asp:HyperLink ID="HyperLink1"
                            runat="server"><%=Proxy[EnumText.enumForum_login_LinkClickHere]%> </asp:HyperLink></u><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="row5" align="center">
                        <p>
                            <asp:Button ID="btnLogin" Text="Login" runat="server" CssClass="btn" OnClick="btnLogin_Click"
                                ValidationGroup="Login" />
                            <br />
                        </p>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="tdButtom">
                        <div class="tbl-f-l">
                            <div class="tbl-f-r">
                                <div class="tbl-f-c">
                                </div>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:PlaceHolder ID="phHttps" runat="server"></asp:PlaceHolder>
</asp:Content>
