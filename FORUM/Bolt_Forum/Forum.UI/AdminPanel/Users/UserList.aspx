<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="Forum.UI.AdminPanel.Users.UserList"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script type="text/javascript">
        function SetRecipient(recipientId, recipientName) {
            document.getElementById('<%=hidRecipientId.ClientID %>').value = recipientId;
            document.getElementById('txtRecipient').value = recipientName;
        }

        function SetBanUser(banUserId, banUserName) {
            document.getElementById('<%=hidBanUserId.ClientID %>').innerText = banUserId;
            document.getElementById('spanBanUserName').innerHTML = banUserName;
        }
        function activeConfirm(flag) {
            if (flag == "True") {
                return confirm("<%=Proxy[EnumText.enumForum_User_InactiveConfirm] %>");
            }
            else {
                return confirm("<%=Proxy[EnumText.enumForum_User_ActiveConfirm] %>");
            }
        }
        function deleteConfirm() {
            return confirm("<%=Proxy[EnumText.enumForum_User_ConfirmDelete]%>");
        }
        function liftBanConfirm() {
            return confirm("<%=Proxy[EnumText.enumForum_User_LiftBanConfirm] %>");
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" />
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnQueryButton1" runat="server" CssClass="mbtn" OnClick="btnQueryButton_Click">
            </asp:Button>
        </div>
        <br />
        <div class="divTable">
            <center>
                <table class="form-table">
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_QueryEmailOrDisplayName]%>
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
            <asp:Button ID="btnQueryButton2" runat="server" CssClass="mbtn" OnClick="btnQueryButton_Click">
            </asp:Button>
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnNewUser1" runat="server" CssClass="mbtn" OnClick="btnNewUser_Click" />
        </div>
        <br />
        <div class="divTable">
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <%--<th width="8%">
                        <asp:LinkButton ID="lbtnId" runat="server" OnClick="btnSort_click">Id</asp:LinkButton>
                        <asp:Image ID="imgId" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <th width="160px">
                        <asp:LinkButton ID="lbtnEmail" CommandName="rEmail" runat="server" OnClick="btnSort_click"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                        <asp:Image ID="imgEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="18%">
                        <asp:LinkButton ID="lbtnDisplayName" runat="server" CommandName="Name" OnClick="btnSort_click"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                        <asp:Image ID="imgName" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnJoinedTime" runat="server" CommandName="JoinedTime" OnClick="btnSort_click"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgJoinedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnLastLoginTime" runat="server" CommandName="LastLoginTime"
                            OnClick="btnSort_click"><%=Proxy[EnumText.enumForum_User_ColumnLastJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgLastLoginTime" Visible="true" runat="server" ImageUrl="~/Images/sort_down.gif" />
                    </th>
                    <th width="120px" style="<%=IfDisplay%>">
                        <%=Proxy[EnumText.enumForum_User_ColumnIsAdministrator]%>
                    </th>
                    <th width="18%" class="cth">
                        <%=Proxy[EnumText.enumForum_User_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="gdvUserList" runat="server">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.gdvUserList.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <%--<td>
                                <%# Eval("id") %>
                            </td>--%>
                            <td>
                                <span title="<%#GetTooltipString(Eval("email").ToString()) %>">
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("email")),25))%></span>
                            </td>
                            <td>
                                <span title="<%#GetTooltipString(Eval("DisplayName").ToString())%>">
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("DisplayName")), 12))%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td>
                                <%# Convert.ToDateTime(Eval("LastLoginTime")) > OriginalComparisonDate ? Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastLoginTime"))) : ""%>
                            </td>
                            <td class="cth" style="<%=IfDisplay%>">
                                <%#Convert.ToBoolean(Eval("IfForumAdmin")) ? "Yes" : "No" %>
                            </td>
                            <td class="cth">
                                <a href="#" onclick="javascript:window.open('UserView.aspx?id=<%#Eval("Id")%>&siteid=<%= SiteId %>',null,
                                     'height=600,width=550,status=yes,toolbar=yes,menubar=yes,location=no,scrollbars=yes',
                                     true);" title="<%#Proxy[EnumText.enumForum_User_HelpView]%>" />
                                <img alt='<%#Proxy[EnumText.enumForum_User_HelpView]%>' src="../../images/view.gif" /></a>
                                <a href="EditUser.aspx?Id=<%#Eval("Id")%>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                    title="<%#Proxy[EnumText.enumForum_User_HelpEdit] %>">
                                    <img alt="<%#Proxy[EnumText.enumForum_User_HelpEdit] %>" src="../../images/database_edit.gif" /></a>
                                <a href="UserList.aspx?action=<%#Convert.ToBoolean(Eval("IfActive")) ? "Inactive" : "active"%>&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                    title="<%#Convert.ToBoolean(Eval("IfActive")) ? Proxy[EnumText.enumForum_User_HelpInactive] : Proxy[EnumText.enumForum_User_HelpActive]%>">
                                    <img alt="<%#Convert.ToBoolean(Eval("IfActive")) ? Proxy[EnumText.enumForum_User_HelpInactive] : Proxy[EnumText.enumForum_User_HelpActive] %>"
                                        src="<%#Convert.ToBoolean(Eval("IfActive")) ? "../../Images/Inactive_bak.GIF" : "../../Images/Active_bak.GIF" %>"
                                        onclick="return activeConfirm('<%#Convert.ToBoolean(Eval("IfActive"))%>');" /></a>
                                <%# IfUserBanById(Convert.ToInt32(Eval("Id"))) ? "<a href=\"UserList.aspx?action=LiftBan&id=" + Convert.ToString(Eval("Id")) + "&pageindex=" + aspnetPager.PageIndex + "&pagesize=" + aspnetPager.PageSize + "&siteid=" + SiteId + "\" title=\"" + Proxy[EnumText.enumForum_User_HelpLiftBan] + "\"><img alt=\""+Proxy[EnumText.enumForum_User_HelpLiftBan]+"\" src=\"../../Images/Lift-Ban_bak.GIF\"  onclick=\"return liftBanConfirm();\" /></a>" : "<a href=\"#\" title=\""+Proxy[EnumText.enumForum_User_HelpBan]+"\"><img alt=\""+Proxy[EnumText.enumForum_User_HelpBan]+"\" src=\"../../Images/Ban_bak.GIF\"  onclick=\"javascript:showWindow('divThickInnerBan', 'divThickOuter');SetBanUser('" + Convert.ToString(Eval("Id")) + "','" + Convert.ToString(Eval("DisplayName")) + "');\" /></a>"%>
                                <%if (IfEnableMessage)
                                  {%>
                                <a href="#" title="<%#Proxy[EnumText.enumForum_User_HelpSendMessage] %>">
                                    <img alt="<%#Proxy[EnumText.enumForum_User_HelpSendMessage] %>" src="../../Images/sendmessage_bak.gif"
                                        onclick="javascript:showWindow('divThickInnerSendMessage', 'divThickOuter');SetRecipient('<%#Eval("Id") %>','<%#Eval("DisplayName") %>');"
                                        style="cursor: pointer;" /></a>
                                <%}%>
                                <a href="UserList.aspx?action=delete&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                    title="<%#Proxy[EnumText.enumForum_User_HelpDelete]%>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpDelete]%>' onclick="return deleteConfirm();"
                                        src="../../images/database_delete.gif" /></a>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <%--<AlternatingItemTemplate>
                        <tr class="trStyle1" onmousedown="highLightRow(this)">
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
                                <%#Convert.ToBoolean(Eval("IfForumAdmin")) ? "True" : "False" %>
                            </td>
                            <td class="cth">
                                <a href="#" onclick="javascript:window.open('UserView.aspx?id=<%#Eval("Id")%>&siteid=<%= SiteId %>',null,
                                     'height=600,width=550,status=yes,toolbar=yes,menubar=yes,location=no,scrollbars=yes',
                                     true);" title="<%#Proxy[EnumText.enumForum_User_HelpView]%>" />
                                <img alt='<%#Proxy[EnumText.enumForum_User_HelpView]%>' src="../../images/view.gif" /></a>
                                <a href="EditUser.aspx?Id=<%#Eval("Id")%>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                    title="<%#Proxy[EnumText.enumForum_User_HelpEdit] %>">
                                    <img alt="<%#Proxy[EnumText.enumForum_User_HelpEdit] %>" src="../../images/database_edit.gif" /></a>
                                <a href="UserList.aspx?action=<%#Convert.ToBoolean(Eval("IfActive")) ? "Inactive" : "active"%>&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                        title="<%#Convert.ToBoolean(Eval("IfActive")) ? Proxy[EnumText.enumForum_User_HelpInactive] : Proxy[EnumText.enumForum_User_HelpActive]%>">
                                        <img alt="<%#Convert.ToBoolean(Eval("IfActive")) ? Proxy[EnumText.enumForum_User_HelpInactive] : Proxy[EnumText.enumForum_User_HelpActive] %>" src="<%#Convert.ToBoolean(Eval("IfActive")) ? "../../Images/Inactive_bak.GIF" : "../../Images/Active_bak.GIF" %>"
                                            onclick="return activeConfirm('<%#Convert.ToBoolean(Eval("IfActive"))%>');" /></a>
                                <%# IfUserBanById(Convert.ToInt32(Eval("Id"))) ?
                                                                    "<a href=\"UserList.aspx?action=LiftBan&id=" + Convert.ToString(Eval("Id")) + "&pageindex=" + aspnetPager.PageIndex + "&pagesize=" + aspnetPager.PageSize + "&siteid=" + SiteId + "\" title=\"Lift Ban\"><img alt=\"Lift Ban\" src=\"../../Images/Lift-Ban_bak.GIF\"  onclick=\"return confirm('Are you sure to lift ban this user?');\" /></a>" : "<a href=\"#\" title=\"Ban\"><img alt=\"Ban\" src=\"../../Images/Ban_bak.GIF\"  onclick=\"javascript:showWindow('divThickInnerBan', 'divThickOuter');SetBanUser('" + Convert.ToString(Eval("Id")) + "','" + Convert.ToString(Eval("DisplayName")) + "');\" /></a>"%>
                                <%if (IfEnableMessage)
                                  {%>
                                <a href="#" title="Send Message">
                                    <img alt="Send Message" src="../../Images/sendmessage_bak.gif" onclick="javascript:showWindow('divThickInnerSendMessage', 'divThickOuter');SetRecipient('<%#Eval("Id") %>','<%#Eval("DisplayName") %>');"
                                        style="cursor: pointer;" /></a>
                                <%}%>
                                <a href="UserList.aspx?action=delete&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>"
                                    title="<%#Proxy[EnumText.enumForum_User_HelpDelete]%>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpDelete]%>' onclick="return confirm('<%=Proxy[EnumText.enumForum_User_ConfirmDelete]%>');"
                                        src="../../images/database_delete.gif" /></a>
                            </td>
                        </tr>
                    </AlternatingItemTemplate>--%>
                </asp:Repeater>
            </table>
        </div>
        <div>
            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                OnPaging="aspnetPager_Paging" EnableViewState="true">
            </wcw:ASPNetPager>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnNewUser2" runat="server" CssClass="mbtn" OnClick="btnNewUser_Click" />
        </div>
    </div>
    <asp:HiddenField ID="hidBanUserId" runat="server" />
</asp:Content>
<asp:Content ID="ContentThickBox" ContentPlaceHolderID="cphThickBox" runat="server">
    <div id="divThickInnerSendMessage" style="position: absolute; height: 200px; width: 600px;
        display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div class="divh">
                <div class="divh-header">
                    <div style="float: left; font-size: 1.1em;">
                        <span class="divh-header-spantitle">
                            <%=Proxy[EnumText.enumForum_User_TitleSendMessage]%></span>
                    </div>
                </div>
                <br />
                <div class="divh-table">
                    <table class="form-table">
                        <tr>
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_SendMessage_FieldReceiver] %>
                            </td>
                            <td class="ctd">
                                <input id="txtRecipient" type="text" class="txtnormal" disabled="disabled" />
                            </td>
                            <td class="rtd">
                            </td>
                        </tr>
                        <tr>
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_SendMessage_FieldSubject] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtSubject" runat="server" CssClass="txtnormal"></asp:TextBox>
                            </td>
                            <td class="rtd">
                                *
                                <asp:RequiredFieldValidator ID="requiredSubject" runat="server" ControlToValidate="txtSubject"
                                    Display="Dynamic" ValidationGroup="SendMessage"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_SendMessage_FieldMessage]%>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMessage" runat="server" TextMode="MultiLine" CssClass="areanormal"></asp:TextBox>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td class="rtd" colspan="2">
                                <%=Proxy[EnumText.enumForum_UserGroups_FieldRequired] %>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div class="divh-bottom" style="text-align: center">
                    <asp:Button ID="btnSend" runat="server" CssClass="lbtn" ValidationGroup="SendMessage"
                        OnClick="btnSend_Click" />
                    <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInnerSendMessage','divThickOuter');"
                        value='<%=Proxy[EnumText.enumForum_UserGroups_FieldClose]%>' />
                </div>
            </div>
            <asp:HiddenField ID="hidRecipientId" runat="server" />
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>
    <div id="divThickInnerBan" style="position: absolute; height: auto; width: 600px;
        display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div class="divh">
                <div class="divh-header">
                    <div style="float: left; font-size: 1.1em;">
                        <span class="divh-header-spantitle">
                            <%=Proxy[EnumText.enumForum_User_TitleBanUser]%></span>
                    </div>
                   <%-- <div style="float: right;">
                        <span class="divh-header-spanclose">[<a class="linkClose" href="#" onclick="javascript:WindowClose('divThickInnerBan','divThickOuter');">Close</a>]</span>
                    </div>--%>
                </div>
                <br />
                <div class="divh-table">
                    <div class="divh-table">
                        <table class="form-table">
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_User_FieldBanUser] %>
                                </td>
                                <td class="ctd">
                                    <span id="spanBanUserName"></span>
                                </td>
                                <td class="rtd">
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_User_FieldExpireTime]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtExpireTime" runat="server" CssClass="txtshort"></asp:TextBox>
                                    <asp:DropDownList ID="ddlExpireTime" runat="server">
                                    </asp:DropDownList>
                                </td>
                                <td class="rtd">*
                                    <asp:RequiredFieldValidator ID="requiredExpireTime" runat="server" ControlToValidate="txtExpireTime"
                                        Display="Dynamic" ValidationGroup="Ban"></asp:RequiredFieldValidator>
                                    <asp:RangeValidator ID="VaildExpireTime" runat="server" MaximumValue="2147483647"
                                        MinimumValue="-2147483648" Display="Dynamic" ControlToValidate="txtExpireTime"
                                        Type="Integer" ValidationGroup="Ban"></asp:RangeValidator>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_User_FieldNote]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" CssClass="areanormal"></asp:TextBox>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <br />
                <div class="divh-bottom" style="text-align: center">
                    <asp:Button ID="btnBan" runat="server" CssClass="mbtn" ValidationGroup="Ban" OnClick="btnBan_Click" />
                    <input type="button" class="mbtn" onclick="javascript:WindowClose('divThickInnerBan','divThickOuter');"
                        value='<%=Proxy[EnumText.enumForum_UserGroups_FieldClose]%>' />
                </div>
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

    <script language="javascript" type="text/javascript">
        function SelectExprie() {
            //debugger;
            //alert(1);
            var ddl = document.getElementById("<%=ddlExpireTime.ClientID%>");
            var txt = document.getElementById("<%=txtExpireTime.ClientID%>");
            if (ddl.options[0].selected == true) {
                txt.value = "100";
                txt.style.display = "none";
            }
            else {
                txt.value = "";
                txt.style.display = "";
            }
        }
        SelectExprie();
    </script>

</asp:Content>
