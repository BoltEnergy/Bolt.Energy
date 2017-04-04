<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MyOutBox.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.MyOutBox" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function showDetail() {
            var messageIds = document.getElementById("<%=hdMessageIds.ClientID %>");
            var ids = new Array();
            ids = messageIds.value.split(";");
            var i = 0;
            for (i = 0; i < ids.length - 1; i++) {
                if (ids[i] != "") {
                    var obj = document.getElementById("trDetail" + ids[i]);
                    if (obj != null) {
                        obj.style.display = "";
                    }
                }
            }
        }
        function changeReceiveUserMore(index) {
            if (document.getElementById("moreReceiveUser" + index).style.display == "none") {
                document.getElementById("moreReceiveUser" + index).style.display = "";
            }
            else {
                document.getElementById("moreReceiveUser" + index).style.display = "none";
            }
        }
        function changeUserGroupMore(index) {
            if (document.getElementById("moreUserGroup" + index).style.display == "none") {
                document.getElementById("moreUserGroup" + index).style.display = "";
            }
            else {
                document.getElementById("moreUserGroup" + index).style.display = "none";
            }
        }
        function changeUserReputationGroupMore(index) {
            if (document.getElementById("moreUserReputationGroup" + index).style.display == "none") {
                document.getElementById("moreUserReputationGroup" + index).style.display = "";
            }
            else {
                document.getElementById("moreUserReputationGroup" + index).style.display = "none";
            }
        }
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_OutBox_DeleteConfirm] %>');
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
                        <%=Proxy[EnumText.enumForum_OutBox_Title]%>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_OutBox_SubTitle]%>')"
                            onmouseout="CloseHelp()" />
                    </div>
                </div>
            </div>
        </div>
        <table class="tb_forum" cellspacing='0'>
            <tr>
                <th width="50%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Message_ColumnSubject] %>
                    </p>
                </th>
                <th width="20%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Message_ColumnSendTime] %>
                    </p>
                </th>
                <th width="23%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Message_ColumnReceiver] %>
                    </p>
                </th>
                <th width="7%">
                    <p>
                        <%=Proxy[EnumText.enumForum_Message_ColumnOperation] %>
                    </p>
                </th>
            </tr>
            <asp:Repeater ID="RepeaterOutMessages" runat="server" OnItemCommand="RepeaterOutMessages_ItemCommand">
                <ItemTemplate>
                    <tr class="trOdd">
                        <td class="row1">
                            <p>
                                <asp:LinkButton ID="lkbtnSubject" runat="server" CommandName="Subject" CommandArgument='<%#Convert.ToString(Eval("Id"))%>'
                                    OnClientClick="showDetail();" ToolTip='<%# GetTooltipString(ReplaceProhibitedWords(Convert.ToString(Eval("Subject"))))%>'>
                        <%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Convert.ToString(Eval("Subject")))),60)%></asp:LinkButton></p>
                        </td>
                        <td class="row1">
                            <p>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("CreateDate")))%></p>
                        </td>
                        <td class="row1">
                            <p>
                                <%# ReceiveUsers(Convert.ToInt32(Eval("Id"))) %>
                            </p>
                            <p>
                                <%# ReceiveGroups(Convert.ToInt32(Eval("Id"))) %></p>
                            <p>
                                <%# ReceiveReputationGroups(Convert.ToInt32(Eval("Id")))%></p>
                        </td>
                        <td class="row2" align="center">
                            <p>
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="../Images/database_delete.gif"
                                    CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("Id"))%>' ToolTip="<%#Proxy[EnumText.enumForum_Message_HelpDelete] %>"
                                    OnClientClick="return deleteConfirm();" />
                            </p>
                        </td>
                    </tr>
                    <tr id="trDetail<%#Convert.ToString(Eval("Id"))%>" style="display: none;">
                        <td colspan="4" class="row5" align="left">
                            <p>
                                <%# System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Convert.ToString(Eval("Message"))))%>
                                &nbsp;
                            </p>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
            <tr>
                <td colspan="4" class="row5" align="center">
                    <p>
                        <div>
                            <cc1:ASPNetPager ID="aspnetPager" runat="server" OnPaging="aspnetPager_Paging" EnableViewState="True"
                                Mode="LinkButton" PageSize="10">
                            </cc1:ASPNetPager>
                        </div>
                    </p>
                </td>
            </tr>
        </table>
    </div>
    <input type="hidden" id="hdMessageIds" runat="server" value="" />

    <script type="text/javascript" language="javascript">
        showDetail();
    </script>

</asp:Content>
