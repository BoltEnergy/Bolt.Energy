<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
 AutoEventWireup="true" CodeBehind="SMTPSettings.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.SMTPSettings" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" type="text/javascript">
        function checkAuthentication(chbid, lb1id, lb2id, tb1id, tb2id, rfv1id, rfv2id) {
            var chb = document.getElementById(chbid);
            var lb1 = document.getElementById(lb1id);
            var lb2 = document.getElementById(lb2id);
            var tb1 = document.getElementById(tb1id);
            var tb2 = document.getElementById(tb2id);
            var rfv1 = document.getElementById(rfv1id);
            var rfv2 = document.getElementById(rfv2id);
            
            if (chb.checked == true) {
                lb1.innerHTML = "*";
                lb2.innerHTML = "*";
                tb1.disabled = false;
                tb2.disabled = false;
                rfv1.enabled = true;
                rfv2.enabled = true;
            }
            else {
                lb1.innerHTML = "";
                lb2.innerHTML = "";
                tb1.disabled = true;
                tb2.disabled = true;
                rfv1.enabled = false;
                rfv2.enabled = false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label id="lblError" runat="server" CssClass="ErrorLabel" EnableViewState ="false"></asp:Label>
        <asp:Label id="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click" ValidationGroup="Save" />
            <asp:Button ID="btnReturn1" runat="server" CssClass="mbtn" CausesValidation="false" 
                onclick="btnCancel_Click" />
        </div>
        <div class="divTable">
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldSMTPServer]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtSMTPServer" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        *
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorSMTPServer" runat="server"
                            ControlToValidate="txtSMTPServer" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldSMTPPort]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtSMTPPort" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        *
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorSMTPPort" runat="server"
                            ControlToValidate="txtSMTPPort" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldAuthenticationRequired] %>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chbAuthentication" runat="server" />
                    </td>
                    <td class="rtd"></td>
                </tr>
                <tr>
                    <td class="ttd">
                         <%=Proxy[EnumText.enumForum_SMTPSettings_FieldUserName] %>
                    </td>
                    <td class="ctd">
                         <asp:TextBox ID="txtUserName" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorUserName" runat="server"
                            ControlToValidate="txtUserName" Enabled="false" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>        
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldPassword] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                        <asp:RequiredFieldValidator ID="requiredFieldValidatorPassword" runat="server"
                            ControlToValidate="txtPassword" Enabled="false" ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>         
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldSenderEmailAddress] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtSenderEmailAddress" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd"></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldSenderName] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtSenderName" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd"></td>
                </tr>
                <tr>
                    <td class="ttd" >
                        <%=Proxy[EnumText.enumForum_SMTPSettings_FieldSSL] %>
                    </td>
                    <td class="ctd" >
                        <asp:CheckBox ID="chbSSL" runat="server" />
                    </td>
                    <td class="rtd"></td>
                </tr>
            </table>
        </div>
        <div class="divButtomButton">            
            <asp:Button ID="btnSave" runat="server" CssClass="mbtn" onclick="btnSave_Click" ValidationGroup="Save" />
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn" CausesValidation="False"
                onclick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>
