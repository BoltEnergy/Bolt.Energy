<%@ Page Title="Site Settings" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="SiteSettings.aspx.cs" Inherits="Forum.UI.AdminPanel.Settings.SiteSettings"
    ValidateRequest="false" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register src="../../UserControl/HTMLEditor.ascx" tagname="HTMLEditor" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function OpenSite() {
            var trClosed = document.getElementById("trClosed");
            if (trClosed != null) {
                trClosed.style.display = 'none';
            }            
        }
        function CloseSite() {
            var trClosed = document.getElementById("trClosed");
            if (trClosed != null) {
                trClosed.style.display = '';
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
            <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
            <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
        </div>        
        <div class="divContent">
            <div class="divTopButton">
            <asp:Button ID="btnSaveSiteSetting1" runat="server" CssClass="mbtn" OnClick="btnSave" ValidationGroup="Save" />
            <asp:Button ID="btnReturn1" runat="server" CssClass="mbtn" CausesValidation="false" 
                    onclick="btnCancel_Click" />
            </div>
            <div class="divTable">
                <br />
                <table class="form-table">
                    <tr>
                        <td class="ttd" width="200">
                            <%=Proxy[EnumText.enumForum_Settings_FieldSiteName]%>
                        </td>
                        <td class="ctd" width="300">
                            <asp:TextBox ID="txtSiteName" runat="server" Text="" CssClass="txtnormal" />
                        </td>
                        <td class="rtd">                            
                            *<asp:RequiredFieldValidator ID="requiredFieldValidatorSiteName" runat="server" ControlToValidate="txtSiteName"  ValidationGroup="Save"></asp:RequiredFieldValidator>
                        </td>
                    </tr>  
                    <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldMetaKeywords] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtMetaKeywords" runat="server" Text="" CssClass="txtnormal" />
                    </td>
                    <td class="rtd">
                    </td>
                </tr>
                
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldMetaDescription] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtMetaDescription" runat="server" Text="" CssClass="txtnormal" />
                    </td>
                    <td class="rtd">
                    </td>
                </tr>  
                
                <tr id="trContactDetails" runat="server" style="display:none">
                    <td class="ttd"><%=Proxy[EnumText.enumForum_Settings_FieldContactDetails] %></td>
                    <td class="ctd" width="300">
                        <textarea id="heContactDetails" runat="server" cols="20" rows="15"></textarea>
                    </td>
                    <td></td>
                </tr>
                   
                <tr>
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPageSize] %>
                    </td>
                    <td class="ctd">
                        <asp:TextBox ID="txtPageSize" runat="server" CssClass="txtmid"></asp:TextBox> 
                    </td>
                    <td class="rtd">
                        <asp:RequiredFieldValidator ID="ValidRequiredPageSize" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ValidationGroup="Save"></asp:RequiredFieldValidator>
                        <asp:RangeValidator ID="ValidRangePageSize" runat="server" ControlToValidate="txtPageSize" Display="Dynamic" ValidationGroup="Save" Type="Integer" MaximumValue="2147483647" MinimumValue="1"></asp:RangeValidator>
                    </td>
                </tr>              
                <tr>
                    <td class="ttd" width="200">
                        <%=Proxy[EnumText.enumForum_Settings_FieldRadioSite]%>
                    </td>
                    <td class="ctd" width="300">
                        <input type="radio" runat="server" name="radioSiteSetting" checked="true" onclick="OpenSite();"
                            id="radioOpen" value="Open" /><%=Proxy[EnumText.enumForum_Settings_FieldRadioSiteOpen]%>&nbsp;&nbsp;
                        <input type="radio" runat="server" name="radioSiteSetting" onclick="OpenSite();"
                            id="radioVisitOnly" value="VisitOnly" /><%=Proxy[EnumText.enumForum_Settings_FieldRadioVisitOnly] %>&nbsp;&nbsp;
                        <input type="radio" runat="server" name="radioSiteSetting" onclick="CloseSite();"
                            id="radioClose" value="Close" /><%=Proxy[EnumText.enumForum_Settings_FieldRadioSiteClose]%>
                    </td>
                    <td class="rtd">
                    </td>
                </tr>
                <tr id="trClosed">
                    <td class="ttd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldSiteCloseReason]%>
                    </td>
                    <td class="ctd" width="300">
                        <uc1:HTMLEditor ID="heCloseReason" runat="server" />
                    </td>
                    <td class="rtd">
                        *
                    </td>
                </tr>
                <tr>
                    <td width="200">
                    </td>
                    <td width="300" class="rtd">
                        *<%=Proxy[EnumText.enumForum_Public_RequiredField]%>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnSaveSiteSetting2" runat="server" CssClass="mbtn" OnClick="btnSave" ValidationGroup="Save" />
            <asp:Button ID="btnReturn2" runat="server" CssClass="mbtn" CausesValidation="false"
                onclick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>
