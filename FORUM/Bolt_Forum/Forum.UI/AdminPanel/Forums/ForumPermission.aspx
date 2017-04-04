<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master"
    AutoEventWireup="true" CodeBehind="ForumPermission.aspx.cs" Inherits="Com.Comm100.Forum.UI.AdminPanel.Forums.ForumPermission" %>

<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script src="../../JS/Common/ThickBox.js" type="text/javascript"></script>

    <script src="../../JS/Common/show.js" type="text/javascript"></script>

    <script type="text/javascript">
        function $(id) { return document.getElementById(id); }

        function checkViewForumOrTopic() {
            var chbViewForum = $("<%=chbViewForum.ClientID %>");
            var chbViewTopic = $("<%=chbViewTopic.ClientID %>");
            var chbPost = $("<%=chbPost.ClientID %>");
            if (chbViewForum != null) {
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
        }
    </script>

    <script language="javascript" type="text/javascript">
        function doCustom(isChecked) {
            var radioInherit = $("radioCustom1");
            var radioCustom = $("radioCustom2");
            var hdIfInherit = $("<%= hdIfInherit.ClientID %>");
            var ifUserGroupEnabled=<%=ifUserGroupEnabled.ToString().ToLower() %>;
            var ifReputationEnabled=<%=ifReputationEnabled.ToString().ToLower() %>;
            if (isChecked) {
                document.getElementById("trPermissions").style.display = "";
                radioInherit.checked = false;
                radioCustom.checked = true;
                hdIfInherit.value = 0;
                document.getElementById("dvInheritsDescriptionOfUserGroup").style.display = "none";
                document.getElementById("dvInheritsDescriptionOfReputationGroup").style.display = "none";
                document.getElementById("dvInheritsDescriptionOfNoGroup").style.display = "none";
                document.getElementById("dvInheritsDescriptionOfBothGroup").style.display = "none";
                document.getElementById("dvCommonInheritsDescription").style.display = "none";
                document.getElementById("dvCustomDescription").style.display = "";
                
                
            }
            else {
                document.getElementById("trPermissions").style.display = "none";
                radioInherit.checked = true;
                radioCustom.checked = false;
                hdIfInherit.value = 1;
                //debugger;
                if(ifUserGroupEnabled==false&&ifReputationEnabled==false)
                {
                    document.getElementById("dvInheritsDescriptionOfUserGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfReputationGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfNoGroup").style.display = "";
                    document.getElementById("dvInheritsDescriptionOfBothGroup").style.display = "none";
                }
                 if(ifUserGroupEnabled==true&&ifReputationEnabled==false)
                {
                    document.getElementById("dvInheritsDescriptionOfUserGroup").style.display = "";
                    document.getElementById("dvInheritsDescriptionOfReputationGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfNoGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfBothGroup").style.display = "none";
                }
                 if(ifUserGroupEnabled==false&&ifReputationEnabled==true)
                {
                    document.getElementById("dvInheritsDescriptionOfUserGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfReputationGroup").style.display = "";
                    document.getElementById("dvInheritsDescriptionOfNoGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfBothGroup").style.display = "none";
                }
                 if(ifUserGroupEnabled==true&&ifReputationEnabled==true)
                {
                    document.getElementById("dvInheritsDescriptionOfUserGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfReputationGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfNoGroup").style.display = "none";
                    document.getElementById("dvInheritsDescriptionOfBothGroup").style.display = "";
                    
                }
                document.getElementById("dvCommonInheritsDescription").style.display = "";
                document.getElementById("dvInheritsDescription").style.display = "";
                document.getElementById("dvCustomDescription").style.display = "none";
            }
        }
        function changeTabStyle(index) {
            if (index == 1) {
                document.getElementById("<%=lkbUserGroups.ClientID %>").className = "aSel";
                document.getElementById("<%=lkbUserGroups.ClientID %>").childNodes[0].className = "spanSel";
                document.getElementById("<%=lkbReputationGroups.ClientID %>").className = "aNSel";
                document.getElementById("<%=lkbReputationGroups.ClientID %>").childNodes[0].className = "spanNSel";
                document.getElementById("divUserGroupInForum").style.display = "";
                document.getElementById("divReputationGroupInForum").style.display = "none";
                document.getElementById("<%=hdGroupType.ClientID %>").value = "1";
                document.getElementById("dvInheritsDescription").style.display = "none";
                document.getElementById("dvCustomDescription").style.display = "";

            }
            else {
                document.getElementById("<%=lkbReputationGroups.ClientID %>").className = "aSel";
                document.getElementById("<%=lkbReputationGroups.ClientID %>").childNodes[0].className = "spanSel";
                document.getElementById("<%=lkbUserGroups.ClientID %>").className = "aNSel";
                document.getElementById("<%=lkbUserGroups.ClientID %>").childNodes[0].className = "spanNSel";
                document.getElementById("divUserGroupInForum").style.display = "none";
                document.getElementById("divReputationGroupInForum").style.display = "";
                document.getElementById("<%=hdGroupType.ClientID %>").value = "2";
                document.getElementById("dvInheritsDescription").style.display = "none";
                document.getElementById("dvCustomDescription").style.display = "";
            }
        }
        function selectGroup(obj, idHead) {

            var lists = document.getElementsByTagName("div");

            for (var i = 0; i < lists.length; i++) {
                if (lists[i].id.indexOf(idHead) > -1) {
                    lists[i].className = "divGroupItem";
                }
            }
            if (obj != null) {
                //alert(obj.id);
                obj.className = "divGroupItemSelected";
            }
        }

        function setHighLightDIV(obj) {
            var hd = document.getElementById('<%= hdHighLightDIV.ClientID %>');

            hd.value = obj.id;
        }
        function highLightDiv(type) {
            //debugger;
            var hd = document.getElementById('<%= hdHighLightDIV.ClientID %>');
            if (hd.value != "") {
                var divH = document.getElementById(hd.value);
                selectGroup(divH, type);
            }
        }
    </script>

    <style type="text/css">
        #permissionTable
        {
            background-color: #d7e7ee;
            width: 80%;
            text-align: center;
        }
        #permissionTable td
        {
            background-color: #ffffff;
        }
        .tabSpan1
        {
            border: solid 1px #009999;
            border-bottom-width: 0px;
            background-color: #dddddd;
            padding: 2px 5px 2px 2px;
            height: 20px;
            font-weight: bold;
        }
        .tabSpan2
        {
            border: solid 0px #009999;
            background-color: #eeeeee;
            padding: 2px 5px 2px 2px;
            height: 20px;
        }
        #tabPage
        {
            /*background-color:#fff;*/
            border: solid 1px #009999;
        }
        #divGroupItems
        {
            width: 200px;
        }
        .divGroupItem
        {
            height: 20px;
            cursor: pointer;
        }
        .divGroupItemSelected
        {
            height: 20px;
            cursor: pointer;
            background-color: #ffffff;
            font-weight: bold;
        }
        #divGp1
        {
        }
        /* Tab */#GroupsBar #GroupsDivTab
        {
            height: 22px;
            background: url(../../images/after-tag.gif) repeat-x;
        }
        #GroupsBar #GroupsDivTabLine
        {
            height: 15px;
            background: url(../../images/line-under-tag.gif) repeat-x;
        }
        #GroupsBar li
        {
            float: left;
            list-style-type: none;
            height: 22px;
            line-height: 22px;
            text-align: center;
        }
        #GroupsBar ul
        {
            height: 22px;
            display: inline;
        }
        #GroupsBar li a
        {
            text-decoration: none;
            font-size: 1.2em;
            display: block;
            width: auto;
            font-weight: bold;
            padding-left: 10px;
        }
        #GroupsBar a span
        {
            display: block;
            padding-right: 10px;
        }
        #GroupsBar .aSel
        {
            color: #f8f8f7;
            display: block;
            width: auto;
            background: url(../../images/tag-left.gif) no-repeat left -22px;
        }
        #GroupsBar .aNSel
        {
            color: #838362;
            display: block;
            width: auto;
            background: url(../../images/tag-left.gif) no-repeat left 0px;
        }
        #GroupsBar .spanSel
        {
            display: block;
            background: url(../../images/tag-right.gif) no-repeat right -22px;
        }
        #GroupsBar .spanNSel
        {
            display: block;
            background: url(../../images/tag-right.gif) no-repeat right 0px;
        }
        /* Tbale Permission Item */#tblPermissionsSet
        {
            background-color: #d7e7ee;
            width: 100%;
            text-align: left;
        }
        #tblPermissionsSet td
        {
            background-color: #ffffff;
            padding: 10px;
        }
        .weightTD
        {
            font-weight: bold;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">
        <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
    </div>
    <div class="divSubTitle">
        <asp:Label ID="lblSubTitle" runat="server"></asp:Label></div>
    <br />
    <div class="divMsg">
        <asp:Label ID="lblMessage" runat="server" CssClass="ErrorLabel" EnableViewState="false"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" CssClass="SuccessLabel" EnableViewState="false"></asp:Label>
    </div>
    <div class="divContent">
        <div class="divTopButton">
            <asp:Button ID="btnSave1" runat="server" CssClass="mbtn" OnClick="btnUserGroupSave_Click"
                ValidationGroup="add"></asp:Button>
            <input type="button" class="mbtn" value="<%=Proxy[EnumText.enumForum_Forums_ButtonReturn] %>"
                onclick="window.location='ForumList.aspx?siteId=<%=SiteId %>'" />
        </div>
        <div class="divTable">
            <div>
                <table class="form-table">
                    <tr>
                        <td class="ttd">
                            <asp:Label ID="lblName" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldCurrentForum] %></asp:Label>:
                        </td>
                        <td class="ctd" colspan="2" style="padding-left: 4px;">
                            <asp:Label ID="lblForumName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            <asp:Label ID="Label1" runat="server"><%=Proxy[EnumText.enumForum_Forums_FieldPermissions] %></asp:Label>:
                        </td>
                        <td class="ctd" colspan="2" style="padding-left: 4px;">
                            <input type="radio" id="radioCustom1" name="radioCustom" onclick="doCustom(false);" /><%=Proxy[EnumText.enumForum_Forums_ForumPermissionInherit] %>&nbsp;
                            <input type="radio" id="radioCustom2" name="radioCustom" checked="checked" onclick="doCustom(true);" /><%=Proxy[EnumText.enumForum_Forums_ForumPermissionCustom] %>
                            <%--<span>--%>
<%--                                <img src="../../Images/help.gif" style="margin-bottom: -2px;" alt="" onmouseover="showHelp('divHelp','');"
                                    onmouseout="closeHelp('divHelp');" /></span>--%>
                        </td>
                    </tr>
                    <tr>
                        <td class="ttd">
                            &nbsp;
                        </td>
                        <td class="ctd" colspan="2">
                            <div id="dvInheritsDescription" style="display: none;">
                                <div id="dvInheritsDescriptionOfUserGroup" style="display: none;">
                                    <%=Proxy[EnumText.enumForum_Forums_InheritsDescriptionOfUserGroup]%>
                                </div>
                                <div id="dvInheritsDescriptionOfReputationGroup" style="display: none;">
                                    <%=Proxy[EnumText.enumForum_Forums_InheritsDescriptionOfReputationGroup]%>
                                </div>
                                <div id="dvInheritsDescriptionOfNoGroup" style="display: none;">
                                    <%=Proxy[EnumText.enumForum_Forums_InheritsDescriptionOfNoGroup]%>
                                </div>
                                <div id="dvInheritsDescriptionOfBothGroup" style="display: none;">
                                    <%=Proxy[EnumText.enumForum_Forums_InheritsDescriptionOfBothGroup]%>
                                </div>
                                <div id="dvCommonInheritsDescription" style="">
                                    <%=Proxy[EnumText.enumForum_Forums_CommonInheritsDescription]%>
                                </div>
                            </div>
                            <div id="dvCustomDescription" style="display: none;">
                                <%=GetCustomDescribtion()%>
                            </div>
                            <br/>
                            <div>
                            <b><%=Proxy[EnumText.enumForum_Forums_NoteDescription_Note]%></b>
                            <%=Proxy[EnumText.enumForum_Forums_AdditionalInformation]%>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" class="ctd" style="padding-left: 30px">
                            <%-- <div id="tabHeader" runat="server">--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="trPermissions" style="padding-left: 55px; padding-right: 25px">
            <div id="GroupsBar" style="width: 100%">
                <div id="GroupsDivTab">
                    <ul>
                        <li>
                            <img alt="" src="../../images/before-tag.gif" style="vertical-align: bottom;" /></li>
                        <li style="<%=VisibleUserGroups%>">
                            <asp:LinkButton ID="lkbUserGroups" runat="server" OnClientClick="changeTabStyle(1);"
                                CssClass="aSel" OnClick="lkbUserGroups_Click" Style="text-decoration: none">
                                <span class="spanSel">
                                <%=Proxy[EnumText.enumForum_Forums_ForumPermissionTabUserGroups] %></span>
                            </asp:LinkButton>
                        </li>
                        <li style="<%=VisibleReputationGroups%>">
                            <asp:LinkButton ID="lkbReputationGroups" runat="server" OnClientClick="changeTabStyle(2);"
                                CssClass="aNSel" OnClick="lkbReputationGroups_Click" Style="text-decoration: none">
                            <span class="spanNSel"><%=Proxy[EnumText.enumForum_Forums_ForumPermissionTabUserReputationGroups] %></span>
                            </asp:LinkButton>
                        </li>
                    </ul>
                </div>
                <div id="GroupsDivTabLine">
                </div>
            </div>
            <%--</div>--%>
            <div id="tabPage" style="border: none; margin-bottom: 10px;">
                <table width="100%" cellpadding='0' cellspacing="0">
                    <tr>
                        <td class="ctd" style="padding-left: 4px; padding-right: 0px; vertical-align: top;">
                            <div id="divGroupItems">
                                <div id="divUserGroupInForum" style="height: auto;">
                                    <table width="100%" cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td>
                                                <input type="button" value="<%=Proxy[EnumText.enumForum_UserGroups_ButtonAdd] %>"
                                                    class="mbtn" onclick="showWindow('divUserGroupNotInForum', 'divThickOuter');return false;"
                                                    causesvalidation="true" />
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rpUserGroup" runat="server">
                                            <ItemTemplate>
                                                <tr id='divUserGroupItem<%# Eval("usergroupId") %>' class="divGroupItem" onclick="setHighLightDIV(this)">
                                                    <td>
                                                        <div style="float: left; width: 90%; display: block; text-align: left">
                                                            <asp:LinkButton ID="lbtnName" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString())%>'
                                                                CommandArgument='<%# Eval("usergroupId") %>' OnClick="lbtnName_Click" Style="display: block;
                                                                width: 100%; text-decoration: none"></asp:LinkButton>
                                                        </div>
                                                        <div style="float: right; width: 10%;">
                                                            <asp:ImageButton ID="ibtnReputatioinGroupDel" runat="server" ImageUrl="~/Images/database_delete.gif"
                                                                OnClick="ibtnGroupDel_Click" CommandArgument='<%# Eval("usergroupId") %>' ToolTip="<%#Proxy[EnumText.enumForum_Forums_HelpDelete]%>" />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                                <div id="divReputationGroupInForum" style="display: none; padding-right: 0px; height: auto;">
                                    <table width="100%" cellpadding='0' cellspacing='0'>
                                        <tr>
                                            <td>
                                                <input type="button" value="<%=Proxy[EnumText.enumForum_UserGroups_ButtonAdd] %>"
                                                    class="mbtn" onclick="showWindow('divReputationGroupNotInForum', 'divThickOuter');return false;"
                                                    causesvalidation="true" />
                                            </td>
                                        </tr>
                                        <asp:Repeater ID="rpReputationGroups" runat="server">
                                            <ItemTemplate>
                                                <tr id='divReputationGroupItem<%# Eval("GroupId") %>' class="divGroupItem" onclick="setHighLightDIV(this)">
                                                    <td>
                                                        <div style="float: left; width: 90%;">
                                                            <asp:LinkButton ID="lbtnName" runat="server" Text='<%# System.Web.HttpUtility.HtmlEncode(Eval("Name").ToString()) %>'
                                                                CommandArgument='<%# Eval("GroupId") %>' OnClick="lbtnReputationGroupName_Click"
                                                                Style="display: block; width: 100%; text-decoration: none; text-align: left"></asp:LinkButton>
                                                        </div>
                                                        <div style="float: right; width: 10%;">
                                                            <asp:ImageButton ID="ibtnReputatioinGroupDel" runat="server" ImageUrl="~/Images/database_delete.gif"
                                                                OnClick="ibtnReputatioinGroupDel_Click" CommandArgument='<%# Eval("GroupId") %>' />
                                                        </div>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </table>
                                </div>
                            </div>
                        </td>
                        <td class="ctd" style="border: solid 10px #ffffff">
                            <div id="divGroupPermission" runat="server">
                                <table width="100%" id="tblPermissionsSet">
                                    <tr>
                                        <td width="30%" align="right" nowrap="nowrap" class="style1">
                                            <%=Proxy[EnumText.enumForum_Permission_AllowViewForum] %>
                                        </td>
                                        <td width="24%" >
                                            <asp:CheckBox ID="chbViewForum" runat="server" />
                                        </td>
                                        <td width="46%">
                                            <%--<asp:Image ID="imgViewForum" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblViewForum" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_AllowViewTopicOrPost] %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chbViewTopic" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgViewTopic" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblViewTopic" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_AllowPostTopicOrPost] %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chbPost" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgPost" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_MinInterValTimeForPosting] %>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMinIntervalPost" CssClass="txtshortScore" runat="server" customattr="0"
                                                onkeyup="value=value.replace(/[^\d]/g,'')" onbeforepaste="clipboardData.setData('text', clipboardData.getData('text').replace(/[^\d]/g,''))"></asp:TextBox>s
                                            <asp:CompareValidator ID="cvMinIntervalPost" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                                ControlToValidate="txtMinIntervalPost" Operator="GreaterThanEqual" ValidationGroup="add">
                                                <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                            </asp:CompareValidator>
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgIntervalPost" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblIntervalPost" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_MaxLengthOfTopicOrPost] %>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMaxPostLength" CssClass="txtshortScore" runat="server" 
                                                customattr="1000"
                                                onkeyup="value=value.replace(/[^\d]/g,'')"
                                                onbeforepaste="clipboardData.setData('text', clipboardData.getData('text').replace(/[^\d]/g,''))">
                                            </asp:TextBox>
                                            <asp:CompareValidator ID="cvMaxPostLength" Display="Dynamic" runat="server" Type="Integer" ValueToCompare="0"
                                                ControlToValidate="txtMaxPostLength"  Operator="GreaterThanEqual" ValidationGroup="add">
                                                <%=Proxy[EnumText.enumForum_Permission_ErrorPleaseInputPositiveInteger]%>
                                            </asp:CompareValidator>
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgPostLength" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblPostLength" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_AllowLink] %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chbURL" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgURL" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblURL" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" nowrap="nowrap">
                                            <%=Proxy[EnumText.enumForum_Permission_AllowInsertImage] %>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="chbImage" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgImage" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblImage" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <%=Proxy[EnumText.enumForum_Permission_PostModerationNotNeeded] %>
                                        </td>
                                        <td colspan="1" class="ctd">
                                            <asp:CheckBox ID="chbPostNotModeration" runat="server" />
                                        </td>
                                        <td>
                                            <%--<asp:Image ID="imgPostNotModeration" runat="server" Visible="true" ImageUrl="~/Images/help.gif" />--%>
                                            <asp:Label ID="lblPostNotModeration" CssClass="helpMsg" runat="server" Visible="true"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <%--<div id="divGp2" style="display: none; border: solid 2px #d7e7ee; width: 550px; height: 310px;
                            background-color: #ffffff">
                            Please Select a User group or Reputation group. &nbsp;roup. &nbsp;</div>--%>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="divButtomButton">
            <asp:Button ID="btnSave2" runat="server" CssClass="mbtn" OnClick="btnUserGroupSave_Click"
                ValidationGroup="add"></asp:Button>
            <input type="button" class="mbtn" onclick="window.location='ForumList.aspx?siteId=<%=SiteId %>'"
                value="<%=Proxy[EnumText.enumForum_Forums_ButtonReturn] %>" />
        </div>
    </div>
    <input type="hidden" runat="server" id="hdGroupId" />
    <input type="hidden" runat="server" id="hdForumId" />
    <input type="hidden" runat="server" id="hdGroupType" value="1" />
    <input type="hidden" runat="server" id="hdHighLightDIV" />
    <input type="hidden" runat="server" id="hdIfInherit" />
</asp:Content>
<asp:Content ID="ContentThickBox" ContentPlaceHolderID="cphThickBox" runat="server">
    <div id="divThickOuter" style="position: absolute; filter: alpha(opacity=50); -moz-opacity: 0.5;
        opacity: 0.5; border: 0px; display: none; background-color: #ccc">
    </div>

    <div id="divUserGroupNotInForum" style="position: absolute; height: auto;
        width: 600px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
                   <div class="divh">
            <div class="divh-header">
                <div style="float: left; font-size: 1.1em;">
                    <span class="divh-header-spantitle">
                        <asp:Label ID="lblSelectUserGroupTitle" runat="server">Select User Gorups</asp:Label></span>
                </div>
              <%--  <div style="float: right;">
                    <span class="divh-header-spanclose">[<a class="linkClose" href="#" onclick="javascript:WindowClose('divUserGroupNotInForum','divThickOuter');">Close</a>]</span>
                </div>--%>
            </div>
            <br />
            <div class="divh-table">
                <div class="divContent">
                    <%--<div class="divTopButton">
                        <asp:Button ID="btnAddGroup1" runat="server" Text="Save" OnClick="btnAddUserGroup_Click"
                            CssClass="mbtn"></asp:Button>
                        <input class="mbtn" type="button" value='Cancel' onclick="WindowClose('divUserGroupNotInForum','divThickOuter'); return false;" />
                    </div>--%>
                    <div class="divTable" style="text-align: center">
                        <table class="form-table" style="text-align: center; width: 60%">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Label ID="lblNoUserGorupsNotInForun" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="rpUserGroupNotInForum" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td style="width: 30%">
                                            <asp:Label ID="lblGroupId" runat="server" Text='<%# Eval("UserGroupId") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td style="width: 70%; text-align: left;">
                                            <asp:CheckBox ID="chbGroup" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td>
                                </td>
                                <td class="ctd">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="divButtomButton">
                        <asp:Button ID="btnAddGroup2" runat="server" Text="Save" OnClick="btnAddUserGroup_Click"
                            CssClass="mbtn"></asp:Button>
                        <input class="mbtn" type="button" value='Cancel' onclick="WindowClose('divUserGroupNotInForum','divThickOuter'); return false;" />
                    </div>
                </div>
            </div>
        </div>
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>

    <div id="divReputationGroupNotInForum" style="position: absolute; height: auto;
        width: 600px; display: none;">
        <b class="R_outer_top"><b class="R_outer_1"></b><b class="R_outer_2"></b><b class="R_outer_3">
        </b><b class="R_outer_4"></b></b>
        <div class="R_inner">
            <b class="R_inner_top"><b class="R_inner_1"></b><b class="R_inner_2"></b><b class="R_inner_3">
            </b><b class="R_inner_4"></b></b>
        <div class="divh">
            <div class="divh-header">
                <div style="float: left; font-size: 1.1em;">
                    <span class="divh-header-spantitle">
                        <asp:Label ID="lblSelectReputationGroups" runat="server">Select Reputation Gorups</asp:Label></span>
                </div>
               <%-- <div style="float: right;">
                    <span class="divh-header-spanclose">[<a class="linkClose" href="#" onclick="javascript:WindowClose('divReputationGroupNotInForum','divThickOuter');">Close</a>]</span>
                </div>--%>
            </div>
            <br />
            <div class="divh-table">
                <div class="divContent">
                    <div class="divTable" style="text-align: center">
                        <table class="form-table" style="text-align: center; width: 60%">
                            <tr>
                                <td colspan="2" style="text-align: center">
                                    <asp:Label ID="lblNoReputationGroupsNotInForum" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <asp:Repeater ID="rpReputationGroupsNotInForum" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblGroupId" runat="server" Text='<%# Eval("GroupId") %>' Visible="false"></asp:Label>
                                        </td>
                                        <td class="ctd" style="text-align: left;">
                                            <asp:CheckBox ID="chbGroup" runat="server" Text='<%# Eval("Name") %>' />
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                            <tr>
                                <td>
                                </td>
                                <td class="ctd">
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="divButtomButton">
                        <asp:Button ID="btnAddGroup4" runat="server" Text="Save" OnClick="btnAddReputationGroup_Click"
                            CssClass="mbtn"></asp:Button>
                        <input class="mbtn" type="button" value='Cancel' onclick="WindowClose('divReputationGroupNotInForum','divThickOuter'); return false;" />
                    </div>
                </div>
            </div>
        </div>
            <b class="R_inner_bottom"><b class="R_inner_4"></b><b class="R_inner_3"></b><b class="R_inner_2">
            </b><b class="R_inner_1"></b></b>
        </div>
        <b class="R_outer_bottom"><b class="R_outer_4"></b><b class="R_outer_3"></b><b class="R_outer_2">
        </b><b class="R_outer_1"></b></b>
    </div>
</asp:Content>
