<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUserGroups.aspx.cs"
    Inherits="Com.Comm100.Forum.UI.UserPanel.SelectUserGroups" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleTemplate_Default/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var lastchooseId = "";

        function forumchoosed(forumId) {

        }

        function checkHideValueAndConfirm() {

        }

        function closeWindow() {
            window.parent.WindowClose('divSelectGroups', 'divThickOuter');
        }

        function ChooseAllRepatation() {
            var cbAllRepuation = document.getElementById("cbAllRepuation");
            var all = document.getElementsByTagName("input");
            for (var i = 0; i < all.length; i++) {
                if (all[i].id.indexOf('r_') >= 0) {
                    all[i].checked = cbAllRepuation.checked;
                }
            }
        }

        function ChooseAllUsers() {
            var cbAllUsers = document.getElementById("cbAllUsers");
            var all = document.getElementsByTagName("input");
            for (var i = 0; i < all.length; i++) {
                if (all[i].id.indexOf('u_') >= 0) {
                    all[i].checked = cbAllUsers.checked;
                }
            }
        }

        function SelectUserGroups() {
            //        var o="aaa";
            //        o.replace(,)
            //            var cbAllRepuation = document.getElementById('cbAllRepuation').
            var all = document.getElementsByTagName("input");
            var GroupText = "";
            var UserGroupIds = "";
            var ReputationIds = "";
            var IfAdminGroup = false;
            var IfModeratorGroup = false;

            for (var i = 0; i < all.length; i++) {
                if (all[i].id.indexOf('a_admin') >= 0) {
                    if (!all[i].checked) { continue; }
                    GroupText += "Administrators;";
                    IfAdminGroup = true;
                }
                else if (all[i].id.indexOf('m_morder') >= 0) {
                    if (!all[i].checked) { continue; }
                    GroupText += "Morderators;";
                    IfModeratorGroup = true;
                }
                else if (all[i].id.indexOf('u_') >= 0) {
                    if (!all[i].checked) { continue; }
                    var text = document.getElementById('span_' + all[i].id).innerHTML;
                    GroupText += text + ";";
                    UserGroupIds += all[i].id.replace('u_', '') + ";";
                }
                else if (all[i].id.indexOf('r_') >= 0) {
                    if (!all[i].checked) { continue; }
                    var text = document.getElementById('span_' + all[i].id).innerHTML;
                    GroupText += text + ";";
                    ReputationIds += all[i].id.replace('r_', '') + ";";
                }
            }
            //             alert(UserGroupIds);
            //             alert(ReputationIds);
            //             alert(GroupText);
            var tbGroups = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_tbGroups');
            var hdUserGroups = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_hdUserGroups');
            var hdReputationGroups = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_hdReputationGroups');
            var hdIfAdminGroup = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_hdIfAdminGroup');
            var hdIfModeratorGroup = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_hdIfModeratorGroup');
            hdUserGroups.value = UserGroupIds;
            hdReputationGroups.value = ReputationIds;
            tbGroups.value = GroupText;
            if (IfAdminGroup)
                hdIfAdminGroup.value = 'true';
            else
                hdIfAdminGroup.value = 'false';
            if (IfModeratorGroup)
                hdIfModeratorGroup.value = 'true';
            else
                hdIfModeratorGroup.value = 'false';
            if (IfAdminGroup == true || IfModeratorGroup == true || UserGroupIds != ""
                 || ReputationIds != "") {
                var error = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_lblSelectUserOrGroupIsNuLL');
                if (error != null)
                    error.style.display = "none";
            }
            closeWindow();
        }
    </script>

</head>
<body style="background:#fff">
    <form id="form1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <div style="float: left">
                            <%=Proxy[EnumText.enumForum_SendMessage_SelectGroupsTitle] %></div>
                        <div style="clear: both">
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellpadding='5px' cellspacing='0' style="width: 560px">
            <tr>
                <td class="row1">
                    <p>
                        <input id="a_admin" type="checkbox" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_GroupItemsAdministrators] %></b></p>
                </td>
            </tr>
            <tr>
                <td class="row1">
                    <p>
                        <input id="m_morder" type="checkbox" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_GroupItemsModerators] %></b></p>
                </td>
            </tr>
            <%--User Groups--%>
            <tr>
                <td class="row1">
                    <p>
                        <input type="checkbox" id="cbAllUsers" onclick="ChooseAllUsers();" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_GroupItemsUserGroups] %></b></p>
                </td>
            </tr>
            <asp:Repeater ID="rptUsersData" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td class="row1" align="right">
                            <p>
                                <input id="u_<%#Eval("UserGroupId")%>" type="checkbox" />
                            </p>
                        </td>
                        <td class="row2">
                            <p>
                                <b><span id="span_u_<%#Eval("UserGroupId")%>" style="padding-left: 20px;">
                                    <%#Server.HtmlEncode(Convert.ToString(Eval("Name")))%></span></b></p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <%--Repuatation Groups--%>
            <tr id="trReputationGroups" runat="server">
                <td class="row1">
                    <p>
                        <input type="checkbox" id="cbAllRepuation" onclick="ChooseAllRepatation();" />
                    </p>
                </td>
                <td class="row2">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_SendMessage_GroupItemsReputationGroups] %></b></p>
                </td>
            </tr>
            <asp:Repeater ID="rptReputationData" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td class="row1" align="right">
                            <p>
                                <input id="r_<%#Eval("GroupId")%>" type="checkbox" />
                            </p>
                        </td>
                        <td class="row2">
                            <p>
                                <b><span id="span_r_<%#Eval("GroupId")%>" style="padding-left: 20px;"><%#Server.HtmlEncode(Convert.ToString(Eval("Name")))%></span></b></p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td class="row5" align="center" colspan='2'>
                    <p>
                        <asp:Button ID="btnSelect" runat="server" OnClientClick="SelectUserGroups();return false;"
                            CssClass="btn" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
