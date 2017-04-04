<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserPermissionView.aspx.cs"
    Inherits="Com.Comm100.Forum.UI.UserPermissionView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="App_Themes/StyleTemplate_Default/Style.css" rel="stylesheet" type="text/css" />
    <style>
        td
        {
            border: 1px solid green;
        }
    </style>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblError" runat="server" CssClass="alertMsg" EnableViewState="false"></asp:Label>
    <div style="text-align: center; font-size: 20px;">
        <b>user permission list</b><br />
    </div>
    <br />
    <div>
        <center>
            <b>refreash user permission list<br />
            </b>
        </center>
        <center>
            User:<input runat="server" id="txtUserName" type="text" /><br />
            Pass:<input runat="server" id="txtPassword" type="text" />(default password is 1)<br />
            <asp:Button ID="Button1" runat="server" Text="refreash" CssClass="lbtn" OnClick="Button1_Click" />
        </center>
    </div>
    <br />
    <b>user base permission</b>
    <table cellpadding="0" cellspacing="0">
        <%-- <tr>
            <td>
                _ifAdministrator
            </td>
            <td>
                <asp:Label ID="lb_ifAdministrator" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>--%>
        <tr>
            <td>
                _ifAllowCustomizeAvatar
            </td>
            <td>
                <asp:Label ID="lb_ifAllowCustomizeAvatar" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _ifAllowHTMLSignature
            </td>
            <td>
                <asp:Label ID="lb_ifAllowHTMLSignature" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _ifAllowLinkSignature
            </td>
            <td>
                <asp:Label ID="lb_ifAllowLinkSignature" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _ifAllowImageSignature
            </td>
            <td>
                <asp:Label ID="lb_ifAllowImageSignature" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _maxLengthofSignature
            </td>
            <td>
                <asp:Label ID="lb_maxLengthofSignature" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _ifAllowUploadAttachment
            </td>
            <td>
                <asp:Label ID="lb_ifAllowUploadAttachment" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _maxCountOfAttacmentsForOnePost
            </td>
            <td>
                <asp:Label ID="lb_maxCountOfAttacmentsForOnePost" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _maxSizeOfOneAttachment
            </td>
            <td>
                <asp:Label ID="lb_maxSizeOfOneAttachment" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _maxSizeOfAllAttachments
            </td>
            <td>
                <asp:Label ID="lb_maxSizeOfAllAttachments" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _maxCountOfMessageSendOneDay
            </td>
            <td>
                <asp:Label ID="lb_maxCountOfMessageSendOneDay" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _ifAllowSearch
            </td>
            <td>
                <asp:Label ID="lb_ifAllowSearch" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                _minIntervalForSearch
            </td>
            <td>
                <asp:Label ID="lb_minIntervalForSearch" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Allow View Forum
            </td>
            <td>
                <asp:Label ID="lbViewForum" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Allow View Topic/Post
            </td>
            <td>
                <asp:Label ID="lbViewTopicOrPost" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Allow Post Topic/Post
            </td>
            <td>
                <asp:Label ID="lbAllowPostTopicOrPost" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Min Interval Time for Posting
            </td>
            <td>
                <asp:Label ID="lbMinIntervalTimeForPosting" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Max Length of Topic/Post
            </td>
            <td>
                <asp:Label ID="lbMaxLengthOfTopicOrPost" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <%--   <td>
                Allow HTML
            </td>--%>
        <tr>
            <td>
                Allow URL
            </td>
            <td>
                <asp:Label ID="lbAllowURL" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Allow Insert Image
            </td>
            <td>
                <asp:Label ID="lbAllowInsertImage" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                Post Moderation Not Required
            </td>
            <td>
                <asp:Label ID="lbPostModerationNotRequired" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <br />
    <b>user forum permission</b>
    <table class="tableForums" style="text-align: center; border: 1px solid blue;" cellpadding="0"
        cellspacing="0">
        <tr>
            <td>
                Forum Id
            </td>
            <td>
                Forum Name
            </td>
            <%-- <td>
                IfModerator
            </td>--%>
            <td>
                Allow View Forum
            </td>
            <td>
                Allow View Topic/Post
            </td>
            <td>
                Allow Post Topic/Post
            </td>
            <td>
                Min Interval Time for Posting
            </td>
            <td>
                Max Length of Topic/Post
            </td>
            <%--   <td>
                Allow HTML
            </td>--%>
            <td>
                Allow URL
            </td>
            <td>
                Allow Insert Image
            </td>
            <td>
                Post Moderation Not Required
            </td>
        </tr>
        <asp:Repeater ID="rptData" runat="server">
            <ItemTemplate>
                <tr>
                    <td>
                        <%#Eval("ForumId")%>
                    </td>
                    <td>
                        <b>
                            <%# GetForumName(Convert.ToInt32(Eval("ForumId")))%></b>
                    </td>
                    <%-- <td>
                        <%#Eval("IfModerator")%>
                    </td>--%>
                    <td>
                        <%#Eval("IfAllowViewForum")%>
                    </td>
                    <td>
                        <%#Eval("IfAllowViewTopic")%>
                    </td>
                    <td>
                        <%#Eval("IfAllowPost")%>
                    </td>
                    <td>
                        <%#Eval("MinIntervalForPost")%>
                    </td>
                    <td>
                        <%#Eval("MaxLengthOfPost") %>
                    </td>
                    <%-- <td>
                        <%#Eval("IfAllowHTML")%>
                    </td>--%>
                    <td>
                        <%#Eval("IfAllowUrl")%>
                    </td>
                    <td>
                        <%#Eval("IfAllowUploadImage") %>
                    </td>
                    <td>
                        <%#Eval("IfPostNotNeedModeration")%>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
    </table>
    </form>
</body>
</html>
