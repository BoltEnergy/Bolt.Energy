<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="EmailVerification.aspx.cs" Inherits="Com.Comm100.Forum.UI.EmailVerification" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <asp:PlaceHolder ID="phAutoSubmit" runat="server" EnableViewState="false"></asp:PlaceHolder>
    <div class="alertMsg">
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Login_TitleEmailVerification]%>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td colspan='2' class="row4">
                    <p>
                        <span>
                            <asp:Label ID="lblAfterEmailVerification" runat="server" Text="" Visible="false"
                                EnableViewState="false"></asp:Label>
                        </span>
                        <p style="text-align: center">
                            <asp:HyperLink ID="linkToUserControl" runat="server" Visible="false" EnableViewState="false"><%=Proxy[EnumText.enumForum_Login_LinkGotoUserPanel] %></asp:HyperLink>
                        </p>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
