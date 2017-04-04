<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserSignatureEdit.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserSignatureEdit"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="../UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_User_TitleEditSignature]%></span>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_User_SubtitleEditSignature]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellpadding="0" cellspacing="0">
            <tr>
                <td class="row4" style="border-left: none; border-right: none; border-bottom: none">
                    <p>
                        <uc1:HTMLEditor ID="HTMLEditor1" runat="server" />
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row4" align="center">
                    <p>
                        <asp:Button ID="btnSave1" runat="server" CssClass="btn" OnClick="btnSave1_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
