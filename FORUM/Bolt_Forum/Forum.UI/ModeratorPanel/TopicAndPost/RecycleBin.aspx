<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorPanel/ModeratorMasterPage.Master"
    AutoEventWireup="true" CodeBehind="RecycleBin.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.TopicAndPost.RecycleBin" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript" src="../../JS/DatePicker/WdatePicker.js"></script>

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

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
    <!-- Query -->
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery1_Click" />
        </div>
        <br />
        <div class="divTable" style="width: 95%">
            <center>
                <table class="form-table">
                    <tr>
                        <td class="ttd" >
                            <%=Proxy[EnumText.enumForum_Topic_FieldKeyWords] %>
                        </td>
                        <td class="ctd" >
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_Topic_FieldCreateUser] %>
                        </td>
                        <td class="ctd">
                            <input type="text" class="txtmid" runat="server" id="txtUser" />
                            <input type="hidden" runat="server" value="-1" id="hdUserId" />
                            <input type="button" class="lbtn" value="<%=Proxy[EnumText.enumForum_Topic_ButtonSelectUser] %>"
                                onclick="showWindow('divSelectUsers','divThickOuter');return false;" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_Topic_FieldPostData] %>
                        </td>
                        <td class="ctd">
                            <b>
                                <%=Proxy[EnumText.enumForum_Topic_FieldFrom] %></b>
                            <asp:TextBox ID="txtStartTime" runat="server" CssClass="txtmid" Width="100px"></asp:TextBox>
                            <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtStartTime')})"
                                src="../../images/datePicker.gif" align="absmiddle" style="cursor: pointer; width: 16px;
                                height: 20px;" />
                            <b>
                                <%=Proxy[EnumText.enumForum_Topic_FieldTo] %></b>
                            <asp:TextBox ID="txtEndTime" runat="server" CssClass="txtmid" Text="" Width="100px"></asp:TextBox>
                            <img onclick="WdatePicker({el:$dp.$('ctl00_ContentPlaceHolder1_txtEndTime')})" src="../../images/datePicker.gif"
                                align="absmiddle" style="cursor: pointer; width: 16px; height: 20px;" />
                        </td>
                        <td>
                            <asp:RegularExpressionValidator ID="revBeginDate" runat="server" ControlToValidate="txtStartTime"
                                ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)"
                                Display="Dynamic"></asp:RegularExpressionValidator>
                            <asp:RegularExpressionValidator ID="revEndDate" runat="server" ControlToValidate="txtEndTime"
                                ValidationExpression="(^\s*$)|(^\s*(((0?2-29-(19|20)(([02486][048])|([13579][26]))))|((((0?[1-9])|(1[0-2]))-((0?[1-9])|(1\d)|(2[0-8])))|((((0?[13578])|(1[02]))-31)|(((0?[1,3-9])|(1[0-2]))-(29|30))))-((20[0-9][0-9])|(19[0-9][0-9])))\s*$)"
                                Display="Dynamic"></asp:RegularExpressionValidator>
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
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr runat="server" id="tbHeader">
                    <th width="30%">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnSubject] %>
                    </th>
                    <th width="30%">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnForum] %>
                    </th>
                    <th width="22%">
                        <asp:LinkButton ID="lbtnPostDate" runat="server" CommandName="Post Date" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnPostDate] %></asp:LinkButton>
                        <asp:Image ID="imgPostDate" runat="server" Visible="false" EnableViewState="false"
                             />
                    </th>
                    <th width="18%">
                        <asp:LinkButton ID="lbtnPostUser" runat="server" CommandName="Post User" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnCreateUser] %></asp:LinkButton>
                        <asp:Image ID="imgPostUser" runat="server" Visible="false" EnableViewState="false"
                             />
                    </th>
                    <th width="60px" style="text-align: center">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="rptData" runat="server" OnItemCommand="rpData_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rptData.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td>
                                <span class="linkTopic"><a href="../../Topic.aspx?siteId=<%=SiteId%>&postId=<%#Eval("PostId")%>&topicId=<%#Eval("TopicId")%>&forumId=<%#GetForumId(Convert.ToInt32(Eval("TopicId")))%>&type=recycle"
                                    target="_blank" title='<%#Eval("Subject")%>'>
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("Subject")), 25))%>
                                </a></span>
                                <br />
                            </td>
                            <td>
                                <%#Server.HtmlEncode(Convert.ToString(GetForumPathOfPost(Convert.ToInt32(Eval("TopicId")))))%>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("PostTime")))%>
                            </td>
                            <td>
                                <a href="../../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("PostUserOrOperatorId")%>"
                                    target="_blank"><span style="color: #50b846; font-weight: bold;" title="<%#Eval("PostUserOrOperatorName")%>">
                                        <%#Server.HtmlEncode(Convert.ToString(Eval("PostUserOrOperatorName")))%></span></a>
                            </td>
                            <td style="text-align: center">
                                <asp:ImageButton ID="btnResore" runat="server" ImageUrl="../../Images/post restore.gif"
                                    CommandName="Restore" CommandArgument='<%#Convert.ToString(Eval("PostId"))%>'
                                    ToolTip="<%#Proxy[EnumText.enumForum_Topic_HelpRestore] %>" CausesValidation="False"
                                    OnClientClick="return restoreConfirm();" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("PostId"))%>'
                                    ToolTip="<%#Proxy[EnumText.enumForum_Topic_HelpDelete] %>" CausesValidation="False"
                                    OnClientClick="return deleteConfirm();" />
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

        <script type="text/javascript">
            function restoreConfirm() {
                return confirm('<%=Proxy[EnumText.enumForum_Topic_RecycleBinRestoreConfirm] %>');
            }
            function deleteConfirm() {
                return confirm('<%=Proxy[EnumText.enumForum_Topic_RecycleBinDeleteConfirm] %>');
            }
        </script>

    </div>
</asp:Content>
<asp:Content ID="ContentThickBox" ContentPlaceHolderID="cphThickBox" runat="server">
    <%-- <div id="" style="position: absolute; display: none; ">
        <div class="divContent" style="border: solid 3px #aaa; padding: 10px 5px 10px 5px;
            width: 720px; height: 560px;">
            <div style="text-align: right">
                <a href="javascript:WindowClose('divSelectUsers','divThickOuter');"><b>[<%=Proxy[EnumText.enumForum_Topic_FieldClose] %>]</b></a>
            </div>
           
        </div>
    </div>--%>
    <div id="divSelectUsers" style="position: absolute; width: 740px;
        height: 550px;display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div class="divh">
                <div class="divh-table">
                     <iframe id='iframeSelectUsers' width="700px" height="550px" frameborder='0' scrolling="yes"
                        src="SelectUser.aspx?siteId=<%=SiteId%>"></iframe>
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
