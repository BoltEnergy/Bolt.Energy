<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="FindPassword.aspx.cs" Inherits="Com.Comm100.Forum.UI.FindPassword" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function btnCancelClick() {
            var url = window.location.href;
            url = url.substr(0, url.indexOf('FindPassword') - 1);
            window.location = url;
        }
    </script>

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
                        <span class="TitleName">
                            <%=Proxy[EnumText.enumForum_Login_TitleFindPassword]%></span>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td colspan='2' class="row4">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Login_NoteEnterEmail]%></span></b></p>
                </td>
            </tr>
            <tr>
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldEmail]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtEmailToResetPassword" runat="server" MaxLength="64" CssClass="txt"></asp:TextBox>
                        &nbsp; <span class="require">*
                            <asp:RegularExpressionValidator ID="RegularExpressionTxtEmailToReset" runat="server"
                                Display="Dynamic" ControlToValidate="txtEmailToResetPassword" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                ValidationGroup="Send"></asp:RegularExpressionValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldTxtEmailToReset" runat="server" ControlToValidate="txtEmailToResetPassword"
                                Display="Dynamic" ValidationGroup="Send"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblErrorEmail" runat="server" Text="" CssClass="errorMsg" EnableViewState="false"></asp:Label></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnToSendEmail" runat="server" Text="" OnClick="btnToSendEmail_Click"
                            ValidationGroup="Send" CssClass="btn" />
                        &nbsp;&nbsp;
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
