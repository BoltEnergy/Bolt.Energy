﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="EditUser.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Users.EditUser" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
    <asp:Label ID="lblSubTitle" runat="server" />       
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" 
                onclick="btnSave_Click" />
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" CausesValidation="false"
                onclick="btnCancel_Click" />
        </div>
        <br />
        <div class="divTable">
            <div class="divCpFirstTitle">
                <%=Proxy[EnumText.enumForum_User_FieldBasicInformation] %>
            </div>
            <br />
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldEmail]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txtmid"></asp:TextBox>                       
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowEmail" runat="server" />
                    </td>
                    <td class="rtd">
                         *
                        <asp:RequiredFieldValidator ID="ValidEmailRequired" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValidEmailExpression" runat="server" ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldDisplayName]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="txtmid"></asp:TextBox>                       
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowDisplayName" runat="server" Enabled="False" Checked="true" />
                    </td>
                    <td class="rtd">
                        <%--<img src="../../Images/help.gif" onmouseover="showHelp('divHelp',<%=Proxy[EnumText.enumForum_User_HelpDisplayname] %>);" onmouseout="closeHelp('divHelp');" />--%>*
                        <asp:RequiredFieldValidator ID="ValidDisplayNameRequired" runat="server" ControlToValidate="txtDisplayName"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <%--<asp:RegularExpressionValidator ID="ValidDisplayNameExpression" runat="server" ControlToValidate="txtDisplayName"></asp:RegularExpressionValidator>--%>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldFirstName]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd" rowspan="2">
                        <asp:CheckBox ID="chkIfShowUserName" runat="server" />
                    </td>
                    <td>                        
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldLastName]%>
                    </td>
                    <td class="" style="">
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldAge]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtAge" runat="server" CssClass="txtshort"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowAge" runat="server" />
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidAgeRequired" runat="server" ControlToValidate="txtAge"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="VaildAgeRange" runat="server" MaximumValue="100" MinimumValue="0"
                    ControlToValidate="txtAge" Type="Integer"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldGender]%>
                    </td>
                    <td class="ctd">
                        <asp:DropDownList ID="ddlGender" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowGender" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldOccupation]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtOccupation" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowOccupation" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldCompany]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowCompany" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldPhoneNumber]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowPhoneNumber" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldFaxNumber]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowFaxNumber" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldInterests]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtInterests" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowInterests" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldHomePage]%>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtHomePage" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chkIfShowHomePage" runat="server" />
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td class="rtd" colspan="3"><%=Proxy[EnumText.enumForum_Forums_FieldRequireField]%></td>
                </tr>
            </table>
            <br />
            
            <asp:Panel ID="panelUserGroup" runat="server">
            <div class="divCpFirstTitle">
                <%=Proxy[EnumText.enumForum_User_ColumnUserGroup] %>
            </div>
            <br />
            <table class="form-table">
                <tr>
                    <td class="ttd" width="100px">
                        <%=Proxy[EnumText.enumForum_User_FieldUserGroup] %>
                    </td>
                    <td>
                        <asp:CheckBox ID="chkAdministrator" runat="server" />
                       <asp:CheckBoxList ID="cblGroup" runat="server">
                        </asp:CheckBoxList>
                        <asp:Label ID="lblAddUserGroup" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            </asp:Panel>
            
            <asp:Panel ID="panelAdditionInfor" runat="server">
            <div class="divCpFirstTitle">
               <%=Proxy[EnumText.enumForum_User_FieldAdditionalInformation]%>
            </div>
            <br />
            <table class="form-table">
                <tr id="trfieldscore" runat="server">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldScore] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtScore" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidScoreRequired" runat="server" ControlToValidate="txtScore"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="VaildScoreRange" runat="server" MaximumValue="2147483647" MinimumValue="-2147483648"
                    ControlToValidate="txtScore" Type="Integer"></asp:RangeValidator>
                    </td>
                </tr>
                <tr id="trfieldreputation" runat="server">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_User_FieldReputation] %>
                    </td>
                    <td class="cth">
                        <asp:TextBox ID="txtReputation" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidReputationRequired" runat="server" ControlToValidate="txtReputation"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="VaildReputationRange" runat="server" MaximumValue="2147483647" MinimumValue="-2147483648"
                    ControlToValidate="txtReputation" Type="Integer"></asp:RangeValidator>
                    </td>
                </tr>
                
            </table>
            </asp:Panel>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn"
                onclick="btnSave_Click" />
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" CausesValidation="false"
                onclick="btnCancel_Click" />
        </div>
    </div>
    
</asp:Content>
