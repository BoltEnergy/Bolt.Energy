<%@ Page Title="" Language="C#" MasterPageFile="~/MainMasterPage.Master" AutoEventWireup="true"
    CodeBehind="ForumIsClosed.aspx.cs" Inherits="Com.Comm100.Forum.UI.ForumIsClosed" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="sitePath">
        <a href="Default.aspx?siteId=<%=SiteId%>">
            <%=ReplaceProhibitedWords(SiteName)%>
        </a>
        <%=Proxy[EnumText.enumForum_ForumIsClosed_TitleCurrentForumHasBeenClosed]%>
    </div>
    <div class="divMsg">
        <asp:Label ID="lblMessage" CssClass="errorMsg" runat="server" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_ForumIsClosed_ForumHasBeenClosed]%>
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing="0" width="100%">
            <tr>
                <td class="row1" width="50%" style="text-align: left; padding-left: 100px;">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_ForumIsClosed_FiledCurrentForumHasBeenClosed]%></b>
                    </p>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
