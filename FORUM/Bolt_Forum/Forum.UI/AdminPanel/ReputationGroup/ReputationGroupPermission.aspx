<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ReputationGroupPermission.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.ReputationGroup.ReputationGroupPermission" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <script type="text/javascript">
        function $(id) { return document.getElementById(id); }
        
        function checkViewForumOrTopic(){
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
    <div class="divTitle"><asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label></div>
    <div class="divSubTitle"><asp:Label ID="lblSubTitle" runat="server"></asp:Label></div>
    <br/>
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false">
        </asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false">
        </asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnSave_Click" ValidationGroup="add"></asp:Button>
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" 
                CausesValidation="false" onclick="btnCancel_Click" />
        </div>
        <div class="divTable">
        <br />
            <table class="form-table">
                <tr>
                    <td class="ttd">
                        <asp:Label ID="lblName" runat="server"><%=Proxy[EnumText.enumForum_Reputation_FieldCurrentReputationGroup]%></asp:Label>
                    </td>
                    <td class="ctd">
                        <asp:Label ID="lblCurrentReputationGroup" runat="server"></asp:Label>
                    </td>
                    <td class="rtd">
                        
                    </td>
                </tr>                               
                <tr>
                    <td class="ttd" style=" vertical-align:top;">
                        <%=Proxy[EnumText.enumForum_UserGroups_FieldPermissions] %>
                    </td>
                    <td class="ctd">
                         <table class="tblPermissionsSet">
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowViewForum] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbViewForum" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblViewForum" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgViewForum" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowViewTopicOrPost] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbViewTopic" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblViewTopic" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgViewTopic" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowPostTopicOrPost] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbPost" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgPost" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowCustomizeAvatar] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbAvatar" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblAvatar" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgAvatar" runat="server" Visible="true"  ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxSignature] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxSignature" CssClass="txtshortScore" runat="server" customattr="1000"></asp:TextBox>
                                <asp:CompareValidator ID="cvMaxSignature" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxSignature" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                
                                <asp:Label ID="lblSignature" CssClass="helpMsg" runat="server" Visible="true" ></asp:Label>
                                <%--<asp:Image ID="imgSignature" runat="server" Visible="true"  ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowLinkSignature] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbAllowLinkSignature" runat="server" Visible="true" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblAllowLinkSignature" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="ttd">
                               <%=Proxy[EnumText.enumForum_Permission_AllowImageSignature] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbAllowImageSignature" runat="server" Visible="true" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblAllowImageSignature" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MinInterValTimeForPosting] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMinIntervalPost" CssClass="txtshortScore" runat="server" customattr="0"></asp:TextBox>s
                                <asp:CompareValidator ID="cvMinIntervalPost" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMinIntervalPost" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblIntervalPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgIntervalPost" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxLengthOfTopicOrPost] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxPostLength" CssClass="txtshortScore" runat="server" customattr="1000"></asp:TextBox>
                                 <asp:CompareValidator ID="cvMaxPostLength" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxPostLength" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblPostLength" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgPostLength" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                       
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowLink] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbURL" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblURL" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgURL" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_AllowInsertImage] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbImage" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblImage" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgImage" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr >
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_Attachment] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbAttachment" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblAttachment" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgAttachment" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxAttachmentsOnePost] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxAttachmentsOnePost" CssClass="txtshortScore" runat="server" customattr="20"></asp:TextBox>
                                 <asp:CompareValidator ID="cvMaxAttachmentsOnePost" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxAttachmentsOnePost" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblMaxAttachmentsOnPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgMaxAttachmentsOnPost" runat="server" Visible="true"  
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxSizeOfAttachment] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxSizeOfAttachment" CssClass="txtshortScore" runat="server" customattr="10"></asp:TextBox>
                                K
                                <asp:CompareValidator ID="cvMaxSizeOfAttachment" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxSizeOfAttachment" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblMaxSizeOfAttachment" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgMaxSizeOfAttachment" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr >
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxSizeOfAllAttachments] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxSizeOfAllAttachments" CssClass="txtshortScore" runat="server" customattr="100"></asp:TextBox>
                                K
                                <asp:CompareValidator ID="cvMaxSizeOfAllAttachments" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxSizeOfAllAttachments" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblMaxSizeOfAllAttachments" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgMaxSizeOfAllAttachments" runat="server" Visible="true"  ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_MaxMessage] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMaxMessage" CssClass="txtshortScore" runat="server" customattr="20"></asp:TextBox>
                                <asp:CompareValidator ID="cvtxtMaxMessage" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMaxMessage" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblMaxMessage" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgMessage" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_Search] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbSearch" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgSearch" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_IntervalSearch] %>
                            </td>
                            <td class="ctd">
                                <asp:TextBox ID="txtMinIntervalSearch" CssClass="txtshortScore" runat="server" customattr="20"></asp:TextBox>s
                                 <asp:CompareValidator ID="cvMinIntervalSearch" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                    ControlToValidate="txtMinIntervalSearch" Operator="GreaterThanEqual" ValidationGroup="add">
                                        <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                </asp:CompareValidator>
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblIntervalSearch" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgIntervalSearch" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                        <tr>
                            <td  class="ttd">
                                <%=Proxy[EnumText.enumForum_Permission_PostModerationNotNeeded] %>
                            </td>
                            <td class="ctd">
                                <asp:CheckBox ID="chbPostNotModeration" runat="server" />
                            </td>
                            <td class="htd">
                                <asp:Label ID="lblPostNotModeration" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                <%--<asp:Image ID="imgPostNotModeration" runat="server" Visible="true" 
                                    ImageUrl="~/Images/help.gif" />--%>
                            </td>
                        </tr>
                    </table>              
                    </td>
                    <td class="rtd">
                    </td>
                </tr>             
            </table>
        </div>
        <br />
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" OnClick="btnSave_Click" ValidationGroup="add"/>
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" CausesValidation="false" onclick="btnCancel_Click" />
        </div>    
    </div> 
</asp:Content>


