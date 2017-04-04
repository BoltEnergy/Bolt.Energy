<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserMyPosts.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.UserMyPosts" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="errorMsg" EnableViewState="False"></asp:Label>
    </div>
    <div class="pos_bottom_10">
        <div class="cat2">
            <div class="top_cat2">
                <div class="top_cat2_left">
                    <div class="top_cat2_right">
                        <%=Proxy[EnumText.enumForum_Topic_TitleMyPosts]%>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_Topic_HelpMyPosts]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum"  cellspacing='0' >
            <tr>
                <td class="row2" width="40%" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Public_FiledSubjectContent]%></b></p>
                </td>
                <td class="row2" width="60%">
                    <p>
                        <input type="text" id="tbKeywords" runat="server" class="txt" /></p>
                </td>
            </tr>
            <tr>
                <td class="row2" align="right">
                    <p>
                        <b>
                            <%=Proxy[EnumText.enumForum_Topic_FieldPostData]%></b></p>
                </td>
                <td class="row2">
                    <p>
                        <asp:DropDownList ID="ddlPostTime" runat="server">
                        </asp:DropDownList>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row5" align="center" colspan="2">
                    <p>
                        <asp:Button ID="btnQuery" runat="server" CssClass="btn" OnClick="btnQuery_Click" />
                    </p>
                </td>
            </tr>
        </table>
        <table class="tb_forum" cellspacing='0' style="border-top:none" >
            <tr>
                <th width="50%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnSubject]%>
                    </p>
                </th>
                <th width="200px">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnDate]%>
                    </p>
                </th>
                <th>
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_ColumnForum]%>
                    </p>
                </th>
            </tr>
            <asp:Repeater ID="RepeaterMyPosts" runat="server" EnableViewState="false" OnItemDataBound="RepeaterMyPosts_ItemDataBound">
                <ItemTemplate>
                    <tr class="<%#((this.RepeaterMyPosts.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> "
                        onmousemove="changeCSS(this,'trOnMouseOverStyle');" onmouseout="changeCSS(this,'<%#((this.RepeaterMyPosts.Items.Count+1)%2 == 0)?"trEven":"trOdd"%> ');">
                        <td class="row1">
                            <p>
                                <span class="linkTopic">
                                    <asp:HyperLink ID="linkPost" Target="_blank" runat="server"></asp:HyperLink>
                                </span>
                            </p>
                        </td>
                        <td class="row1">
                            <p>
                                <%#(Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString( Convert.ToDateTime(Eval("PostTime"))))%>
                            </p>
                        </td>
                        <td class="row2">
                            <p>
                                <asp:Label ID="lbFroumPath" runat="server" />
                            </p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="3" class="row5" align="center" style="padding-bottom: 5px; padding-left: 10px">                    
                    <div>
                        <cc1:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                            OnPaging="aspnetPager_Paging" EnableViewState="true" Mode="LinkButton" PageSize="10">
                        </cc1:ASPNetPager>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
