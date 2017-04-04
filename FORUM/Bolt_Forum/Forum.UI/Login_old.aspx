<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="Login_old.aspx.cs" Inherits="Com.Comm100.Forum.UI.Login_old" EnableSessionState="True" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="JS/Common/Common.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_login_TitleUser]%>
                    </div>
                </div>
            </div>
        </div>
        <table id="tableLogin" class="tb_forum" cellspacing='0' runat="server" enableviewstate="false">
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
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorEmail" runat="server" ControlToValidate="txtUserName"
                            Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="regularExpressionValidatorEmail" runat="server"
                            Display="Dynamic" ControlToValidate="txtUserName" ValidationExpression="\s*\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*\s*"
                            ValidationGroup="Login"></asp:RegularExpressionValidator>
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
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorPassword" runat="server" ControlToValidate="txtPassword"
                            Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr style="display:none;">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_login_FieldVerificationCode]%></b></p>
                </td>
                <td class="row2" valign="middle">
                    <p>
                       <asp:TextBox ID="txtVerification" runat="server" Text="aaa" CssClass="txt" Width="60px"></asp:TextBox>
                     <%--    <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/ImageVerificationCode.aspx"
                            onclick="this.setAttribute('src', 'Images/ImageVerificationCode.aspx?siteid=<%= SiteId %>&random=' + Math.random())"
                            Style="cursor: hand; padding: 0px 2px 0px 0px; vertical-align: middle" /><span class="require">*</span>
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorCode" runat="server" ControlToValidate="txtVerification"
                            Display="Dynamic" ValidationGroup="Login"></asp:RequiredFieldValidator>
                        <asp:CustomValidator ID="customValidatorCode" runat="server" ControlToValidate="txtVerification"
                            Display="Dynamic" OnServerValidate="customValidatorCode_ServerValidate" ValidationGroup="Login"></asp:CustomValidator>--%>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1">
                    
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                        <%=Proxy[EnumText.enumForum_login_FieldRememberMe]%>
                        <br />
                        <%=Proxy[EnumText.enumForum_login_FieldForgetPassword]%>&nbsp;<asp:HyperLink ID="HyperLink1"
                            runat="server"><%=Proxy[EnumText.enumForum_login_LinkClickHere]%></asp:HyperLink><br />
                        <%=Proxy[EnumText.enumForum_login_FieldNewToForum]%>&nbsp;<asp:HyperLink ID="HyperLink2"
                            runat="server"><%=Proxy[EnumText.enumForum_Login_LinkRegisterHere]%></asp:HyperLink>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnLogin" runat="server" CssClass="btn" OnClick="btnLogin_Click"
                            ValidationGroup="Login" />
                        <br />
                    </p>
                </td>
            </tr>
        </table>
        <table id="tableInfo" class="tb_forum" cellspacing='0' runat="server" visible="false"
            enableviewstate="false">
            <tr>
                <td class="row4">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Login_StateSuccess]%></b></p>
                    <%=Proxy[EnumText.enumForum_Login_WaitJump]%>
                    <center>
                        <br />
                        <br />
                        <p class="pLoginSuccess" >
                            <asp:HyperLink ID="lnkBack" runat="server" EnableViewState="false"></asp:HyperLink><br />
                            <br />
                            <asp:HyperLink ID="lnkHome" runat="server" EnableViewState="false"></asp:HyperLink>
                            <br />
                            <br />
                            <asp:HyperLink ID="lnkUserPanel" runat="server" EnableViewState="false"></asp:HyperLink>
                        </p>
                    </center>
                </td>
            </tr>
        </table>
    </div>
    <asp:PlaceHolder ID="phHttps" runat="server"></asp:PlaceHolder>
</asp:Content>
