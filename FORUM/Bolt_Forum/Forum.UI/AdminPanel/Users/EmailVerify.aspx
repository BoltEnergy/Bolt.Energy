<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="EmailVerify.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Users.EmailVerify"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"> </asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnQuery1" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
        </div>
        <br />
        <div class="divTable">
            <center>
                <table class="form-table">
                    <tr>
                        <td class="ttd">
                            <%=Proxy[EnumText.enumForum_User_QueryEmailOrDisplayName]%>
                        </td>
                        <td class="ctd">
                            <asp:TextBox ID="txtQuery" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </center>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" OnClick="btnQuery_Click" />
        </div>
    </div>
    <br />
    <div class="divContent">
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <%--<th style="width: 10%; ">
                        <asp:LinkButton ID="lbtnUserId" runat="server" CommandName="Id" OnClick="rptEmailVerifyUsers_sorting">
                        <%=Proxy[EnumText.enumForum_User_ColumnId]%></asp:LinkButton>
                        <asp:Image ID="imgId" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>--%>
                    <th width="25%">
                        <asp:LinkButton ID="lbtnUserEmail" runat="server" CommandName="Email" OnClick="rptEmailVerifyUsers_sorting">
                        <%=Proxy[EnumText.enumForum_User_ColumnEmail]%></asp:LinkButton>
                        <asp:Image ID="imgEmail" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width: 15%;">
                        <asp:LinkButton ID="lbtnUserDisplayName" runat="server" CommandName="Name" OnClick="rptEmailVerifyUsers_sorting">
                        <%=Proxy[EnumText.enumForum_User_ColumnDisplayName]%></asp:LinkButton>
                        <asp:Image ID="imgName" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width: 20%;">
                        <asp:LinkButton ID="lbtnJoinedTime" runat="server" CommandName="JoinedTime" OnClick="rptEmailVerifyUsers_sorting">
                        <%=Proxy[EnumText.enumForum_User_ColumnJoinedTime]%></asp:LinkButton>
                        <asp:Image ID="imgJoinedTime" runat="server" Visible="false" ImageUrl="~/Images/sort_up.gif" />
                    </th>
                    <th style="width: 15%;" class="cth">
                        <%=Proxy[EnumText.enumForum_EmailVerify_ColumnResendEmail] %>
                    </th>
                    <th style="width: 15%;" class="cth">
                        <%=Proxy[EnumText.enumForum_EmailVerify_ColumnEmailVerify] %>
                    </th>
                </tr>
                <asp:Repeater ID="rptEmailVerifyUsers" runat="server" OnItemCommand="rptEmailVerifyUsers_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rptEmailVerifyUsers.Items.Count+1)%2%>" onmousedown="highLightRow(this)">
                            <%--<td>
                                <%# Eval("id") %>
                            </td>--%>
                            <td>
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("Email").ToString()) %>'>
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("email")), 25))%></span>
                            </td>
                            <td>
                                <span class="linkTopic" title='<%#GetTooltipString(Eval("DisplayName").ToString()) %>'>
                                    <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("DisplayName"))), 12)%></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("JoinedTime")))%>
                            </td>
                            <td class="cth">
                                <asp:ImageButton runat="server" ID="btnSendEmail" CommandName="ResendEmail" ToolTip="<%#Proxy[EnumText.enumForum_EmailVerify_HelpResendEmail] %>"
                                    CommandArgument='<%#Eval("Id") %>' ImageUrl="~/Images/btnbg_next.gif" />
                            </td>
                            <td class="cth">
                                <asp:ImageButton runat="server" ID="btnEmailVerifyPage" CommandName="EmailVerify"
                                    ToolTip="<%#Proxy[EnumText.enumForum_EmailVerify_HelpEmailVerify] %>" CommandArgument='<%#Eval("Id") %>'
                                    ImageUrl="~/Images/view.gif" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
        <div class="divTable">
        </div>
        <div>
            <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                EnableViewState="true" OnPaging="aspnetPager_Paging">
            </wcw:ASPNetPager>
        </div>
        <br />
    </div>

    <script src="../../JS/Clipboard/ZeroClipboard.js" type="text/javascript"></script>

    <script language="JavaScript" type="text/javascript">
        var clip = null;

        function $(id) { return document.getElementById(id); }

        function init() {
            clip = new ZeroClipboard.Client();
            clip.setHandCursor(true);
            clip.addEventListener('load', my_load);
            clip.addEventListener('mouseOver', my_mouse_over);
            clip.addEventListener('complete', my_complete);
            clip.glue('btnCopyUrl');
        }

        function my_load(client) {
        }

        function my_mouse_over(client) {
            // we can cheat a little here -- update the text on mouse over
            clip.setText($('<%=txtEmailVerifyUrl.ClientID%>').value);
        }

        function my_complete(client, text) {
            closeWindow();
        }

        function debugstr(msg) {
        }
        function closeWindow() {
            WindowClose('divThickInner', 'divThickOuter');
            clip.hide();
        }
    </script>

    
   
</asp:Content>

<asp:Content ContentPlaceHolderID="cphThickBox" runat="server">
    <div id="divThickInner" style="position: absolute; height: 140px; width: 700px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
            <div class="divh">
                <div class="divh-table">
                    <div class="divTable">
                        <table>
                            <tr>
                                <td width="250px">
                                    <span style="padding: 5px; font-weight: bold">
                                        <%=Proxy[EnumText.enumForum_EmailVerify_FieldEmailVerifyUrl] %>:</span>
                                </td>
                                <td width="400px">
                                    <asp:TextBox ID="txtEmailVerifyUrl" Width="500px" runat="server" TextMode="MultiLine"
                                        Rows="5"></asp:TextBox>
                                </td>
                                <td>
                                    <img src="../../Images/help.gif" style="margin-bottom: -2px;" alt="" onmouseover="showHelp('divHelp','<%=Proxy[EnumText.enumForum_EmailVerify_HelpEmailVerifyUrl]%>');"
                                        onmouseout="closeHelp('divHelp');" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center;">
                                    <input type="button" id="btnCopyUrl" value="<%=Proxy[EnumText.enumForum_EmailVerify_ButtonCopyUrl] %>"
                                        style="z-index: 100000; cursor: default;" class="lbtn" />
                                    <input type="button" class="mbtn" onclick="closeWindow();" value='<%=Proxy[EnumText.enumForum_Topic_FieldClose]%>' />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>
     <div id="divThickOuter" style="position: absolute; filter: alpha(opacity=50); -moz-opacity: 0.5;
        opacity: 0.5; border: 0px; display: none; background-color: #ccc;">
    </div>
</asp:Content>
