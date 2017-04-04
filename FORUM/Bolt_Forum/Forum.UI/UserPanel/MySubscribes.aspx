<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MySubscribes.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.MySubscribes" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_SubscribesDeleteConfirm] %>');
        }
        
    </script>

    <style type="text/css">
        p.MsoNormal
        {
            margin-top: 0cm;
            margin-right: 0cm;
            margin-bottom: 10.0pt;
            margin-left: 0cm;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: "Calibri" , "sans-serif";
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
    </div>
    <div class="divTableHeader">
        <div class="tbl-h-l">
            <div class="tbl-h-r">
                <div class="tbl-h-c">
                    <div class="divTitleUserPanel" style="float: left; margin-top: 3px">
                        <span style="color: #888; font-size: 12px; margin-top: 5px; float: left;">
                            <%=Proxy[EnumText.enumForum_Topic_SubscribesTitle] %>&nbsp; </span>
                        <div style="margin-top: 3px; float: left;">
                            <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_Topic_SubscribesSubTitle] %>')"
                                onmouseout="CloseHelp()" />
                        </div>
                    </div>
                    <div style="clear: both;">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table class="tableForums" cellpadding='0' cellpadding='0' style="_width: 50%">
        <tr>
            <td class="tdMyLeft" width="40%">
                <p>
                    <%=Proxy[EnumText.enumForum_Topic_FieldKeyWords] %>
                </p>
            </td>
            <td class="tdMyMiddle">
                <p>
                    <asp:TextBox ID="QueryWord" runat="server" Width="160px" CssClass="txtnormal"></asp:TextBox>
                    <asp:Button CssClass="mbtn" ID="btnQuery" runat="server" OnClick="btnQuery_Click" />
                </p>
            </td>
        </tr>
        <tr>
            <td colspan='2' style="border-left: solid 1px #aaa; border-right: solid 1px #aaa;
                padding-left: 5px; text-align: center;">
                <br />
            </td>
        </tr>
    </table>
    <table class="tableForums" cellpadding='0' cellspacing='0' style="_width: 98.9%">
        <tr>
            <td class="tdStatus" width="10%" style="padding-left: 10px; text-align: left">
                <p>
                    <%=Proxy[EnumText.enumForum_Topic_ColumnStatus] %>
                </p>
            </td>
            <td class="tdSubject" width="34%" style="padding-left: 5px; text-align: left">
                <p>
                    <%=Proxy[EnumText.enumForum_Public_Topic] %></p>
            </td>
            <td class="tdDate" width="20%" style="padding-left: 5px; text-align: left">
                <p>
                    <%=Proxy[EnumText.enumForum_Topic_SubscribeDate] %></p>
            </td>
            <td class="tdLastPost" width="16%" style="padding-left: 5px; text-align: left">
                <p>
                    <%=Proxy[EnumText.enumForum_Topic_ColumnLastPost] %></p>
            </td>
            <td class="tdPosts" width="15%">
                <p>
                    <%=Proxy[EnumText.enumForum_Topic_ColumnOperation] %>
                </p>
            </td>
        </tr>
        <asp:Repeater ID="RepeaterMySubscribes" runat="server" OnItemCommand="RepeaterMySubscribes_ItemCommand">
            <ItemTemplate>
                <tr class="<%#((this.RepeaterMySubscribes.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> "
                    onmousemove="changeCSS(this,'trOnMouseOverStyle');" onmouseout="changeCSS(this,'trOdd');">
                    <td style="border-left: solid 1px #aaa; text-align: left; padding-left: 25px; padding-top: 5px;
                        padding-bottom: 5px;">
                        <p>
                            <%# ImageStutas(Convert.ToInt32(Eval("TopicId")),Convert.ToBoolean(Eval("IfClosed")), Convert.ToBoolean(Eval("IfMarkedAsAnswer")), Convert.ToBoolean(Eval("IfParticipant")))%>
                        </p>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px;">
                        <p>
                            <span class="linkTopic"><a href='../Topic.aspx?forumId=<%#Eval("ForumId") %>&topicId=<%#Eval("TopicId")%>&siteId=<%=SiteId%>'
                                target="_blank" title='<%#GetTooltipString(Eval("Subject").ToString()) %>'>
                                <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("Subject"))),20)%></a></span>
                            <br />
                            <%=Proxy[EnumText.enumForum_Topic_FieldBy] %>&nbsp;<a href="../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("PostUserOrOperatorId")%>"
                                target="_blank"><span style="color: #50b846; font-weight: bold;">
                                    <%#Eval("PostUserOrOperatorName")%></span></a>
                        </p>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px">
                        <p>
                            <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("SubscribeDate")))%>
                        </p>
                    </td>
                    <td style="padding-top: 5px; padding-bottom: 5px;">
                        <p>
                            <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("LastPostTime")))%>
                            <br />
                            <%=Proxy[EnumText.enumForum_Topic_FieldBy] %>
                            &nbsp;<a href="../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("LastPostUserOrOperatorId")%>"
                                target="_blank"><span style="color: #50b846; font-weight: bold;">
                                    <%#Eval("LastPostUserOrOperatorName")%></span></a>
                        </p>
                    </td>
                    <td style="border-right: solid 1px #aaa; text-align: center; padding-top: 5px; padding-bottom: 5px;">
                        <p>
                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../Images/database_delete.gif"
                                CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("TopicId"))%>'
                                ToolTip="<%#Proxy[EnumText.enumForum_Topic_HelpDelete] %>" OnClientClick="return deleteConfirm();" />
                        </p>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="5" class="cat-bottom" style="padding-bottom: 5px; padding-left: 10px">
                <p>
                    <div>
                        <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                            Mode="LinkButton" PageSize="10">
                        </cc1:ASPNetPager>
                    </div>
                </p>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="tdButtom">
                <div class="tbl-f-l">
                    <div class="tbl-f-r">
                        <div class="tbl-f-c">
                        </div>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div style="padding-bottom: 5px; padding-top: 10px">
        <b>
            <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
        </b>
    </div>
    <div class="divLegend">
        <table cellpadding="1" cellspacing="1" width='100%'>
            <tr>
                <td width="25%">
                    <img src="../Images/status/noread_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/noread_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/noread_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/participate_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <img src="../Images/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/participate_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/read_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>
                </td>
                <td width="25%">
                    <img src="../Images/status/read_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>
                </td>
            </tr>
            <tr>
                <td width="25%">
                    <img src="../Images/status/read_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>'
                        align="absmiddle" />
                    <%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
