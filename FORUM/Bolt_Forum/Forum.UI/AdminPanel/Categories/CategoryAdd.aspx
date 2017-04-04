﻿<%@ Page Title="Category" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="CategoryAdd.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Categories.CategoryAdd" ValidateRequest="false"%>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
// <!CDATA[

function ButtonCancel2_onclick() {

}

function ButtonCancel1_onclick() {

}

// ]]>
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
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server"  OnClick="btnSave1_Click" CssClass="mbtn">
            </asp:Button>
            <input class="mbtn" onclick="javascript:window.location='CategoryList.aspx?siteid=<%= SiteId %>';"
                type="button" value='<%=Proxy[EnumText.enumForum_Categories_ButtonCancel]%>' />
        </div>
        <div class="divTable">
        <br />
        <table class="form-table">
            <tr>
                <td class="ttd">
                    <asp:Label ID="lblName" runat="server"><%=Proxy[EnumText.enumForum_Categories_FieldName]%></asp:Label>:
                </td>
                <td class="ctd">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtnormal" ></asp:TextBox><span
                        style="color: red">*</span>
                </td>
                <td class="rtd">
                    <asp:RequiredFieldValidator ID="requiredFieldValidatorName" ControlToValidate="txtName"
                        runat="server" ><%=Proxy[EnumText.enumForum_Categories_ErrorNameRequired]%></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td valign="top" class="ttd">
                    <asp:Label ID="lblDescription" runat="server"><%=Proxy[EnumText.enumForum_Categories_FieldDescription]%></asp:Label>:
                </td>
                <td class="ctd">
                    <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine"
                        CssClass="areanormal"></asp:TextBox>
                </td>
                <td>&nbsp;</td>
            </tr>
            <%--<tr>
                <td class="ttd">
                    <asp:Label ID="lblStatus" runat="server"><%=Proxy[EnumText.enumForum_Categories_FieldStatus]%></asp:Label>
                </td>
                <td class="ctd">
                    <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatColumns="5">
                    </asp:RadioButtonList>
                </td>
                <td></td>
            </tr>--%>
            <tr>
            <td>&nbsp;</td>
                <td class="rtd"><asp:Label ID="lblRequiredField" runat="server"><%=Proxy[EnumText.enumForum_Categories_ErrorRequireField]%></asp:Label></td>
                <td>&nbsp;</td>
            </tr>
        
        </table>
        <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" class="mbtn" OnClick="btnSave2_Click" />
            <input  class="mbtn" onclick="javascript:window.location='CategoryList.aspx?siteid=<%= SiteId %>';"
                type="button"  value='<%=Proxy[EnumText.enumForum_Categories_ButtonCancel]%>' />
        </div>
    </div>
</asp:Content>
