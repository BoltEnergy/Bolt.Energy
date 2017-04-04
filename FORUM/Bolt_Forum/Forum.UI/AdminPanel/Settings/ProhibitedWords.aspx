<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ProhibitedWords.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.ProhibitedWords" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" ></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label id="lblError" runat="server" CssClass="ErrorLabel" EnableViewState ="false"></asp:Label>
        <asp:Label id="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" onclick="btnSave_Click"  />
            <asp:Button ID="btnReturn1" runat="server" CausesValidation="False" 
                 CssClass="mbtn" onclick="btnCancel_Click"></asp:Button>
        </div>
        <div class="divTable">
        <br \>
            <table class="form-table">
         <tr>
                    <td class="ttd" >
                        <%=Proxy[EnumText.enumForum_ProhibitedWords_FieldEnable] %>
                    </td>
                    <td class="ctd" >                      
                        <asp:CheckBox ID="chbEnable" runat="server" oncheckedchanged="chbEnable_CheckedChanged" AutoPostBack="true" />
                    </td>
                    <td class="rtd"></td>
                </tr>
                <tr>
                    <td class="ttd" >
                        <%=Proxy[EnumText.enumForum_ProhibitedWords_FieldCharacterReplace]%>
                    </td>
                    <td class="ctd" >
                        <asp:TextBox ID="txtReplaceWords" runat="server" CssClass="txtnormal"></asp:TextBox>
                    </td>
                    <td class="rtd"></td>
                </tr>   
                <tr>
                    <td class="ttd" >
                        <%=Proxy[EnumText.enumForum_ProhibitedWords_FieldProhibitedWords] %>
                    </td>
                    <td class="ctd" >
                    <asp:TextBox TextMode="MultiLine" ID="txtProhibitedWords" runat="server" CssClass="areanormal"></asp:TextBox>
                    </td>
                    <td>
                    <%=Proxy[EnumText.enumForum_Settings_HelpProhibitedWords] %>                   
                    </td>
                </tr>          
               
            </table>
        </div>
        <div  class="divButtomButton">            
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" onclick="btnSave_Click"/>
            <asp:Button ID="btnReturn2" runat="server" CausesValidation="False"  onclick="btnCancel_Click" CssClass="mbtn"></asp:Button>
        </div>
        
    </div>
</asp:Content>
