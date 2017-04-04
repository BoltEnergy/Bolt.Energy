<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="HotTopicStrategy.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.HotTopicStrategy" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click" ValidationGroup="Save" />
            <asp:Button ID="btnReturn1" runat="server" CausesValidation="False"
                CssClass="mbtn" OnClick="btnCancel_Click"></asp:Button>
        </div>
        <div class="divTable">
            <table class="form-table">
                <tr>
                    <td class="ttd" width="200">
                        <%=Proxy[EnumText.enumForum_HotTopicStrategy_FieldView] %>
                    </td>
                    <td>
                        &nbsp;>=
                    </td>
                    <td class="ctd" width="200">
                        <asp:TextBox ID="txtViews" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:RangeValidator ID="rvViews" runat="server"
                            ControlToValidate="txtViews" Type="Integer" MaximumValue="2147483647" MinimumValue="1" Display="Dynamic"
                            ValidationGroup="Save" ></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorViews" runat="server" ControlToValidate="txtViews" Display="Dynamic"  ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                    </td>
                    <td>
                    </td>
                    <td class="ctd">
                        <asp:DropDownList ID="ddlLogical" runat="server"></asp:DropDownList>
                    </td>
                    <td class="rtd">
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_HotTopicStrategy_FieldReplies]%>
                    </td>
                    <td>
                        &nbsp;>=
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtPosts" runat="server" CssClass="txtmid"></asp:TextBox>
                    </td>
                    <td class="rtd">
                        <span style="color: Red">*</span>
                    </td>
                    <td>
                        <asp:RangeValidator ID="rvPosts" runat="server"
                            ControlToValidate="txtPosts" Type="Integer" MaximumValue="2147483647" MinimumValue="1" Display="Dynamic"
                            ValidationGroup="Save" ></asp:RangeValidator>
                            <asp:RequiredFieldValidator ID="requiredFieldValidatorPosts" runat="server" ControlToValidate="txtPosts" Display="Dynamic"  ValidationGroup="Save"></asp:RequiredFieldValidator>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn"  OnClick="btnSave_Click"  ValidationGroup="Save"/>
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn" OnClick="btnCancel_Click">
            </asp:Button>
        </div>
    </div>
</asp:Content>
