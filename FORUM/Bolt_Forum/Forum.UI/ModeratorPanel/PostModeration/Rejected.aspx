<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorPanel/ModeratorMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Rejected.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.PostModeration.Rejected" %>
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
    <!-- Query -->
    <div class="divContent">
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
                                <%=Proxy[EnumText.enumForum_Public_FiledSubjectContent] %></div>
                        </td>
                        <td class="ctd">
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
        <br />
        <div class="divTable">
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr id="tbHeader" runat="server">
                    <th>
                        <%=Proxy[EnumText.enumForum_Post_ColumnSubject] %>
                    </th>
                    <th width="150px">
                        <asp:LinkButton ID="lbtnCreateDate" runat="server" CommandName="Create Date" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Post_ColumnCreateDate] %></asp:LinkButton>
                        <asp:Image ID="imgCreateDate" runat="server" Visible="false" EnableViewState="false"
                            />
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnCreateUser" runat="server" CommandName="Create User" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Post_ColumnCreateUser] %></asp:LinkButton>
                        <asp:Image ID="imgCreateUser" runat="server" Visible="false" EnableViewState="false"
                           />
                    </th>
                    <th width="30%">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnForum] %>
                    </th>
                    <th width="100px" style="text-align:center">
                        <%=Proxy[EnumText.enumForum_Topic_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="rpData" runat="server" OnItemCommand="rpData_ItemCommand" OnItemDataBound="rpData_ItemDataBound">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpData.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td>
                                <span class="linkTopic">
                                <a id="lkSubject" runat="server"  target="_blank" title='<%#GetTooltipString(Eval("Subject").ToString())%>'>
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("Subject")), 12))%>
                                </a></span>
                            </td>
                            <td>
                                <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("PostTime"))) %>
                            </td>
                            <td>
                                <a href="../../User_Profile.aspx?siteId=<%=SiteId %>&userid=<%#Eval("PostUserOrOperatorId") %>"
                                    target="_blank"><span style="color: #50b846; font-weight: bold;" title="<%#GetTooltipString(Eval("PostUserOrOperatorName").ToString())%>">
                                        <%#Server.HtmlEncode(Convert.ToString(Eval("PostUserOrOperatorName")))%></span> </a>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="phForumPathOfPost" runat="server"></asp:PlaceHolder>
                            </td>
                            <td style="text-align:center">
                                <asp:ImageButton ID="btnAccept" runat="server" ImageUrl="~/Images/post approve.gif" CommandName="Accept"
                                    CommandArgument='<%#Convert.ToString(Eval("PostId"))%>' ToolTip="<%#Proxy[EnumText.enumForum_Post_HelpReApprove] %>" CausesValidation="False"
                                    OnClientClick="return reapproveConfirm();" />
                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("PostId"))%>'  ToolTip="<%#Proxy[EnumText.enumForum_Post_HelpDelete] %>" 
                                     CausesValidation="False" OnClientClick="return deleteConfirm();" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <script type="text/javascript">
            function reapproveConfirm() {
                return confirm('<%=Proxy[EnumText.enumForum_Post_RejectedPostsReApproveConfirm] %>');
            }
            function deleteConfirm() {
                return confirm('<%=Proxy[EnumText.enumForum_Post_RejectedPostsDeleteConfirm] %>');
            }
        </script>
        <div style="text-align: center; padding-top: 10px">
            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                OnPaging="aspnetPager_Paging" EnableViewState="true">
            </wcw:ASPNetPager>
        </div>
    </div>
</asp:Content>
