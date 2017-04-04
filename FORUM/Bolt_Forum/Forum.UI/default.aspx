<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="default.aspx.cs" Inherits="Com.Comm100.Forum.UI.Default" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Import Namespace="Com.Comm100.Forum.UI.Common" %>

<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <!DOCTYPE HTML>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
        <meta name="viewport" content="width=device-width, initial-scale=1.0">
        <title>Bolt.Energy</title>
        
        <script src="https://code.jquery.com/jquery-1.9.1.min.js"></script>
    </head>
    <body>
        <div class="pos_bottom_5" style="display: none;">
            <div class="toolbar">
                <div class="reg-btn-outer">                   
                    <span class="addnewtopic" title="<%=Proxy[EnumText.enumForum_Topic_HelpNewTopic]%>">
                        <%=Proxy[EnumText.enumForum_Topic_HelpNewTopic] %></span>
                </div>
                <div id="dvaspnetPagerTop" class="pager" style="display: none;">
                    <cc1:ASPNetPager ID="aspnetPagerTop" runat="server" OnPaging="aspnetPager_Paging"
                        EnableViewState="true" Mode="LinkButton" PageSize="20" ItemsName='sss' ItemName="">
                    </cc1:ASPNetPager>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>

        <div class="pos_bottom_5">
            <div class="toolbar">
                <div class="reg-btn-outer" style="float: left; margin-top: 4px;">
                    <a id="lnkNewTopic1" runat="server" enableviewstate="false">
                        <span class="addnewtopic" title="<%=Proxy[EnumText.enumForum_Topic_HelpNewTopic]%>">
                            <%=Proxy[EnumText.enumForum_Topic_HelpNewTopic] %></span>
                    </a>
                </div>
                <div class="toolbar1">
                    <div class="view_post" style="display: none">
                        <a href="" title="<%=Proxy[EnumText.enumForum_Forum_ButtonAll] %>" onclick="javascript:buttonClick('<%=btnAll.ClientID %>');">
                            <%=Proxy[EnumText.enumForum_Forum_ButtonAll] %></a>
                        <!--change by techtier for hiding the View Featured Topics -->
                        <%--|--%>  <a style="display: none;" href="#" title="<%=Proxy[EnumText.enumForum_Forum_ButtonFeatured] %>"
                            onclick="javascript:buttonClick('<%=btnFeatured.ClientID %>' );">
                            <%=Proxy[EnumText.enumForum_Forum_ButtonFeatured] %></a>
                        <div style="display: none">
                            <asp:Button ID="btnAll" runat="server" CssClass="mbtn" OnClick="btnAll_Click" />
                            <asp:Button ID="btnFeatured" runat="server" CssClass="mbtn" OnClick="btnFeatured_Click" />
                        </div>
                    </div>
                    <%--<asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />--%>
                    <asp:Button ID="btnSearch" runat="server" CssClass="btn" Visible="false" OnClick="btnSearch_Click" />
                    <div class="search_bar" runat="server" id="divSearchBar" style="display: none;">
                        <!------Adding the dropdown for sorting.--------->
                        <asp:Label ID="lbldpName" runat="server">Sort By</asp:Label>
                        <asp:DropDownList ID="ddlForumJump" runat="server" Width="108" OnTextChanged="ddlForumJump_TextChanged" AutoPostBack="True">
                            <asp:ListItem Value="LastPostTime">Date</asp:ListItem>
                            <asp:ListItem Value="NumberOfHits">Most Popular</asp:ListItem>
                        </asp:DropDownList>

                        <%-- <input type="text" id="tbSearch" class="txt" runat="server" onkeyup="SearchOnKeyUp();" />--%>
                    </div>
                    <div class="clear">
                    </div>

                    <script lang="javascript" type="text/javascript">
                        function SearchOnKeyUp() {
                            if (event.keyCode == 13) {
                                document.getElementById('<%=btnSearch.ClientID%>').click();
                                return false;
                            }
                        }

                        function buttonClick(buttonID) {
                            //debugger;       alert(0);
                            var objbtn = document.getElementById(buttonID);
                            if (objbtn != null)
                                objbtn.click();
                        }
                    </script>

                </div>
            </div>

            <div class="divMsg">
                <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
            </div>
            <div class="pos_bottom_10">
                <div class="cat2">
                    <%--hide the default forum  strip--%>
                    <div class="top_cat2" style="display: none;">
                        <div class="top_cat2_left">
                            <div class="top_cat2_right">
                                <asp:Label ID="lblForumName2" runat="server" Text="" EnableViewState="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>

                <table width="100%" class="bolt_topic_list" border="0" cellspacing="0" cellpadding="0">
                    <thead>
                        <tr>
                            <th width="90" align="left" class="bolt_only_dst">Status</th>
                            <th align="left">Topic</th>
                            <th width="80">Posted By</th>
                            <th width="80">Activity
                                <asp:ImageButton ID="ImageButtonLastPost" runat="server" CssClass="sorting-image" ImageUrl="~/Images/up-short-icon.png" OnClick="ImageButtonEvent_Click" />
                            </th>
                            <th width="80" align="center" class="bolt_only_dst">Replies
                                <asp:ImageButton ID="ImageButtonReplies" runat="server" CssClass="sorting-image" ImageUrl="~/Images/up-short-icon.png" OnClick="ImageButtonEvent_Click" />
                            </th>
                            <th width="80" align="center" class="bolt_only_dst">Views
                                <asp:ImageButton ID="ImageButtonNumberofHits" runat="server" CssClass="sorting-image" ImageUrl="~/Images/up-short-icon.png" OnClick="ImageButtonEvent_Click" />
                            </th>
                            <th width="80" align="center" class="bolt_only_dst">Promotion</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <!--repeater for geting the data frm db------->
                            <asp:Repeater ID="RepeaterForum" runat="server" EnableViewState="false" OnItemDataBound="RepeaterForum_ItemDataBound">
                                <ItemTemplate>
                                    <tr >
                                        <td class="bolt_only_dst" align="left">
                                            <p>
                                                <%#InitTopicStatus(Convert.ToBoolean(Eval("IfMoveHistory")), TopicIfRead(Convert.ToInt32(Eval("TopicId"))), Convert.ToBoolean(Eval("IfClosed")), Convert.ToBoolean(Eval("IfMarkedAsAnswer")), Convert.ToBoolean(Eval("IfParticipant")))%>
                                            </p>
                                        </td>
                                        <td class="">
                                            <p>
                                                <span title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("Subject").ToString()).Replace("'","\'"))%>'>
                                                    <a href='<%#Com.Comm100.Forum.UI.Common.WebUtility.GetTopicUrlRewritePath(Eval("Subject").ToString(),Convert.ToInt32(Eval("TopicId")) )%>'>
                                                        <%#TopicIfRead(Convert.ToInt32(Eval("TopicId"))) ? Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "Subject").ToString())), 60) : "<strong>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "Subject").ToString())), 60) + "</strong>"%>
                                                    </a></span>
                                                <%#Convert.ToBoolean(Eval("IfSticky")) ? "<img src=\""+this.ImagePath+"/Status/sticky.gif\" title='" + Proxy[EnumText.enumForum_Topic_StatusSticky] + "' alt='" + Proxy[EnumText.enumForum_Topic_StatusSticky] + "' />" : ""%>
                                                <%#InitTopicState(Container.DataItem as Com.Comm100.Forum.Bussiness.TopicWithPermissionCheck)%>
                                            </p>
                                        </td>
                                        <td class="" align="center">
                                            <div id="dvinitial" runat="server" >                                                
                                                <p id="user_name" runat="server">                                                   
                                                    <%# Convert.ToBoolean(Eval("PostUserOrOperatorIfDeleted")) ? "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString())), 1) + "</span>" : "<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString())) + "'><a>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString().Substring(0,1).ToUpper())), 1) + "</a></span>"%>
                                                </p>
                                            </div>
                                        </td>
                                        <td class="" align="center">
                                            <p>
                                                <%#(Convert.ToInt32(Eval("NumberOfReplies")) == 0) ? "" : "<a class='date_link' href='" + Com.Comm100.Framework.Common.CommonFunctions.URLReplace(Eval("Subject").ToString().Trim()) + "_t" + Eval("TopicId") + ".aspx?siteId=" + SiteId + "&forumId=" + Eval("ForumId") + "&postId=-1&goToPost=true&a=1#bottom'>" + Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("LastPostTime"))) + "</a>"%>
                                            </p>
                                            <p style="display: none;">
                                                <%#(Convert.ToInt32(Eval("NumberOfReplies")) == 0) ? "" : Convert.ToBoolean(Eval("LastPostUserOrOperatorIfDeleted")) ? Proxy[EnumText.enumForum_Topic_FieldBy] + "&nbsp;<span title='" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostUserOrOperatorName").ToString())), 30) + "'>" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString())) + "</span>" : Proxy[EnumText.enumForum_Topic_FieldBy] + "&nbsp;<span title='" + System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("LastPostUserOrOperatorName").ToString())) + "'><a class='user_link' href='User_Profile.aspx?userId=" + Eval("LastPostUserOrOperatorId") + "&Siteid=" + SiteId.ToString() + "' target='_blank'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(DataBinder.Eval(Container.DataItem, "LastPostUserOrOperatorName").ToString())), 30) + "</a></span>"%>
                                            </p>
                                        </td>
                                        <td class="bolt_only_dst" align="center">
                                            <p>
                                                <%#Convert.ToBoolean(Eval("IfMoveHistory")) ? "&nbsp;" : Eval("NumberOfReplies")%>
                                            </p>
                                        </td>
                                        <td class="bolt_only_dst" align="center">
                                            <p>
                                                <%#Convert.ToBoolean(Eval("IfMoveHistory")) ? "&nbsp;" : Eval("NumberOfHits")%>
                                            </p>
                                        </td>
                                        <!--added for promotion-->
                                        <td class="bolt_only_dst" align="center">
                                            <p>
                                                <%#Convert.ToBoolean(Eval("IfMoveHistory")) ? "&nbsp;" : Eval("TotalPromotion")%>
                                            </p>
                                            <%--PromotedVoteValue.ToString()--%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>

                        </tr>

                    </tbody>
                </table>
            </div>
            <div class="clear"></div>
    </body>
    </html>

    <%--OLD FORM HTML--%>
    <div class="pos_bottom_10" style="display: none">
        <div class="forum_head">
            <h2 class="forum_title">
                <asp:Label ID="lblForumName" runat="server" EnableViewState="false"></asp:Label>
            </h2>
            <%--hide part--%>
            <p class="moderators" style="display: none;">
                <%=Proxy[EnumText.enumForum_Topic_FieldModerators]%>
                <asp:Repeater ID="RepeaterModerator" runat="server" OnItemDataBound="RepeaterModerator_ItemDataBound"
                    EnableViewState="false">
                    <ItemTemplate>
                        <span title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("DisplayName").ToString()).Replace("'","\'"))%>'>
                            <a class="user_link" href='User_Profile.aspx?userId=<%#Eval("Id") %>&Siteid=<%= SiteId %>'
                                target="_blank">
                                <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("DisplayName").ToString()))%>
                            </a></span>
                        <asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>
                    </ItemTemplate>
                </asp:Repeater>
            </p>
        </div>
    </div>

    <div class="pos_bottom_5">
        <div class="toolbar">
            <div class="reg-btn-outer">

                <a id="lnkNewTopic2" runat="server" enableviewstate="false">
                    <span class="addnewtopic" title="<%=Proxy[EnumText.enumForum_Topic_HelpNewTopic] %>">
                        <%=Proxy[EnumText.enumForum_Topic_HelpNewTopic] %></span>

                </a>
                <% if (aspnetPager.Visible != false)
                   {  %>
                <div class="pager">
                    <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="true"
                        Mode="LinkButton" PageSize="20" ItemsName="" ItemName="">
                    </cc1:ASPNetPager>
                </div>
                <%} %>
                <div class="clear">
                </div>
            </div>
        </div>
        <br />
        <!-------------------------------Updated on 2/3/2017 bu techtier for showing 3 legend ----------------------------------------------------------->
        <div class="pos_bottom_10" style="display: none;">
            <table class="tb_legend" width="100%" cellspacing="0">
                <tbody>
                    <tr>
                        <td class="cat3">LEGEND
                        </td>
                    </tr>
                    <tr>
                        <td class="row3">
                            <table width="100%" cellspacing="1" cellpadding="1">
                                <tbody>
                                    <tr>
                                        <td width="25%">
                                            <table>
                                                <tr>
                                                    <td width="26" align="center">
                                                        <img src="<%=ImagePath %>/status/read_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>'
                                                            title='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>' align="absmiddle" />
                                                    </td>
                                                    <td>
                                                        <%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="25%">
                                            <table>
                                                <tr>
                                                    <td width="26" align="center">
                                                        <img src="<%=ImagePath %>/status/read_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>'
                                                            title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>' align="absmiddle" />
                                                    </td>
                                                    <td>
                                                        <%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="25%">
                                            <table>
                                                <tr>
                                                    <td width="26" align="center">
                                                        <img src="<%=ImagePath %>/status/read_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>'
                                                            title='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>' align="absmiddle" />
                                                    </td>
                                                    <td>
                                                        <%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="25%">
                                            <table>
                                                <tr>
                                                    <td width="26" align="center">
                                                        <img src="<%=ImagePath %>/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
                                                            title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>' align="absmiddle" />
                                                    </td>
                                                    <td>
                                                        <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <!---Hide the div content for not showing extra content in forum page //Done by techtier -->
        <div class="pos_bottom_10" style="display: none;">
            <table class="tb_legend" cellspacing="0" width="100%">
                <tr>
                    <td class="cat3">
                        <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
                    </td>
                </tr>
                <tr>
                    <td class="row3">
                        <table cellpadding="1" cellspacing="1" width='100%'>
                            <tr>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/noread_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedUnRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/noread_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedUnRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/noread_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalUnRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/participate_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedParticipated]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/participate_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedParticipated]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/participate_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalParticipated]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/read_close.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusClosedRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/read_mark.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusMarkedRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/read_normal.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>'
                                                    title='<%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>' align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusNormalRead]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/announcement-topic.GIF" alt='<%=Proxy[EnumText.enumForum_Forum_ToolTipAnnoucement]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Forum_ToolTipAnnoucement]%>" align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Forum_ToolTipAnnoucement]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/move.gif" alt='<%=Proxy[EnumText.enumForum_Forum_ToolTipMoved]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Forum_ToolTipMoved]%>" align="absmiddle" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Forum_ToolTipMoved]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <div class="line"></div>
                                </td>
                            </tr>
                            <tr>
                                <td width="25%" height="25">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/status/sticky.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusSticky]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Topic_StatusSticky]%>" align="absmiddle" style="margin-left: 3px;" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusSticky]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/featured.gif" alt='<%=Proxy[EnumText.enumForum_Topic_StatusFeaturedTopic]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Topic_StatusFeaturedTopic]%>" align="absmiddle"
                                                    style="margin-left: 3px;" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusFeaturedTopic]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/hot.GIF" alt='<%=Proxy[EnumText.enumForum_Topic_StatusHotTopic]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Topic_StatusHotTopic]%>" align="absmiddle"
                                                    style="margin-left: 3px;" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Topic_StatusHotTopic]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="25%">
                                    <table>
                                        <tr>
                                            <td width="26" align="center">
                                                <img src="<%=ImagePath %>/vote.GIF" alt='<%=Proxy[EnumText.enumForum_Forum_ToolTipVote]%>'
                                                    title="<%=Proxy[EnumText.enumForum_Forum_ToolTipVote]%>" align="absmiddle"
                                                    style="margin-left: 3px;" />
                                            </td>
                                            <td>
                                                <%=Proxy[EnumText.enumForum_Forum_ToolTipVote]%>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <input type="hidden" runat="server" id="currentAspNetPage" />
    </div>
    </div>
</asp:Content>
