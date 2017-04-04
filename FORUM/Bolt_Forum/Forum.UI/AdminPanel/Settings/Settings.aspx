<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.Settings" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function setTRStyle(tabId) {
            //debugger;
            var tabObject = document.getElementById(tabId);
            var trArray = document.getElementsByTagName("TR");
            for (var i = 0; i < trArray.length; i++) {
                if (i % 2 != 0) trArray[i].className = "trStyle2";
                else trArray[i].className = "trStyle1";
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
        <br />
    </div>
    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <br />
    <div class="divContent">
        <br/>
        <div class="divTable">
            <table id="tabLink" class="the-table">
                <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlSiteOption" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                      <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlRegistration" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                  <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlHeaderAndFooter" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                  <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlTemplate" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlGuestUserPermission" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                  <tr id="trUserPermission" runat="server">
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlUserPermission" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                  <tr id="trReputationStrategy" runat="server">
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlReputationStrategy" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr id="trScoreStrategy" runat="server">
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlScoreStrategy" runat="server"></asp:HyperLink>
                    </td>
                </tr>
               <tr>
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlProhibitedWords" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                 <tr id="trHotTopic" runat="server">
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlHotTopic" runat="server"></asp:HyperLink>
                    </td>
                </tr>
                <tr id="trSMTP" runat="server">
                    <td style="height: 20px; font-weight: bold;">
                        <asp:HyperLink ID="hlSMTP" runat="server"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <br/>
    </div>
    <script type="text/javascript">
        setTRStyle("tabLink");
    </script>
</asp:Content>
