<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="RatifyUser.aspx.cs" Inherits="Forum.UI.AdminPanel.Users.RatifyUser"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function approveConfirm() {
            return confirm("<%=Proxy[EnumText.enumForum_User_ConfirmAccept]%>");
        }
        function refuseConfirm() {
            return confirm("<%=Proxy[EnumText.enumForum_User_ConfirmRefuse]%>");
        }
    </script>
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"> 
        </asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
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
            <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <%--<th style="width: 10%;">
                        <asp:LinkButton ID="lbtnUserId" runat="server" OnClick="repeaterModeration_sorting"><%=Proxy[EnumText.enumForum_User_ColumnId]%></asp:LinkButton>
                        <asp:Image ID="imgId" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <th width="100px">
                        <asp:LinkButton ID="lbtnUserEmail" CommandName="rEmail" runat="server" OnClick="repeaterModeration_sorting"><%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                        <asp:Image ID="imgEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width: 20%;">
                        <asp:LinkButton ID="lbtnUserDisplayName" runat="server" CommandName="Name" OnClick="repeaterModeration_sorting"><%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                        <asp:Image ID="imgName" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width: 20%;">
                        <asp:LinkButton ID="lbtnJoinedTime" runat="server" CommandName="JoinedTime" OnClick="repeaterModeration_sorting"><%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgJoinedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width:15%;" class="cth">
                        <%=Proxy[EnumText.enumForum_User_ColumnOperation] %>
                    </th>
                    <%--<th style="width: 10%;" class="cth">
                        <%=Proxy[EnumText.enumForum_User_ColumnModerate]%>
                    </th>
                    <th style="width: 8%;" class="cth">
                        <%=Proxy[EnumText.enumForum_User_ColumnRefuse]%>
                    </th>--%>
                </tr>
                <asp:Repeater ID="repeaterModeration" runat="server">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.repeaterModeration.Items.Count+1)%2 %>" onmousedown="highLightRow(this)">
                            <%--<td>
                                <%# Eval("id") %>
                            </td>--%>
                            <td>
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("email")), 30))%></span>
                            </td>
                            <td>
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("DisplayName")), 12))%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td class="cth">
                                <a href="RatifyUser.aspx?action=Moderate&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpModerateApprove]%>' title="<%#Proxy[EnumText.enumForum_User_HelpModerateApprove]%>" onclick="return approveConfirm();"
                                        src="../../images/database_accept.gif" /></a>
                            <%--</td>
                            <td class="cth">--%>
                                <a href="RatifyUser.aspx?action=Refuse&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpRefuse]%>' title="<%#Proxy[EnumText.enumForum_User_HelpRefuse]%>" onclick="return refuseConfirm();"
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
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("email").ToString()) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("email"))), 12)%></span>
                            </td>
                            <td>
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td class="cth">
                                <a href="RatifyUser.aspx?action=Moderate&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpModerateApprove]%>' title="<%#Proxy[EnumText.enumForum_User_HelpModerateApprove]%>" onclick="return approveConfirm();"
                                        src="../../images/database_accept.gif" /></a>
                            </td>
                            <td class="cth">
                                <a href="RatifyUser.aspx?action=Refuse&id=<%#Eval("Id") %>&pageindex=<%=aspnetPager.PageIndex%>&pagesize=<%=aspnetPager.PageSize%>&siteid=<%= SiteId %>">
                                    <img alt='<%#Proxy[EnumText.enumForum_User_HelpRefuse]%>' title="<%#Proxy[EnumText.enumForum_User_HelpRefuse]%>" onclick="return refuseConfirm();"
                                        src="../../images/database_delete.gif" /></a>
                            </td>
                        </tr>
                        </ItemTemplat>
                    </AlternatingItemTemplate>--%>
                </asp:Repeater>
            </table>
        </div>
        <div class="divTable">
        </div>
        <div>
            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                EnableViewState="true" OnPaging="aspnetPager_Paging">
            </wcw:ASPNetPager>
        </div>
        <br />
    </div>
</asp:Content>
