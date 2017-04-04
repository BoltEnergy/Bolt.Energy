<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="User_Profile.aspx.cs" Inherits="Forum.UI.User_Profile" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server" EnableViewState="false">

    <script src="JS/Common/ThickBox.js" type="text/javascript"></script>

    <style type="text/css">
        .WordBreak
        {
            word-wrap: break-word;
            word-break: break-all;
            overflow: hidden;
            width: 400px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server"
    EnableViewState="false">
    <div>
        <asp:Label ID="lblError" runat="server" CssClass="alertMsg" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_User_TitleUserProfile]%></div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" style="table-layout: auto">
            <tr>
                <th colspan='2' style="text-align:left">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Public_TextAbout]%></b></p>
                </th>
            </tr>
            <tr id="trAvatar" runat="server" enableviewstate="false">
                <td class="row1" width="20%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldAvatar]%></b></p>
                </td>
                <td class="row2" width="80%">
                    <p>
                        <asp:Image ID="imgPicture" runat="server" EnableViewState="false" /></p>
                </td>
            </tr>
            <tr id="trDisplayName" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldDisplayName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblDisplayName" runat="server" EnableViewState="False"></asp:Label>
                        <a runat="server" id="linkSendUser" visible="false" enableviewstate="false">
                            <img src='<%=this.ImagePath + "/sendmessage.gif" %>' alt="Ban User" />
                        </a>
                    </p>
                </td>
            </tr>
            <tr id="trUserName" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldUserName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblUserName" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trAge" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldAge]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblAge" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trGender" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldGender]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblGender" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trOccupation" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldOccupation]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblOccupation" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trCompany" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldCompany]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblCompany" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trPhoneNumber" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldPhoneNumber]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblPhoneNumber" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trFaxNumber" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldFaxNumber]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblFaxNumber" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trEmail" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldEmail]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblEmail" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trInterests" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldInterests]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblInterests" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr id="trHomePage" runat="server" enableviewstate="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldHomePage]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <div class="WordBreak">
                            <asp:Label ID="lblHomePage" runat="server" EnableViewState="False"></asp:Label>
                        </div>
                    </p>
                </td>
            </tr>
            <tr>
                <th colspan='2' style="text-align:left">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Public_TextStatistics]%></b></p>
                </th>
            </tr>
            <tr>
                <td class="row1" align="right" width="15%">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldJoined]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblJoined" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldLastVisit]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblLastVisit" runat="server" EnableViewState="False"></asp:Label></p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldPosts]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblPosts" runat="server" EnableViewState="false"></asp:Label>
                        <asp:LinkButton ID="lbtnUserPosts" Visible="false" EnableViewState="false" runat="server"><%=Proxy[EnumText.enumForum_User_UserProfileLinkUserPosts] %></asp:LinkButton></p>
                </td>
            </tr>
            <tr id="trScore" runat="server">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldScore]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:Label ID="lblScore" runat="server" EnableViewState="false"></asp:Label></p>
                </td>
            </tr>
            <tr id="trReputation" runat="server">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldReputation]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <%--<asp:Label ID="lblRepuation" runat="server" EnableViewState="false"></asp:Label>--%>
                        <asp:PlaceHolder runat="server" ID="PHUserReputations"></asp:PlaceHolder>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <%--Backgroud--%>
    <div id="divThickOuter" class="thick_outer">
    </div>
    <%-- Send Message--%>
    <div id="divSendMessageToUser" style="display: none; width: 640px; height: 320px;">
        <table cellspacing='0' cellpadding='0' border='0' class='boxy-wrapper'>
            <tr>
                <td class='top-left'>
                </td>
                <td class='boxy-top'>
                </td>
                <td class='top-right'>
                </td>
            </tr>
            <tr>
                <td class='left'>
                </td>
                <td class='boxy-inner'>
                    <div class="title-bar">
                        <span class="spanHover" onclick="javascript:WindowClose('divSendMessageToUser','divThickOuter');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_UserProfile_TitleSendMessage]%>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <iframe id='iframeSendMessageToUser' width="600px" height="320px" frameborder='0'
                            scrolling="no" src=""></iframe>
                    </div>
                </td>
                <td class='right'>
                </td>
            </tr>
            <tr>
                <td class='bottom-left'>
                </td>
                <td class='bottom'>
                </td>
                <td class='bottom-right'>
                </td>
            </tr>
        </table>

        <script language="javascript" type="text/javascript">
            function SetSendMessageToUserId(userId) {
                var iframeSendMessageToUser = document.getElementById('iframeSendMessageToUser');
                iframeSendMessageToUser.src = "SendMessages.aspx?userId=" + userId + "&siteId=" + "<%=SiteId%>";
            }
        </script>

    </div>
</asp:Content>
