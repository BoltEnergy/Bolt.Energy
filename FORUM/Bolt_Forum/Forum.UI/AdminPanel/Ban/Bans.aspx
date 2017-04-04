<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Bans.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Ban.Bans" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel"></asp:Label>
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
                        <td>
                            <div class="ttd">
                                <%=Proxy[EnumText.enumForum_Ban_FieldBanUserIP]%></div>
                        </td>
                        <td class="ctd">
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtmid"></asp:TextBox>
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
            <asp:Button ID="btnBanUser1" runat="server" CssClass="mbtn" OnClick="btnBanUser_Click"/>
            <asp:Button ID="btnBanIP1" runat="server" CssClass="mbtn" OnClick="btnBanIP_Click" />
        </div>
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <th width="20%">
                        <%=Proxy[EnumText.enumForum_Ban_ColumnBanUserIP] %>
                    </th>
                    <th width="15%">
                        <asp:LinkButton ID="lbtnBeginTime" CommandName="BeginTime" runat="server" OnClick="lbtnSort_click"><%=Proxy[EnumText.enumForum_Ban_ColumnBeginTime]%></asp:LinkButton>
                        <asp:Image ID="imgBeginTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="18%">
                        <asp:LinkButton ID="lbtnUnbannedTime" runat="server" CommandName="UnbannedTime" OnClick="lbtnSort_click"><%=Proxy[EnumText.enumForum_Ban_ColumnUnbannedTime] %></asp:LinkButton>
                        <asp:Image ID="imgUnbannedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="12%">
                        <asp:LinkButton ID="lbtnOperator" runat="server" CommandName="Operator" OnClick="lbtnSort_click"><%=Proxy[EnumText.enumForum_Ban_ColumnOperator] %></asp:LinkButton>
                        <asp:Image ID="imgOperator" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="20%">
                        <%=Proxy[EnumText.enumForum_Ban_ColumnNotes] %>
                    </th>
                    <th width="15%" class="cth">
                        <%=Proxy[EnumText.enumForum_Ban_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="repeaterBans" runat="server" OnItemCommand="repeaterBans_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.repeaterBans.Items.Count)%2%>" onmousedown="highLightRow(this);">
                            <td>
                                <span title="<%#GetTooltipString(Eval("UserOrIp").ToString())%>">
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("UserOrIp").ToString(),20))%>
                                </span>
                            </td>
                            <td>
                                <span title='<%#GetTooltipString(Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("BanStartDate"))).ToString("MM-dd-yyyy HH:mm:ss")) %>'>
                                    <%#Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("BanStartDate"))).ToString("MM-dd-yyyy HH:mm:ss")%></span>
                            </td>
                            <td>
                                <span title='<%#GetTooltipString(Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("BanEndDate"))).ToString("MM-dd-yyyy HH:mm:ss")) %>'>
                                    <%#Com.Comm100.Framework.Common.DateTimeHelper.UTCToLocal(Convert.ToDateTime(Eval("BanEndDate"))).ToString("MM-dd-yyyy HH:mm:ss")%></span>
                            </td>
                            <td>
                                <a href="../Users/UserView.aspx?siteId=<%=SiteId %>&id=<%#Eval("OperatedUserOrOperatorId") %>"
                                    target="_blank"><span style="color: #50b846; font-weight: bold;" title='<%#GetTooltipString(this.GetOperator(Convert.ToInt32(Eval("OperatedUserOrOperatorId")))) %>'>
                                        <%#this.GetOperator(Convert.ToInt32(Eval("OperatedUserOrOperatorId"))) %></span>
                                </a>
                            </td>
                            <td>
                                <%#Server.HtmlEncode(Convert.ToString(Eval("Note")).ToString()) %>
                            </td>
                            <td class="cth">
                                <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CommandArgument='<%#Eval("Id") %>'
                                    ImageUrl="~/images/database_edit.gif" ToolTip="<%#Proxy[EnumText.enumForum_Ban_HelpEdit] %>"
                                    runat="server" />
                                &nbsp;
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("Id") %>'
                                    OnClientClick="return deleteConfirm();" ImageUrl="~/images/Lift-Ban_bak.GIF"
                                    ToolTip="<%#Proxy[EnumText.enumForum_User_HelpLiftBan] %>" runat="server" />
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
        <div class="divButtomButton">
            <asp:Button ID="btnBanUser2" runat="server" CssClass="mbtn" OnClick="btnBanUser_Click" />
            <asp:Button ID="btnBanIP2" runat="server" CssClass="mbtn" OnClick="btnBanIP_Click" />
        </div>
    </div>

    <script type="text/javascript">
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Ban_ConfirmUnBan] %>');
        }
    </script>

</asp:Content>
