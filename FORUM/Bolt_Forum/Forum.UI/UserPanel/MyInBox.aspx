<%@ Page Title="" Language="C#" MasterPageFile="~/UserPanel/UserMasterPage.Master"
    AutoEventWireup="true" CodeBehind="MyInBox.aspx.cs" Inherits="Com.Comm100.Forum.UI.UserPanel.MyInbox" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript">
        function showDetail() {
            var Hidden = document.getElementById('<%=hdMessageIds.ClientID%>');
            var ids = new Array();
            ids = Hidden.value.split(";");
            var i;
            for (i = 0; i < ids.length - 1; i++) {
                if (ids[i] != "") {
                    var message = document.getElementById("trDetail" + ids[i]);
                    if (message != null)
                        message.style.display = "";
                }
            }
        }
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_InBox_DeleteConfirm] %>');
        }
    </script>

    <style type="text/css">
        .weightFont
        {
            font-weight: bold;
        }
        .normalFont
        {
            font-weight: normal;
        }
    </style>
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
                       
                        <%=Proxy[EnumText.enumForum_InBox_Title] %>
                        <img src="../Images/help.gif" onmouseover="ShowHelp('<%=Proxy[EnumText.enumForum_InBox_SubTitle] %>')"
                            onmouseout="CloseHelp()" />
                        
                    </div>
                </div>
            </div>
        </div>
    
    <input id="hdMessageIds" type="hidden" value="" runat="server" />
    <table class="tb_forum" cellspacing='0' >
        <tr>
            <th width="80px">
                <p>
                    <%=Proxy[EnumText.enumForum_Message_ColumnStatus] %>
                </p>
            </th>
            <th>
                <p>
                    <%=Proxy[EnumText.enumForum_Message_ColumnSubject] %>
                </p>
            </th>
            <th width="150">
                <p>
                    <%=Proxy[EnumText.enumForum_Message_ColumnReceiveTime] %>
                </p>
            </th>
            <th width="120">
                <p>
                    <%=Proxy[EnumText.enumForum_Message_ColumnSender] %>
                </p>
            </th>
            <th width="80">
                <p>
                    <%=Proxy[EnumText.enumForum_Message_ColumnOperation] %>
                </p>
            </th>
        </tr>
        <asp:Repeater ID="RepeaterInMessages" runat="server" OnItemCommand="RepeaterInMessages_ItemCommand">
            <ItemTemplate>
                <tr class="trOdd">
                    <td class="row1" style="text-align:center">
                        <p>
                            <%#Status(Convert.ToBoolean(Eval("IfView"))) %>
                        </p>
                    </td>
                    <td class="row1">
                        <p>
                            <asp:LinkButton ID="LinkbtnMessage" class='<%# Convert.ToBoolean(Eval("IfView")) ? "normalFont":"weightFont" %>'
                                CommandArgument='<%#Eval("Id") + ";" +Convert.ToString(Eval("IfView"))%>' CommandName="Message"
                                runat="server" ToolTip='<%# GetTooltipString(ReplaceProhibitedWords(Convert.ToString( Eval("Subject"))))%>'><%# Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Convert.ToString(Eval("Subject")))),60)%></asp:LinkButton>
                        </p>
                    </td>
                    <td class="row1">
                        <p>
                            <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("CreateDate")))%>
                        </p>
                    </td>
                    <td class="row1">
                        <p>
                            <a href='../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%# Convert.ToInt32(Eval("FromUserOrOperatorId")) %>'
                                title='<%#GetTooltipString(ReplaceProhibitedWords(Eval("FromUserOrOperatorDisplayName").ToString()))%>'  target="_blank">
                                <%#System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Eval("FromUserOrOperatorDisplayName").ToString()))%>
                            </a>
                        </p>
                    </td>
                    <td class="row2" align="center">
                        <p>
                            <a href="MySendMessage.aspx?siteId=<%=SiteId %>&userId=<%# Convert.ToInt32(Eval("ToUserOrOperatorId"))%>&inMessageId=<%#Convert.ToString(Eval("Id"))%>">
                                <img src="../<%=ImagePath %>/replay.gif" alt="<%=Proxy[EnumText.enumForum_Message_HelpReply] %>"
                                    title="<%=Proxy[EnumText.enumForum_Message_HelpReply] %>" style="vertical-align:top"/></a>&nbsp;&nbsp;
                            <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="../Images/database_delete.gif"
                                CommandName="Delete" CommandArgument='<%#Convert.ToString(Eval("Id"))%>' ToolTip='<%#Proxy[EnumText.enumForum_Message_HelpDelete]%>'
                                OnClientClick="return deleteConfirm()" />
                        </p>
                    </td>
                </tr>
                <tr id="trDetail<%#Convert.ToInt32(Eval("Id"))%>" style="display: none;">
                    <td colspan="5" class="row5">
                        <p>
                            <%# System.Web.HttpUtility.HtmlEncode(ReplaceProhibitedWords(Convert.ToString(Eval("Message")))) %>
                            &nbsp;
                        </p>
                    </td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        <tr>
            <td colspan="5" class="row5" align="center">
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
    <div class="pos_bottom_10">
        <table class="tb_legend" cellspacing="0" width="100%">
            <tr>
                <td class="cat3">
                    <p>
                        <%=Proxy[EnumText.enumForum_Topic_FieldLegend]%>
                    </p>
                </td>
            </tr>
            <tr>
                <td class="row3">
                    <table cellpadding="1" cellspacing="1" width='100%'>
                        <tr>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="28" align="center">
                                            <p>
                                                <img src="../<%=ImagePath %>/status/read_normal.gif" title="<%=Proxy[EnumText.enumForum_InBox_LengendReadMessage]%>"
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_InBox_LengendReadMessage]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                                <table>
                                    <tr>
                                        <td width="28" align="center">
                                            <p>
                                                <img src="../<%=ImagePath %>/status/noread_normal.gif" title="<%=Proxy[EnumText.enumForum_InBox_LengendUnReadMessage]%>"
                                                    align="absmiddle" />
                                            </p>
                                        </td>
                                        <td>
                                            <p>
                                                <%=Proxy[EnumText.enumForum_InBox_LengendUnReadMessage]%>
                                            </p>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td width="25%">
                            </td>
                            <td width="25%">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>

    <script type="text/javascript" language="javascript">
        showDetail();
    </script>

</asp:Content>
