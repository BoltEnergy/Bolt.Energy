<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserProfileEdit.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserProfileEdit"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
                    <div class="top_cat2_right" >
                        <%=Proxy[EnumText.enumForum_User_TitleEditProfile]%>
                        <img alt="" src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_User_SubtitleEditProfile]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldEmail]%></b></p>
                </td>
                <td class="row2" style="width: 20%;">
                    <p>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2" style="width: 18%;">
                    <p>
                        <asp:CheckBox ID="chkIfShowEmail" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:RequiredFieldValidator ID="ValidEmailRequired" runat="server" ControlToValidate="txtEmail"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="ValidEmailExpression" runat="server" ControlToValidate="txtEmail"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldDisplayName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtDisplayName" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowDisplayName" runat="server" Enabled="False" Checked="true" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:RequiredFieldValidator ID="ValidDisplayNameRequired" runat="server" ControlToValidate="txtDisplayName"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <%--  <tr>
            <td class="row1">
            </td>
            <td colspan='3' class="row2">
            </td>
        </tr>--%>
            <tr>
                <td class="row2" align="right" style="border-bottom: none;">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldFirstName]%></b></p>
                </td>
                <td class="row2" style="border-bottom: none;">
                    <p>
                        <asp:TextBox ID="txtFirstName" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td rowspan='2' class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowUserName" runat="server" />
                    </p>
                </td>
                <td class="row2" style="border-bottom: none;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldLastName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtLastName" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2" colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldAge]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtAge" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowAge" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        &nbsp;
                        <asp:RangeValidator ID="VaildAgeRange" runat="server" MaximumValue="100" MinimumValue="0"
                            ControlToValidate="txtAge" Type="Integer"></asp:RangeValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldGender]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:DropDownList ID="ddlGender" runat="server">
                        </asp:DropDownList>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowGender" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldOccupation]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtOccupation" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowOccupation" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldCompany]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtCompany" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowCompany" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldPhoneNumber]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowPhoneNumber" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldFaxNumber]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtFaxNumber" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowFaxNumber" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldInterests]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtInterests" runat="server" CssClass="txt"></asp:TextBox></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowInterests" runat="server" /></p>
                </td>
                <td class="row2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldHomePage]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="txtHomePage" runat="server" CssClass="txt"></asp:TextBox>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:CheckBox ID="chkIfShowHomePage" runat="server" />
                    </p>
                </td>
                <td class="row2">
                    &nbsp;
                    <%--<asp:RegularExpressionValidator ID="ValidHomePageExpression" runat="server" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?"
                    ControlToValidate="txtHomePage"></asp:RegularExpressionValidator>--%>
                </td>
            </tr>
            <tr id="Score" runat="server">
                <td class="row2" align="right">
                    <p>
                        <b>
                            <asp:Label ID="lblScore" runat="server"><%=Proxy[EnumText.enumForum_User_FieldScore] %></asp:Label></b></p>
                </td>
                <td class="row2" colspan="2">
                    <p>
                        <asp:TextBox ID="txtScore" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                        <span class="require">*</span>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:RequiredFieldValidator ID="ValidScoreRequired" runat="server" ControlToValidate="txtScore"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidScoreRange" runat="server" ControlToValidate="txtScore"
                            Display="Dynamic" Type="Integer" MaximumValue="2147483647" MinimumValue="-2147483648"></asp:RangeValidator>
                    </p>
                </td>
            </tr>
            <tr id="Reputation" runat="server">
                <td class="row2" align="right">
                    <p>
                        <b>
                            <asp:Label ID="lblReputation" runat="server"><%=Proxy[EnumText.enumForum_User_FieldReputation] %></asp:Label></b></p>
                </td>
                <td class="row2" colspan="2">
                    <p>
                        <asp:TextBox ID="txtReputation" runat="server" CssClass="txt" Enabled="false"></asp:TextBox>
                        <span class="require">*</span>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:RequiredFieldValidator ID="ValidReputationRequired" runat="server" ControlToValidate="txtReputation"
                            Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidReputationRange" runat="server" ControlToValidate="txtReputation"
                            Display="Dynamic" Type="Integer" MaximumValue="2147483647" MinimumValue="-2147483648"></asp:RangeValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row2">
                    &nbsp;
                </td>
                <td class="row2" colspan='3'>
                    <p>
                        <span class="require">*
                            <%=Proxy[EnumText.enumForum_Public_RequiredField]%></span>
                    </p>
                </td>
            </tr>
            <tr>
                <td colspan="4" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnSave1" runat="server" CssClass="btn" OnClick="btnSave1_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
