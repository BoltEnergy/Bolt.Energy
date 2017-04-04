<%@ Page Title="Drafts" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="DraftEdit.aspx.cs" Inherits="Forum.UI.AdminPanel.Drafts.DraftEdit"
    ValidateRequest="false" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<%@ Register Src="../../UserControl/HTMLEditor.ascx" TagName="HTMLEditor" TagPrefix="uc1" %>
<%@ Register Assembly="Framework" Namespace="Com.Comm100.Framework.WebControls" TagPrefix="wcw" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" type="text/javascript">
        function onImgClick(id) {
            if (document.getElementById("tr" + id).style.display == "none") {
                document.getElementById("img" + id).src = "../../Images/icon_min.GIF";
                document.getElementById("tr" + id).style.display = "";
            }
            else {
                document.getElementById("img" + id).src = "../../Images/icon_max.GIF";
                document.getElementById("tr" + id).style.display = "none";
            }
        }
        function TextKeyDown(txtId) {
        }
    </script>

    <style type="text/css">
        .style2
        {
            height: 13px;
            width: 468px;
        }
        .style3
        {
            width: 468px;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        
        <asp:Label runat="server" ID="lblTitle" CssClass="TitleLabel"></asp:Label>
            </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server" Text=""></asp:Label>
        <br />
        <br />
    </div>
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent" style="min-width: 700px;">
        <div class="divTable">
            <div class="divSecContent">
                <div class="divSecMsg">
                    <div>
                        <%=Proxy[EnumText.enumForum_Topic_FieldPosts]%>
                    </div>
                </div>
            </div>
            <div class="divSecTable" style="text-align: left;">
                <asp:Repeater ID="repeaterTopicAndPosts" runat="server">
                    <HeaderTemplate>
                        <table width="100%" class="the-table" cellpadding="0" cellspacing="0">
                            <tr>
                                <th width="20%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnAuthor]%>
                                </th>
                                <th width="80%">
                                    <%=Proxy[EnumText.enumForum_Topic_ColumnMessage]%>
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="trStyle<%#(this.repeaterTopicAndPosts.Items.Count)%2%>" onmousedown="highLightRow(this);">
                            <td align="center" width="20%">
                                <img id="img<%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Layer"))%>" alt=""
                                    src="../../Images/icon_max.GIF" onclick="onImgClick(<%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Layer"))%>);"
                                    style="cursor: hand" />
                                <%#Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Layer")) %># &nbsp;&nbsp;<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "IfPostUserOrOperatorDeleted")) ? Proxy[EnumText.enumForum_Public_DeletedUser] : "<span title='" + GetTooltipString(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString()) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorName").ToString()), 20) + "</span>"%>
                            </td>
                            <td width="80%">
                                <div style="float: left">
                                    <b>
                                        <%=Proxy[EnumText.enumForum_Topic_FieldSubject]%></b>
                                    <%#"<span title='" + GetTooltipString(Eval("Subject").ToString()) + "'>" + Com.Comm100.Framework.Common.StringHelper.GetMarkedLengthOfString(System.Web.HttpUtility.HtmlEncode(Eval("Subject").ToString()), 30) + "</span>"%>
                                </div>
                                <div style="float: right">
                                    <b>
                                        <%=Proxy[EnumText.enumForum_Topic_ColumnPosted]%></b>
                                    <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(Eval("PostTime")))%></div>
                            </td>
                        </tr>
                        <tr id="tr<%#Convert.ToInt32(DataBinder.Eval(Container.DataItem, "Layer"))%>" style="display: none"
                            class="trStyle<%#(this.repeaterTopicAndPosts.Items.Count)%2%>" onmousedown="highLightRow(this);">
                            <td width="20%" style="background-color: #ddd">
                                <div>
                                    <b>
                                        <%=Proxy[EnumText.enumForum_Topic_ColumnJoined]%></b>
                                    <%#Com.Comm100.Framework.Common.DateTimeHelper.DateTransferToString(Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "PostUserOrOperatorJoinedTime")))%></div>
                                <div>
                                    <b>
                                        <%=Proxy[EnumText.enumForum_Topic_ColumnPosts]%></b>
                                    <%#DataBinder.Eval(Container.DataItem, "PostUserOrOperatorNumberOfPosts")%></div>
                            </td>
                            <td width="80%" style="background-color: #ddd">
                                <%#DataBinder.Eval(Container.DataItem, "Content")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <br />
            <div style="text-align: center; margin-top: -20px">
                <wcw:ASPNetPager ID="aspnetPager" runat="server" OnChangePageSize="aspnetPager_ChangePageSize"
                    OnPaging="aspnetPager_Paging" EnableViewState="true" PageSize="10">
                </wcw:ASPNetPager>
            </div>
            <br />
            <div class="divSecContent">
                <div class="divSecMsg">
                    <div>
                        <%=Proxy[EnumText.enumForum_Topic_FieldDraft]%>
                    </div>
                </div>
                <div class="divSecTable" style="text-align: left;">
                    <table style="width: 100%;" class="form-table">
                        <tr>
                            <td width="10%" class="ttd" style="height: 13px">
                                <%=Proxy[EnumText.enumForum_Topic_FieldSubject]%>
                            </td>
                            <td align="left" class="style2">
                                <asp:TextBox ID="txtSubject" runat="server" Width="99%"></asp:TextBox>
                            </td>
                            <td>
                                <span class="DraftTitleAlert">*</span>
                                <asp:RequiredFieldValidator ID="rfvSubject" runat="server" ControlToValidate="txtSubject" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="middle" class="ttd">
                                <%=Proxy[EnumText.enumForum_Topic_FieldContent]%>
                            </td>
                            <td align="left" class="style3">
                                <div>
                                    <uc1:HTMLEditor ID="HTMLEditor1" runat="server" />
                                </div>
                                <asp:Label ID="draftEditInfo" runat="server" Text=""></asp:Label>
                            </td>
                            <%--                        <td class="lftd">
                            <asp:Label ID="draftEditInfo" runat="server" Text=""></asp:Label>
                        </td>--%>
                        </tr>
                        <tr id="trUploadAttachmentList" runat="server" style="border-right: solid 1px #aaa">
                            <td class="ttd">
                                <div id="divUploadTempAttachmentList">
                                    <%=Proxy[EnumText.enumForum_Topic_FieldUploadattachment]%>>
                                </div>
                                <br />
                            </td>
                            <td>
                                <asp:Repeater ID="rptPostAttachmentsList" runat="server" 
                                    OnItemDataBound="rptPostAttachmentsList_ItemDataBound"
                                    OnItemCommand="rptPostAttachmentsList_ItemCommand">
                                    <ItemTemplate>
                                        <div>
                                            <asp:HiddenField ID="hdAttachId" runat="server" />
                                            <%#Eval("Name")%>
                                            <asp:ImageButton ID="img" runat="server" ImageUrl="~/Images/database_delete.gif"
                                                ToolTip="Delete" CommandName="Delete" CommandArgument='<%#Eval("Id")%>' />
                                        </div>
                                        <div>
                                            <div style="width: 70px; float: left; text-align: right">
                                                <%=Proxy[EnumText.enumForum_Topic_FieldDownload]%></div>
                                            <div style="float: left">
                                                <asp:TextBox ID="tbScore" runat="server" CssClass="txtshort" />
                                                <asp:RegularExpressionValidator ID="revDownLoadScore" runat="server" Display="Dynamic"
                                                    ValidationExpression="^\d*$" ErrorMessage="please input number." />
                                                <asp:RequiredFieldValidator ID="rfvDownloadScore" runat="server" Display="Dynamic"
                                                    ControlToValidate="tbScore" ErrorMessage="pay for downlaod is required." />
                                            </div>
                                            <div style="clear: both">
                                            </div>
                                            <div style="width: 70px; float: left; text-align: right">
                                                <%=Proxy[EnumText.enumForum_Topic_FieldDescription]%></div>
                                            <div style="float: left">
                                                <asp:TextBox ID="tbDescription" runat="server" CssClass="txtmid" /></div>
                                            <div style="clear: both">
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                                <div id="prgUpload" style="width: 300px; background-color: #f0f0f0; display: none;">
                                    <div id="prgUploadF" style="width: 1%; background-color: Blue;">
                                    </div>
                                </div>
                                <div id="divAttachments">
                                    <asp:FileUpload ID="file" runat="server" />
                                    <asp:Button ID="btnUpload" CssClass="mbtn" runat="server" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="rtd">
                                <asp:Label ID="lblRequiredField" runat="server">*<%=Proxy[EnumText.enumForum_Public_RequiredField]%></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="mbtn"  />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnPost" runat="server" OnClick="btnPost_Click" CssClass="mbtn" />
            &nbsp;&nbsp;&nbsp;
            <input id="btnCancel" type="button" value='<%=Proxy[EnumText.enumForum_Topic_ButtonCancel]%>'
                onclick="window.location.href='DraftList.aspx?siteid=<%=SiteId%>'" class="mbtn" />
        </div>
    </div>
</asp:Content>
