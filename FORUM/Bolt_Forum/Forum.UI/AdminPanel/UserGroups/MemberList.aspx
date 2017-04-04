<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MemberList.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.UserGroups.MemberList" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Import Namespace="Com.Comm100.Forum.Bussiness" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script type="text/javascript">
        function checkAll(e, itemName) {
            var aa = document.getElementsByName(itemName);
            for (var i = 0; i < aa.length; i++) {
                aa[i].checked = e.checked;
            }
        }

        function checkItem(e, allName) {
            var all = document.getElementsByName(allName)[0];
            if (!e.checked) all.checked = false;
            else {
                var aa = document.getElementsByName(e.name);
                for (var i = 0; i < aa.length; i++)
                    if (!aa[i].checked) return;
                all.checked = true;
            }
        }

        function selectSingle(itemCheck, selAllId) {
            if (itemCheck.tagName != "INPUT" || itemCheck.type != "checkbox") return;
            if (!itemCheck.checked) {
                document.getElementById(selAllId).checked = false;
            }
            else {
                var tabObject = itemCheck.parentNode.parentNode.parentNode.parentNode;
                var inputArray = tabObject.getElementsByTagName("INPUT");
                for (var i = 0; i < inputArray.length; i++) {
                    if (inputArray[i].type == "checkbox" && inputArray[i].id != selAllId) {
                        if (!inputArray[i].checked) return;
                    }
                }
                document.getElementById(selAllId).checked = true;
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
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
        </div>
        <br />
        <div class="divTable" style="text-align: center;">
            <center>
                <table class="form-table">
                    <tr>
                        <td class="ttd">
                            <asp:Label ID="lblCurrent" runat="server"></asp:Label>
                        </td>
                        <td class="ctd">
                            <asp:Label ID="lblCurrentUserGroup" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <asp:Label ID="lblEmailOrDisplayName" runat="server"></asp:Label>
                        </td>
                        <td class="ctd">
                            <asp:TextBox ID="txtQuery" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTopButton">
            <input type="button" class="lbtn" value="<%=Proxy[EnumText.enumForum_UserGroups_ButtonAddMembers] %>"
                onclick="javascript:showWindow('divThickInner', 'divThickOuter');" />
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" OnClick="btnCancel_Click" />
        </div>
        <br />
        <div class="divTable">
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <%--<th width="10%">
                        <asp:LinkButton ID="lbtnId" runat="server" CommandName="Id" OnClick="Sort" ><%=Proxy[EnumText.enumForum_User_ColumnId]%></asp:LinkButton>
                        <asp:Image ID="imgId" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <th width="140px">
                        <asp:LinkButton ID="lbtnEmail" CommandName="Email" runat="server" OnClick="Sort"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                        <asp:Image ID="imgEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="15%">
                        <asp:LinkButton ID="lbtnDisplayName" runat="server" CommandName="Name" OnClick="Sort"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                        <asp:Image ID="imgName" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnJoinedTime" runat="server" CommandName="JoinTime" OnClick="Sort"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgJoinedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnLastLoginTime" runat="server" CommandName="LastLoginTime"
                            OnClick="Sort"><%=Proxy[EnumText.enumForum_User_ColumnLastJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgLastLoginTime" Visible="true" runat="server" ImageUrl="~/Images/sort_down.gif" />
                    </th>
                    <th width="15%" class="cth">
                        <%=Proxy[EnumText.enumForum_UserGroups_ColumnOperation]%>
                    </th>
                </tr>
                <asp:Repeater ID="rpMembers" runat="server">
                    <%--<AlternatingItemTemplate>
                        <tr class="trStyle2" onmousedown="highLightRow(this)">
                            <td>
                                <%# Eval("id") %>
                            </td>
                            <td>
                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                            </td>
                            <td>
                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td>
                                <%# Convert.ToDateTime(Eval("LastLoginTime")) > OriginalComparisonDate ? Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastLoginTime"))) : ""%>                                
                            </td>
                            <td class="cth">
                                <a href="#" onclick="javascript:window.open('../Users/UserView.aspx?id=<%#Eval("Id")%>&siteid=<%= SiteId %>',null,
                                     'height=600,width=550,status=yes,toolbar=yes,menubar=yes,location=no,scrollbars=yes',
                                     true);"  />
                                     <img alt='<%#Proxy[EnumText.enumForum_User_HelpView]%>' title="<%#Proxy[EnumText.enumForum_User_HelpView]%>" src="../../images/view.gif" /></a>
                            </td>
                            <td class="cth">
                                <a href="memberlist.aspx?siteId=<%=SiteId %>&groupId=<%=UserGroupId %>&action=delete&Id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>">
                                    <img title="<%=Proxy[EnumText.enumForum_UserGroups_HelpDelete] %>" alt="<%=Proxy[EnumText.enumForum_UserGroups_HelpDelete]%>" onclick="return confirm('<%=Proxy[EnumText.enumFourm_UserGroups_ConfirmDeleteMember] %>');"
                                        src="../../images/database_delete.gif" /></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>--%>
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpMembers.Items.Count+1)%2 %>" onmousedown="highLightRow(this)">
                            <%--<td>
                                <%# Eval("id") %>
                            </td>--%>
                            <td>
                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                            </td>
                            <td>
                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td>
                                <%# Convert.ToDateTime(Eval("LastLoginTime")) > OriginalComparisonDate ? Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastLoginTime"))) : ""%>
                            </td>
                            <td class="cth">
                                <a href="#" onclick="javascript:window.open('../Users/UserView.aspx?id=<%#Eval("Id")%>&siteid=<%= SiteId %>',null,
                                     'height=600,width=550,status=yes,toolbar=yes,menubar=yes,location=no,scrollbars=yes',
                                     true);" />
                                <img alt='<%#Proxy[EnumText.enumForum_User_HelpView]%>' title="<%#Proxy[EnumText.enumForum_User_HelpView]%>"
                                    src="../../images/view.gif" /></a> <a href="memberlist.aspx?siteId=<%=SiteId %>&groupId=<%=UserGroupId %>&action=delete&Id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>">
                                        <img title='<%=Proxy[EnumText.enumForum_UserGroups_HelpDelete]%>' alt="<%=Proxy[EnumText.enumForum_UserGroups_HelpDelete]%>"
                                            onclick="return confirm('<%=Proxy[EnumText.enumFourm_UserGroups_ConfirmDeleteMember] %>');"
                                            src="../../images/database_delete.gif" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                OnPaging="aspnetPager_Paging" EnableViewState="true">
            </wcw:ASPNetPager>
        </div>
        <br />
        <div class="divTopButton">
            <input type="button" class="lbtn" value="<%=Proxy[EnumText.enumForum_UserGroups_ButtonAddMembers] %>"
                onclick="javascript:showWindow('divThickInner', 'divThickOuter');" />
            <asp:Button ID="btnCancel" runat="server" CssClass="mbtn" OnClick="btnCancel_Click" />
        </div>
    </div>
    <div id="divScript" runat="server">
    </div>
</asp:Content>
<asp:Content ID="ContentThickBox" ContentPlaceHolderID="cphThickBox" runat="server">
    <%-- <div id="divThickInner" style="position: absolute; display: none; height: 500px;
        width: 700px; border: solid 3px #aaa; padding: 5px 5px; background-color: #fff;
        overflow-y: scroll">
        <div class="divh">
            <div class="divh-header">
            </div>
            <br />
            <div class="divh-table">
            </div>
            <br />
        </div>
    </div>--%>
    <div id="divThickInner" style="position: absolute; height: 565px; width: 700px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div  style="overflow-y: scroll;height: 565px;">
                <div class="divh-header">
                    <div style="float: left; font-size: 12px">
                        <span class="divh-header-spantitle">
                            <%=Proxy[EnumText.enumForum_UserGroups_TitleAddMemgers] %></span>
                    </div>
                </div>
                <br />
                <div>
                    <div class="divContent">
                        <div class="divTopButton">
                            <asp:Button ID="btnQueryUser1" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click" />
                        </div>
                        <br />
                        <div class="divTable" style="text-align: center;">
                            <center>
                                <table class="form-table">
                                    <tr>
                                        <td>
                                            <div class="ttd">
                                                <%=Proxy[EnumText.enumForum_UserGroups_FieldEmailOrDisplayName] %></div>
                                        </td>
                                        <td class="ctd">
                                            <asp:TextBox ID="txtKeyWord" runat="server" CssClass="txtmid"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trUserType" runat="server" visible="false">
                                        <td class="ttd">
                                            <%=Proxy[EnumText.enumForum_UserGroups_FieldUserType] %>
                                        </td>
                                        <td class="ctd">
                                            <asp:DropDownList ID="ddlUserType" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                        <br />
                        <div class="divButtomButton">
                            <asp:Button ID="btnQueryUser2" runat="server" CssClass="mbtn" OnClick="btnQueryUser_Click" />
                        </div>
                    </div>
                    <br />
                    <div class="divContent" >
                        <div class="divTopButton">
                            <asp:Button ID="btnSelectTop" runat="server" OnClick="btnSelect_Click" CssClass="mbtn" />
                            <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');"
                                value='<%=Proxy[EnumText.enumForum_UserGroups_FieldClose]%>' />
                        </div>
                        <br />
                        <div class="divTable" >
                            <table class="the-table" cellpadding='0' cellspacing='0'>
                                <tr>
                                    <th width="5%">
                                        <input type="checkbox" id="selAll" onclick="selectAll(this);" />
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserEmail" CommandName="UserEmail" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                                        <asp:Image ID="imgUserEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserUserType" CommandName="UserUserType" runat="server" OnClick="UserSort"
                                            CausesValidation="false"><%=Proxy[EnumText.enumForum_UserGroups_ColumnUserType]%></asp:LinkButton>
                                        <asp:Image ID="imgUserUserType" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th width="150px">
                                        <asp:LinkButton ID="lbtnUserDisplayName" CommandName="UserDisplayName" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                                        <asp:Image ID="imgUserDisplayName" runat="server" Visible="true" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                    <th>
                                        <asp:LinkButton ID="lbtnUserJoinedTime" CommandName="UserJoinedTime" runat="server"
                                            OnClick="UserSort" CausesValidation="false"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                                        <asp:Image ID="imgUserJoinedTime" Visible="false" runat="server" ImageUrl="~/Images/sort_up.gif" />
                                    </th>
                                </tr>
                                <asp:Repeater ID="rpUser" runat="server">
                                    <AlternatingItemTemplate>
                                        <tr class="trStyle2" onmousedown="highLightRow(this)">
                                            <td>
                                                <input type="checkbox" id="chbSelSingle" runat="server" value='<%#Eval("Id") %>'
                                                    onclick="selectSingle(this, 'selAll');" />
                                            </td>
                                            <%--<td>
                                                    <%# Eval("id") %>
                                                </td>--%>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                                            </td>
                                            <td>
                                                <%#GetItemType((UserOrOperator)Container.DataItem)%>
                                            </td>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                                            </td>
                                            <td>
                                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                                            </td>
                                        </tr>
                                    </AlternatingItemTemplate>
                                    <ItemTemplate>
                                        <tr class="trStyle1" onmousedown="highLightRow(this)">
                                            <td>
                                                <input type="checkbox" id="chbSelSingle" runat="server" value='<%#Eval("Id") %>'
                                                    onclick="selectSingle(this, 'selAll');" />
                                            </td>
                                            <%--<td>
                                                    <%# Eval("id") %>
                                                </td>--%>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                                    <%#Server.HtmlEncode(Convert.ToString(Eval("email")))%></span>
                                            </td>
                                            <td>
                                                <%#GetItemType((UserOrOperator)Container.DataItem)%>
                                            </td>
                                            <td>
                                                <span title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                                            </td>
                                            <td>
                                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </table>
                        </div>
                        <div style="text-align: center;">
                            <wcw:ASPNetPager ID="aspnetPager1" runat="server" OnChangePageSize="aspnetPager1_ChangePageSize"
                                OnPaging="aspnetPager1_Paging" EnableViewState="true" PageSize="10">
                            </wcw:ASPNetPager>
                        </div>
                        <br />
                        <div class="divButtomButton">
                            <asp:Button ID="btnSelectButtom" runat="server" OnClick="btnSelect_Click" CssClass="mbtn" />
                             <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInner','divThickOuter');"
                                value='<%=Proxy[EnumText.enumForum_UserGroups_FieldClose]%>' />
                        </div>
                    </div>
                </div>
                <br />
            </div>
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>
    <div id="divThickOuter" style="position: absolute; filter: alpha(opacity=50); -moz-opacity: 0.5;
        opacity: 0.5; border: 0px; display: none; background-color: #ccc">
    </div>
</asp:Content>
