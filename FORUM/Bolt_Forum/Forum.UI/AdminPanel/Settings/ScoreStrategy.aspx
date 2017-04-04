<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" AutoEventWireup="true" CodeBehind="ScoreStrategy.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Settings.ScoreStrategy" %>
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
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" onclick="btnSave_Click" ValidationGroup="Save"/>
            <asp:Button ID="btnCancel1" runat="server" CssClass="mbtn" onclick="btnCancel_Click" CausesValidation="false"/>
        </div>
        <div class="divTable" style="text-align: center">
            <br />
               <table id="form-table" class="form-table" width="100%">
                <tr>
                    <td class="ttd" style="width: 100px;">
                    </td>
                    <td class="ttd" style="text-align: center;width:500px;">
                    </td>
                    <td class="ttd" style="text-align: center;">
                        <span class="Font3"><%=Proxy[EnumText.enumForum_Settings_ColumnScore] %></span>
                    </td>
                    <td class="ttd" >
                    </td>
                </tr>
                
                <!-- General Begin -->
                <tr>
                    <td colspan="4" align="left">
                        <span class='Font3'><%=Proxy[EnumText.enumForum_Settings_ColumnGeneral] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td rowspan="6" class="ttd">
                    </td>
                    <td class="sstd" style="width:30%; " >
                        <%=Proxy[EnumText.enumForum_Settings_FieldRegister]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Register" runat="server" CssClass="txtshortScore">
                        </asp:TextBox>
                        <asp:RangeValidator ID="rvI_Register" runat="server" 
                        ControlToValidate="txtI_Register" Display="Dynamic" ValidationGroup="Save" 
                        Type="Integer" MaximumValue="2147483647" MinimumValue="-2147483648"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                    <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreRegister]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldFirstLogin] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Login" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Login" runat="server" 
                            ControlToValidate="txtI_Login" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreFirstLogin]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldModeratorAdded] %> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_ModeratorAdded" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_ModeratorAdded" runat="server" 
                            ControlToValidate="txtI_ModeratorAdded" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreModeratorAdded] %> </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldModeratorRemoved]%> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_ModeratorRemoved" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_ModeratorRemoved" runat="server" 
                            ControlToValidate="txtI_ModeratorRemoved" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreModeratorRemoved]%> </span>
                    </td>
                </tr>
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldUserAccountBanned] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Ban" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Ban" runat="server" 
                            ControlToValidate="txtI_Ban" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreUserAccountBanned] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldUserAccountUnbanned] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Unban" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Unban" runat="server" 
                            ControlToValidate="txtI_Unban" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreUserAccountUnbanned] %></span>
                    </td>
                </tr>
                
                <!-- General End -->
                
                <!-- Topic Begin -->
                <tr>
                    <td colspan="4" align="left">
                        <span class='Font3'><%=Proxy[EnumText.enumForum_Settings_ColumnTopic] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td rowspan="12">
                    </td>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicPosted] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_NewTopic" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_NewTopic" runat="server" 
                            ControlToValidate="txtI_NewTopic" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicPosted] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldMakeAsFeature] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Featured" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Featured" runat="server" 
                            ControlToValidate="txtI_Featured" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreMakeAsFeature] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicSticky] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Sticky" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Sticky" runat="server" 
                            ControlToValidate="txtI_Sticky" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicSticky] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicDeleted] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_TopicDeleted" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_TopicDeleted" runat="server" 
                            ControlToValidate="txtI_TopicDeleted" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicDeleted] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicRestored] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_TopicRestored" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_TopicRestored" runat="server" 
                            ControlToValidate="txtI_TopicRestored" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicRestored]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">                                                                                                                                                                                                                                                                                                                      
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicAddedIntoFavorites]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_AddedIntoFavorites" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_AddedIntoFacorites" runat="server" 
                            ControlToValidate="txtI_AddedIntoFavorites" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicAddedIntoFavorites]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">                                                                                                                                                                                                                                                                                                                      
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicRemovedFromFavorites]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_RemovedFromFavorites" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_RemovedFromFavorites" runat="server" 
                            ControlToValidate="txtI_RemovedFromFavorites" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicRemovedFromFavorites]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicViewed]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_TopicViewed" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_TopicViewed" runat="server" 
                            ControlToValidate="txtI_TopicViewed" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicViewed]%>
                        </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicReplied]%> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_TopicReplied" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_TopicReplied" runat="server" 
                            ControlToValidate="txtI_TopicReplied" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicReplied]%> </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldTopicVerifiedAsSpam]%> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_TopicVerifiedAsSpam" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_TopicVerifiedAsSpam" runat="server" 
                            ControlToValidate="txtI_TopicVerifiedAsSpam" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreTopicVerifiedAsSpam]%> </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldVoteforaPoll]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_VoteForaPoll" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_VoteForaPoll" runat="server" 
                            ControlToValidate="txtI_VoteForaPoll" Display="Dynamic" 
                            MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer" 
                            ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreVoteforaPoll]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPollVoted]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_PollVoted" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_PollVoted" runat="server" 
                            ControlToValidate="txtI_PollVoted" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScorePollVoted]%></span>
                    </td>
                </tr>
                <!-- Topic End -->
                
                <!-- Post Begin -->
                <tr>
                    <td colspan="4" align="left">
                        <span class='Font3'><%=Proxy[EnumText.enumForum_Settings_ColumnPost] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td rowspan="5" class="ttd">
                    </td>
                    <td class="sstd">
                       <%=Proxy[EnumText.enumForum_Settings_FieldNewPost]%>  
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_NewPost" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_NewPost" runat="server" 
                            ControlToValidate="txtI_NewPost" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreNewPost]%> </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPostDeleted]%> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_PostDeleted" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_PostDeleted" runat="server" 
                            ControlToValidate="txtI_PostDeleted" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScorePostDeleted]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPostRestored]%> 
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_PostRestored" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_PostRestored" runat="server" 
                            ControlToValidate="txtI_PostRestored" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScorePostRestored]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPostVerifiedAsSpam] %>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_PostVerifiedAsSpam" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_PostVerifiedAsSpam" runat="server" 
                            ControlToValidate="txtI_PostVerifiedAsSpam" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScorePostVerifiedAsSpam] %></span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldPostMarkAsAnswer]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_PostMarkedAsAnswer" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_PostMarkedAsAnswer" runat="server" 
                            ControlToValidate="txtI_PostMarkedAsAnswer" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScorePostMarkAsAnswer]%></span>
                    </td>
                </tr>
                <!-- Post End -->
                
                <!-- Other Begin -->
                <tr>
                    <td colspan="4" align="left">
                        <span class='Font3'><%=Proxy[EnumText.enumForum_Settings_ColumnOthers]%></span>
                    </td>
                </tr>
                
                <tr>
                    <td rowspan="2" class="ttd">
                    </td>
                    <td class="sstd">
                       <%=Proxy[EnumText.enumForum_Settings_FieldReportAbuse]%>  
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_ReportAbuse" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_ReportAbuse" runat="server" 
                            ControlToValidate="txtI_ReportAbuse" Display="Dynamic" 
                            MaximumValue="2147483647" MinimumValue="-2147483648" Type="Integer" 
                            ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreReportAbuse]%> </span>
                    </td>
                </tr>
                
                <tr>
                    <td class="sstd">
                        <%=Proxy[EnumText.enumForum_Settings_FieldSearch]%>
                    </td>
                    <td class="std">
                        <asp:TextBox ID="txtI_Search" runat="server" CssClass="txtshortScore"></asp:TextBox>
                        <asp:RangeValidator ID="rvI_Search" runat="server" 
                            ControlToValidate="txtI_Search" Display="Dynamic" MaximumValue="2147483647" 
                            MinimumValue="-2147483648" Type="Integer" ValidationGroup="Save"></asp:RangeValidator>
                    </td>
                    <td class="std" >
                        <span class="helpMsg">
                        <%=Proxy[EnumText.enumForum_Settings_HelpScoreSearch]%></span>
                    </td>
                </tr>
                <!-- Other End -->
                
            </table>
        </div>
        <br/>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" onclick="btnSave_Click" ValidationGroup="Save"/>
            <asp:Button ID="btnCancel2" runat="server" CssClass="mbtn" onclick="btnCancel_Click"  CausesValidation="false"/>
        </div>
    </div>
</asp:Content>
