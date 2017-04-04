<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="SiteCustmizeLogo.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Styles.SiteCustmizeLogo" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle"><span class="TitleLabel"><%=Proxy[EnumText.enumForum_Styles_TitleSiteCustomizeLogo] %></span></div>
    <div class="divSubTitle"><asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnUpload1" runat="server" CssClass="mbtn" Text="Upload"
                        OnClick="btnUpload_Click" /> &nbsp;&nbsp;   
            <asp:Button ID="btnCancel1" runat="server" 
                CssClass="mbtn"  OnClick="btnCancel_Click" CausesValidation="False"></asp:Button>    
        </div>
        <br />
        <div class="divCpFirstContent" runat="server">
            <fieldset class="fieldsetCp">
                <legend><%=Proxy[EnumText.enumForum_Styles_LabelLogo]%></legend>
                <br />
                <div style="text-align: left;">
                    <asp:Image ID="imgLogo" ImageUrl="" runat="server"/>
                </div>
                <br />
                <div style="text-align: left">
                    <asp:FileUpload ID="fileUploadLogo" runat="server" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
                </div>
                <br />
                <div style="color: Red; text-align:left">
                    <span><%=Proxy[EnumText.enumForum_Styles_FieldUploadDescription]%></span>                    
                </div>
            </fieldset>
        </div>        
        
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnUpload2" runat="server" CssClass="mbtn" OnClick="btnUpload_Click" /> &nbsp;&nbsp;
            <asp:Button ID="btnCancel2" runat="server"  CssClass="mbtn" CausesValidation="False" OnClick="btnCancel_Click"></asp:Button>  
        </div>
    </div>
</asp:Content>
