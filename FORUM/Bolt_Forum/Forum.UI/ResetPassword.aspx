<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ResetPassword.aspx.cs" Inherits="Com.Comm100.Forum.UI.ResetPassword" %>

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
                        <%=Proxy[EnumText.enumForum_User_TitleResetPassword]%>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0' id="tblEdit" runat="server" enableviewstate="false">
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldEmail] %></b></p>
                </td>
                <td class="row2" width="80%">
                    <p>
                        <asp:TextBox ID="txtEmail" runat="server" Text="" Enabled="false" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldNewPassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtPassword" runat="server" MaxLength="16" TextMode="Password" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtPassword" runat="server" ErrorMessage=""
                                ControlToValidate="txtPassword" Display="Dynamic"></asp:RequiredFieldValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldRetypePassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtRetypePassword" runat="server" MaxLength="16" TextMode="Password"
                            CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*&nbsp;
                            <asp:RequiredFieldValidator ID="RequiredTxtRetypePassword" runat="server" ErrorMessage=""
                                ControlToValidate="txtRetypePassword" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:CompareValidator ID="ComparePassword" runat="server" ErrorMessage="" ControlToCompare="txtPassword"
                                ControlToValidate="txtRetypePassword" Display="Dynamic"></asp:CompareValidator>
                        </span>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnResetPassword" runat="server" Text="" OnClick="btnResetPassword_Click"
                            CssClass="btn" />
                    </p>
                </td>
            </tr>
        </table>
        <table class="tb_forum" cellspacing='0' runat="server" id="tblError" visible="false"
            enableviewstate="false">
            <tr>
                <td colspan="2" align="center" class="row4">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_MessageInvalidResetPasswordLink]%></b></p>
                </td>
            </tr>
        </table>
    </div>
    <asp:PlaceHolder ID="phHttps" runat="server"></asp:PlaceHolder>
</asp:Content>
