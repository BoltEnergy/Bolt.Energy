<%@ Page Title="User Registration Settings" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="RegistrationSettings.aspx.cs" Inherits="Forum.UI.AdminPanel.Settings.RegistrationSettings" ValidateRequest="false" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register src="../../UserControl/HTMLEditor.ascx" tagname="HTMLEditor" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label id="lblError" runat="server" CssClass="ErrorLabel" EnableViewState ="false"></asp:Label>
        <asp:Label id="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button runat="server" ID="btnSaveRegistrationSetting1" CssClass="mbtn" OnClick="btnSaveRegistrationSetting_Click" ValidationGroup="Save" />
            <asp:Button ID="btnReturn1" runat="server" CssClass="mbtn" CausesValidation="false" onclick="btnCancel_Click" />
        </div>
        <div class="divTable">
        <br \>
        <fieldset class="fieldsetCp">
            <legend><%=Proxy[EnumText.enumForum_Register_TitleRegister]%></legend>
             <table class="form-table">
                <tr>
                    <td class="ttd">                        
                    </td>
                    <td class="ctd">      
                        <asp:CheckBox ID="chkAllowNewUser" runat="server" CssClass="RadioButton" /><%=Proxy[EnumText.enumForum_Settings_FieldRegistrationAllowed]%>&nbsp;
                        
                        <br/>               
                        <asp:CheckBox ID="chkModerateNewMembers" runat="server"  
                            CssClass="RadioButton"/><%=Proxy[EnumText.enumForum_Settings_FieldRegistrationModerate]%>&nbsp;
                        <img src="../../Images/help.gif" style="margin-bottom:-3px" 
                            onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Settings_HelpRegistrationModrate].Replace("'","\\'"))%>');" 
                            onmouseout="closeHelp('divHelp');" alt='<%=Proxy[EnumText.enumForum_Public_Help]%>'/>
                                               
                        <br />
                        <asp:CheckBox ID="chkVerifyEmail" runat="server" 
                        CssClass="RadioButton"/><%=Proxy[EnumText.enumForum_Settings_FieldRegistrationEmailVerify]%>&nbsp;
                        <img src="../../Images/help.gif" style="margin-bottom:-3px" 
                        onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Settings_HelpRegistrationEmailVerify].Replace("'","\\'"))%>');" 
                        onmouseout="closeHelp('divHelp');" alt='<%=Proxy[EnumText.enumForum_Public_Help]%>'/>                        
                    </td>
                </tr>
                
                
             </table>
        </fieldset>
        
        
        <fieldset class="fieldsetCp">
            <legend><%=Proxy[EnumText.enumForum_Settings_FieldDisplayName]%></legend>
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldMinimumDisplayNameLength] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDisplayNameMinLength" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidRequiredMinLengthOfDisplayName" runat="server" ControlToValidate="txtDisplayNameMinLength" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidRangeMinLengthOfDisplayName" runat="server" ControlToValidate="txtDisplayNameMinLength" Display="Dynamic" Type="Integer" MaximumValue="2147483647" MinimumValue="1" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldMaximumDisplayNameLength]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDisplayNameMaxLength" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidRequiredMaxLengthOfDisplayName" runat="server" ControlToValidate="txtDisplayNameMaxLength" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidRangeMaxLengthOfDisplayName" runat="server" ControlToValidate="txtDisplayNameMaxLength" Type="Integer" MaximumValue="2147483647" MinimumValue="1" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldNotAllowedDisplayNames]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtNotAllowedDisplayName" runat="server" CssClass="txtnormal"></asp:TextBox>
                        <img src="../../Images/help.gif" style="margin-bottom:-3px" 
                            onmouseover="showHelp('divHelp','<%=GetTooltipString(Proxy[EnumText.enumForum_Settings_HelpNotAllowedDisplayNames].Replace("'","\\'"))%>');" 
                            onmouseout="closeHelp('divHelp');" alt='<%=Proxy[EnumText.enumForum_Public_Help]%>'/>
                    </td>
                </tr>
                
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldDisplayNameRegularExpression]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDisplayNameExpression" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:RegularExpressionValidator ID="revDisplayNameExpression" ControlToValidate="txtDisplayNameExpression" runat="server" Display="Dynamic" ValidationGroup="Save" ValidationExpression="^\^.*\$$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldDisplayNameRegularExpressionInstruction] %> 
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDisplayNameExpressionInstruction" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </fieldset>
        
        <fieldset class="fieldsetCp">
            <legend><%=Proxy[EnumText.enumForum_Settings_FieldGreetingMessage]%> 
           </legend>
            <table class="form-table">
                <tr>                    
                    <td class="ctd">
                        <uc1:HTMLEditor ID="heGreetingMessage" runat="server" />
                    </td>
                </tr>
            
            </table>
        </fieldset>
        
        <fieldset class="fieldsetCp">
            <legend><%=Proxy[EnumText.enumForum_Settings_FieldAgreement]%> </legend>
            <table class="form-table">
                <tr>
                    <td class="ctd">
                        <textarea id="txtAgreement" cols="20" rows="15" runat="server"></textarea>
                    </td>
                    
                </tr>
            </table>
        </fieldset>
            <br \>
        </div>
        <div  class="divButtomButton">            
            <asp:Button runat="server" ID="btnSaveRegistrationSetting2" CssClass="mbtn" OnClick="btnSaveRegistrationSetting_Click" ValidationGroup="Save" />
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn"  CausesValidation="false" onclick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>
