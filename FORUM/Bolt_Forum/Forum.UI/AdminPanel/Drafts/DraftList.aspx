<%@ Page Title="Drafts" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="DraftList.aspx.cs" Inherits="Forum.UI.AdminPanel.Drafts.DraftList" ValidateRequest="false" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script type="text/javascript" language="javascript">
        function changeCSS(obj, cssName) {
            obj.className = cssName;
        }
        function deleteDraftConfirm() {
            return confirm('<%=Proxy[EnumText.enumForum_Topic_ConfirmDeleteDraft]%>');
        }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <div class="divTitle">
            <asp:Label runat="server" ID="lblTitle" CssClass="TitleLabel"></asp:Label></div>
        <div class="divSubTitle">
            <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label><br />
            <br />
        </div>
        <div class="divMsg">
            <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label></div>
        <div class="divContent">
            <div class="divTopButton">
                <asp:Button ID="btnQuery1"  runat="server" CssClass="mbtn" 
                onclick="btnQuery_Click" />
            </div>
           <%-- <div class="divTopQuery">
                <b><%=Proxy[EnumText.enumForum_Topic_FieldSubject]%></b>
                <asp:TextBox ID="tbSubject" runat="server" CssClass="txtmid"></asp:TextBox>
                <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" 
                    onclick="btnQuery_Click" />
            </div>--%>
             <br />
             <div class="divTable" style="text-align: center;">
                <center>
                    <table class="form-table">
                        <tr>
                            <td>
                                <div class="ttd">
                                    <%=Proxy[EnumText.enumForum_Topic_FieldSubject]%>
                                </div>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="tbSubject" runat="server" CssClass="txtmid"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </center>
            </div>
            <br />
            <div class="divButtomButton">
                <asp:Button ID="btnQuery2" runat="server" CssClass="mbtn" onclick="btnQuery_Click"  />
            </div>
        </div>
        <br />
        <div class="divContent">
            <div class="divTopQuery"></div>
            <div class="divTable">
                <br />
                <table class="the-table" cellpadding='0' cellspacing='0'>
                    <tr>
                               <%-- <th align="center" width="10%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnTopicId]%>
                                </th>--%>
                                <th align="center" width="20%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnTopicSubject]%>
                                </th>
                                 <th align="center" width="15%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnTopicStatus]%>
                                </th>
                                <th align="center" width="20%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnTopicStarter]%>
                                </th>
                                
                               
                                <th align="center" width="20%">
                                  <%=Proxy[EnumText.enumForum_Topic_ColumnPostTime]%>
                                </th>
                               <th width="15%" class="cth">
                                   <%=Proxy[EnumText.enumForum_Topic_ColumnOperation]%>
                                </th>
                            </tr>
                            
                    <asp:Repeater ID="repeaterDrafts" runat="server" 
                    OnItemCommand="RepeaterDrafts_ItemCommand" 
                        onitemdatabound="repeaterDrafts_ItemDataBound" >

                    <ItemTemplate>
                        <tr class="trStyle<%#(this.repeaterDrafts.Items.Count)%2%>" onmousedown="highLightRow(this);">
                            <%--<td>
                                <%#DataBinder.Eval(Container.DataItem,"TopicId") %>
                            </td>--%>
                             <td>
                                <%#"<a href='../../Topic.aspx?topicId=" + Eval("TopicId") + "&siteid=" + SiteId + "&forumId=" + Eval("ForumId") + "#divpostreplay' title='" + GetTooltipString(DataBinder.Eval(Container.DataItem, "Subject").ToString()) + "' target='_blank'>" + System.Web.HttpUtility.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(DataBinder.Eval(Container.DataItem, "Subject").ToString(), 10)) + "</a>"%>
                            </td>
                            <td>
                                <%#
                                Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IfClosed")) == true ? Proxy[EnumText.enumForum_Topic_TopicStatusClosed] : Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IfMarkedAsAnswer")) == true ? Proxy[EnumText.enumForum_Topic_TopicStatusMakeasanswer] : Proxy[EnumText.enumForum_Topic_TopicStatusOpen]
                                %>
                            </td>
                            <td>
                            <span title='<%#Eval("PostUserOrOperatorName").ToString()%>'>
                                <%#Convert.ToBoolean(Eval("PostUserOrOperatorIfDeleted")) ? Eval("PostUserOrOperatorName").ToString() : "<a href='../Users/UserView.aspx?id=" + Eval("PostUserOrOperatorId") + "&siteid=" + SiteId + "' target='_blank' style=\"color: #50b846; font-weight: bold;\">" + System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString()) + "</a>"%>
                            </span>
                            </td>
                           
                            
                            <td>
                                <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%>
                            </td>
                            <td class="ctd">
                                <asp:ImageButton ID="imgbtnView" CommandName="view" CommandArgument='<%#Eval("TopicId") %>'
                                    ImageUrl="~/images/view.gif"  runat="server" />
                               
                            &nbsp;
                                <asp:ImageButton ID="imgbtnDelete" CommandName="delete" CommandArgument='<%#Eval("TopicId") %>'
                                    ImageUrl="~/images/database_delete.gif"  runat="server" OnClientClick="javascript:return deleteDraftConfirm();" />
                            </td>
                        </tr>
                        
                    </ItemTemplate>
                    </asp:Repeater>                
                </table>
                <div style="text-align: center">
                    <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                    OnPaging="aspnetPager_Paging" EnableViewState="true" PageSize ="10">
                    </wcw:ASPNetPager>
                </div>
                <br />
            </div>
             <div class="divButtomButton"></div>
        </div>
    </div>
</asp:Content>
