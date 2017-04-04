<%@ Page Title="" Language="C#" MasterPageFile="~/ModeratorPanel/ModeratorMasterPage.Master" AutoEventWireup="true" CodeBehind="AbuseReport.aspx.cs" Inherits="Com.Comm100.Forum.UI.ModeratorPanel.Abuse.AbuseReport" %>
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
                                <%=Proxy[EnumText.enumForum_Post_FieldSubject]%></div>
                        </td>
                        <td class="ctd">
                            <asp:TextBox ID="txtKeywords" runat="server" CssClass="txtmid"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div class="ttd">
                                <%=Proxy[EnumText.enumForum_Abuse_FieldStatus] %></div>
                        </td>
                        <td class="ctd">
                            <asp:DropDownList ID="ddlState" runat="server">
                            </asp:DropDownList>
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
        <div class="divTable">
            <br />
            <table class="the-table" cellpadding='0' cellspacing='0'>
                <tr runat="server" id="tbHeader">
                    <th width="35%">
                        <%=Proxy[EnumText.enumForum_Post_ColumnSubject] %>
                    </th>
                    <th width="20%">
                        <asp:LinkButton ID="lbtnReportDate" runat="server" CommandName="Report Date" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Abuse_ColumnReportTime]%></asp:LinkButton>
                        <asp:Image ID="imgReportDate" runat="server" Visible="false" EnableViewState="false" />
                    </th>
                    <th width="15%">
                        <asp:LinkButton ID="lbtnReportUser" runat="server" CommandName="Report User" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Abuse_ColumnReportUser] %></asp:LinkButton>
                        <asp:Image ID="imgReportUser" runat="server" Visible="false" EnableViewState="false" />
                    </th>
                    <th width="15%">
                        <asp:LinkButton ID="lbtnPostUser" runat="server" CommandName="Post User" OnCommand="btnSort_click">
                        <%=Proxy[EnumText.enumForum_Abuse_ColumnPostUser] %></asp:LinkButton>
                        <asp:Image ID="imgPostUser" runat="server" Visible="false" EnableViewState="false"  />
                    </th>
                    <th width="100px">
                        <%=Proxy[EnumText.enumForum_Abuse_ColumnStatus] %>
                    </th>
                    <th width="20%">
                        <%=Proxy[EnumText.enumForum_Abuse_ColumnNotes] %>
                    </th>
                </tr>
                <asp:Repeater ID="rptData" runat="server">
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.rptData.Items.Count)%2%>" onmousedown="highLightRow(this)">
                            <td>
                                <span class="linkTopic"><a href="../../Topic.aspx?<%#GetQueryString(Convert.ToInt32(Eval("PostId")))%>&siteId=<%=SiteId %>&type=abuse&GoToPost=true#Post<%#Eval("PostId") %>"
                                    target="_blank" title="<%#GetTooltipString(Eval("PostSubject").ToString())%>">
                                    <%#Server.HtmlEncode(Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Convert.ToString(Eval("PostSubject")), 25))%></a></span>
                            </td>
                            <td>
                                <%# Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(
                                                                        Convert.ToDateTime(Eval("Date")))%>
                            </td>
                            <td>
                                <a href="../../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("AbuseUserOrOperatorId")%>"
                                    target="_blank" >
                                    <span style="color: #50b846; font-weight: bold;" title="<%#GetTooltipString(Eval("AbuseUserOrOperatorName").ToString())%>">
                                        <%#Server.HtmlEncode(Convert.ToString(Eval("AbuseUserOrOperatorName")))%></span></a>
                            </td>
                            <td>
                                <a href="../../User_Profile.aspx?siteId=<%=SiteId %>&userId=<%#Eval("PostUserOrOperatorId")%>"
                                    target="_blank" >
                                    <span style="color: #50b846; font-weight: bold;" title="<%#GetTooltipString(Eval("PostUserOrOperatorName").ToString())%>">
                                        <%#Server.HtmlEncode(Convert.ToString(Eval("PostUserOrOperatorName")))%></span></a>
                            </td>
                            <td>
                                <%#GetStatus((Com.Comm100.Framework.Enum.Forum.EnumAbuseStatus)Eval("Status"))%>
                            </td>
                            <td>
                            <span title="<%#Server.HtmlEncode(Convert.ToString(Eval("Note")))%>">
                                <%#Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(Server.HtmlEncode(Convert.ToString(Eval("Note"))),12)%>
                            </span>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div style="text-align: center; padding-top: 10px">
                <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                    OnPaging="aspnetPager_Paging" EnableViewState="true">
                </wcw:ASPNetPager>
            </div>
        </div>
    </div>
</asp:Content>