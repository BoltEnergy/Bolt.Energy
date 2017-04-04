<%@ Page Title="Categories" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="CategoryList.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Categories.CategoryList"
    ValidateRequest="false" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>

<%@ Register Assembly="FrameWork" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">

        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
    </script>

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
    <div class="divContent">
        <div class="divTopButton">
            <input type="button" value='<%=Proxy[EnumText.enumForum_Categories_ButtonNew]%>' class="lbtn" onclick="javascript:window.location='CategoryAdd.aspx?siteid=<%= SiteId %>';" />
        </div>
        <br />
        <div class="divTable">
            <table width="100%" class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <%--<th width="5%">
                        <asp:Label ID="lblId" runat="server"></asp:Label>
                    </th>--%>
                    <th width="12%">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </th>
                    <th width="20%">
                        <asp:Label ID="lblDescription" runat="server"></asp:Label>
                    </th>
                    <%--<th class="cth" width="8%">
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                    </th>--%>
                    <%--<th class="cth" width="10%">
                        <asp:Label ID="lblForums" runat="server"></asp:Label>
                    </th>--%>
                    <th class="cth" width="10%" >
                         <asp:Label ID="lblOrder" runat="server"></asp:Label>
                    </th>
                    <th class="cth" width="15%" >
                        <asp:Label ID="lblOperation" runat="server"></asp:Label>
                    </th>
                </tr>
                <asp:Repeater ID="repeaterCategories" runat="server" OnItemCommand="repeaterCategories_ItemCommand"
                    OnItemDataBound="repeaterCategories_ItemDataBound">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.repeaterCategories.Items.Count)%2%>" onmousedown="highLightRow(this);">
                            <%--<td align="center">
                                <%#DataBinder.Eval(Container.DataItem,"CategoryId").ToString()%>
                            </td>--%>
                            <td>
                                <span title="<%# GetTooltipString(Eval("Name").ToString()) %>">
                                <%#System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem,"Name").ToString()) %>
                                    <%--<%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "Name").ToString()), 10)%>--%>
                                </span>
                            </td>
                            <td title='<%# GetTooltipString(Eval("Description").ToString())%>'>
                                <%#System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem,"Description").ToString()) %>
                            </td>
                            <%--<td class="ctd">
                                <%#GetState((Com.Comm100.Framework.Enum.Forum.EnumCategoryStatus)Eval("Status"))%>
                            </td>--%>
                            <%--<td class="ctd">
                                <asp:ImageButton ID="imgbtnForums" CommandName="Forums" CommandArgument='<%#Eval("CategoryId") %>'
                                    ImageUrl="~/Images/categoris_forums.gif" ToolTip='Forums' runat="server" />
                            </td>--%>
                            <td class="ctd">
                                <asp:ImageButton ID="imgbtnUp" CommandName="Up" CommandArgument='<%#Eval("CategoryId") %>'
                                    ImageUrl="~/images/sort_up.gif" ToolTip='<%#Proxy[EnumText.enumForum_Categories_HelpUp]%>' runat="server" />
                                <asp:ImageButton ID="imgbtnDown" CommandName="Down" CommandArgument='<%#Eval("CategoryId") %>'
                                    ImageUrl="~/images/sort_down.gif" ToolTip='<%#Proxy[EnumText.enumForum_Categories_HelpDown]%>' runat="server" />
                            </td>
                            <td class="ctd">
                                <asp:ImageButton ID="imgbtnEdit" CommandName="Edit" CommandArgument='<%#Eval("CategoryId") %>'
                                    ImageUrl="~/images/database_edit.gif" ToolTip='<%#Proxy[EnumText.enumForum_Categories_HelpEdit]%>' runat="server" />
                                &nbsp;
                                <asp:ImageButton ID="imgbtnDelete" CommandName="Delete" CommandArgument='<%#Eval("CategoryId") %>'
                                    ImageUrl="~/images/database_delete.gif" ToolTip='<%#Proxy[EnumText.enumForum_Categories_HelpDelete]%>' runat="server" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <br />
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnNewCategory" runat="server" CssClass="lbtn" OnClick="btnNewCategory_Click" />
        </div>
    </div>
</asp:Content>
