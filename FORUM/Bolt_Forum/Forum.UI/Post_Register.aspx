<%@ Page Title="Register" Language="C#" MasterPageFile="~/MainMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Post_Register.aspx.cs" Inherits="Forum.UI.Post_Register" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        #tbSuject
        {
            width: 350px;
        }
        #htmlEditor
        {
            width: 352px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alertMsg">
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Register_TitlePostRegister]%></div>
                </div>
            </div>
        </div>
    </div>
    <div class="divContent">
        <div class="divTable">
            <table class="tb_forum" cellspacing='0'>
                <tr>
                    <td colspan='2' class="row4">
                        <p>
                            <b>
                                <asp:Label ID="LabelSuccess" runat="server" EnableViewState="false"></asp:Label>
                            </b>
                        </p>
                        <br />
                        <p>
                            <b>
                                <asp:Label ID="lblAfterRegister" Text="" runat="server" Visible="true" EnableViewState="false"></asp:Label>
                            </b>
                        </p>
                        <br />
                        <asp:Label ID="lblWait" runat="server" Text="" EnableViewState="false"></asp:Label>
                        <p>
                            <div id="PostRegisterLink" align="center">
                                <asp:HyperLink ID="linkBack" runat="server" EnableViewState="false" visible="false"></asp:HyperLink>
                                <br />
                                <br />
                                <asp:HyperLink ID="linkHomePage" runat="server" EnableViewState="false"></asp:HyperLink>
                                <br />
                                <br />
                                <asp:HyperLink ID="linkUserPanel" runat="server" Visible="false" EnableViewState="false"></asp:HyperLink>
                            </div>
                        </p>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
