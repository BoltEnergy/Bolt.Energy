<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="SendResetPasswordEmail.aspx.cs" Inherits="Com.Comm100.Forum.UI.SendResetPasswordEmail" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title></title>
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
                        <%=Proxy[EnumText.enumForum_Login_TitleSendResetPasswordEmail]%>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td colspan='2' class="row4">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Login_MessageSendResetPasswordEmailSuccess]%></b></p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
