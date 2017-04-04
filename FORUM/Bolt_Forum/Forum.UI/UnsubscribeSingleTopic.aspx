<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true" CodeBehind="UnsubscribeSingleTopic.aspx.cs" Inherits="Com.Comm100.Forum.UI.UnsubscribeSingleTopic" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="alertMsg">
        <asp:Label ID="lblMessage" runat="server" Text="" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
            <div class="divTable">
            <div class="divTableHeader">
                <div class="tbl-h-l">
                    <div class="tbl-h-r">
                        <div class="tbl-h-c">
                            <div class="divTitleCP">
                                <span class="TitleName">
                                   Unsubscribe Topic</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
                <table class="tableForums" cellpadding='0' cellspacing='0'>
                    <tr>
                        <td colspan='2' class="tdInlineInfo">
                            <span class="bordFont">
                                <asp:Label ID="lblSuccess" runat="server" EnableViewState="false"></asp:Label></span>
                            <br />
                            <br />                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="cat-bottom">
                            
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="tdButtom">
                            <div class="tbl-f-l">
                                <div class="tbl-f-r">
                                    <div class="tbl-f-c">
                                    </div>
                                </div>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
</asp:Content>
