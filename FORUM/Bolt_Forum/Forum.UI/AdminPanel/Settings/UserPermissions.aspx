<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="UserPermissions.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.UserPermissions" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <style type="text/css">
        .Font1
        {
            font-family: Terminal;
            padding:3px;
        }
        .Font2
        {
            font-family: Terminal;
            
        }
        .Font3
        {
            font-size: 1.2em;
            font-weight: bold;
        }
        #form-table
        {
            background-color: #d7e7ee;
        }
        #form-table td
        {
            background-color: #ffffff;            
        }
        #form-table tr
        {
            height:35px;                     
        }
        #form-table .std
        {     
            text-align:left;       
            padding:5px                   
        }
        #form-table .sstd
        {     
            text-align:right;       
            font-weight:normal;                
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function $(id) { return document.getElementById(id); }

        function checkViewForumOrTopic() {
            var chbViewForum = $("<%=chbViewForum.ClientID %>");
            var chbViewTopic = $("<%=chbViewTopic.ClientID %>");
            var chbPost = $("<%=chbPost.ClientID %>");
            if (chbViewForum.checked == false) {
                chbViewTopic.checked = false;
                chbViewTopic.disabled = true;
                chbPost.checked = false;
                chbPost.disabled = true;
            }
            else {
                chbViewTopic.disabled = false;
                if (chbViewTopic.checked == false) {
                    chbPost.checked = false;
                    chbPost.disabled = true;
                }
                else {
                    chbPost.disabled = false;
                }
            }
        }
    </script>

    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label>
    </div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblError" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click"
                ValidationGroup="Save" />
            <asp:Button ID="btnCancel1" runat="server" CausesValidation="False" CssClass="mbtn"
                OnClick="btnCancel_Click" />
        </div>
        <div class="divTable">
            <table class="form-table">
                <tr>
                    <td>
                        <table class="tblPermissionsSet" width="100%">
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowViewForum]%>
                                </td>
                                <td class="ctd" style="width:110px">
                                    <asp:CheckBox ID="chbViewForum" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblViewForum" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowViewTopicOrPost]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbViewTopic" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblViewTopic" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowPostTopicOrPost]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbPost" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblPost" runat="server" CssClass="helpMsg" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowCustomizeAvatar]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbAvatar" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblAvatar" runat="server" CssClass="helpMsg" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxSignature]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtSignature" CssClass="txtshortScore" runat="server" customattr="1000"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revSignatureLength" runat="server" ControlToValidate="txtSignature"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblSignature" runat="server" CssClass="helpMsg" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowLinkSignature]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbLinkSignature" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblAllowLinkSignature" runat="server" CssClass="helpMsg" 
                                        Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowImageSignature]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbImageSignature" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblAllowImageSignature" runat="server" CssClass="helpMsg" 
                                        Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MinInterValTimeForPosting]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtIntervalPost" CssClass="txtshortScore" runat="server" customattr="0"></asp:TextBox>s
                                    <asp:RegularExpressionValidator ID="revIntervalPosting" runat="server" ControlToValidate="txtIntervalPost"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblIntervalPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxLengthOfTopicOrPost]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtPostLength" CssClass="txtshortScore" runat="server" customattr="1000"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revPostLength" runat="server" ControlToValidate="txtPostLength"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblPostLength" runat="server" CssClass="helpMsg" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowLink]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbURL" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblURL" runat="server" CssClass="helpMsg" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_AllowInsertImage]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbImage" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblImage" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_Attachment]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbAttachment" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblAttachment" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxAttachmentsOnePost]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtMaxAttachmentsOnePost" CssClass="txtshortScore" runat="server"
                                        customattr="20"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revMaxAttachmentsInOnePost" runat="server" ControlToValidate="txtMaxAttachmentsOnePost"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblMaxAttachmentsOnPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxSizeOfAttachment]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtMaxSizeOfAttachment" CssClass="txtshortScore" runat="server"
                                        customattr="10"></asp:TextBox>K
                                    <asp:RegularExpressionValidator ID="revMaxSizeOfAttachment" runat="server" ControlToValidate="txtMaxSizeOfAttachment"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                   <asp:Label ID="lblMaxSizeOfAttachment" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxSizeOfAllAttachments]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtMaxSizeOfAllAttachments" CssClass="txtshortScore" runat="server"
                                        customattr="100"></asp:TextBox>K
                                    <asp:RegularExpressionValidator ID="revMaxSizeOfAllAttachments" runat="server" ControlToValidate="txtMaxSizeOfAllAttachments"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblMaxSizeOfAllAttachments" runat="server" CssClass="helpMsg" 
                                        Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_MaxMessage]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtMessage" CssClass="txtshortScore" runat="server" customattr="20"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="revMaxMessagesOnDay" runat="server" ControlToValidate="txtMessage"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblMaxMessage" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_Search]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbSearch" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_IntervalSearch]%>
                                </td>
                                <td class="ctd">
                                    <asp:TextBox ID="txtIntervalSearch" CssClass="txtshortScore" runat="server" customattr="20"></asp:TextBox>s
                                    <asp:RegularExpressionValidator ID="revMinIntervalSearching" runat="server" ControlToValidate="txtIntervalSearch"
                                        ValidationExpression="^\d*$" Display="Dynamic" ValidationGroup="Save"></asp:RegularExpressionValidator>
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblIntervalSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="ttd">
                                    <%=Proxy[EnumText.enumForum_Permission_PostModerationNotNeeded]%>
                                </td>
                                <td class="ctd">
                                    <asp:CheckBox ID="chbPostNotModeration" runat="server" />
                                </td>
                                <td class="htd">
                                    <asp:Label ID="lblPostNotModeration" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>s
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" OnClick="btnSave_Click"
                ValidationGroup="Save" />
            <asp:Button ID="btnCancel2" runat="server" CausesValidation="False" OnClick="btnCancel_Click"
                CssClass="mbtn" />
        </div>
    </div>
</asp:Content>
