<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectUser.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.TopicAndPost.SelectUser" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../css/inner/common.css" rel="stylesheet" type="text/css" />
    <link href="../../css/inner/layout.css" rel="Stylesheet" type="text/css" />
    <link href="../../css/inner/container.css" rel="Stylesheet" type="text/css" />
    <script src="../../JS/Common/Common.js" type="text/javascript"></script>

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

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

        function GetSelectUsers() {
            var name = document.getElementById("hdUserName").value;
            var id = document.getElementById("hdUserId").value
            window.parent.document.getElementById("ctl00_ContentPlaceHolder1_hdUserId").value = id;
            window.parent.document.getElementById("ctl00_ContentPlaceHolder1_txtUser").value = name;
            closeWindow();
        }
        function SelectUser(o, name, id) {

            if (o != null) {
                var lists = document.getElementsByTagName("input");
                for (var i = 0; i < lists.length; i++) {
                    if (lists[i].id == 'radioSelect') {
                        lists[i].checked = false;
                    }
                }
            }
            var hdName = document.getElementById("hdUserName");
            hdName.value = name;
            var hdId = document.getElementById("hdUserId");
            hdId.value = id;
            o.checked = true;
        }
    </script>

    <style type="text/css">
        .style1
        {
            height: 45px;
        }
    </style>
</head>
<body>
    <form runat="server">
        <div id="Content" style="margin-left: 0px;">
            <div class="divMsg">
                <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
            </div>
               <div class="divh-header">
            <div style="float: left; font-size: 10px; font-weight:bold">
                    <asp:Label CssClass="divTitle" ID="lblTitle" runat="server"></asp:Label>
                </div>
                <div style="clear: none">
                </div>
            </div>    
            <br />
            <div class="divContent">
                <div class="divTopButton">
                    <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
                </div>
                <div class="divTable">
                    <table class="form-table" style="width: 100%">
                        <tr>
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_User_QueryEmailOrDisplayName] %>
                            </td>
                            <td class="ctd">
                                <input type="text" runat="server" id="tbDisplayName" class="txtmid" />
                            </td>
                        </tr>
                        <tr id="trUserType" runat="server" visible="false">
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_User_FieldUserType] %>
                            </td>
                            <td class="ctd">
                                <asp:DropDownList ID="ddlUserType" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="divButtomButton">
                    <asp:Button ID="btnQuery2" CssClass="mbtn" runat="server" OnClick="btnQuery_Click" />
                </div>
            </div>
            <br />
            <div class="divContent">
            <div class="divTopButton">
                <asp:Button ID="btnSelect1" runat="server" OnClientClick="javascript:GetSelectUsers();return false;" CssClass="mbtn" />
                  <input type="button" class="mbtn" onclick="closeWindow();" value='<%=Proxy[EnumText.enumForum_Topic_FieldClose] %>' />
            </div>
            <div class="divTable" >
                <table class="the-table" style="width: 100%">
                    <tr id="tbHeader">
                        <th width="5%">
                        </th>
                        <%--<th width="10%">
                            <%=Proxy[EnumText.enumForum_User_ColumnId] %>
                        </th>--%>
                        <th width="20%">
                            <%=Proxy[EnumText.enumForum_User_ColumnEmail] %>
                        </th>
                        <th width="10%" style="text-align: center; padding-right: 10px;">
                            <%=Proxy[EnumText.enumForum_User_ColumnIfDelete] %>
                        </th>
                        <th width="12%" style="text-align: center; padding-right: 10px">
                            <asp:LinkButton ID="lbtnUserType" runat="server" OnClick="btnSort_click">
                            <%=Proxy[EnumText.enumForum_User_ColumnUserType] %></asp:LinkButton>
                            <asp:Image ID="imgUserType" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                        </th>
                        <th width="15%">
                            <%=Proxy[EnumText.enumForum_User_ColumnDisplayName] %>
                        </th>
                        <th width="18%">
                            <asp:LinkButton ID="lbtnJoinedTime" runat="server" OnClick="btnSort_click">
                            <%=Proxy[EnumText.enumForum_User_ColumnJoinedTime] %></asp:LinkButton>
                            <asp:Image ID="imgJoinedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                        </th>
                    </tr>
                    <asp:Repeater ID="rptUsers" runat="server" EnableViewState="false">
                        <ItemTemplate>
                            <tr class="trStyle<%#(this.rptUsers.Items.Count)%2%>" onmousedown="highLightRow(this)">
                                <td>
                                    <input type="radio" id="radioSelect" onclick='SelectUser(this,"<%#Eval("DisplayName") %>","<%#Eval("Id") %>");' />
                                </td>
                                <%--<td class="tdForumNoChoosed">
                                    <%#Eval("Id")%>
                                </td>--%>
                                <td class="tdForumNoChoosed">
                                    <%#Eval("Email")%>
                                </td>
                                <td class="tdForumNoChoosed" style="text-align: center">
                                    <%#Eval("IfDeleted") %>
                                </td>
                                <td class="tdForumNoChoosed" style="text-align: center">
                                    <%#GetItemType((Com.Comm100.Forum.Bussiness.UserOrOperator)Container.DataItem)%>
                                </td>
                                <td class="tdForumNoChoosed" style="padding-left: 10px">
                                    <span id="span_u_<%#Eval("Id")%>">
                                        <%#Server.HtmlEncode(Eval("DisplayName").ToString())%></span>
                                </td>
                                <td class="tdForumNoChoosed" style="padding-left: 10px">
                                    <%--<%#Eval("JoinedTime")%>--%>
                                    <%--<%#Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("JoinedTime"))).ToString("MM-dd-yyyy HH:mm:ss")%>--%>
                                    <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                            Convert.ToDateTime(Eval("JoinedTime")))%>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                    <%--<tr>
                        <td colspan="7" style="padding-bottom: 5px; padding-left: 10px; text-align: center">
                            <cc1:ASPNetPager ID="aspnetPager1" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                                OnPaging="aspnetPager_Paging" EnableViewState="True" Mode="ImageButton" PageSize="30">
                            </cc1:ASPNetPager>
                        </td>
                    </tr>--%>
                   
                </table>
                 <div style="text-align: center; padding-top: 10px">
                    <cc1:ASPNetPager ID="aspnetPager1" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                        OnPaging="aspnetPager_Paging" EnableViewState="True" Mode="ImageButton" PageSize="10">
                    </cc1:ASPNetPager>
                </div>
            </div>
            <div class="divButtomButton">
                <asp:Button ID="btnSelect2" runat="server" OnClientClick="javascript:GetSelectUsers();return false;"
                   CssClass="mbtn" />
                     <input type="button" class="mbtn" onclick="closeWindow();" value='<%=Proxy[EnumText.enumForum_Topic_FieldClose] %>' />
            </div>
        </div>
    
    <input type="hidden" id="hdUserName" value="" />
    <input type="hidden" id="hdUserId" value="-1" />

    <script type="text/javascript">

        var os = document.getElementsByTagName('input');
        for (var i = 0; i < os.length; i++) {
            if (document.URL.toLowerCase().indexOf('type=single') < 0)
                continue;
            if (os[i].type == 'checkbox') {
                //alert();
                os[i].style.display = "none";
                //os[i].innerHTML = "a";
                //os[i].type = 'radio';//<input id="Radio1" type=""
            }
            else if (os[i].type == 'radio') {
                os[i].style.display = "";
            }
        }

    </script>
    </div>
    </form>
</body>
</html>