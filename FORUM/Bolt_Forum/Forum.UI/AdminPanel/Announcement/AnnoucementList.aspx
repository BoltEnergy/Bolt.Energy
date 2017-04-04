<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="AnnoucementList.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Annoucement.AnnoucementList" %>

<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel">Announcements</asp:Label></div>
    <div class="divSubTitle">
        Announcements are the important information you want to publish in your Category
        and Forum. You can create a new announcement as well as view and edit the existing
        announcements.
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <!-- Query -->
    <div class="divContent" style="<%=ShowStyle1%>">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" Text="Query" runat="server" CssClass="mbtn" OnClick="btnQuery1_Click" />
        </div>
        <br />
        <div class="divTable" style="text-align: center;">
            <center>
                <table class="form-table">
                    <tr>
                        <td>
                            <div class="ttd">
                                Subject:</div>
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
            <asp:Button ID="btnQuery2" Text="Query" runat="server" CssClass="mbtn" OnClick="btnQuery1_Click" />
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTopButton">
            <input type="button" value="New Announcement" class="slbtn" 
            onclick="window.location='CreateAnnoucement.aspx?siteId=<%=SiteId%>';" 
            style="<%=ShowStyle1%>"/>
            <input type="button" value="Cancel" class="lbtn" onclick="javascript:window.location='../Forums/ForumList.aspx'"
                name='btnCancel' style="<%=ShowStyle2%>"/>
        </div>
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr id="tbHeader" runat="server">
                    <th>
                        Subject
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnBeginDate" runat="server" CommandName="BeginDate" OnCommand="btnSort_click">Begin Date</asp:LinkButton>
                        <asp:Image ID="imgBeginDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnExpireDate" runat="server" CommandName="ExpireDate" OnCommand="btnSort_click">Expire<br/> Date</asp:LinkButton>
                        <asp:Image ID="imgExpireDate" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="75px">
                        <asp:LinkButton ID="lbtnPostTime" runat="server" CommandName="PostTime" OnCommand="btnSort_click">Create<br/> Time</asp:LinkButton>
                        <asp:Image ID="imgPostTime" runat="server" Visible="false" EnableViewState="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th width="12%">
                        <asp:LinkButton ID="lbtnCreateUser" runat="server" CommandName="CreateUser" OnCommand="btnSort_click">Create User</asp:LinkButton>
                        <asp:Image ID="imgCreateUser" Visible="false" runat="server" EnableViewState="false" ImageUrl="~/Images/sort_down.gif" />
                    </th>
                    <th width="20%">
                        Forum
                    </th>
                    <th width="70px">
                        Operation
                    </th>
                </tr>
                <asp:Repeater ID="rpAnnoucements" runat="server" OnItemCommand="rpData_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpAnnoucements.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td>
                                <%#Eval("Subject")%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("BeginDate")))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("ExpireDate")))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
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
                            <td>
                                <a href='EditAnnoucement.aspx?siteId=<%=SiteId%>&Id=<%#Eval("TopicId")%>'>
                                    <img alt='Edit' src="../../images/database_edit.gif" style="<%=ShowStyle1%>" /></a>
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("TopicId"))%>'
                                    ToolTip="Delete" CausesValidation="False" OnClientClick="javascript:if(!confirm('Are you sure to delete this annoucement?')){return false;};" />
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
            <input type="button" value="New Announcement" class="slbtn" 
            onclick="window.location='CreateAnnoucement.aspx?siteId=<%=SiteId%>';"
            style="<%=ShowStyle1%>" />
            <input type="button" value="Cancel" class="lbtn" onclick="javascript:window.location='../Forums/ForumList.aspx'"
                name='btnCancel' style="<%=ShowStyle2%>" />
        </div>
    </div>

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
