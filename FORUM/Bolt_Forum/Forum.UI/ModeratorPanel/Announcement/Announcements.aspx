<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorPanel/ModeratorMasterPage.Master" AutoEventWireup="true" CodeBehind="Announcements.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.Announcement.Announcements" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="clear"></div>
    <!-- Query -->
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
                                <%=Proxy[EnumText.enumForum_Announcement_FieldSubject] %></div>
                        </td>
                        <td class="ctd" style="height: 22px">
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="ttd">
                                <%=Proxy[EnumText.enumForum_Announcement_FieldForum] %></div>
                        </td>
                        <td class="ctd" style="height:"22px">
                            <asp:DropDownList ID="ddlForum" runat="server" CssClass="txtmid"></asp:DropDownList>                        
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
            <asp:Button ID="btnNewAnnouncement1" CssClass="slbtn" runat="server" onclick="btnNewAnnouncement_Click"/>
            <asp:Button ID="btnCancel1" CssClass="lbtn" runat="server" onclick="btnCancel_Click" />  
        </div>
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr id="tbHeader" runat="server">
                    <th>
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnSubject] %>
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnBeginDate" runat="server" CommandName="BeginDate" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnBeginDate] %></asp:LinkButton>
                        <asp:Image ID="imgBeginDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnExpireDate" runat="server" CommandName="ExpireDate" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnExpireDate] %></asp:LinkButton>
                        <asp:Image ID="imgExpireDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnPostTime" runat="server" CommandName="PostTime" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnCreateTime] %></asp:LinkButton>
                        <asp:Image ID="imgPostTime" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="12%">
                        <asp:LinkButton ID="lbtnCreateUser" runat="server" CommandName="CreateUser" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnCreateUser] %></asp:LinkButton>
                        <asp:Image ID="imgCreateUser" Visible="false" runat="server" EnableViewState="false" ImageUrl="~/Images/sort_down.gif" />
                    </th>
                    <th width="20%">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnForum] %>
                    </th>
                    <th width="70px">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="rpAnnoucements" runat="server" OnItemCommand="rpData_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpAnnoucements.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td>
                                <span class="linkTopic"><a href="../../Topic.aspx?siteId=<%=SiteId%>&topicId=<%#Eval("TopicId")%>&forumId=<%#GetForumIdOfAnnouncement(Convert.ToInt32(Eval("TopicId")))%>"
                                    target="_blank">
                                    <%#Eval("Subject")%>
                                </a></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateFormateWithoutHours(
                                                                        Convert.ToDateTime(Eval("BeginDate")))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateFormateWithoutHours(
                                                                        Convert.ToDateTime(Eval("ExpireDate")))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateFormateAsMMDDYYYYHHmmss(
                                                                        Convert.ToDateTime(Eval("PostTime")))%>
                            </td>
                            <td>
                                <a href='../../User_Profile.aspx?siteId=<%=SiteId %>&userid=<%#Eval("PostUserOrOperatorId") %>'
                                    target="_blank"><span style="color: #50b846; font-weight: bold;">
                                        <%# Eval("PostUserOrOperatorName")%></span></a>
                            </td>
                            <td>
                                <%# GetForumsOfAnnoucement(Convert.ToInt32(Eval("TopicId")))%>
                            </td>
                            <td class="ctd">
                                <%--<asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="../../Images/database_edit.gif" CommandArgument='<%#Eval("TopicId") %>' CommandName="Edit"/>--%>
                                <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="../../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("TopicId"))%>'
                                    ToolTip="<%#Proxy[EnumText.enumForum_Announcement_HelpDelete] %>" CausesValidation="False" OnClientClick="javascript:if(!confirm('Are you sure to delete this annoucement?')){return false;};" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="text-align: center; padding-top: 10px">
                <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                    OnPaging="aspnetPager_Paging" EnableViewState="true">
                </wcw:ASPNetPager>
            </div>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnNewAnnouncement2" CssClass="slbtn" runat="server" onclick="btnNewAnnouncement_Click"/>
            <asp:Button ID="btnCancel2" CssClass="lbtn" runat="server" onclick="btnCancel_Click" /> 
        </div>
    </div>
</asp:Content>
