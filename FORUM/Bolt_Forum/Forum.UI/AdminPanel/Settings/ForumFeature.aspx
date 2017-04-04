<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ForumFeature.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.ForumFeature" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    
    <%--<style type="text/css">
        .spanNote
        {
            color:#aaa;
        }
    </style>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" >
    function $(id) { return document.getElementById(id); }

    function contactReputationAndReputationPermission() {
        var reputation = $("<%=chxReputation.ClientID%>");
        var reputationPermission = $("<%=chxReputationPermission.ClientID%>");
        if (reputation.checked == false) {
            reputationPermission.checked = false;
            reputationPermission.disabled = true;
        }
        else {
            reputationPermission.disabled = false;
        }
    }
    </script>
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
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave" />
        </div>
        <div class="divTable">
            <br />
            <table class="form-table" >
                <tr>
                    <%--<td class="ttd" width="10%">
                    </td>--%>
                    <td class="ctd" >
                        <asp:CheckBox ID="chxMessage" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%= Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableMessage]%>
                        </span>
                        </div>
                        <br />
                    </td>
                    <%--<td class="htd" width="80%">
                        
                                    <asp:Image ID="imgViewMessage" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <tr>
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxFavorite" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableFavorite]%></span>
                        </div><br />
                    </td>
                    <%--<td class="htd">
                        
                                    <asp:Image ID="imgViewFavorite" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <%--<tr>
                    <td class="ttd">
                    </td>
                    <td class="ctd">
                        <asp:CheckBox ID="chxSubscribe" runat="server" />
                        <br /><br />
                        <span class="spanNote">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableSubscribe] %><br /><br /></span>
                    </td>
                    <td class="htd">
                                    <asp:Image ID="imgViewSubscribe" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>
                </tr>--%>
                <tr>
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxScore" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableScore]%></span>
                        </div>
                        <br />
                    </td>
                    <%--<td class="htd">
                                    <asp:Image ID="imgViewScore" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <tr >
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxHotTopic" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableHotTopic]%></span>
                        </div>
                        <br />
                    </td>
                    <%--<td class="htd">
                                    <asp:Image ID="imgViewHotTopic" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <tr >
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxReputation" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableReputation]%></span>
                        </div>
                        <br />
                        
                    </td>
                    <%--<td class="htd">
                                    <asp:Image ID="imgViewReputation" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <tr >
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxReputationPermission" runat="server" /><asp:Label ID="lblRP" runat="server"></asp:Label> 
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableReputationPermission]%></span>
                        </div>
                        <br />
                    </td>
                    <%--<td class="htd">
                                    <asp:Image ID="imgViewReputationPermission" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
                <tr >
                    <%--<td class="ttd">
                    </td>--%>
                    <td class="ctd">
                        <asp:CheckBox ID="chxGroupPermission" runat="server" />
                        <br />
                        <div style="padding-left:20px">
                        <span class="helpMsg">
                        <%=Proxy[Com.Comm100.Forum.Language.EnumText.enumForum_Settings_HelpEnableGroupPermission]%></span>
                        </div>
                        <br />
                    </td>
                    <%--<td class="htd">
                                    <asp:Image ID="imgViewGroupPermission" runat="server" Visible="true" 
                                        ImageUrl="~/Images/help.gif" />
                    </td>--%>
                </tr>
            </table>
            <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" OnClick="btnSave" />
        </div>
    </div>
</asp:Content>
