<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorPanel/ModeratorMasterPage.Master"
    AutoEventWireup="true" CodeBehind="Forums.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.Forum.Forums" ValidateRequest="false"  EnableEventValidation="false"%>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
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
    <div class="clear">
    </div>
    <br />
    <div class="divContent">
        <div class="divTable">
        <br />
            <table class="the-table">
                <tr id="tbHeader">
                
                <th width="25%"><%=Proxy[EnumText.enumForum_Forums_ColumnName] %></th>
                <th width="40%"><%=Proxy[EnumText.enumForum_Forums_ColumnDescription] %></th>
                <th width="20%" class="cth"><%=Proxy[EnumText.enumForum_Forums_ColumnStatus] %></th>
                
                <th width="15%" class="cth" style="<%=IfDisplay %>"><%=Proxy[EnumText.enumForum_Forums_ColumnPermissions] %></th>
                </tr>
               
                <asp:Repeater ID="rpForums" EnableViewState="true" runat="server" OnItemCommand="rpForums_ItemCommand">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rpForums.Items.Count)%2%>" onmousedown="highLightRow(this)" style="width:100%">
                            
                            <td>
                               <span title='<%#Eval("Name")%>'>
                                <%#System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("Name").ToString(),25)) %>
                                </span>
                            </td>
                            <td>
                                <span title='<%#Eval("Description")%>'>
                                <%#System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Eval("Description").ToString(),50)) %>
                                </span>
                            </td>
                                
                            <td class="ctd"><%#GetForumStatusName((Com.Comm100.Framework.Enum.Forum.EnumForumStatus)Eval("Status"))%></td>
                            
                            <td class="ctd" style="<%=IfDisplay %>"><asp:ImageButton ID="imgbtnPermissions" CommandName="Permissions" CommandArgument='<%#Eval("ForumId") %>' ImageUrl="~/Images/permission-manager.GIF" ToolTip="<%#Proxy[EnumText.enumForum_Forums_ColumnPermissions] %>" runat="server" /></td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
                
            </table>
            <br/>
        </div>
    </div>
    <br />
</asp:Content>
