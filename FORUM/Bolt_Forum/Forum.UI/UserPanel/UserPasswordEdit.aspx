<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserPasswordEdit.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserPasswordEdit"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="../JS/Common/Common.js"></script>

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
                        <%=Proxy[EnumText.enumForum_User_TitleEditPassword]%>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_User_SubtitleEditPassword]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td class="row2" style="width: 35%;" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldCurrentPassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtCurrentPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldNewPassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtNewPassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                        <span class="require">*</span>
                        <asp:RequiredFieldValidator ID="ValidNewPasswordRequired" runat="server" ControlToValidate="txtNewPassword"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldRetypePassword]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="txt" TextMode="Password"></asp:TextBox>
                        <span class="require">*</span>
                        <asp:RequiredFieldValidator ID="ValidRetypePasswordRequired" runat="server" ControlToValidate="txtRetypePassword"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <nobr><asp:CompareValidator ID="vaildRetypePasswordCompare" runat="server" 
                            ControlToCompare="txtNewPassword" ControlToValidate="txtRetypePassword" Display="Dynamic"></asp:CompareValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                &nbsp;
                </td>
                <td class="row2">
                    <p>
                        <span class="require">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row5" colspan='2' align="center">
                    <p>
                        <asp:Button ID="btSave1" runat="server" CssClass="btn" OnClick="btSave1_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <asp:PlaceHolder ID="phHttps" runat="server"></asp:PlaceHolder>
</asp:Content>
