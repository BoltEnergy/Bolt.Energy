<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="UserBanned.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserBanned" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <table class="tb_forum" cellpadding='0' cellspacing='0'>
            <tr>
                <td class="tdMyLeft" style="text-align: left; padding-left: 100px;">
                    <p>
                        <br />
                        <span class="spanNormal">
                            <asp:Label ID="lblUserBannedMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
                        </span>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
