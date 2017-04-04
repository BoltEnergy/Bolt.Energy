<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="AnnouncementList.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Announcement.AnnouncementList" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"> </asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="LabelDescription" runat="server" CssClass="lblSubTitle"> </asp:Label></div>
        
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <!-- Query -->
    <div class="divContent" style="<%=ShowStyle1%>">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery1_Click" />
        </div>
        <br />
        <div class="divTable" style="text-align: center;">
            <center>
                <table class="form-table">
                    <tr>
                        <td>
                            <div class="ttd">
                                <%=Proxy[EnumText.enumForum_Public_FiledSubjectContent] %>
                            </div>
                        </td>
                        <td class="ctd" style="height: 22px">
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" OnClick="btnQuery1_Click" />
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnNewAnnouncement1" runat="server" CssClass="slbtn"  
                onclick="btnNewAnnouncemengt1_Click"/>
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" 
                onclick="btnCancel_Click" />
        </div>
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr id="tbHeader" runat="server">
                    <th width="150px">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnSubject] %>
                    </th>
                   <%-- <th width="75px">
                        <asp:LinkButton ID="lbtnBeginDate" runat="server" CommandName="BeginDate" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnBeginDate] %></asp:LinkButton>
                        <asp:Image ID="imgBeginDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <%--<th width="75px">
                        <asp:LinkButton ID="lbtnExpireDate" runat="server" CommandName="ExpireDate" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnExpireDate] %></asp:LinkButton>
                        <asp:Image ID="imgExpireDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <th width="150px">
                        <asp:LinkButton ID="lbtnPostTime" runat="server" CommandName="PostTime" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnCreateTime] %></asp:LinkButton>
                        <asp:Image ID="imgPostTime" runat="server" Visible="false" EnableViewState="false" />
                    </th>
                    <th width="15%">
                        <asp:LinkButton ID="lbtnCreateUser" runat="server" CommandName="CreateUser" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnCreateUser] %></asp:LinkButton>
                        <asp:Image ID="imgCreateUser" Visible="false" runat="server" EnableViewState="false" />
                    </th>
                    <th width="20%">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnForum] %>
                    </th>
                    <th width="70px" style="text-align:center">
                        <%=Proxy[EnumText.enumForum_Announcement_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="rpAnnoucements" runat="server" OnItemCommand="rpData_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpAnnoucements.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td >
                            <span class="linkTopic"><a href="../../Topic.aspx?siteId=<%=SiteId%>&topicId=<%#Eval("TopicId")%>&forumId= <%# GetForumIdOfAnnouncement(Convert.ToInt32(Eval("TopicId")))%>"
                                    target="_blank" title="<%#GetTooltipString(Eval("Subject").ToString()) %>">
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(Eval("Subject").ToString()),25)%>
                                </a></span>
                                 
                            </td>
                           <%-- <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateFormateWithoutHours(
                                                                        Convert.ToDateTime(Eval("BeginDate")))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateFormateWithoutHours(
                                                                        Convert.ToDateTime(Eval("ExpireDate")))%>
                            </td>--%>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("PostTime")))%>
                            </td>
                            <td>
                                <a href='../Users/UserView.aspx?siteId=<%=SiteId %>&id=<%#Eval("PostUserOrOperatorId") %>'
                                    target="_blank" title="<%# GetTooltipString(Eval("PostUserOrOperatorName").ToString())%>"><span style="color: #50b846; font-weight: bold;" title="<%# GetTooltipString(Eval("PostUserOrOperatorName").ToString())%>">
                                        <%# System.Web.HttpUtility.HtmlEncode(Eval("PostUserOrOperatorName").ToString())%></span></a>
                            </td>
                            <td>
                                <span title="<%#GetTooltipString(GetForumsOfAnnoucementTitle(Convert.ToInt32(Eval("TopicId"))).ToString())%>">
                                <%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(GetForumsOfAnnoucement(Convert.ToInt32(Eval("TopicId"))).ToString(), 20)%>
                                </span>
                            </td>
                            <td style="text-align:center">
                                <a href='EditAnnouncement.aspx?siteId=<%=SiteId%>&Id=<%#Eval("TopicId")%>'>
                                    <img title='<%#Proxy[EnumText.enumForum_Announcement_HelpEdit] %>' alt='<%#Proxy[EnumText.enumForum_Announcement_HelpEdit] %>' src="../../images/database_edit.gif" style="<%=ShowStyle1%>" /></a>
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("TopicId"))%>'
                                    ToolTip="<%#Proxy[EnumText.enumForum_Announcement_HelpDelete] %>" CausesValidation="False" OnClientClick="return deleteConfirm();" />
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
            <asp:Button ID="btnNewAnnouncement2" runat="server" CssClass="slbtn" 
            onclick="btnNewAnnouncemengt1_Click"/>
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" 
                onclick="btnCancel_Click"/>
        </div>
    </div>
    <script type="text/javascript">
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_AnnouncementList_ConfirmDelete] %>');
        }
    </script>
   <%-- <script type="text/javascript">
        if (document.URL.toLowerCase().indexOf('forumid=') >= 0) {
            var os = document.getElementsByTagName('input');
            for (var i = 0; i < os.length; i++) {
                if (os[i].name == "btnCancel") {
                    os[i].style.display = "";
                }
            }
            var oo = document.getElementById("divQueryForm");
            oo.style.display = "none";
        }
    </script>--%>

</asp:Content>
