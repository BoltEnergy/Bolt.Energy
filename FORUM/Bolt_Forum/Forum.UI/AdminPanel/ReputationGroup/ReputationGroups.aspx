<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ReputationGroups.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.ReputationGroup.ReputationGroups" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        function deleteConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Reputation_ConfirmDeleteReputation] %>');
        }
    </script>
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
            <asp:Button ID="btnNewReputation1" runat="server" CssClass="slbtn"
                OnClick="btnNewReputation_OnClick" />
        </div>
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr>
                    <th >
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnName] %>
                    </th>
                    <th >
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnDescription] %>
                    </th>
                    <th >
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnReputationRange] %>
                    </th>
                    <th>
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnRank] %>
                    </th>
                    <th width="100px" class="cth" style="<%=IfPermissionDisplay %>">
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnPermissions] %>
                    </th>
                    <th width="100px" class="cth">
                        <%=Proxy[EnumText.enumForum_Reputation_ColumnOperation] %>
                    </th>
                </tr>
                <asp:Repeater ID="rpGroups" runat="server" onitemdatabound="rpGroups_ItemDataBound">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpGroups.Items.Count+1)%2%>" onmousedown="highLightRow(this)">
                            <td title="<%# GetTooltipString(Eval("Name").ToString()) %>">
                                <%# Server.HtmlEncode(Eval("Name").ToString()) %>
                            </td>
                            <td title="<%# GetTooltipString(Eval("Description").ToString()) %>">
                                <%# Server.HtmlEncode(Eval("Description").ToString()) %>
                            </td>
                            <td>
                                <%# String.Format("{0}~{1}",Eval("limitedBegin"),Eval("limitedExpire")) %>
                            </td>
                            <td>
                                <asp:Repeater ID="rpImages" runat="server">
                                    <ItemTemplate>
                                        <img src="../../Images/user reputation.GIF" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lblImages" runat='server' Text='<%# Eval("icoRepeat") %>'></asp:Label>
                            </td>                    
                            <td class="cth" style="<%=IfPermissionDisplay %>">
                                <a href="ReputationGroupPermission.aspx?siteid=<%=SiteId%>&groupid=<%# Eval("groupid") %>">
                                    <img alt="<%=Proxy[EnumText.enumForum_Reputation_HelpSetPermission] %>" title="<%=Proxy[EnumText.enumForum_Reputation_HelpSetPermission] %>" src="../../Images/permission-manager.GIF" /></a>
                            </td>
                            <td class="cth">
                                <a href="EditReputationGroup.aspx?siteid=<%=SiteId%>&groupid=<%# Eval("groupid") %>">
                                    <img alt="<%=Proxy[EnumText.enumForum_Reputation_HelpEdit] %>" title="<%=Proxy[EnumText.enumForum_Reputation_HelpEdit] %>" src="../../images/database_edit.gif" /></a>                                
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="<%#Proxy[EnumText.enumForum_Reputation_HelpDelete] %>"  ImageUrl="~/Images/database_delete.gif" OnClick="ibtnDelete_OnClick" 
                                OnClientClick="return deleteConfirm();" CommandArgument='<%# Eval("groupId") %>' />                                        
                            </td>
                        </tr>        
                    </ItemTemplate>
                    <%--<AlternatingItemTemplate>
                        <tr class="trStyle2" onmousedown="highLightRow(this)">
                            <td>
                                <%# Eval("Name") %>
                            </td>
                            <td>
                                <%# Eval("Description") %>
                            </td>
                            <td>
                                <%# String.Format("{0}~{1}",Eval("limitedBegin"),Eval("limitedExpire")) %>
                            </td>
                            <td>
                                <asp:Repeater ID="rpImages" runat="server">
                                    <ItemTemplate>
                                        <img src="../../Images/user reputation.GIF" />
                                    </ItemTemplate>
                                </asp:Repeater>
                                <asp:Label ID="lblImages" runat='server' Text='<%# Eval("icoRepeat") %>'></asp:Label>
                            </td>                    
                            <td class="cth">
                                <a href="ReputationGroupPermission.aspx?siteid=<%=SiteId%>&groupid=<%# Eval("groupid") %>">
                                    <img alt="<%=Proxy[EnumText.enumForum_Reputation_HelpSetPermission] %>" title="<%=Proxy[EnumText.enumForum_Reputation_HelpSetPermission] %>" src="../../Images/permission-manager.GIF" /></a>
                            </td>
                            <td class="cth">
                                <a href="EditReputationGroup.aspx?siteid=<%=SiteId%>&groupid=<%# Eval("groupid") %>">
                                    <img alt="<%=Proxy[EnumText.enumForum_Reputation_HelpEdit] %>" title="<%=Proxy[EnumText.enumForum_Reputation_HelpEdit] %>" src="../../images/database_edit.gif" /></a>                                
                                <asp:ImageButton ID="ibtnDelete" runat="server" ToolTip="<%#Proxy[EnumText.enumForum_Reputation_HelpDelete] %>" ImageUrl="~/Images/database_delete.gif" OnClick="ibtnDelete_OnClick" 
                                OnClientClick="return deleteConfirm();" CommandArgument='<%# Eval("groupId") %>' />                                        
                            </td>
                        </tr>
                    </AlternatingItemTemplate>--%>
                </asp:Repeater>
            </table>
            <div style="text-align: center; padding-top: 10px">

            </div>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnNewReputation2" runat="server"  CssClass="slbtn"
                 OnClick="btnNewReputation_OnClick" />
        </div>

        <script type="text/javascript">
            initTabletrColor("ctl00_ContentPlaceHolder1_gdvOperatorList", "trStyle1", "trStyle2");
        </script>

    </div>
    
</asp:Content>
