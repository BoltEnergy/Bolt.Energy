<%@ Page Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ResetPasswordByAdmin.aspx.cs" Inherits="Forum.UI.AdminPanel.Operators.OperatorResetPassword"
    Title="Untitled Page" ValidateRequest="false"%>
<%@ Import Namespace="Com.Comm100.Language" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <span class="TitleLabel"><%=Proxy[EnumText.enumForum_Operator_TitleResetPasswordPage] %></span></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="mbtn">
            </asp:Button>
            <asp:Button ID="btnCancel1" runat="server" Text="Cancel" CausesValidation="False"
                OnClick="btnCancel_Click" CssClass="mbtn"></asp:Button>
        </div>
        <br />
        <div class="divTable">
            <table class="form-table">
                <tr>
                    <td class="ttd" width="150">
                        <%=Proxy[EnumText.enumForum_Operator_FieldNewPassword]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="txtnormal" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        *<asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword"
                                Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorPasswordRequired]%></asp:RequiredFieldValidator>
                    </td>                  
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Operator_FieldRetypePassword]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtRetypePassword" runat="server" CssClass="txtnormal" TextMode="Password"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        *<asp:RequiredFieldValidator ID="rfvRetypePassword" runat="server" ControlToValidate="txtRetypePassword"
                                Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorRetypePasswordRequired]%></asp:RequiredFieldValidator>
                        <asp:CompareValidator ID="cvRetypePassword" runat="server" ControlToCompare="txtPassword"
                                ControlToValidate="txtRetypePassword"><%=Proxy[EnumText.enumForum_Operator_ErrorPasswordsMatch]%></asp:CompareValidator>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td  class="rtd">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></td>
                    <td></td>
                 </tr>              
            </table>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="mbtn">
            </asp:Button>
            <asp:Button ID="btnCancel2" runat="server" Text="Cancel" CausesValidation="False"
                OnClick="btnCancel_Click" CssClass="mbtn"></asp:Button>
        </div>
    </div>
</asp:Content>
