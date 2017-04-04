<%@ Page Title="" Language="C#" MasterPageFile="~/AdminPanel/AdminMasterPage.Master" 
AutoEventWireup="true" CodeBehind="CodePlan.aspx.cs" Inherits="Forum.UI.AdminPanel.Settings.CodePlan" %>
<%@ Import Namespace="Com.Comm100.Forum.Language" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<script language="javascript" type="text/javascript">
    function CopyWebSiteLink(txt) {
        var txt = document.getElementById("<%=txtWebSiteUrl.ClientID%>");
        window.clipboardData.setData('text', txt.value);
    }    
</script>
	<script type="text/javascript" src="../../JS/Clipboard/ZeroClipboard.js"></script>
	<script language="JavaScript" type="text/javascript">
	    var clip = null;
	    var clip2 = null;

	    function $(id) { return document.getElementById(id); }

	    function init() {
	        clip = new ZeroClipboard.Client();
	        clip.setHandCursor(true);

	        clip.addEventListener('load', my_load);
	        clip.addEventListener('mouseOver', my_mouse_over);
	        clip.addEventListener('complete', my_complete);
	        

	        clip.glue('btnCopyCode');

	        clip2 = new ZeroClipboard.Client();
	        clip2.setHandCursor(true);

	        clip2.addEventListener('load', my_load);
	        clip2.addEventListener('mouseOver', my_mouse_over2);
	        clip2.addEventListener('complete', my_complete);

	        clip2.glue('btnCopyCode2');
	        
	     }

	    function my_load(client) {
	    }

	    function my_mouse_over(client) {
	        // we can cheat a little here -- update the text on mouse over
	        clip.setText($('<%=txtWebSiteUrl.ClientID%>').value);
	    }
	    function my_mouse_over2(client) {
	        // we can cheat a little here -- update the text on mouse over
	        clip2.setText($('<%=txtWebSiteUrl.ClientID%>').value);
	    }

	    function my_complete(client, text) {
	        var CopyCode=document.getElementById('btnCopyCode');
	        CopyCode.addEventListener('click', document.getElementById('<%=txtWebSiteUrl.ClientID%>').focus(), false);
	    }

	    function debugstr(msg) {
	    }

//	    function getFocus() {
//	        debugger;
//	        document.getElementById('<%=txtWebSiteUrl.ClientID%>').focus();
//	    }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="divTitle">       
            <asp:Label ID="lblTitle" runat="server" CssClass="TitleLabel"></asp:Label>
        </div>
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
            <input id="btnCopyCode" type="button"  value='<%=Proxy[EnumText.enumForum_Settings_ButtonCopyCode] %>' class="lbtn" />
            </div>
            <div class="divTable">
                <br />
                <table  style="width: 100%;" class="form-table" cellpadding='0' cellspacing='0'>
                    <tr>
                        <td class="ttd" width="200">
                            <%=Proxy[EnumText.enumForum_Settings_FieldWebLink] %>
                        </td>
                        <td class="ctd" width="300">
                            <asp:TextBox ID="txtWebSiteUrl" onfocus="javascript:this.select();"  TextMode="MultiLine" Rows="4" runat="server" Text="" CssClass="txtnormal" />
                        </td>
                        <td class="rtd">
                            &nbsp;&nbsp;
                        
                        </td>
                    </tr>    
                </table>
            </div>            
            <div class="divTopButton">
            <input id="btnCopyCode2" type="button" value='<%=Proxy[EnumText.enumForum_Settings_ButtonCopyCode] %>' class="lbtn" />
            </div>
        </div>
<script language="javascript" type="text/javascript">
    window.onload = init;
</script>
</asp:Content>
