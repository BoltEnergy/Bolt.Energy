<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="UserGroupAdd.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.UserGroups.UserGroupAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function cleanRequireAlert() {
            var RequireAlert = document.getElementById("<%=RequireAlert.ClientID %>");
            RequireAlert.innerText = "";
        }
    </script>
    <div class="divTitle">        
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" ></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server"  OnClick="btnSave_Click" CssClass="mbtn">
            </asp:Button>
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn"  CausesValidation="false" OnClick="btnCancel_Click" />
        </div>
        <div class="divTable">
        <br />
        <table class="form-table">
            <tr>
                <td class="ttd">
                    <asp:Label ID="lblName" runat="server"></asp:Label>:
                </td>
                <td class="ctd">
                    <asp:TextBox ID="txtName" runat="server" CssClass="txtnormal"></asp:TextBox>
                </td>
                <td class="rtd">
                    *
                    <asp:RequiredFieldValidator ID="requiredName" runat="server" Display="Dynamic" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                     <asp:Label ID="RequireAlert" CssClass="rtd" runat="server" ></asp:Label>
                </td>
            </tr>        
            <tr>
                <td class="ttd">
                    <asp:Label ID="lblDescript" runat="server"></asp:Label>:
                </td>
                <td class="ctd">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="txtnormal"></asp:TextBox>
                </td>                   
                <td class="rtd">
                </td>
            </tr>  
    
                <tr>
                    <td>
                    </td>
                    <td colspan="2" class="rtd">
                        <asp:Label ID="lblRequired" runat="server"></asp:Label>
                    </td>
                </tr>
 
        </table>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server"  class="mbtn" OnClick="btnSave_Click" CssClass="mbtn" />
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" CausesValidation="false" OnClick="btnCancel_Click" />
        </div>
    </div>
</asp:Content>
