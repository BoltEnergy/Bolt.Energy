<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUsers.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.SelectUsers" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<%@ register assembly="Framework" namespace="Com.Comm100.Framework.WebControls" tagprefix="cc1" %>
<head runat="server">
    <title></title>
    <link href="../App_Themes/StyleTemplate_Default/Style.css" rel="stylesheet" type="text/css" />

    <script src="../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        var lastchooseId = "";

        function forumchoosed(forumId) {
            window.close();
        }

        function checkHideValueAndConfirm() {

        }

        function closeWindow() {
            window.parent.WindowClose('divSelectUsers', 'divThickOuter');
        }

        function CheckUser(id) {
            var cb = document.getElementById('u_' + id);
            var name = document.getElementById('span_u_' + id).innerHTML;
            var hdUserIds = document.getElementById("hdUserIds");
            var hdUserNames = document.getElementById("hdUserNames");
            if (cb.checked) {
                /*add id and name*/
                hdUserIds.value += id + ";";
                hdUserNames.value += name + ";";
            }
            
            else {
                /*remove id and name*/
                var tIds = ""; 
                var tNames = "";
                var arryids = hdUserIds.value.split(';');
                var arrynames = hdUserNames.value.split(';');
                for (var i = 0; i < arryids.length; i++) {
                    if (arryids[i] != "" && arryids[i] != id) {
                        tIds += arryids[i] + ";";
                    }
                }
                for (var i = 0; i < arrynames.length; i++) {
                    if (arrynames[i] != "" && arrynames[i] != name) {
                        tNames += arrynames[i] + ";";
                    }
                }
                hdUserIds.value = tIds;
                hdUserNames.value = tNames;
            }
            var o = document.getElementById('test');
            o.value = hdUserNames.value;
        }
        function GetSelectUsers() {
            var hdUserIds = document.getElementById("hdUserIds");
            var hdUserNames = document.getElementById("hdUserNames");
            var ids = hdUserIds.value;
            var names = hdUserNames.value;

            window.parent.document.getElementById("ctl00_ContentPlaceHolder1_hdUserIds").value = ids;
            var tbUsers = window.parent.document.getElementById("ctl00_ContentPlaceHolder1_tbUsers");
            tbUsers.value = names;
            if (ids != "") {
                var error = window.parent.document.getElementById('ctl00_ContentPlaceHolder1_lblSelectUserOrGroupIsNuLL');
                if (error != null)
                    error.style.display = "none";
            }
            closeWindow();
        }

        function CheckSigleUser(id, name) {
            var hdUserIds = document.getElementById("hdUserIds");
            var hdUserNames = document.getElementById("hdUserNames");
            hdUserIds.value = id;
            hdUserNames.value = name;
        }
                
    </script>

</head>
<body style="background:#fff">
    <form id="form1" runat="server">
    <div class="cat2">
                            <div class="top_cat2">
                                <div class="top_cat2_left">
                                    <div class="top_cat2_right">
                                        <%=Proxy[EnumText.enumForum_SendMessage_ButtonSelectUsers]%>
                                    </div>
                                </div>
                            </div>
                        </div>
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <%--  <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <div style="float: left">
                            <span class="TitleName">
                                <%=Proxy[EnumText.enumForum_User_SelectUserSearchTitle] %>
                                <span style="font-size: 10px;">&nbsp; </span></span>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <table class="tb_forum" cellspacing='0' width="100%">
            <tr>
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Public_FieldDisplayName]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <input type="text" runat="server" id="tbDisplayName" class="txt" /></p>
                </td>
            </tr>
            <tr id="trUserType" runat="server" visible="false">
                <td class="row1" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_User_FieldUserType] %></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:DropDownList ID="ddlUserType" runat="server">
                        </asp:DropDownList>
                    </p>
                </td>
            </tr>
            <tr align="center">
                <td colspan="2" class="row5" align="center">
                    <p>
                        <asp:Button ID="btnQuery" CssClass="btn" runat="server" OnClick="btnQuery_Click" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div class="pos_bottom_10">
        <%--<div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <div style="float: left">
                            <span class="TitleName">
                                <%=Proxy[EnumText.enumForum_User_SelectUserUsersTitle] %>
                                <span style="font-size: 10px;">&nbsp; </span></span>
                        </div>
                        <div class="clear">
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>
        <table class="tb_forum" cellspacing='0' width="100%">
            <tr>
                <th width="5%">
                </th>
                <th width="150px">
                    <p>
                        <asp:LinkButton ID="lbtnUserDisplayName" CommandName="UserDisplayName" runat="server"
                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                        <asp:Image ID="imgUserDisplayName" runat="server" Visible="true" ImageUrl="~/Images/sort_up.gif" />
                    </p>
                </th>
                <th width="150px">
                    <p>
                        <asp:LinkButton ID="lbtnUserUserType" CommandName="UserUserType" runat="server" OnClick="UserSort"
                            CausesValidation="false"><%=Proxy[EnumText.enumForum_UserGroups_ColumnUserType]%></asp:LinkButton>
                        <asp:Image ID="imgUserUserType" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </p>
                </th>
                <th>
                    <p>
                        <asp:LinkButton ID="lbtnUserJoinedTime" CommandName="UserJoinedTime" runat="server"
                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgUserJoinedTime" Visible="false" runat="server" ImageUrl="~/Images/sort_up.gif" />
                    </p>
                </th>
            </tr>
            <asp:Repeater ID="rptUsers" runat="server" EnableViewState="false">
                <ItemTemplate>
                    <tr>
                        <td class="row1">
                            <p>
                                <%#GetInputHtml(Convert.ToInt32(Eval("Id")), Convert.ToString(Eval("DisplayName")))%>
                            </p>
                        </td>
                        <td class="row1"">
                            <p>
                                <span id="span_u_<%#Eval("Id")%>"><%#Server.HtmlEncode(Convert.ToString(Eval("DisplayName")))%></span>
                            </p>
                        </td>
                        <td class="row1">
                            <p>
                                <%#GetItemType((Com.Comm100.Forum.Bussiness.UserOrOperator)Container.DataItem)%>
                            </p>
                        </td>
                        <td class="row2">
                            <p>
                                <%-- <%#Eval("JoinedTime")%>--%>
                                <%#Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("JoinedTime"))).ToString("MM-dd-yyyy HH:mm:ss")%>
                            </p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="6" class="row5" align="center">
                    <p>
                        <cc1:ASPNetPager ID="aspnetPager1" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                            OnPaging="aspnetPager_Paging" EnableViewState="True" Mode="ImageButton" PageSize="10">
                        </cc1:ASPNetPager>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row5" align="center" colspan="6">
                    <p>
                        <asp:Button ID="btnSelect" runat="server" OnClientClick="javascript:GetSelectUsers();return false;"
                            CssClass="btn" />
                        <input type="hidden" value="" runat="server" id="hdUserIds" />
                        <input type="hidden" value="" runat="server" id="hdUserNames" />
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <div style="display:none">
        <input type="text" class="txt" id="test" runat="server" value="" readonly="readonly" />
    </div>
    <div id="divScript" runat="server">
    </div>

    <script language="javascript" type="text/javascript">
        /*choose Have choosed Users when page Init*/
        var hdUserIds = document.getElementById("hdUserIds");
        var arryids = hdUserIds.value.split(';');
        for (var i = 0; i < arryids.length; i++) {
            if (arryids[i] != "") {
                var o = document.getElementById('u_' + arryids[i]);
                if (o != null)
                    o.checked = true;
            }
        }
    </script>

    </form>
</body>
</html>
