<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true" CodeBehind="User_Posts.aspx.cs" Inherits="Com.Comm100.Forum.UI.User_Posts" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/NavigationBar.ascx" TagName="NavigationBar" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divSearchResult" style="margin-bottom: 16px">
        <div class="divResult">
            <div class="divResult divLeft">
                <p>
                    <b>Posts:
                        <%--<%=Proxy[EnumText.enumForum_Topic_TitleSearchResult]%>--%></b>
                    <asp:Label ID="lblResultCount" runat="server" EnableViewState="false"></asp:Label>
                </p>
            </div>
            <div class="buttons" style="float: right; padding-right: 15px;">
            <ul class="buttons_menu">
            <li><asp:LinkButton ID="btnReturnBack" runat="server" OnClick="btnReturnBack_Click" >
                    <span class="move">
                        <%=Proxy[EnumText.enumForum_SearchResult_ButtonReturn]%></span><li class="li_end"></li></asp:LinkButton>
                    </li></ul>
            </div>
            <div style="clear:both; height:0px"></div>
            <div class="errorMsg">
                <asp:Label ID="lblMessage" runat="server" EnableViewState="False"></asp:Label></div>
        </div>
    </div>
    <div class="clear">
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <asp:Label ID="lblForumName2" runat="server" Text="" EnableViewState="false"></asp:Label></div>
                </div>
            </div>
        </div>
        <asp:Repeater ID="rtpData" runat="server" OnItemDataBound="rtpData_ItemDataBound">
            <HeaderTemplate>
                <table class="tb_forum"  cellspacing='0'>
                    <tr>
                        <th width="200px">
                            <%=Proxy[EnumText.enumForum_SearchResult_FiledAuthor]%>
                        </th>
                        <th>
                            <%=Proxy[EnumText.enumForum_SearchResult_FiledMessage]%>
                        </th>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td colspan="2" class="row5">
                            <p>
                                <asp:PlaceHolder ID="phaddCode" runat="server"></asp:PlaceHolder>
                            </p>
                        </td>
                    </tr>
                    <tr class="trOdd">
                        <td class='row1' width="200px" align="center">
                            <p>
                                <a class="user_link" href='User_Profile.aspx?siteId=<%=SiteId%>&userId=<%#Eval("PostUserOrOperatorId")%>'
                                    target="_blank">
                                    <%# Server.HtmlEncode(ReplaceProhibitedWords(Eval("PostUserOrOperatorName").ToString()))%>
                                </a>
                            </p>
                        </td>
                        <td class='row2'>
                            <p>
                                <%=Proxy[EnumText.enumForum_SearchResult_FiledPostSubject]%>
                                <a class="topic_link" href="Topic.aspx?siteId=<%=SiteId%>&forumId=<%#GetForumId(Convert.ToInt32(Eval("TopicId")))%>&topicId=<%#Eval("TopicId")%>&postId=<%#Eval("PostId")%>&GotoPost=true#Post<%#Eval("PostId") %>">
                                    <%# Server.HtmlEncode(ReplaceProhibitedWords(Eval("Subject").ToString()))%></a>
                                <%=Proxy[EnumText.enumForum_SearchResult_FiledPosted]%>
                                <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%>
                            </p>
                            <p>
                                <a class="topic_link" href="Topic.aspx?siteId=<%=SiteId%>&forumId=<%#GetForumId(Convert.ToInt32(Eval("TopicId")))%>&topicId=<%#Eval("TopicId")%>&postId=<%#Eval("PostId")%>&GotoPost=true#Post<%#Eval("PostId") %>">
                                    <%# HtmlReplaceProhibitedWords(Eval("Content").ToString())%>
                                </a>
                            </p>
                        </td>
                    </tr>
                     <tr>
                        <td colspan="2" class="spacer_line">
                        </td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div class="pos_bottom_5">
        <div class="toolbar">
            <div class="divPager" style="text-align: right">
                <cc1:aspnetpager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                    Mode="LinkButton" PageSize="10">
                </cc1:aspnetpager>
            </div>
        </div>
    </div>
    <div class="clear">
    </div>
</asp:Content>
