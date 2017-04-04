<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserPermissionListControl.ascx.cs" Inherits="Com.Comm100.Forum.UI.UserControl.UserPermissionListControl" %>
<style type="text/css">
    #tblPermissionsSet
    {
        background-color: #d7e7ee;
        width: 100%;
        text-align: left;
    }
    #tblPermissionsSet td
    {
        background-color: #ffffff;
    }
</style>

<script language="javascript">
    function setPermissionInputEnabled(isShow) {
        var inputs = document.getElementById("tblPermissionsSet").getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox") {
                inputs[i].parentNode.disabled = !isShow;
            }
            inputs[i].disabled = !isShow;
        }
    }
    function resetPermissionInput() {
        var inputs = document.getElementById("tblPermissionsSet").getElementsByTagName("input");
        for (var i = 0; i < inputs.length; i++) {
            if (inputs[i].type == "checkbox") {
                inputs[i].checked = true;
            }
            else if (inputs[i].type == "text") {
                inputs[i].value = inputs[i].customattr;
            }
        }
    }
</script>

<div>
    <table width="100%" id="tblPermissionsSet">
        <asp:Panel ID="panelPermissionSet" runat="server">
            <tr>
                <td width="40%" align="right" nowrap="nowrap">
                    Allow View Forum:
                </td>
                <td width="10%">
                    <asp:CheckBox ID="CheckBox1" runat="server" Checked="true" />
                </td>
                <td width="50%">
                <asp:Image ID="Image1" runat="server" Visible="true" 
                ToolTip="The usergroup has the permission to view all the categories and forums." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Allow View Topic/Post:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox2" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image2" runat="server" Visible="true" ToolTip="The usergroup has the permission to  view all the topics, votes and posts." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Allow Post Topic/Post:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox3" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image3" runat="server" Visible="true" ToolTip="The usergroup has the permission to post a new topic, vote and post." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr1" runat="server">
                <td align="right" nowrap="nowrap">
                    Allow Customize Avatar:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox4" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image4" runat="server" Visible="true" ToolTip="" ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td align="right" nowrap="nowrap">
                    Max Length of Signature:
                </td>
                <td>
                    <asp:TextBox ID="TextBox1" Text="1000" CssClass="txtshort" runat="server" customattr="1000"></asp:TextBox>
                </td>
                <td>
                <asp:Image ID="Image5" runat="server" Visible="true" ToolTip="" ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Min Interval Time for Posting:
                </td>
                <td>
                    <asp:TextBox ID="TextBox2" Text="0" CssClass="txtshort" runat="server" customattr="0"></asp:TextBox>S
                </td>
                <td>
                <asp:Image ID="Image6" runat="server" Visible="true" ToolTip="You can set a minumum time for posting interval." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Max Length of Topic/Post:
                </td>
                <td>
                    <asp:TextBox ID="TextBox3" Text="1000" CssClass="txtshort" runat="server" customattr="1000"></asp:TextBox>
                </td>
                <td>
                <asp:Image ID="Image7" runat="server" Visible="true" ToolTip="You can set a maximum Bytes for a topic/post for this usergroup." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Allow HTML:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox5" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image8" runat="server" Visible="true" ToolTip="The usergroup has the permission to post through HTML editor. " ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Allow URL:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox6" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image9" runat="server" Visible="true" ToolTip="The usergroup has the permission to post with a URL." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    Allow Insert Image:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox7" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image10" runat="server" Visible="true" ToolTip="The usergroup has the permission to post with an image." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr4" runat="server">
                <td align="right" nowrap="nowrap">
                    Allow Attachment:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox8" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image11" runat="server" Visible="true" ToolTip="The usergroup has the permission to post with attachments." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr5" runat="server">
                <td align="right" nowrap="nowrap">
                    Max Attachments in One Post:
                </td>
                <td>
                    <asp:TextBox ID="TextBox4" Text="20" CssClass="txtshort" runat="server" customattr="20"></asp:TextBox>
                </td>
                <td>
                <asp:Image ID="Image12" runat="server" Visible="true" ToolTip="The users can set a maximum number of the attachments in each post." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr6" runat="server">
                <td align="right" nowrap="nowrap">
                    Max Size of the Attachment:
                </td>
                <td>
                    <asp:TextBox ID="TextBox5" Text="10" CssClass="txtshort" runat="server" customattr="10"></asp:TextBox>M
                </td>
                <td>
                <asp:Image ID="Image13" runat="server" Visible="true" ToolTip="The users can set a maxium size for each file uploaded." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr3" runat="server" nowrap="nowrap">
                <td align="right">
                    Max Size of all the Attachments:
                </td>
                <td>
                    <asp:TextBox ID="TextBox6" Text="100" CssClass="txtshort" runat="server" customattr="100"></asp:TextBox>M
                </td>
                <td>
                <asp:Image ID="Image14" runat="server" Visible="true" ToolTip="" ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="trMessages" runat="server">
                <td align="right" nowrap="nowrap">
                    Max Messages Sent in One Day:
                </td>
                <td>
                    <asp:TextBox ID="TextBox8" Text="10" CssClass="txtshort" runat="server" customattr="20"></asp:TextBox>
                </td>
                <td>
                <asp:Image ID="Image15" runat="server" Visible="true" ToolTip="You can set a maximum number of the messages this user can send out in one day." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr7" runat="server">
                <td align="right" nowrap="nowrap">
                    Allow Search:
                </td>
                <td>
                    <asp:CheckBox ID="CheckBox10" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image16" runat="server" Visible="true" ToolTip="The usergroup has the permission to search." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr id="tr8" runat="server">
                <td align="right">
                    Min Interval Time for Searching:
                </td>
                <td>
                    <asp:TextBox ID="TextBox7" Text="20" CssClass="txtshort" runat="server" customattr="20"></asp:TextBox>S
                </td>
                <td>
                <asp:Image ID="Image17" runat="server" Visible="true" ToolTip="You can set a minimum interval time of search for this usergroup." ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    Post Moderation Not Required:
                </td>
                <td colspan="1" class="ctd">
                    <asp:CheckBox ID="CheckBox9" runat="server" Checked="true" />
                </td>
                <td>
                <asp:Image ID="Image18" runat="server" Visible="true" 
                ToolTip="This user's post does not need your moderation" ImageUrl="~/Images/help.gif" />
                </td>
            </tr>
        </asp:Panel>
    </table>
</div>
