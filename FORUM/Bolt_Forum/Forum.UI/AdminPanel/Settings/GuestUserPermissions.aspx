<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="GuestUserPermissions.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.GuestUserPermissions" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        #tblPermissionsSet
        {
            background-color: #d7e7ee;
            width: 100%;
            text-align: left;
        }
        #tblPermissionsSet td
        {
            background-color: #ffffff;
        }
        .style1
        {
            height: 24px;
        }
    </style>
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" onclick="btnSave_Click" ValidationGroup="Save"/>
            <asp:Button ID="btnReturn1" runat="server" CausesValidation="False" CssClass="mbtn" onclick="btnCancel_Click">
            </asp:Button>
        </div>
        <div class="divTable">
            <div>
                <table width="100%" id="tblPermissionsSet">
                    <tr>
                        <td width="30%" align="right" nowrap="nowrap">
                            <%=Proxy[EnumText.enumForum_Settings_FieldAllowViewForum] %>
                        </td>
                        <td width="20%">
                            <asp:CheckBox ID="ckbViewForum" runat="server" />
                        </td>
                        <td width="50%">
                           <%-- <asp:Image ID="ImageViewForum" runat="server" Visible="true" 
                                ImageUrl="~/Images/help.gif" />--%>
                            <asp:Label ID="lblViewForum" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <%=Proxy[EnumText.enumForum_Settings_FieldAllowSearch] %>
                        </td>
                        <td>
                            <asp:CheckBox ID="ckbSearch" runat="server" />
                        </td>
                        <td>
                           <%-- <asp:Image ID="ImageSearch" runat="server" Visible="true" 
                                ImageUrl="~/Images/help.gif" />--%>
                                <asp:Label ID="lblSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <%=Proxy[EnumText.enumForum_Settings_FieldMinIntervalTimeSearching] %>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSearchInterval" runat="server" CssClass="txtshortScore" customattr="20"></asp:TextBox>s
                            <asp:RangeValidator ID="rvSearchInterval" runat="server" ControlToValidate="txtSearchInterval" Display="Dynamic" ValidationGroup="Save" Type="Integer" MaximumValue="2147483647" MinimumValue="1"></asp:RangeValidator>

                        </td>
                        <td>
                            <%--<asp:Image ID="ImageSearchInterval" runat="server" Visible="true" 
                                ImageUrl="~/Images/help.gif" />--%>
                             <asp:Label ID="lblIntervalSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn"  onclick="btnSave_Click" ValidationGroup="Save"/>
            <asp:Button ID="btnReturn2" runat="server" CausesValidation="False" CssClass="mbtn" onclick="btnCancel_Click">
            </asp:Button>
        </div>
    </div>
</asp:Content>
