<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="CreateAnnoucement.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Annoucement.CreateAnnoucement" %>

<%@ Register Src="../../UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="../../JS/DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <span class="TitleLabel">
            <asp:Label ID="Label2" runat="server">New Announcement</asp:Label></span></div>
    <div class="divSubTitle">
        Here you can create an announcement.
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" Text="Cancel" OnClientClick="window.location.href='AnnoucementList.aspx?siteId=<%=SiteId%>';return false;" />
        </div>
        <div class="divTable">
            <br />
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        Subject:
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="tbSubject" runat="server" CssClass="txtnormal"></asp:TextBox>
                        <span class="" style="color: Red">*</span>
                        <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ErrorMessage="Subject is required"
                            ControlToValidate="tbSubject"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        Content:
                    </td>
                    <td class="ctd">
                        <uc1:HTMLEditor ID="htmlEditor" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        Begin Date:
                    </td>
                    <td class="ctd" id="divBeginTime">
                        <asp:TextBox ID="txtStartTime" runat="server" CssClass="txtmid"></asp:TextBox>
                        <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtStartTime')})"
                            src="../../images/datePicker.gif" align="absmiddle" style="cursor: pointer; width: 16px;
                            height: 20px;" />
                        <span class="alertMsg" style="color: Red">*</span> (MM-dd-yyyy)
                        <asp:RequiredFieldValidator ID="rfvBeginDate" runat="server" ErrorMessage="Begin Date is required"
                            ControlToValidate="txtStartTime" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="revBeginDate" runat="server" ControlToValidate="txtStartTime"
                            ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)"
                            Display="Dynamic" ErrorMessage="Begin Date's format error"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        Expire Date:
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtExpireTime" runat="server" CssClass="txtmid"></asp:TextBox>
                        <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtExpireTime')})"
                            src="../../images/datePicker.gif" align="absmiddle" style="cursor: pointer; width: 16px;
                            height: 20px;" />
                        <span class="alertMsg" style="color: Red">*</span> (MM-dd-yyyy)
                        <asp:RequiredFieldValidator ID="rfvExpireDate" runat="server" ErrorMessage="Expire Date is required"
                            ControlToValidate="txtExpireTime" Display="Dynamic"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtExpireTime"
                            ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)"
                            Display="Dynamic" ErrorMessage="Expire Date's format error"></asp:RegularExpressionValidator>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td class="ttd">
                        Forum:
                    </td>
                    <td class="ctd">
                        <asp:ListBox ID="ddlForum" runat="server" SelectionMode="Multiple"></asp:ListBox>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td colspan="2">
                        <span class="alertMsg" style="color: Red"><b>* Required Field</b></span>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" Text="Save" OnClick="btnSave_Click" />
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" Text="Cancel" OnClientClick="window.location.href='AnnoucementList.aspx?siteId=<%=SiteId%>';return false;" />
        </div>
    </div>
</asp:Content>
