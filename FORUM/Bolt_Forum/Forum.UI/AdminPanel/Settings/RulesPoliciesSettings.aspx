<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="RulesPoliciesSettings.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.RulesPoliciesSettings" %>
<%@ Import Namespace="Com.Comm100.Language" %>
<%@ Register Src="../../UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div class="divTitle">       
            <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"><%=Proxy[EnumText.enumForum_Settings_TitleRegistrationRulesSettings]%></asp:Label>           
</div>
<div class="divSubTitle">
            <asp:Label ID="lblSubTitle" runat="server"><%=Proxy[EnumText.enumForum_Settings_SubTitleRegistrationRuleSettings]%></asp:Label>            
</div>
<br />
<div class="divMsg">
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
</div>
<div class="divContent">
    
    <div class="divTopButton">
            <asp:Button ID="btnSaveRegistrationRulesSetting1" runat="server" CssClass="mbtn" 
                onclick="btnSaveRegistrationRulesSetting1_Click" />
    </div>
    
    <div class="divTable">
    <br />
    <table  style="width: 100%;" class="form-table" cellpadding='0' cellspacing='0'>
            
            <tr>
                <td>
                <div style="margin-top: 5px; margin-bottom: 5px; margin-right: 5%; margin-left: 5%; overflow:hidden;">
                        <uc1:HTMLEditor runat="server" ID="txtRulesContent" Limit="true"/>
                 </div>
                </td>
            </tr>
    </table>
    </div>
    
    <div class="divButtomButton">
            <asp:Button ID="btnSaveRegistrationRulesSetting2" runat="server" cssclass="mbtn"  text="proxy  onclick="btnSaveRegistrationRulesSetting1_Click" />
    </div>
</div>  
</asp:Content>
