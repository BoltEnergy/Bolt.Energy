<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MySendMessage.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.MySendMessage" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../JS/Common/ThickBox.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_SendMessage_Title] %>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_SendMessage_SubTitle] %>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <td class="row1" width="140px" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_FieldSubject] %></b>
                    </p>
                </td>
                <td class="row2" style="width: 75%">
                    <p>
                        <asp:TextBox ID="Subject" runat="server" CssClass="txt" Columns="80"></asp:TextBox>
                        <span class="require">*</span>
                        <asp:RequiredFieldValidator ID="ValidSubjectRequired" runat="server" ControlToValidate="Subject"></asp:RequiredFieldValidator>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_FieldMessage] %></b>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <asp:TextBox ID="Message" runat="server" class="txtnormal" Rows="10" Columns="80"
                            TextMode="MultiLine"></asp:TextBox>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_FieldReceiver] %></b>
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <div runat="server" id="divNew">
                            <p>
                                <input id="tbUsers" readonly="readonly" runat="server" class="txt" type="text" />
                                <asp:Button ID="btnSelectUsers" runat="server" CssClass="btn" OnClientClick="javascript:showWindow('divSelectUsers', 'divThickOuter');return false;"
                                    Height="18px" Width="140px" />
                                <span class="require">*</span>
                                <asp:Label ID="lblSelectUserOrGroupIsNuLL" ForeColor="Red" runat="server" Visible="false"></asp:Label><br />
                                <input id="hdIfAdminGroup" runat="server" type="hidden" value="false" />
                                <input id="hdIfModeratorGroup" runat="server" type="hidden" value="false" />
                                <input id="hdUserIds" runat="server" type="hidden" /></p>
                            <p>
                                <asp:TextBox ID="tbGroups" Visible="false" runat="server" CssClass="txt" ReadOnly="true"></asp:TextBox>
                                <asp:Button ID="btnSelectGroups" Visible="false" runat="server" CssClass="btn" Height="18px"
                                    Width="140px" OnClientClick="javascript:showWindow('divSelectGroups', 'divThickOuter');return false;" />
                                <input id="hdUserGroups" runat="server" type="hidden" />
                                <input id="hdReputationGroups" runat="server" type="hidden" /></p>
                        </div>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row5" colspan='2' align="center">
                    <p>
                        <asp:Button ID="btnSend" runat="server" CssClass="btn" OnClick="btSend_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <%--background--%>
    <%--  <div id="divThickOuter" style="position: absolute; filter: alpha(opacity=50); -moz-opacity: 0.5;
        opacity: 0.5; width: 550px; border: 0px; display: none; background-color: #ccc">
    </div>--%>
</asp:Content>
<asp:Content ID="ThickBox" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <%--select Groups--%>
    <div id="divSelectGroups" style="display: none; width: 630px; height: 400px;">
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
                        <span class="spanHover" onclick="javascript:WindowClose('divSelectGroups','divThickOuter');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <iframe id='iframeSelectGroups' width="580px" height="380px" frameborder='0' scrolling="yes">
                        </iframe>
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
    </div>
    <%--select users--%>
    <div id="divSelectUsers" style="position: absolute; display: none; width: 750px;
        height: 500px;">
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
                        <span class="spanHover" onclick="javascript:WindowClose('divSelectUsers','divThickOuter');">
                            <%=Proxy[EnumText.enumForum_Public_LinkCloseSelectForumWindow]%></span>
                    </div>
                    <div class="boxy-content">
                        <iframe id='iframeSelectUsers' width="700px" height="480px" frameborder='0' scrolling="yes">
                        </iframe>
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
    </div>

    <script type="text/javascript">
        function CheckUserType() {
            var selectUser = document.getElementById("iframeSelectUsers");
            var single = '<%=IfUser()%>';
            var siteId = '<%=pageSiteId %>';
            selectUser.src = "SelectUsers.aspx?ifSingle=" + single + "&siteId=" + siteId;
            var selectGroups = document.getElementById("iframeSelectGroups");
            selectGroups.src = "SelectUserGroups.aspx?siteId=" + siteId;
        }
        CheckUserType();
    </script>

</asp:Content>
