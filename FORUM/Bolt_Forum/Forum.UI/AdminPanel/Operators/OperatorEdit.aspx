<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="OperatorEdit.aspx.cs" Inherits="Forum.UI.AdminPanel.Operators.OperatorEdit" 
    ValidateRequest="false"%>
<%@ Import Namespace="Com.Comm100.Language" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle"><span class="TitleLabel"><%=Proxy[EnumText.enumForum_Operator_TitleEditPage]%></span></div>
    <div class="divSubTitle"><asp:Label ID="lblSubTitle" runat="server" Text=""><%=Proxy[EnumText.enumForum_Operator_SubtitleEditPage]%></asp:Label></div>
    <br/>
    <div class="divMsg"><asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" OnClick="btnSave_Click"  CssClass="mbtn"></asp:Button>
            <asp:Button ID="btnCancel1" runat="server" CausesValidation="False" OnClick="btnCancel_Click" CssClass="mbtn"></asp:Button>
        </div>
        <br/>
        <div class="divTable">
        <table class="form-table">
            <tr>
                <td class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldEmail] %></td>
                <td class="ctd"><asp:TextBox ID="txtEmail" runat="server" CssClass="txtnormal"></asp:TextBox></td>
                <td><img alt="" id="imgEmail" src="../../images/help.gif" runat="server" onmouseout="closeHelp('divHelp');" /></td>
                <td class="rtd">
                    *<asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorEmailRequired]%></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"><%=Proxy[EnumText.enumForum_Operator_ErrorEmailInvalid]%></asp:RegularExpressionValidator>
                </td>
            </tr>
            <tr>
            <td class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldRetypeEmail]%></td>
            <td class="ctd"><asp:TextBox ID="txtRetypeEmail" runat="server" CssClass="txtnormal"></asp:TextBox></td>
            <td></td>
            <td class="rtd">
                *<asp:RequiredFieldValidator ID="rfvRetypeEmail" runat="server" ControlToValidate="txtRetypeEmail"
                        Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorRetypeEmailRequired]%></asp:RequiredFieldValidator>
                <asp:CompareValidator ID="cvRetypeEmail" runat="server" ControlToCompare="txtEmail" ControlToValidate="txtRetypeEmail" 
                    ErrorMessage=""><%=Proxy[EnumText.enumForum_Operator_ErrorEmailsMatch]%></asp:CompareValidator>
            </td>
            </tr>
            <tr>
                <td class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldDisplayName]%></td>
                <td  class="ctd"><asp:TextBox ID="txtName" runat="server" CssClass="txtnormal"></asp:TextBox></td>
                <td><img alt="" id="imgDisplayName" src="../../images/help.gif" runat="server" onmouseout="closeHelp('divHelp');" /></td>
                <td class="rtd">
                    *<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage=""
                        Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorNameRequired]%></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldFirstName]%></td>
                <td class="ctd"><asp:TextBox ID="txtFirstName" runat="server" CssClass="txtnormal"></asp:TextBox></td>
                <td></td>
                <td class="rtd">
                    *<asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage=""
                        Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorFirstNameRequired]%></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldLastName]%></td>
                <td class="ctd"><asp:TextBox ID="txtLastName" runat="server" CssClass="txtnormal"></asp:TextBox></td>
                <td></td>
                <td class="rtd">
                    *<asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage=""
                        Display="Dynamic"><%=Proxy[EnumText.enumForum_Operator_ErrorLastNameRequired]%></asp:RequiredFieldValidator>
                </td>
            </tr>                
            <tr>
                <td  class="ttd"><%=Proxy[EnumText.enumForum_Operator_FieldDescription]%></td>
                <td class="ctd" ><asp:TextBox ID="txtDescription" runat="server" CssClass="areanormal" TextMode="MultiLine"></asp:TextBox></td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="ctd">
                     <asp:CheckBox ID="cbxIsAdmin" runat="server"/><%=Proxy[EnumText.enumForum_Operator_FieldIsAdministrator]%>&nbsp;
                    <img alt="" id="imgIsAdmin" src="../../images/help.gif" runat="server" onmouseout="closeHelp('divHelp');" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td class="ctd">
                    <asp:CheckBox ID="cbxIsActive" runat="server" Checked="True" /><%=Proxy[EnumText.enumForum_Operator_FieldIsActive]%>&nbsp;
                    <img alt="" id="imgIsActive" src="../../images/help.gif" runat="server" onmouseout="closeHelp('divHelp');" />
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td></td>
                <td  class="rtd">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></td>
            </tr>
            </table>
            </div>
            <br/>
                <div class="divButtomButton">
                    <asp:Button ID="btnSave2" runat="server" OnClick="btnSave_Click" Text="Save" CssClass="mbtn" />                 
                    <asp:Button ID="btnCancel2" runat="server" CausesValidation="False" OnClick="btnCancel_Click" Text="Cancel" CssClass="mbtn" />
                </div>
        </div>
</asp:Content>
