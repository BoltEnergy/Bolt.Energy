<%@ Page Title="Register" Language="C#" MasterPageFile="~/MainMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Forum.UI.Register"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="JS/Common/Common.js"></script>

    <style type="text/css">
        #tbSuject
        {
            width: 350px;
        }
        #htmlEditor
        {
            width: 352px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alertMsg">
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Register_TitleRegister]%></div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldEmail]%></b></p>
                </td>
                <td class="row2" width="80%">
                    <p>
                        <asp:TextBox ID="txtEmail" runat="server" Text="" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp; </span>
                        <asp:Button ID="btnTestExistEmail" CssClass="btn" runat="server" UseSubmitBehavior="false"
                            OnClientClick="if(!IfExistEmail())return;" OnClick="btnTestExistEmail_Click" />
                        <asp:Label ID="lblTestResult" runat="server" EnableViewState="false"></asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionTxtEmail" runat="server" Display="Dynamic"
                            ValidationGroup="Register" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="RequiredTxtEmail" runat="server" ControlToValidate="txtEmail"
                            ValidationGroup="Register" Display="Dynamic"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldRetypeEmail]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtConfirmEmail" runat="server" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtConfirmEmail" runat="server" ControlToValidate="txtConfirmEmail"
                                Display="Dynamic" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="CompareEmail" runat="server" ControlToCompare="txtEmail"
                                ControlToValidate="txtConfirmEmail" ValidationGroup="Register" Display="Dynamic"></asp:CompareValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldDisplayName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtDisplayName" runat="server" Display="Dynamic"
                                ControlToValidate="txtDisplayName" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="revtxtDisplayName" runat="server" Display="Dynamic"
                                ControlToValidate="txtDisplayName" ValidationGroup="Register"></asp:RegularExpressionValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldPassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="txt" OnTextChanged="txtPassword_TextChanged"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtPassword" runat="server" ControlToValidate="txtPassword"
                                Display="Dynamic" ValidationGroup="Register"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldRetypePassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtConfirmPassword" TextMode="Password" runat="server" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtConfirmPassword" runat="server" ControlToValidate="txtConfirmPassword"
                                Display="Dynamic" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtConfirmPassword" Display="Dynamic" ValidationGroup="Register"></asp:CompareValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Register_FieldVerificationCode]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtImageVerification" runat="server" MaxLength="4" CssClass="txt"></asp:TextBox>
                        <span style="vertical-align: middle">
                            <img src="Images/ImageVerificationCode.aspx?siteid=<%= SiteId %>" id="imgVerificationCode"
                                alt='<%=Proxy[EnumText.enumForum_Register_HelpImageVerificationCode]%>' border="0"
                                onclick="reloadcode()" style="cursor: hand; padding: 2px 8px 0px 3px;" />
                        </span>&nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredtxtImageVerification" runat="server" ControlToValidate="txtImageVerification"
                                Display="Dynamic" ValidationGroup="Register"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblVerificationCode" runat="server"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                </td>
                <td class="row2">
                    <p>
                        <span class="require">*
                            <%=Proxy[EnumText.enumForum_Public_RequiredField] %></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button CssClass="btn" ID="btnRegister" runat="server" ValidationGroup="Register"
                            OnClick="btnRegister_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript">
        function reloadcode() {
            var verify = document.getElementById("imgVerificationCode");
            verify.setAttribute("src", "Images/ImageVerificationCode.aspx?siteid=<%= SiteId %>&random=" + Math.random());
        }
        function IfExistEmail() {
            var myreg = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
            var email = document.getElementById("<%=txtEmail.ClientID%>");
            document.getElementById("<%=txtEmail.ClientID%>")
            if (email.value == "") {
                return false;
            }
            else if (!myreg.test(email.value)) {
                return false;
            }
            return true;
        }
        //        function validateEmailFormat(object, args) {
        //            var ddl = document.getElementById();
        //            if (ddl.selectedIndex != 0) {
        //                var txt = document.getElementById();
        //                if (txt.value == "!") {
        //                    args.IsValid = true;
        //                }
        //            }
        //            args.IsValid = false;
        //        }
    </script>

    <script language="javascript" type="text/javascript">
        document.getElementById("<%=btnTestExistEmail.ClientID%>").focus();
    </script>

    <asp:PlaceHolder ID="phHttps" runat="server"></asp:PlaceHolder>
</asp:Content>
